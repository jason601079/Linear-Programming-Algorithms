using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linear_Programming_Algorithms.BNB
{
    internal sealed class PrimalAdapter:IRelaxationSolver
    {
        public RelaxationResult Solve(
           double[,] A, double[] b, double[] c,
           IReadOnlyList<VarBound> extraBounds,
           bool isMax)
        {
           
            int m = A.GetLength(0);
            int n = A.GetLength(1);

            var rowsToAdd = new List<(double[] row, double rhs)>();
            foreach (var vb in extraBounds)
            {
                if (vb.Lower.HasValue)
                {
                    var r = new double[n];
                    r[vb.Index] = +1.0;   
                   
                    for (int j = 0; j < n; j++) r[j] = -r[j];
                    rowsToAdd.Add((r, -(vb.Lower.Value)));
                }
                if (vb.Upper.HasValue)
                {
                    var r = new double[n];
                    r[vb.Index] = +1.0;   
                    rowsToAdd.Add((r, vb.Upper.Value));
                }
            }

            int mm = m + rowsToAdd.Count;
            var A2 = new double[mm, n];
            var b2 = new double[mm];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++) A2[i, j] = A[i, j];
                b2[i] = b[i];
            }
            for (int k = 0; k < rowsToAdd.Count; k++)
            {
                var (row, rhs) = rowsToAdd[k];
                for (int j = 0; j < n; j++) A2[m + k, j] = row[j];
                b2[m + k] = rhs;
            }

            
            double[] cEff = (double[])c.Clone();
            bool flipped = false;
            if (!isMax) { for (int j = 0; j < cEff.Length; j++) cEff[j] = -cEff[j]; flipped = true; }

            var solver = new Primal(A2, b2, cEff);
            try
            {
                solver.Solve();
            }
            catch
            {
                return new RelaxationResult { Feasible = false };
            }

            var (x, z) = solver.GetSolution();
            if (flipped) z = -z;

            return new RelaxationResult
            {
                Feasible = true,
                X = x,
                Z = z,
                OptimalTableau = solver.OptimalTableau,
                TableauList = solver.TableauList
            };
        }
    }
}

