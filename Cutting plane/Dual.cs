using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linear_Programming_Algorithms
{
    public class Dual
    {
        private double[,] tableau;
        private int numConstraints;
        private int numVariables;

        public Dual(double[,] tableau, int numConstraints, int numVariables)
        {
            this.tableau = tableau;
            this.numConstraints = numConstraints;
            this.numVariables = numVariables;
        }

        public void Solve()
        {
            while (true)
            {
                PrintTableau();

                int pivotRow = FindPivotRow();
                if (pivotRow == -1) break; // feasible

                int pivotCol = FindPivotColumn(pivotRow);
                if (pivotCol == -1)
                {
                    Console.WriteLine("Problem is infeasible under dual simplex.");
                    return;
                }

                Pivot(pivotRow, pivotCol);
            }

            Console.WriteLine("Dual feasibility restored.");
        }

        private int FindPivotRow()
        {
            int row = -1;
            double minRhs = 0;
            for (int i = 0; i < numConstraints; i++)
            {
                if (tableau[i, tableau.GetLength(1) - 1] < minRhs)
                {
                    minRhs = tableau[i, tableau.GetLength(1) - 1];
                    row = i;
                }
            }
            return row;
        }

        private int FindPivotColumn(int pivotRow)
        {
            int col = -1;
            double minRatio = double.PositiveInfinity;
            for (int j = 0; j < tableau.GetLength(1) - 1; j++)
            {
                if (tableau[pivotRow, j] < 0)
                {
                    double ratio = Math.Abs(tableau[numConstraints, j] / tableau[pivotRow, j]);
                    if (ratio < minRatio)
                    {
                        minRatio = ratio;
                        col = j;
                    }
                }
            }
            return col;
        }

        private void Pivot(int pivotRow, int pivotCol)
        {
            double pivotVal = tableau[pivotRow, pivotCol];

            for (int j = 0; j < tableau.GetLength(1); j++)
                tableau[pivotRow, j] /= pivotVal;

            for (int i = 0; i < tableau.GetLength(0); i++)
            {
                if (i != pivotRow)
                {
                    double factor = tableau[i, pivotCol];
                    for (int j = 0; j < tableau.GetLength(1); j++)
                        tableau[i, j] -= factor * tableau[pivotRow, j];
                }
            }
        }

        private void PrintTableau()
        {
            Console.WriteLine("Current Dual Tableau:");
            for (int i = 0; i < tableau.GetLength(0); i++)
            {
                for (int j = 0; j < tableau.GetLength(1); j++)
                    Console.Write($"{tableau[i, j],8:F2} ");
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}

