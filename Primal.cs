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
        //ADDED NEW --------------------------------------------
        public bool Solved { get; private set; } = false;
        public bool Unbounded { get; private set; } = false;
        public string LastError { get; private set; } = "";
        //------------------------------------------------------
        public double[,] OptimalTableau { get; private set; }
        public List<double[,]> TableauList { get; private set; } = new List<double[,]>();

        public Primal(double[,] A, double[] b, double[] c)
        {
            //NEW--------------------------------------------------------------
            if (A == null) throw new ArgumentNullException(nameof(A));
            if (b == null) throw new ArgumentNullException(nameof(b));
            if (c == null) throw new ArgumentNullException(nameof(c));
            //-----------------------------------------------------------------
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
            //NEW
            OptimalTableau = null;
        }

        public void Solve()
        {
            //NEW
            Solved = false;
            Unbounded = false;
            LastError = "";
            //-------------------
            try
            {
                while (true)
                {
                    int pivotCol = FindPivotColumn();
                    //NEW
                    if (pivotCol == -1)
                    {
                        OptimalTableau = Clone(tableau);
                        Solved = true;
                        return;
                    }

                    int pivotRow = FindPivotRow(pivotCol);
                    //NEW
                    if (pivotRow == -1)
                    {
                        OptimalTableau = Clone(tableau);
                        Unbounded = true;
                        return;
                    }

                    Pivot(pivotRow, pivotCol);
                    //NEW
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

        public void AddGomoryCut(int rowIndex)
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

            // Build the Gomory cut row
            for (int j = 0; j < oldCols - 1; j++) // skip old RHS
            {
                double val = tableau[rowIndex, j];
                newTableau[oldRows, j] = val - Math.Floor(val); // fractional part
            }

            // New slack column for this cut
            newTableau[oldRows, oldCols - 1] = 1.0; 
                                                   
            double rhs = tableau[rowIndex, oldCols - 1];
            newTableau[oldRows, newCols - 1] = rhs - Math.Floor(rhs);

            for (int j = 0; j < oldCols - 1; j++)
                newTableau[newRows - 1, j] = tableau[oldRows - 1, j];
            newTableau[newRows - 1, oldCols - 1] = 0;
            newTableau[newRows - 1, newCols - 1] = tableau[oldRows - 1, oldCols - 1];

            tableau = newTableau;
            numConstraints++;

            Solved = false;
            Unbounded = false;
            OptimalTableau = null;
        }


        public bool IsFeasible()
        {
            int rhs = tableau.GetLength(1) - 1;
            for (int i = 0; i < numConstraints; i++)
            {
                if (tableau[i, rhs] < -1e-6)
                    return false;
            }
            return true;
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
        public int FindFractionalRow()
        {
            int numRows = TableauPublic.GetLength(0) - 1;
            int numCols = TableauPublic.GetLength(1);
            double maxFrac = 0;
            int fracRow = -1;

            for (int i = 0; i < numRows; i++)
            {
                double rhs = TableauPublic[i, numCols - 1];
                double frac = rhs - Math.Floor(rhs);
                if (frac > 1e-6 && frac < 1 - 1e-6 && frac > maxFrac)
                {
                    maxFrac = frac;
                    fracRow = i;
                }
            }

            return fracRow;
        }

    }
}
