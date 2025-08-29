using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linear_Programming_Algorithms
{
    public class Primal
    {
        private double[,] tableau;
        private int numConstraints;
        private int numVariables;

        public double[,] TableauPublic => tableau;
        public int NumConstraints => numConstraints;
        public int NumVariables => numVariables;

        public double[,] OptimalTableau { get; private set; }
        public List<double[,]> TableauList { get; private set; } = new List<double[,]>();

        public Primal(double[,] A, double[] b, double[] c)
        {
            numConstraints = b.Length;
            numVariables = c.Length;

            tableau = new double[numConstraints + 1, numVariables + numConstraints + 1];

            for (int i = 0; i < numConstraints; i++)
            {
                for (int j = 0; j < numVariables; j++)
                    tableau[i, j] = A[i, j];

                tableau[i, numVariables + i] = 1; // slack
                tableau[i, tableau.GetLength(1) - 1] = b[i];
            }

            for (int j = 0; j < numVariables; j++)
                tableau[numConstraints, j] = -c[j];
        }

        public void Solve()
        {
            while (true)
            {
                int pivotCol = FindPivotColumn();
                if (pivotCol == -1) break;

                int pivotRow = FindPivotRow(pivotCol);
                if (pivotRow == -1) return;

                Pivot(pivotRow, pivotCol);
            }
        }

        private int FindPivotColumn()
        {
            int col = -1;
            double min = 0;
            for (int j = 0; j < tableau.GetLength(1) - 1; j++)
                if (tableau[numConstraints, j] < min)
                {
                    min = tableau[numConstraints, j];
                    col = j;
                }
            return col;
        }

        private int FindPivotRow(int pivotCol)
        {
            int row = -1;
            double minRatio = double.PositiveInfinity;
            for (int i = 0; i < numConstraints; i++)
            {
                if (tableau[i, pivotCol] > 1e-8)
                {
                    double ratio = tableau[i, tableau.GetLength(1) - 1] / tableau[i, pivotCol];
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
                for (int j = 0; j < tableau.GetLength(1); j++)
                    tableau[i, j] -= factor * tableau[pivotRow, j];
            }
        }

        public (double[] solution, double optimalValue) GetSolution()
        {
            double[] solution = new double[numVariables];
            for (int j = 0; j < numVariables; j++)
            {
                int pivotRow = -1;
                bool isBasic = true;
                for (int i = 0; i < numConstraints; i++)
                {
                    if (Math.Abs(tableau[i, j] - 1) < 1e-8)
                    {
                        if (pivotRow == -1) pivotRow = i;
                        else { isBasic = false; break; }
                    }
                    else if (Math.Abs(tableau[i, j]) > 1e-8)
                    {
                        isBasic = false;
                        break;
                    }
                }
                solution[j] = (isBasic && pivotRow != -1) ? tableau[pivotRow, tableau.GetLength(1) - 1] : 0;
            }

            double optimalValue = -tableau[numConstraints, tableau.GetLength(1) - 1];
            return (solution, optimalValue);
        }

        public void AddGomoryCut(List<double> cutCoeffs, double rhsFrac)
        {
            int oldRows = tableau.GetLength(0);
            int oldCols = tableau.GetLength(1);
            int newRows = oldRows + 1;
            int newCols = oldCols + 1;

            double[,] newTableau = new double[newRows, newCols];

            // Copy old tableau
            for (int i = 0; i < oldRows; i++)
                for (int j = 0; j < oldCols; j++)
                    newTableau[i, j] = tableau[i, j];

            // Copy cut coefficients
            for (int j = 0; j < cutCoeffs.Count; j++)
                newTableau[oldRows, j] = cutCoeffs[j];

            // Add new slack for cut
            newTableau[oldRows, oldCols - 1] = 1.0;

            // RHS
            newTableau[oldRows, newCols - 1] = rhsFrac;

            // Copy objective row to new tableau
            for (int j = 0; j < oldCols; j++)
                newTableau[newRows - 1, j] = tableau[oldRows - 1, j];

            tableau = newTableau;
            numConstraints++;
        }



        public bool IsFeasible()
        {
            for (int i = 0; i < numConstraints; i++)
            {
                if (tableau[i, tableau.GetLength(1) - 1] < -1e-6)
                    return false;
            }
            return true;
        }
    }
}
