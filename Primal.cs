using System;
using System.Collections.Generic;
using System.Linq;

namespace Linear_Programming_Algorithms
{
    public class Primal
    {
        private double[,] tableau;
        private int numConstraints;
        private int numVariables;
        private bool[] isBinary;

        public double[,] TableauPublic => tableau;
        public int NumConstraints => numConstraints;
        public int NumVariables => numVariables;
        public bool Solved { get; private set; } = false;
        public bool Unbounded { get; private set; } = false;
        public string LastError { get; private set; } = "";

        public double[,] OptimalTableau { get; private set; }
        public List<double[,]> TableauList { get; private set; } = new List<double[,]>();

        public Primal(double[,] A, double[] b, double[] c, bool[] isBinaryVars)
        {
            if (A == null) throw new ArgumentNullException(nameof(A));
            if (b == null) throw new ArgumentNullException(nameof(b));
            if (c == null) throw new ArgumentNullException(nameof(c));

            numConstraints = b.Length;
            numVariables = c.Length;
            isBinary = isBinaryVars ?? new bool[numVariables];

            tableau = new double[numConstraints + 1, numVariables + numConstraints + 1];

            for (int i = 0; i < numConstraints; i++)
            {
                for (int j = 0; j < numVariables; j++)
                    tableau[i, j] = A[i, j];

                tableau[i, numVariables + i] = 1; // slack
                tableau[i, tableau.GetLength(1) - 1] = b[i]; // RHS
            }

            for (int j = 0; j < numVariables; j++)
                tableau[numConstraints, j] = -c[j];

            OptimalTableau = null;

            AddBinaryBounds(); // Add binary variable bounds (0 <= x <= 1)
        }

        private void AddBinaryBounds()
        {
            if (isBinary == null) return;
            int binCount = isBinary.Count(b => b);
            if (binCount == 0) return;

            int oldRows = tableau.GetLength(0);    // includes objective
            int oldCols = tableau.GetLength(1);    // includes old RHS
            int objRowOld = oldRows - 1;

            int newRows = oldRows + binCount;
            int newCols = oldCols + binCount;

            var t = new double[newRows, newCols];

            // Copy original constraint rows
            for (int i = 0; i < objRowOld; i++)
            {
                // Original variables
                for (int j = 0; j < numVariables; j++)
                    t[i, j] = tableau[i, j];

                // Original slack
                for (int j = 0; j < numConstraints; j++)
                    t[i, numVariables + j] = tableau[i, numVariables + j];

                // RHS to last column
                t[i, newCols - 1] = tableau[i, oldCols - 1];
            }

            // Append binary bounds
            int nextRow = objRowOld;
            int nextSlack = numVariables + numConstraints; // new slack column for binaries
            for (int j = 0; j < numVariables; j++)
            {
                if (!isBinary[j]) continue;

                t[nextRow, j] = 1.0;           // x_j coefficient
                t[nextRow, nextSlack] = 1.0;   // slack for this bound
                t[nextRow, newCols - 1] = 1.0; // RHS = 1

                nextRow++;
                nextSlack++;
            }

            // Copy objective row
            for (int j = 0; j < oldCols - 1; j++)
                t[newRows - 1, j] = tableau[objRowOld, j];
            t[newRows - 1, newCols - 1] = tableau[objRowOld, oldCols - 1];

            tableau = t;
            numConstraints += binCount;
        }

        public void Solve()
        {
            Solved = false;
            Unbounded = false;
            LastError = "";
            TableauList.Add(Clone(tableau));

            try
            {
                while (true)
                {
                    int pivotCol = FindPivotColumn();
                    if (pivotCol == -1)
                    {
                        OptimalTableau = Clone(tableau);
                        Solved = true;
                        return;
                    }

                    int pivotRow = FindPivotRow(pivotCol);
                    if (pivotRow == -1)
                    {
                        OptimalTableau = Clone(tableau);
                        Unbounded = true;
                        return;
                    }

                    Pivot(pivotRow, pivotCol);
                    TableauList.Add(Clone(tableau));
                }
            }
            catch (Exception ex)
            {
                OptimalTableau = Clone(tableau);
                LastError = ex.ToString();
                throw;
            }
        }

        private int FindPivotColumn()
        {
            int col = -1;
            double min = 0;
            int lastRow = numConstraints;
            for (int j = 0; j < tableau.GetLength(1) - 1; j++)
                if (tableau[lastRow, j] < min)
                {
                    min = tableau[lastRow, j];
                    col = j;
                }
            return col;
        }

        private int FindPivotRow(int pivotCol)
        {
            int row = -1;
            double minRatio = double.PositiveInfinity;
            int rhsCol = tableau.GetLength(1) - 1;

            for (int i = 0; i < numConstraints; i++)
            {
                double a = tableau[i, pivotCol];
                if (a > 1e-8)
                {
                    double ratio = tableau[i, rhsCol] / a;
                    if (ratio < minRatio)
                    {
                        minRatio = ratio;
                        row = i;
                    }
                }
            }
            return row;
        }

        private void Pivot(int pivotRow, int pivotCol)
        {
            double pivotVal = tableau[pivotRow, pivotCol];
            for (int j = 0; j < tableau.GetLength(1); j++)
                tableau[pivotRow, j] /= pivotVal;

            for (int i = 0; i < tableau.GetLength(0); i++)
            {
                if (i == pivotRow) continue;
                double factor = tableau[i, pivotCol];
                if (Math.Abs(factor) < 1e-12) continue;

                for (int j = 0; j < tableau.GetLength(1); j++)
                    tableau[i, j] -= factor * tableau[pivotRow, j];
            }
        }

        public (double[] solution, double optimalValue) GetSolution()
        {
            double[] solution = new double[numVariables];
            int rhs = tableau.GetLength(1) - 1;

            for (int j = 0; j < numVariables; j++)
            {
                int pivotRow = -1;
                bool isBasic = true;
                for (int i = 0; i < numConstraints; i++)
                {
                    double v = tableau[i, j];
                    if (Math.Abs(v - 1.0) < 1e-8)
                    {
                        if (pivotRow == -1) pivotRow = i;
                        else { isBasic = false; break; }
                    }
                    else if (Math.Abs(v) > 1e-8)
                    {
                        isBasic = false;
                        break;
                    }
                }
                solution[j] = (isBasic && pivotRow != -1) ? tableau[pivotRow, rhs] : 0.0;
            }

            double optimalValue = tableau[numConstraints, rhs];
            return (solution, optimalValue);
        }

        private static double[,] Clone(double[,] src)
        {
            int r = src.GetLength(0), c = src.GetLength(1);
            var dst = new double[r, c];
            for (int i = 0; i < r; i++)
                for (int j = 0; j < c; j++)
                    dst[i, j] = src[i, j];
            return dst;
        }

        // Gomory cut and other methods remain unchanged...
    }
}
