using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Linear_Programming_Algorithms
{
    public class Dual
    {
        private double[,] tableau;
        private int numConstraints;
        private int numVariables;

        public List<double[,]> TableauList { get; private set; } = new List<double[,]>();

        public double[,] OptimalTableau { get; private set; }

        public Dual(double[,] tableau, int numConstraints, int numVariables)
        {
            this.tableau = (double[,])tableau.Clone(); 
            this.numConstraints = numConstraints;
            this.numVariables = numVariables;
        }

        public void Solve()
        {
            while (true)
            {
                SaveTableau(); 

                int pivotRow = FindPivotRow();
                if (pivotRow == -1) break;

                int pivotCol = FindPivotColumn(pivotRow);
                if (pivotCol == -1)
                {
                    MessageBox.Show("Problem is infeasible under dual simplex.", "Dual Solver", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Pivot(pivotRow, pivotCol);
            }

       
            OptimalTableau = (double[,])tableau.Clone();
        }

        // Find row with most negative RHS
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

        // Find pivot column for a given row
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

        // Perform pivot operation
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

        // Save a clone of the current tableau to the list
        private void SaveTableau()
        {
            TableauList.Add((double[,])tableau.Clone());
        }

        // Display all stored tableaux in a ListBox for frontend logging
        public void DisplayOnFrontend(ListBox listBox)
        {
            listBox.Items.Clear();
            int tableIndex = 1;
            foreach (var matrix in TableauList)
            {
                listBox.Items.Add($"Dual Tableau {tableIndex}:");
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    string row = "";
                    for (int j = 0; j < matrix.GetLength(1); j++)
                        row += matrix[i, j].ToString("F3").PadLeft(12);
                    listBox.Items.Add(row);
                }
                listBox.Items.Add(""); // blank line
                tableIndex++;
            }

            if (OptimalTableau != null)
            {
                listBox.Items.Add("Optimal Dual Tableau:");
                for (int i = 0; i < OptimalTableau.GetLength(0); i++)
                {
                    string row = "";
                    for (int j = 0; j < OptimalTableau.GetLength(1); j++)
                        row += OptimalTableau[i, j].ToString("F3").PadLeft(12);
                    listBox.Items.Add(row);
                }
            }
        }

        // Optional: return dual solution values (like GetSolution in Primal)
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
    }
}


