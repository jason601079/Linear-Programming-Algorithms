using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

public enum ProblemType { Max, Min }
public enum Relation { LessOrEqual, GreaterOrEqual, Equal }
public enum SignRestriction { Positive, Negative, Unrestricted, Integer, Binary }

public class ObjectiveFunction
{
    public ProblemType Type { get; }
    public double[] Coefficients { get; }

    public ObjectiveFunction(ProblemType type, double[] coeffs)
    {
        Type = type;
        Coefficients = coeffs ?? throw new ArgumentNullException(nameof(coeffs));
    }
}

public class Constraint
{
    public double[] Coefficients { get; }
    public Relation Relation { get; }
    public double Rhs { get; }

    public Constraint(double[] coeffs, Relation relation, double rhs)
    {
        Coefficients = coeffs ?? throw new ArgumentNullException(nameof(coeffs));
        Relation = relation;
        Rhs = rhs;
    }
}

public class LPData
{
    public ObjectiveFunction Objective { get; }
    public List<Constraint> Constraints { get; }
    public SignRestriction[] SignRestrictions { get; }

    public int VariableCount => Objective.Coefficients.Length;

    public LPData(ObjectiveFunction objective, List<Constraint> constraints, SignRestriction[] signRestrictions)
    {
        Objective = objective ?? throw new ArgumentNullException(nameof(objective));
        Constraints = constraints ?? throw new ArgumentNullException(nameof(constraints));
        SignRestrictions = signRestrictions ?? throw new ArgumentNullException(nameof(signRestrictions));

        if (SignRestrictions.Length != VariableCount)
            throw new ArgumentException("Sign restrictions must match number of decision variables.");
    }

    // Parse file and return LPData or throw FormatException on invalid format
    public static LPData Parse(string path)
    {
        if (path == null) throw new ArgumentNullException(nameof(path));
        var linesRaw = File.ReadAllLines(path)
                           .Select(l => l.Trim())
                           .Where(l => !string.IsNullOrWhiteSpace(l) && !l.StartsWith("#")) // ignore blank and comment lines
                           .ToArray();

        if (linesRaw.Length < 3)
            throw new FormatException("File must contain at least: objective line, one constraint, and sign restrictions line.");

        // Utility split (whitespace tolerant)
        string[] SplitTokens(string s) => Regex.Split(s.Trim(), @"\s+");

        // ---- Objective line ----
        var firstTokens = SplitTokens(linesRaw[0]);
        if (firstTokens.Length < 2)
            throw new FormatException("Objective line must be: 'max'/'min' followed by coefficients.");

        var typeStr = firstTokens[0].ToLowerInvariant();
        ProblemType ptype;
        if (typeStr == "max") ptype = ProblemType.Max;
        else if (typeStr == "min") ptype = ProblemType.Min;
        else throw new FormatException("Objective line must start with 'max' or 'min'.");

        int varCount = firstTokens.Length - 1;
        double[] objCoeffs = new double[varCount];
        for (int i = 0; i < varCount; i++)
        {
            objCoeffs[i] = ParseDouble(firstTokens[i + 1], $"objective coefficient #{i + 1}");
        }

        var objective = new ObjectiveFunction(ptype, objCoeffs);

        // ---- Constraints ----
        var constraints = new List<Constraint>();
        for (int i = 1; i < linesRaw.Length - 1; i++)
        {
            var tokens = SplitTokens(linesRaw[i]);
            if (tokens.Length < varCount + 2)
                throw new FormatException($"Constraint line {i + 1} has too few tokens. Expected at least {varCount + 2}.");

            // first varCount tokens => coefficients
            var coeffs = new double[varCount];
            for (int j = 0; j < varCount; j++)
                coeffs[j] = ParseDouble(tokens[j], $"constraint {i} coefficient #{j + 1}");

            // relation token
            string relToken = tokens[varCount];
            Relation rel;
            if (relToken == "<=" || relToken.Equals("le", StringComparison.OrdinalIgnoreCase)) rel = Relation.LessOrEqual;
            else if (relToken == ">=" || relToken.Equals("ge", StringComparison.OrdinalIgnoreCase)) rel = Relation.GreaterOrEqual;
            else if (relToken == "=" || relToken.Equals("eq", StringComparison.OrdinalIgnoreCase)) rel = Relation.Equal;
            else throw new FormatException($"Invalid relation in constraint {i}: '{relToken}'. Use <=, >= or =.");

            // RHS is next token
            if (tokens.Length < varCount + 2 + 0) { /*noop*/ } // just clarity
            if (!TryParseDouble(tokens[varCount + 1], out double rhs))
                throw new FormatException($"Invalid RHS in constraint {i}: '{tokens[varCount + 1]}'.");

            // If extra tokens exist (beyond expected), that's an error
            if (tokens.Length > varCount + 2)
                throw new FormatException($"Constraint {i} has extra tokens starting at '{tokens[varCount + 2]}'.");

            constraints.Add(new Constraint(coeffs, rel, rhs));
        }

        // ---- Sign restrictions (last line) ----
        var lastTokens = SplitTokens(linesRaw[linesRaw.Length - 1]);
        if (lastTokens.Length != varCount)
            throw new FormatException($"Sign restriction line must have exactly {varCount} tokens (one per variable).");

        var signRestrictions = new SignRestriction[varCount];
        for (int i = 0; i < varCount; i++)
        {
            signRestrictions[i] = ParseSignToken(lastTokens[i], i + 1);
        }

        // Final consistency checks (every constraint must have same number of coefficients)
        foreach (var c in constraints)
        {
            if (c.Coefficients.Length != varCount)
                throw new FormatException("Inconsistent number of coefficients in constraints.");
        }

        return new LPData(objective, constraints, signRestrictions);
    }

    // Helper to parse sign tokens like "+", "-", "urs", "int", "bin"
    private static SignRestriction ParseSignToken(string token, int varIndex)
    {
        if (token == null) throw new ArgumentNullException(nameof(token));

        token = token.Trim().ToLowerInvariant();

        switch (token)
        {
            case "+":
                return SignRestriction.Positive;
            case "-":
                return SignRestriction.Negative;
            case "urs":
            case "u":
            case "free":
                return SignRestriction.Unrestricted;
            case "int":
                return SignRestriction.Integer;
            case "bin":
                return SignRestriction.Binary;
            default:
                throw new FormatException(
                    $"Invalid sign restriction for variable {varIndex}: '{token}'. Allowed: +, -, urs, int, bin.");
        }
    }


    private static double ParseDouble(string token, string context)
    {
        if (!TryParseDouble(token, out double val))
            throw new FormatException($"Invalid number for {context}: '{token}'.");
        return val;
    }

    private static bool TryParseDouble(string token, out double value)
    {
        // allow scientific, signs, decimal point — use invariant culture
        return double.TryParse(token, NumberStyles.Float | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out value);
    }
}
