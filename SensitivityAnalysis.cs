using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Linear_Programming_Algorithms.Analysis
{
    public class SensitivityAnalysis
    {
        private readonly Primal _primal;

        public SensitivityAnalysis(Primal primal)
        {
            _primal = primal;
        }

        /// Get allowable range for a non-basic variable (approximation using reduced cost).
        public (double min, double max) NonBasicVariableRange(int variableIndex)
        {
            var tableau = _primal.TableauPublic;
            int numRows = tableau.GetLength(0) - 1;
            int lastCol = tableau.GetLength(1) - 1;

            double reducedCost = tableau[numRows, variableIndex];

            double min = reducedCost >= 0 ? 0 : double.NegativeInfinity;
            double max = reducedCost <= 0 ? 0 : double.PositiveInfinity;

            return (min, max);
        }

        /// Apply a change to the objective coefficient of a variable.
        public void ApplyVariableChange(int variableIndex, double newCoefficient)
        {
            int lastRow = _primal.TableauPublic.GetLength(0) - 1;
            _primal.TableauPublic[lastRow, variableIndex] = -newCoefficient;
        }
      
        /// Get allowable range for a basic variable (using RHS fractional parts).
        public (double min, double max) BasicVariableRange(int rowIndex)
        {
            var tableau = _primal.TableauPublic;
            int lastCol = tableau.GetLength(1) - 1;
            double rhs = tableau[rowIndex, lastCol];

            double min = Math.Floor(rhs);
            double max = Math.Ceiling(rhs);

            return (min, max);
        }

        /// Apply a change to a basic variable by updating its RHS.
        public void ApplyBasicVariableChange(int rowIndex, double newValue)
        {
            int lastCol = _primal.TableauPublic.GetLength(1) - 1;
            _primal.TableauPublic[rowIndex, lastCol] = newValue;
        }

        /// Get the range for a constraint RHS (shadow price approximation).
        public (double min, double max) ConstraintRHSRange(int constraintIndex)
        {
            var tableau = _primal.TableauPublic;
            int lastCol = tableau.GetLength(1) - 1;
            double rhs = tableau[constraintIndex, lastCol];

            double min = Math.Floor(rhs);
            double max = Math.Ceiling(rhs);

            return (min, max);
        }

        /// Apply a change to a constraint RHS.
        public void ApplyConstraintChange(int constraintIndex, double newRHS)
        {
            int lastCol = _primal.TableauPublic.GetLength(1) - 1;
            _primal.TableauPublic[constraintIndex, lastCol] = newRHS;
        }

        /// Get shadow price for a constraint (dual value approximation).
        public double GetShadowPrice(int constraintIndex)
        {
            // Simplified: take the coefficient from the objective row in the canonical tableau
            var tableau = _primal.TableauPublic;
            int lastRow = tableau.GetLength(0) - 1;

            // For a more accurate dual value, you'd extract it from the final tableau of the dual
            return tableau[lastRow, constraintIndex];
        }

        /// Add a new activity (variable) to the optimal solution.
        public void AddNewVariable(List<double> coefficients, double objectiveCoeff)
        {
            int oldRows = _primal.TableauPublic.GetLength(0);
            int oldCols = _primal.TableauPublic.GetLength(1);

            double[,] newTableau = new double[oldRows, oldCols + 1];

            for (int i = 0; i < oldRows; i++)
                for (int j = 0; j < oldCols; j++)
                    newTableau[i, j] = _primal.TableauPublic[i, j];

            for (int i = 0; i < oldRows - 1; i++)
                newTableau[i, oldCols - 1] = coefficients[i];

            newTableau[oldRows - 1, oldCols - 1] = -objectiveCoeff;

            _primal.UpdateTableau(newTableau);

        }

        /// Add a new constraint to the optimal solution.
        public void AddNewConstraint(List<double> coefficients, double rhs, string inequality = "<=")
        {
            int oldRows = _primal.TableauPublic.GetLength(0);
            int oldCols = _primal.TableauPublic.GetLength(1);

            double[,] newTableau = new double[oldRows + 1, oldCols];

            for (int i = 0; i < oldRows; i++)
                for (int j = 0; j < oldCols; j++)
                    newTableau[i, j] = _primal.TableauPublic[i, j];

            for (int j = 0; j < coefficients.Count; j++)
                newTableau[oldRows, j] = (inequality == "<=") ? coefficients[j] : -coefficients[j];

            newTableau[oldRows, oldCols - 1] = rhs;

            _primal.UpdateTableau(newTableau);

        }

        /// Solve the dual programming model based on the primal tableau.
        public void SolveDual()
        {
            Dual dual = new Dual(_primal.TableauPublic, _primal.TableauPublic.GetLength(0) - 1,
                                 _primal.TableauPublic.GetLength(1) - 1);
            dual.Solve();
        }
    }
}
