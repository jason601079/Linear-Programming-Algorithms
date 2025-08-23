using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linear_Programming_Algorithms.Cutting_plane;


namespace Linear_Programming_Algorithms
{
    public class Primal
    {
        private double[,] tableau;
        private int numConstraints;
        private int numVariables;

        public double[,] TableauPublic => tableau;


        public Primal(double[,] A, double[] b, double[] c)
        {
            numConstraints = b.Length;
            numVariables = c.Length;

            tableau = new double[numConstraints + 1, numVariables + numConstraints + 1];

            // Fill constraints
            for (int i = 0; i < numConstraints; i++)
            {
                for (int j = 0; j < numVariables; j++)
                {
                    tableau[i, j] = A[i, j];
                }
                tableau[i, numVariables + i] = 1; // slack variable
                tableau[i, tableau.GetLength(1) - 1] = b[i];
            }

            // Objective row
            for (int j = 0; j < numVariables; j++)
                tableau[numConstraints, j] = -c[j];
        }

        public void Solve()
        {
            while (true)
            {
                PrintTableau();

                int pivotCol = FindPivotColumn();
                if (pivotCol == -1) break; // optimal

                int pivotRow = FindPivotRow(pivotCol);
                if (pivotRow == -1)
                {
                    Console.WriteLine("Unbounded solution.");
                    return;
                }

                Pivot(pivotRow, pivotCol);
            }

            Console.WriteLine("Optimal solution found.");
            PrintSolution();
        }

        private int FindPivotColumn()
        {
            int col = -1;
            double min = 0;
            for (int j = 0; j < tableau.GetLength(1) - 1; j++)
            {
                if (tableau[numConstraints, j] < min)
                {
                    min = tableau[numConstraints, j];
                    col = j;
                }
            }
            return col;
        }

        private int FindPivotRow(int pivotCol)
        {
            int row = -1;
            double minRatio = double.PositiveInfinity;
            for (int i = 0; i < numConstraints; i++)
            {
                if (tableau[i, pivotCol] > 0)
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
            Console.WriteLine("Current Tableau:");
            for (int i = 0; i < tableau.GetLength(0); i++)
            {
                for (int j = 0; j < tableau.GetLength(1); j++)
                    Console.Write($"{tableau[i, j],8:F2} ");
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private void PrintSolution()
        {
            double[] solution = new double[numVariables];
            for (int j = 0; j < numVariables; j++)
            {
                int pivotRow = -1;
                bool isBasic = true;
                for (int i = 0; i < numConstraints; i++)
                {
                    if (Math.Abs(tableau[i, j] - 1) < 1e-6)
                    {
                        if (pivotRow == -1) pivotRow = i;
                        else { isBasic = false; break; }
                    }
                    else if (Math.Abs(tableau[i, j]) > 1e-6)
                    {
                        isBasic = false;
                        break;
                    }
                }
                if (isBasic && pivotRow != -1)
                    solution[j] = tableau[pivotRow, tableau.GetLength(1) - 1];
                else
                    solution[j] = 0;
            }

            Console.WriteLine("Solution:");
            for (int i = 0; i < numVariables; i++)
                Console.WriteLine($"x{i + 1} = {solution[i]:F3}");
            Console.WriteLine($"Optimal Value: {tableau[numConstraints, tableau.GetLength(1) - 1]:F3}");
        }

        public void AddGomoryCut(List<double> cutCoeffs, double rhsFrac, string inequality = "<=")
        {
            int oldRows = tableau.GetLength(0);
            int oldCols = tableau.GetLength(1);

            // New tableau: +1 row for cut, +1 column for new slack/excess
            double[,] newTableau = new double[oldRows + 1, oldCols + 1];


            for (int i = 0; i < oldRows; i++)
                for (int j = 0; j < oldCols; j++)
                    newTableau[i, j] = tableau[i, j];
            for (int j = 0; j < cutCoeffs.Count; j++)
            {
                newTableau[oldRows, j] = (inequality == "<=") ? cutCoeffs[j] : -cutCoeffs[j];
            }

            newTableau[oldRows, oldCols] = 1; 

            newTableau[oldRows, oldCols + 1] = rhsFrac;

            numConstraints++;
            tableau = newTableau;
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
