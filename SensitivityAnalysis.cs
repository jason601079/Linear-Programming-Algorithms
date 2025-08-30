using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Linear_Programming_Algorithms
{

    public class SensitivityAnalysis
    {
        private readonly Primal _tableau;
        private readonly int _n; // decision variables
        private readonly int _m; // slack variables
        private readonly double[,] _tab;
        private readonly int[] _basicVariables;
        private List<int> _nonBasicVars;

        public SensitivityAnalysis(Primal model)
        {
            _tableau = model;

            // Use what Primal actually provides
            _tab = model.TableauPublic;
            _n = model.NumVariables;
            _m = _tab.GetLength(1) - _n - 1; // slack variables = total cols - decision vars - RHS

            // Initialize basic variables manually (common assumption: slack vars are basic initially)
            int numConstraints = model.NumConstraints;
            _basicVariables = new int[numConstraints];
            for (int i = 0; i < numConstraints; i++)
            {
                _basicVariables[i] = _n + i; // slack variables are at the end
            }

            _nonBasicVars = GetNonBasics();
        }


        private List<int> GetNonBasics()
        {
            var nonBasics = new List<int>();
            var basicSet = new HashSet<int>(_basicVariables);
            for (int c = 0; c < _n + _m; c++)
            {
                if (!basicSet.Contains(c)) nonBasics.Add(c);
            }
            return nonBasics;
        }

        public (double minDelta, double maxDelta) GetObjectiveCoefRange(int j)
        {
            if (j < 0 || j >= _n) throw new ArgumentOutOfRangeException(nameof(j));
            bool isBasic = Array.IndexOf(_basicVariables, j) >= 0;

            if (!isBasic)
            {
                double bottomJ = _tab[_m, j];
                return (double.NegativeInfinity, bottomJ);
            }
            else
            {
                int r = Array.IndexOf(_basicVariables, j);
                var lowers = new List<double>();
                var uppers = new List<double>();

                foreach (int nb in _nonBasicVars)
                {
                    double a = _tab[r, nb];
                    if (Math.Abs(a) < 1e-9) continue;
                    double bound = -_tab[_m, nb] / a;
                    if (a > 0) lowers.Add(bound); else uppers.Add(bound);
                }

                double minD = lowers.Count > 0 ? lowers.Max() : double.NegativeInfinity;
                double maxD = uppers.Count > 0 ? uppers.Min() : double.PositiveInfinity;
                return (minD, maxD);
            }
        }

        public (double minGamma, double maxGamma) GetRHSRange(int i)
        {
            if (i < 0 || i >= _m) throw new ArgumentOutOfRangeException(nameof(i));
            int slackCol = _n + i;
            var lowers = new List<double>();
            var uppers = new List<double>();

            for (int k = 0; k < _m; k++)
            {
                double a = _tab[k, slackCol];
                if (Math.Abs(a) < 1e-9) continue;
                double rhsK = _tab[k, _n + _m];
                double bound = -rhsK / a;
                if (a > 0) lowers.Add(bound); else uppers.Add(bound);
            }

            double minG = lowers.Count > 0 ? lowers.Max() : double.NegativeInfinity;
            double maxG = uppers.Count > 0 ? uppers.Min() : double.PositiveInfinity;
            return (minG, maxG);
        }

        public (double minDelta, double maxDelta) GetMatrixCoefRange(int i, int j)
        {
            if (i < 0 || i >= _m) throw new ArgumentOutOfRangeException(nameof(i));
            if (j < 0 || j >= _n) throw new ArgumentOutOfRangeException(nameof(j));
            if (Array.IndexOf(_basicVariables, j) >= 0)
                throw new InvalidOperationException("Matrix coefficient range is only for non-basic variables.");

            int slackCol = _n + i;
            double pi = _tab[_m, slackCol];
            double bottomJ = _tab[_m, j];

            if (Math.Abs(pi) < 1e-9)
                return (double.NegativeInfinity, double.PositiveInfinity);

            double bound = -bottomJ / pi;
            return pi > 0 ? (bound, double.PositiveInfinity) : (double.NegativeInfinity, bound);
        }

        public string FormatRange(double min, double max, double current = 0)
        {
            string minStr = double.IsNegativeInfinity(min) ? "-∞" : (current + min).ToString("F4");
            string maxStr = double.IsPositiveInfinity(max) ? "+∞" : (current + max).ToString("F4");
            return $"Range: [{minStr}, {maxStr}] (delta: [{(double.IsNegativeInfinity(min) ? "-∞" : min.ToString("F4"))}, {(double.IsPositiveInfinity(max) ? "+∞" : max.ToString("F4"))}])";
        }

        public string AnalyzeNewVariable(double newVarObjective, List<double> newVarCoeffs)
        {
            int numConstraints = _m;
            double[,] b_inv = new double[numConstraints, numConstraints];

            for (int i = 0; i < numConstraints; i++)
                for (int j = 0; j < numConstraints; j++)
                    b_inv[i, j] = _tab[i, _n + j];

            double[] newCol = new double[numConstraints];
            for (int i = 0; i < numConstraints; i++)
                for (int j = 0; j < newVarCoeffs.Count; j++)
                    newCol[i] += b_inv[i, j] * newVarCoeffs[j];

            double zj = 0;
            for (int i = 0; i < numConstraints; i++)
            {
                int basicVarIndex = _basicVariables[i];
                double cb = basicVarIndex < _n ? _tab[_m, basicVarIndex] : 0;
                zj += cb * newCol[i];
            }
            double zRowValue = zj - newVarObjective;

            return $"New variable Zj-Cj: {zRowValue:F4} (column: [{string.Join(", ", newCol.Select(v => v.ToString("F2")))}])";
        }

        public string AnalyzeNewConstraint(List<double> newConstrCoeffs, double newRhs)
        {
            double lhs = 0.0;  // must be double
            for (int i = 0; i < _m; i++)
            {
                int basicVar = _basicVariables[i];
                lhs += basicVar < _n ? newConstrCoeffs[basicVar] * _tab[i, _n + _m] : 0.0;
            }

            return $"New constraint LHS: {lhs}, RHS: {newRhs}";
        }

    }
}
