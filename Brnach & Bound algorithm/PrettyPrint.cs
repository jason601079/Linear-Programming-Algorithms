using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Brnach___Bound_algorithm
{
    internal class PrettyPrint
    {
        public static bool FormatLP(string raw, out string pretty)
        {
            //Normalize line endings and trim
            var lines = raw.Replace("\r\n", "\n").Replace('\r', '\n')
                .Split('\n')
                .Select(s => s.Trim())
                .ToList();

            var nonEmpty = lines.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
            if (nonEmpty.Count == 0)
            {
                pretty = ""; return false;
            }

            //1. OBJECTIVE FUNCTION
            //Scanning cleaned lines
            int idx = 0; 
            string objSense = null; //hold whether its a max or min problem
            double[] objCoeffs = null; //hold the coefficients 

            while (idx < nonEmpty.Count) //loops through lines until found or end
            {
                var line = nonEmpty[idx].ToLowerInvariant(); //Takes current line and makes it lowercase FOR CHECKING ONLY
                if (line.StartsWith("max") || line.StartsWith("min")) //if line begins with "max" or "min", found obj line,
                                                                        //this makes it dynamic meaning the objective function does not have to be the first line it can be anywhere
                {
                    objSense = line.StartsWith("max") ? "Max" : "Min"; //if line starts with "Max" its "Max" otherwise "Min"
                    var tokens = nonEmpty[idx].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries) //"tokens" are the coeff that gets stored
                                                .Skip(1) //skip the max or min word
                                                .ToArray();
                    objCoeffs = ParseCoeffList(tokens);
                    idx++;
                    break;
                }
                idx++;
            }
            //checks after trying to parse objective line, if it failed
            if(objCoeffs == null)
            {
                pretty = "";
                return false;
            }
            //if it worked
            int nVars = objCoeffs.Length; //number of variables in the problem

            //2. CONSTRAINTS
            var constraints = new List<(double[] coeffs, string rel, double rhs)>(); //list to store constraints

            foreach (var line in nonEmpty.Skip(idx))//loops through all lines after the obj function, skips the first idx lines
            {
                if (string.IsNullOrWhiteSpace(line)) continue;  //skips blank and whitespace lines

                var low = line.ToLowerInvariant(); //create a lowercase version of the line for easy case-insensitive checks

                //Stop if integrality/sign section
                if (low.Contains("int") || low.Contains("bin") || low.Contains("urs")) break; //if the line contains keywords, means it has reached the sign restrictions so stop reading ct's

                string rel = null;      //checks which relation symbol constraint uses store it in rel
                if (low.Contains("<=")) rel = "<=";
                else if (low.Contains(">=")) rel = ">=";
                else if (low.Contains("=")) rel = "=";

                if (rel == null) continue; //if no relation found skip line

                var parts = line.Split(new[] { "<=", ">=", "=" }, StringSplitOptions.None);//split line into two parts, lhs and rhs
                if (parts.Length != 2) continue; //If not 2 parts, format wrong, skip 

                var lhsTokens = parts[0].Split(new[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);//split lhs into tokens(numbers, variable names)
                var lhsCoeffs = ParseCoeffList(lhsTokens);//Parse into numbers with ParseCoeffList method
                lhsCoeffs = NormalizeLength(lhsCoeffs, nVars);

                if (!double.TryParse(parts[1].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out double rhs))//try to parse rhs as a number
                    continue;

                constraints.Add((lhsCoeffs, rel, rhs));//Stores constraint in constraint list
            }

            //3. Sign Restrictions
            bool[] isInt = new bool[nVars];
            bool[] isBin = new bool[nVars];
            bool[] isURS = new bool[nVars];

            var tagLine = nonEmpty.FirstOrDefault(l => l.Contains("int") || l.Contains("bin") || l.Contains("urs"));
            if (!string.IsNullOrEmpty(tagLine))
            {
                var tags = tagLine.Split(new[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries)
                                    .Select(t => t.ToLowerInvariant())
                                    .ToArray();

                for (int i = 0; i < Math.Min(nVars, tags.Length); i++)
                {
                    if (tags[i] == "int") isInt[i] = true;
                    else if (tags[i] == "bin") isBin[i] = true;
                    else if (tags[i] == "urs") isURS[i] = true;
                }
            }

            //4. Build pretty output
            var sb = new StringBuilder();

            sb.AppendLine($"{objSense} z = {FormatLinear(objCoeffs)}");
            sb.AppendLine();

            sb.AppendLine("Constraints:");
            if (constraints.Count == 0)
                sb.AppendLine(" (none detected)");
            else
                foreach (var c in constraints)
                    sb.AppendLine($" {FormatLinear(c.coeffs)} {PrettyRel(c.rel)} {TrimZeros(c.rhs)}");

            sb.AppendLine();
            sb.AppendLine("Variables:");
            if(nVars > 0)
            {
                var names = Enumerable.Range(1, nVars).Select(i => $"x{i}").ToArray();
                var ints = names.Where((_, i) => isInt[i]).ToArray();
                var bins = names.Where((_, i) => isBin[i]).ToArray();
                var urs = names.Where((_, i) => isURS[i]).ToArray();
                var rest = names.Where((_, i) => !isInt[i] && !isBin[i] && !isURS[i]).ToArray();

                if (ints.Length > 0) sb.AppendLine($"  {string.Join(", ", ints)} >= 0 INTEGER");
                if (bins.Length > 0) sb.AppendLine($"  {string.Join(", ", bins)} BINARY");
                if (urs.Length > 0) sb.AppendLine($"  {string.Join(", ", urs)} UNRESTRICTED");
                if (rest.Length > 0) sb.AppendLine($"  {string.Join(", ", rest)} ≥ 0 (real)");
            }

            pretty = sb.ToString();
            return true;
        }

        private static double[] ParseCoeffList(IEnumerable<string> tokens)
        {
            var list = new List<double>();
            foreach (var t in tokens)
            {
                if(double.TryParse(t, NumberStyles.Float, CultureInfo.InvariantCulture, out double v)) 
             //Tries to turn string into a number. Allows decimal points, signs, and exponent notation. Makes sure it always uses "." as the decimal seperator                                   
                    list.Add(v);     //if parsing works, the number goes to v                    
            }
            return list.ToArray();
        }

        private static double[] NormalizeLength(double[] coeffs, int nVars)
        {
            if (coeffs.Length == nVars) return coeffs;
            if (coeffs.Length > nVars) return coeffs.Take(nVars).ToArray();
            var arr = new double[nVars];
            Array.Copy(coeffs, arr, coeffs.Length);
            return arr;
        }

        private static string FormatLinear(double[] coeffs)
        {
            var parts = new List<string>();
            for(int i = 0; i < coeffs.Length; i++)
            {
                double c = coeffs[i];
                if (Math.Abs(c) < 1e-12) continue;
                string coef = TrimZeros(c);
                string term = coef == "1" ? $"x{i + 1}" :
                              coef == "-1" ? $"-x{i + 1}" :
                              $"{coef}x{i + 1}";
                parts.Add(term);
            }
            if (parts.Count == 0) return "0";
            var expr = parts[0];
            for (int i = 1; i < parts.Count; i++)
                expr += parts[i].StartsWith("-") ? " - " + parts[i].Substring(1) : " + " + parts[i];
            return expr;
        }

        private static string TrimZeros(double v) =>
            v.ToString("0.###", CultureInfo.InvariantCulture);

        private static string PrettyRel(string rel) =>
            rel == "<=" ? "<=" : rel == ">=" ? ">=" : "=";
    }
}
