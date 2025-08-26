using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Linear_Programming_Algorithms
{
    internal class RevisedPrimalSimplex
    {
        // Input
        private readonly double[,] A; // m x n
        private readonly double[] b;  // m
        private readonly double[] c;  // n

        // Dimensions
        private readonly int m; // constraints
        private readonly int n; // variables

        // Basis state
        private readonly List<int> basis;      // length m, indices of basic variables (0..n-1)
        private readonly List<int> nonBasis;   // length n-m, indices of non-basic variables

        // Product-Form-of-the-Inverse storage
        private double[,] BInv; // m x m
        private static readonly List<IterationRecord> iterationRecords = new List<IterationRecord>();

        // Iteration log
        public List<IterationRecord> Iterations { get { return iterationRecords; } }

        public RevisedPrimalSimplex(double[,] A, double[] b, double[] c, int[] initialBasis = null)
        {
            this.A = (double[,])A.Clone();
            this.b = (double[])b.Clone();
            this.c = (double[])c.Clone();

            m = A.GetLength(0);
            n = A.GetLength(1);

            // Build initial basis
            if (initialBasis == null)
            {
                basis = Enumerable.Range(n - m, m).ToList();
            }
            else
            {
                if (initialBasis.Length != m)
                    throw new ArgumentException("initialBasis length must equal number of constraints (m)");
                basis = initialBasis.ToList();
            }

            var all = new HashSet<int>(Enumerable.Range(0, n));
            foreach (var bi in basis) all.Remove(bi);
            nonBasis = all.OrderBy(i => i).ToList();

            // Initial BInv
            BInv = Identity(m);
            if (!IsSlackIdentityBasis())
            {
                var B = GetBasisMatrix();
                BInv = Invert(B);
            }

            for (int i = 0; i < m; i++)
                if (this.b[i] < -1e-9)
                    throw new InvalidOperationException("Initial b has negative entries; add Phase I to find a feasible BFS.");
        }

        public LpSolution Solve(int maxIterations = 10000, double tol = 1e-9)
        {
            for (int iter = 0; iter < maxIterations; iter++)
            {
                // 1) Compute basic solution
                var xB = Multiply(BInv, b);

                // 2) y^T = c_B^T * B^{-1}
                var cB = GetCosts(basis);
                var y = Multiply(Transpose(BInv), cB);

                // 3) Reduced costs
                var reducedCosts = new double[n];
                for (int j = 0; j < n; j++) reducedCosts[j] = double.NaN;

                int entering = -1;
                double bestRc = 0.0;
                foreach (var j in nonBasis)
                {
                    var aj = GetColumn(A, j);
                    double rc = c[j] - Dot(y, aj);
                    reducedCosts[j] = rc;
                    if (rc > bestRc + tol)
                    {
                        bestRc = rc;
                        entering = j;
                    }
                }

                var record = new IterationRecord();
                record.Iteration = iter;
                record.xB = (double[])xB.Clone();
                record.Basis = basis.ToArray();
                record.y = y;
                record.ReducedCosts = reducedCosts;
                record.Entering = entering;

                if (entering == -1)
                {
                    var x = new double[n];
                    for (int i = 0; i < m; i++) x[basis[i]] = xB[i];
                    double z = Dot(c, x);
                    record.Note = "Optimal (all reduced costs <= 0)";
                    Iterations.Add(record);
                    return new LpSolution(LpStatus.Optimal, x, z, basis.ToArray());
                }

                // 4) Direction
                var ak = GetColumn(A, entering);
                var d = Multiply(BInv, ak);
                record.Direction_d = d;

                // 5) Ratio test
                int leavingRow = -1;
                double theta = double.PositiveInfinity;
                for (int i = 0; i < m; i++)
                {
                    if (d[i] > tol)
                    {
                        double ratio = xB[i] / d[i];
                        if (ratio < theta - 1e-12)
                        {
                            theta = ratio;
                            leavingRow = i;
                        }
                    }
                }
                if (double.IsInfinity(theta))
                {
                    record.Note = "Unbounded (no positive d_i)";
                    Iterations.Add(record);
                    return new LpSolution(LpStatus.Unbounded, null, double.NaN, basis.ToArray());
                }

                record.LeavingRow = leavingRow;
                record.Theta = theta;
                record.Leaving = basis[leavingRow];

                // 6) Update BInv
                var u = d;
                double pivot = u[leavingRow];
                if (Math.Abs(pivot) < tol)
                    throw new ArithmeticException("Pivot too small -> numerical instability.");

                var Einv = BuildEtaInverseColumn(u, leavingRow);
                record.EtaInverseColumn = (double[])Einv.Clone();

                BInv = LeftMultiplyEtaInverse(Einv, leavingRow, BInv);

                // 7) Update basis
                int leavingVar = basis[leavingRow];
                basis[leavingRow] = entering;
                nonBasis.Remove(entering);
                nonBasis.Add(leavingVar);
                nonBasis.Sort();

                record.Note = "Pivot: enter x" + (entering + 1) + ", leave x" + (leavingVar + 1);
                Iterations.Add(record);
            }

            return new LpSolution(LpStatus.IterationLimit, null, double.NaN, basis.ToArray());
        }

        #region Helpers

        private bool IsSlackIdentityBasis()
        {
            if (basis.Any(bi => bi < 0 || bi >= n)) return false;
            var B = GetBasisMatrix();
            for (int i = 0; i < m; i++)
                for (int j = 0; j < m; j++)
                {
                    if (i == j && Math.Abs(B[i, j] - 1.0) > 1e-12) return false;
                    if (i != j && Math.Abs(B[i, j]) > 1e-12) return false;
                }
            return true;
        }

        private double[,] GetBasisMatrix()
        {
            var B = new double[m, m];
            for (int col = 0; col < m; col++)
            {
                var a = GetColumn(A, basis[col]);
                for (int i = 0; i < m; i++) B[i, col] = a[i];
            }
            return B;
        }

        private double[] GetCosts(List<int> idx)
        {
            var res = new double[idx.Count];
            for (int i = 0; i < idx.Count; i++) res[i] = c[idx[i]];
            return res;
        }

        private static double[] GetColumn(double[,] M, int j)
        {
            int rows = M.GetLength(0);
            var col = new double[rows];
            for (int i = 0; i < rows; i++) col[i] = M[i, j];
            return col;
        }

        private static double[,] Identity(int n)
        {
            var I = new double[n, n];
            for (int i = 0; i < n; i++) I[i, i] = 1.0;
            return I;
        }

        private static double Dot(double[] a, double[] b)
        {
            double s = 0.0;
            for (int i = 0; i < a.Length; i++) s += a[i] * b[i];
            return s;
        }

        private static double[] Multiply(double[,] M, double[] v)
        {
            int r = M.GetLength(0), c = M.GetLength(1);
            if (c != v.Length) throw new ArgumentException("Dimension mismatch");
            var res = new double[r];
            for (int i = 0; i < r; i++)
            {
                double s = 0.0;
                for (int j = 0; j < c; j++) s += M[i, j] * v[j];
                res[i] = s;
            }
            return res;
        }

        private static double[,] Transpose(double[,] M)
        {
            int r = M.GetLength(0), c = M.GetLength(1);
            var T = new double[c, r];
            for (int i = 0; i < r; i++)
                for (int j = 0; j < c; j++)
                    T[j, i] = M[i, j];
            return T;
        }

        private static double[,] Invert(double[,] M)
        {
            int n = M.GetLength(0);
            if (n != M.GetLength(1)) throw new ArgumentException("Matrix must be square");

            var A = (double[,])M.Clone();
            var I = Identity(n);

            for (int col = 0; col < n; col++)
            {
                int piv = col;
                double max = Math.Abs(A[piv, col]);
                for (int r = col + 1; r < n; r++)
                {
                    double val = Math.Abs(A[r, col]);
                    if (val > max) { max = val; piv = r; }
                }
                if (max < 1e-12) throw new ArithmeticException("Singular matrix");

                if (piv != col)
                {
                    SwapRows(A, col, piv);
                    SwapRows(I, col, piv);
                }

                double diag = A[col, col];
                for (int j = 0; j < n; j++) { A[col, j] /= diag; I[col, j] /= diag; }

                for (int r = 0; r < n; r++)
                {
                    if (r == col) continue;
                    double factor = A[r, col];
                    for (int j = 0; j < n; j++)
                    {
                        A[r, j] -= factor * A[col, j];
                        I[r, j] -= factor * I[col, j];
                    }
                }
            }
            return I;
        }

        private static void SwapRows(double[,] M, int r1, int r2)
        {
            int c = M.GetLength(1);
            for (int j = 0; j < c; j++)
            {
                double tmp = M[r1, j];
                M[r1, j] = M[r2, j];
                M[r2, j] = tmp;
            }
        }

        private static double[] BuildEtaInverseColumn(double[] u, int r)
        {
            int m = u.Length;
            double[] col = new double[m];
            double ur = u[r];
            for (int i = 0; i < m; i++)
            {
                if (i == r) col[i] = 1.0 / ur;
                else col[i] = -u[i] / ur;
            }
            return col;
        }

        private static double[,] LeftMultiplyEtaInverse(double[] etaInvColR, int r, double[,] BInv)
        {
            int m = BInv.GetLength(0);
            int k = BInv.GetLength(1);
            var R = (double[,])BInv.Clone();

            var rowr = new double[k];
            for (int j = 0; j < k; j++) rowr[j] = BInv[r, j];

            for (int i = 0; i < m; i++)
            {
                if (i == r)
                {
                    for (int j = 0; j < k; j++) R[i, j] = etaInvColR[i] * rowr[j];
                }
                else
                {
                    double coeff = etaInvColR[i];
                    for (int j = 0; j < k; j++) R[i, j] = BInv[i, j] + coeff * rowr[j];
                }
            }
            return R;
        }
        #endregion
    }

    public enum LpStatus { Optimal, Unbounded, Infeasible, IterationLimit }

    // Replaced 'record' with class for C# 7.3
    public class LpSolution
    {
        public LpStatus Status { get; private set; }
        public double[] x { get; private set; }
        public double z { get; private set; }
        public int[] Basis { get; private set; }

        public LpSolution(LpStatus status, double[] x, double z, int[] basis)
        {
            Status = status;
            this.x = x;
            this.z = z;
            Basis = basis;
        }
    }

    public class IterationRecord
    {
        public int Iteration { get; set; }
        public double[] xB { get; set; }
        public int[] Basis { get; set; }
        public double[] y { get; set; }
        public double[] ReducedCosts { get; set; }
        public int Entering { get; set; }
        public int Leaving { get; set; }
        public int LeavingRow { get; set; }
        public double[] Direction_d { get; set; }
        public double Theta { get; set; }
        public double[] EtaInverseColumn { get; set; }
        public string Note { get; set; }

        public string ToPrettyString()
        {
            Func<double, string> fmt = v => v.ToString("0.000", CultureInfo.InvariantCulture);
            Func<double[], string> vec = v => "[" + string.Join(", ", v.Select(fmt)) + "]";

            var rcPairs = new List<string>();
            if (ReducedCosts != null)
            {
                for (int j = 0; j < ReducedCosts.Length; j++)
                {
                    if (!double.IsNaN(ReducedCosts[j]))
                        rcPairs.Add("x" + (j + 1) + ":" + fmt(ReducedCosts[j]));
                }
            }

            return string.Join("\n", new[]
            {
                "Iter " + Iteration,
                "  Basis:    [" + string.Join(", ", Basis.Select(b => "x" + (b+1))) + "]",
                "  x_B:      " + vec(xB),
                "  y:        " + vec(y),
                "  RC: { " + string.Join(", ", rcPairs) + " }",
                Entering >= 0 ? "  Entering: x" + (Entering+1) : "  Entering: -",
                Leaving >= 0 ? "  Leaving:  x" + (Leaving+1) + " (row " + LeavingRow + ")" : "  Leaving: -",
                Direction_d != null ? "  d:        " + vec(Direction_d) : "",
                !double.IsNaN(Theta) ? "  theta:    " + fmt(Theta) : "",
                EtaInverseColumn != null ? "  Eta^-1 col(r): " + vec(EtaInverseColumn) : "",
                !string.IsNullOrWhiteSpace(Note) ? "  Note: " + Note : ""
            }.Where(s => !string.IsNullOrEmpty(s)));
        }
    }
}
