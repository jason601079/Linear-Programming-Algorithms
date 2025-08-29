using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LPR381Project.Algorithms
{
    public class CuttingPlane
    {
        private double[] _objective;
        private string _objectiveType;
        private List<double[]> _constraints = new List<double[]>();
        private List<string> _constraintSigns = new List<string>();
        private List<double> _rhs = new List<double>();
        private List<string> _variableTypes = new List<string>(); // "int", "bin", etc.

        private double[,] _tableau;
        private int[] _basis;
        private int _numVariables;
        private int _numConstraints;
        private int _numSlackVars;
        private const double Epsilon = 1e-9;
        public List<double[,]> TableauList { get; } = new List<double[,]>();
        public double[,] OptimalTableau { get; private set; }


        public CuttingPlane(string[] inputLines)
        {
            ParseInput(inputLines);
        }

        private void ParseInput(string[] input)
        {
            var firstLine = input[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            _objectiveType = firstLine[0].Trim().ToLower(); 
            _objective = new double[firstLine.Length - 1];
            for (int i = 1; i < firstLine.Length; i++)
                _objective[i - 1] = double.Parse(firstLine[i].Trim());

            // Constraints
            for (int i = 1; i < input.Length - 1; i++)
            {
                var parts = input[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                double[] coeffs = new double[parts.Length - 2];
                for (int j = 0; j < coeffs.Length; j++)
                    coeffs[j] = double.Parse(parts[j].Trim());

                string sign = parts[parts.Length - 2];
                double rhs = double.Parse(parts[parts.Length - 1].Trim());

                _constraints.Add(coeffs);
                _constraintSigns.Add(sign);
                _rhs.Add(rhs);
            }

            // Variable types / signs (last line)
            var lastLine = input[input.Length - 1];
            var typesParts = lastLine.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            _variableTypes = new List<string>();
            for (int i = 0; i < typesParts.Length; i++)
                _variableTypes.Add(typesParts[i].Trim().ToLower());
        }

        public string Solve()
        {
            StringBuilder log = new StringBuilder();
            InitializeTableau();

            log.AppendLine("--- Initial Tableau ---");
            log.Append(DisplayTableau());

            // --- Step 1: Solve LP relaxation first ---
            log.AppendLine("--- Solving LP relaxation with primal simplex ---");
            log.Append(RunSimplex());

            // Save first optimal tableau
            OptimalTableau = (double[,])_tableau.Clone();
            TableauList.Add((double[,])_tableau.Clone());

            int iteration = 0;
            while (iteration < 20) // Safety max iterations
            {
                // --- Step 2: Check integer feasibility ---
                if (IsIntegerSolution())
                {
                    log.AppendLine($">>> Integer solution found at iteration {iteration} <<<");
                    log.Append(GetSolutionString());
                    return log.ToString();
                }

                // --- Step 3: Generate Gomory cut ---
                int cutRow = SelectCutRow();
                if (cutRow == -1)
                {
                    log.AppendLine("No fractional row found for Gomory cut. Stopping.");
                    break;
                }

                AddGomoryCut(cutRow);
                log.AppendLine("Tableau after adding cut (s" + _numSlackVars + "):");
                log.Append(DisplayTableau());

                // --- Step 4: Re-optimize with dual simplex ---
                log.AppendLine("Re-optimizing after cut using dual simplex...");
                log.Append(RunDualSimplex());

                // Save updated tableau
                OptimalTableau = (double[,])_tableau.Clone();
                TableauList.Add((double[,])_tableau.Clone());

                iteration++;
            }

            log.AppendLine("Solver stopped. Final solution (may be non-integer):");
            log.Append(GetSolutionString());
            return log.ToString();
        }


        private void InitializeTableau()
        {
            _numVariables = _objective.Length;
            _numConstraints = _constraints.Count;
            _numSlackVars = _numConstraints;

            int rows = _numConstraints + 1;
            int cols = 1 + _numVariables + _numSlackVars + 1;

            _tableau = new double[rows, cols];
            _basis = new int[rows];

            // Z-row
            _tableau[0, 0] = 1;
            for (int j = 0; j < _numVariables; j++)
            {
                _tableau[0, j + 1] = _objectiveType == "max" ? -_objective[j] : _objective[j];
            }

            // Constraint rows
            for (int i = 0; i < _numConstraints; i++)
            {
                for (int j = 0; j < _numVariables; j++)
                    _tableau[i + 1, j + 1] = _constraints[i][j];

                // Slack variable
                _tableau[i + 1, 1 + _numVariables + i] = 1;
                _tableau[i + 1, cols - 1] = _rhs[i];

                _basis[i + 1] = _numVariables + 1 + i;
            }
        }

        private string RunSimplex()
        {
            StringBuilder log = new StringBuilder();
            while (true)
            {
                int pivotCol = FindPivotColumn();
                if (pivotCol == -1) break;

                int pivotRow = FindPivotRow(pivotCol);
                if (pivotRow == -1)
                {
                    log.AppendLine("Unbounded solution.");
                    return log.ToString();
                }

                Pivot(pivotRow, pivotCol);
                _basis[pivotRow] = pivotCol;
                log.Append(DisplayTableau());
            }
            return log.ToString();
        }

        private int FindPivotColumn()
        {
            int pivotCol = -1;
            double minVal = -Epsilon;
            for (int j = 1; j < _tableau.GetLength(1) - 1; j++)
            {
                if (_tableau[0, j] < minVal)
                {
                    minVal = _tableau[0, j];
                    pivotCol = j;
                }
            }
            return pivotCol;
        }

        private int FindPivotRow(int pivotCol)
        {
            int pivotRow = -1;
            double minRatio = double.MaxValue;
            for (int i = 1; i < _tableau.GetLength(0); i++)
            {
                if (_tableau[i, pivotCol] > Epsilon)
                {
                    double ratio = _tableau[i, _tableau.GetLength(1) - 1] / _tableau[i, pivotCol];
                    if (ratio < minRatio)
                    {
                        minRatio = ratio;
                        pivotRow = i;
                    }
                }
            }
            return pivotRow;
        }

        private void Pivot(int pivotRow, int pivotCol)
        {
            double pivot = _tableau[pivotRow, pivotCol];
            int cols = _tableau.GetLength(1);

            for (int j = 0; j < cols; j++)
                _tableau[pivotRow, j] /= pivot;

            for (int i = 0; i < _tableau.GetLength(0); i++)
            {
                if (i != pivotRow)
                {
                    double factor = _tableau[i, pivotCol];
                    for (int j = 0; j < cols; j++)
                        _tableau[i, j] -= factor * _tableau[pivotRow, j];
                }
            }
        }

        // ------------------- DUAL SIMPLEX -------------------
        private string RunDualSimplex()
        {
            StringBuilder log = new StringBuilder();
            while (true)
            {
                int pivotRow = FindDualPivotRow();
                if (pivotRow == -1) break;

                int pivotCol = FindDualPivotColumn(pivotRow);
                if (pivotCol == -1)
                {
                    log.AppendLine("Infeasible solution in Dual Simplex.");
                    return log.ToString();
                }

                Pivot(pivotRow, pivotCol);
                _basis[pivotRow] = pivotCol;
                log.Append(DisplayTableau());
            }
            return log.ToString();
        }

        private int FindDualPivotRow()
        {
            int pivotRow = -1;
            double minRhs = -Epsilon;
            for (int i = 1; i < _tableau.GetLength(0); i++)
            {
                if (_tableau[i, _tableau.GetLength(1) - 1] < minRhs)
                {
                    minRhs = _tableau[i, _tableau.GetLength(1) - 1];
                    pivotRow = i;
                }
            }
            return pivotRow;
        }

        private int FindDualPivotColumn(int pivotRow)
        {
            int pivotCol = -1;
            double minRatio = double.MaxValue;
            for (int j = 1; j < _tableau.GetLength(1) - 1; j++)
            {
                if (_tableau[pivotRow, j] < -Epsilon)
                {
                    double ratio = Math.Abs(_tableau[0, j] / _tableau[pivotRow, j]);
                    if (ratio < minRatio)
                    {
                        minRatio = ratio;
                        pivotCol = j;
                    }
                }
            }
            return pivotCol;
        }

        // ------------------- INTEGER CHECK -------------------
        private bool IsIntegerSolution()
        {
            for (int i = 1; i < _tableau.GetLength(0); i++)
            {
                double val = _tableau[i, _tableau.GetLength(1) - 1];
                if (Math.Abs(val - Math.Round(val)) > Epsilon) return false;
            }
            return true;
        }

        private int SelectCutRow()
        {
            int cutRow = -1;
            double maxFraction = -1.0;
            for (int i = 1; i < _tableau.GetLength(0); i++)
            {
                double rhs = _tableau[i, _tableau.GetLength(1) - 1];
                double frac = rhs - Math.Floor(rhs);
                if (frac > Epsilon && frac > maxFraction)
                {
                    maxFraction = frac;
                    cutRow = i;
                }
            }
            return cutRow;
        }

        private void AddGomoryCut(int cutRow)
        {
            _numSlackVars++;
            int oldRows = _tableau.GetLength(0);
            int oldCols = _tableau.GetLength(1);

            double[,] newTableau = new double[oldRows + 1, oldCols + 1];
            int[] newBasis = new int[oldRows + 1];

            // Copy old tableau
            for (int i = 0; i < oldRows; i++)
            {
                for (int j = 0; j < oldCols; j++)
                {
                    if (j < oldCols - 1) newTableau[i, j] = _tableau[i, j];
                    else newTableau[i, oldCols] = _tableau[i, j];
                }
                newBasis[i] = _basis[i];
            }

            // New cut row
            int newRow = oldRows;
            for (int j = 1; j < oldCols - 1; j++)
                newTableau[newRow, j] = -(_tableau[cutRow, j] - Math.Floor(_tableau[cutRow, j]));

            double rhs = _tableau[cutRow, oldCols - 1];
            newTableau[newRow, oldCols] = -(rhs - Math.Floor(rhs));
            newTableau[newRow, oldCols - 1] = 1.0;

            newBasis[newRow] = _numVariables + _numSlackVars;

            _tableau = newTableau;
            _basis = newBasis;
        }

        // ------------------- DISPLAY -------------------
        private string DisplayTableau()
        {
            int rows = _tableau.GetLength(0);
            int cols = _tableau.GetLength(1);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0,-7}", "Basis");
            sb.AppendFormat("{0,8}", "Z");
            for (int j = 1; j <= _numVariables; j++)
                sb.AppendFormat("x{0,-7}", j);
            for (int j = _numVariables + 1; j < cols - 1; j++)
                sb.AppendFormat("s{0,-7}", j - _numVariables);
            sb.AppendFormat("{0,8}", "RHS");
            sb.AppendLine();
            sb.AppendLine(new string('-', cols * 8 + 5));

            for (int i = 0; i < rows; i++)
            {
                if (i == 0) sb.AppendFormat("{0,-7}", "Z");
                else if (_basis[i] <= _numVariables) sb.AppendFormat("x{0,-7}", _basis[i]);
                else sb.AppendFormat("s{0,-7}", _basis[i] - _numVariables);

                for (int j = 0; j < cols; j++)
                    sb.AppendFormat("{0,8:F2}", _tableau[i, j]);

                sb.AppendLine();
            }
            sb.AppendLine();
            return sb.ToString();
        }

        private string GetSolutionString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("--- Solution ---");
            sb.AppendLine("Z = " + _tableau[0, _tableau.GetLength(1) - 1].ToString("F2"));

            double[] varValues = new double[_numVariables + 1];
            for (int i = 1; i < _basis.Length; i++)
            {
                if (_basis[i] <= _numVariables)
                    varValues[_basis[i]] = _tableau[i, _tableau.GetLength(1) - 1];
            }

            for (int i = 1; i <= _numVariables; i++)
                sb.AppendLine("x" + i + " = " + Math.Round(varValues[i]));

            sb.AppendLine("----------------");
            return sb.ToString();
        }
    }


}
