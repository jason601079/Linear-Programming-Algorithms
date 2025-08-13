using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;

/// <summary>
/// Basic primal simplex implementation that expects:
///  - Maximization problem
///  - Constraints all <= with RHS >= 0
///  - All decision variables non-negative (+)
/// This is intentionally small and educational, not an industrial-strength solver.
/// </summary>
public class TestAlgo
{
    private readonly LPData _lp;

    public TestAlgo(LPData lpData)
    {
        _lp = lpData ?? throw new ArgumentNullException(nameof(lpData));
        ValidateLpSupportsSimplex(_lp);
    }

    private void ValidateLpSupportsSimplex(LPData lp)
    {
        // Only allow maximization (for simplicity). If min, the caller could multiply objective by -1.
        if (lp.Objective.Type != ProblemType.Max)
            throw new NotSupportedException("TestAlgo only supports maximization problems. Convert 'min' to 'max' by multiplying objective by -1 before calling.");

        int n = lp.VariableCount;

        // Require sign restrictions all to be Positive
        for (int i = 0; i < lp.SignRestrictions.Length; i++)
        {
            if (lp.SignRestrictions[i] != SignRestriction.Positive)
                throw new NotSupportedException($"Sign restriction for variable {i + 1} must be '+' (non-negative). Found: {lp.SignRestrictions[i]}");
        }

        // Require all constraints to be <= and RHS >= 0
        for (int i = 0; i < lp.Constraints.Count; i++)
        {
            var c = lp.Constraints[i];
            if (c.Relation != Relation.LessOrEqual)
                throw new NotSupportedException($"Constraint {i + 1} relation must be '<=' for the TestAlgo (found {c.Relation}).");

            if (c.Rhs < 0)
                throw new NotSupportedException($"Constraint {i + 1} RHS must be non-negative for this simple implementation (found {c.Rhs}).");
            if (c.Coefficients.Length != n)
                throw new FormatException($"Constraint {i + 1} has inconsistent number of coefficients.");
        }
    }

    /// <summary>
    /// Runs the primal simplex and returns a SimplexResult.
    /// </summary>
    public SimplexResult RunSimplex(int maxIterations = 1000, double eps = 1e-9)
    {
        int m = _lp.Constraints.Count;      // number of constraints (rows)
        int n = _lp.VariableCount;          // original decision variables
        // We'll add m slack variables -> total columns = n + m (plus RHS)
        int totalVars = n + m;
        int cols = totalVars + 1; // +1 for RHS column
        int rows = m + 1;         // +1 for objective row

        // Build tableau: rows x cols
        // Row 0..m-1 -> constraints; Row m -> objective
        double[,] tableau = new double[rows, cols];

        // populate constraint rows: [A | I_slack | RHS]
        for (int i = 0; i < m; i++)
        {
            var c = _lp.Constraints[i];
            for (int j = 0; j < n; j++)
                tableau[i, j] = c.Coefficients[j];

            // slack variable column (n + i)
            tableau[i, n + i] = 1.0;

            tableau[i, cols - 1] = c.Rhs; // RHS
        }

        // objective row (last): -c for maximization (simplex uses minimizing reduced costs)
        for (int j = 0; j < n; j++)
            tableau[rows - 1, j] = -_lp.Objective.Coefficients[j];

        // slack columns already 0 in objective row; RHS objective is 0

        // Basic variable tracking: which variable index is basic in each constraint row
        // we map row i -> basicVarIndex (initially slack n+i)
        int[] basicVarIndex = new int[m];
        for (int i = 0; i < m; i++) basicVarIndex[i] = n + i;

        var log = new List<string>();
        log.Add($"Initial tableau: {m} constraints, {n} variables (+{m} slacks).");

        int iter = 0;
        while (iter < maxIterations)
        {
            iter++;

            // Choose entering variable (most negative coefficient in objective row among variable cols)
            int enteringCol = -1;
            double mostNegative = -eps;
            for (int j = 0; j < totalVars; j++)
            {
                double val = tableau[rows - 1, j];
                if (val < mostNegative)
                {
                    mostNegative = val;
                    enteringCol = j;
                }
            }

            if (enteringCol == -1)
            {
                // optimal
                double objVal = tableau[rows - 1, cols - 1];
                var solution = ExtractSolution(tableau, basicVarIndex, n, totalVars, cols);
                log.Add($"Optimal found in {iter - 1} iterations. Objective = {objVal.ToString("G", CultureInfo.InvariantCulture)}");
                return new SimplexResult(SimplexStatus.Optimal, objVal, solution, iter - 1, log);
            }

            // Choose leaving row via minimum ratio test
            int leavingRow = -1;
            double bestRatio = double.PositiveInfinity;
            for (int i = 0; i < m; i++)
            {
                double a_ij = tableau[i, enteringCol];
                if (a_ij > eps)
                {
                    double ratio = tableau[i, cols - 1] / a_ij;
                    if (ratio < bestRatio - 1e-12) // tie broken by smaller ratio
                    {
                        bestRatio = ratio;
                        leavingRow = i;
                    }
                }
            }

            if (leavingRow == -1)
            {
                // unbounded
                log.Add($"Unbounded detected – entering column {enteringCol + 1} has no positive pivots.");
                return new SimplexResult(SimplexStatus.Unbounded, double.NaN, null, iter - 1, log);
            }

            log.Add($"Iter {iter}: Pivot on row {leavingRow + 1}, col {enteringCol + 1} (basic var {basicVarIndex[leavingRow] + 1} -> {enteringCol + 1}). Ratio = {bestRatio.ToString("G", CultureInfo.InvariantCulture)}");

            // Pivot operation: make tableau[leavingRow, enteringCol] == 1 and eliminate others
            Pivot(tableau, leavingRow, enteringCol);

            // update basic var
            basicVarIndex[leavingRow] = enteringCol;
        }

        // If we get here, iteration limit hit
        log.Add($"Iteration limit ({maxIterations}) reached without optimality.");
        return new SimplexResult(SimplexStatus.IterationLimit, double.NaN, null, iter, log);
    }

    private static void Pivot(double[,] tab, int pivotRow, int pivotCol)
    {
        int rows = tab.GetLength(0);
        int cols = tab.GetLength(1);

        double pivot = tab[pivotRow, pivotCol];
        if (Math.Abs(pivot) < 1e-15)
            throw new InvalidOperationException("Pivot element is zero.");

        // normalize pivot row
        for (int j = 0; j < cols; j++)
            tab[pivotRow, j] /= pivot;

        // eliminate pivotCol from other rows
        for (int i = 0; i < rows; i++)
        {
            if (i == pivotRow) continue;
            double factor = tab[i, pivotCol];
            if (Math.Abs(factor) < 1e-15) continue;
            for (int j = 0; j < cols; j++)
                tab[i, j] -= factor * tab[pivotRow, j];
        }
    }

    private static double[] ExtractSolution(double[,] tableau, int[] basicVarIndex, int originalN, int totalVars, int cols)
    {
        double[] sol = new double[originalN];
        int m = basicVarIndex.Length;
        // initialize to 0
        for (int i = 0; i < originalN; i++) sol[i] = 0.0;

        for (int i = 0; i < m; i++)
        {
            int varIdx = basicVarIndex[i];
            if (varIdx < originalN)
            {
                // original variable is basic in this row
                sol[varIdx] = tableau[i, cols - 1];
            }
        }
        return sol;
    }
}

public enum SimplexStatus
{
    Optimal,
    Unbounded,
    IterationLimit
}

public class SimplexResult
{
    public SimplexStatus Status { get; }
    public double ObjectiveValue { get; }
    public double[] PrimalSolution { get; } // length = original variable count (only original variables)
    public int Iterations { get; }
    public IReadOnlyList<string> Log { get; }

    public SimplexResult(SimplexStatus status, double objValue, double[] sol, int iterations, IReadOnlyList<string> log)
    {
        Status = status;
        ObjectiveValue = objValue;
        PrimalSolution = sol;
        Iterations = iterations;
        Log = log ?? new List<string>();
    }
}
