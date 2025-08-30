using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linear_Programming_Algorithms.BNB
{
    internal sealed class BranchAndBoundSolver
    {
        private readonly IRelaxationSolver _relax;
        private readonly bool _isMax;
        private readonly int[] _intVars; 

        
        private readonly double[,] _A;
        private readonly double[] _b;
        private readonly double[] _c;
        private readonly int _m; 
        private readonly int _n; 



        private readonly List<BnBNode> _open = new List<BnBNode>();  
        private readonly List<BnBNode> _history = new List<BnBNode>();
        private int _nextId = 0;

        public bool IsFinished { get; private set; }
        public double[] IncumbentX { get; private set; }
        public double IncumbentZ { get; private set; }
        public BnBNode Current { get; private set; }

        public BranchAndBoundSolver(
            IRelaxationSolver relax,
            double[,] A, double[] b, double[] c,
            IEnumerable<int> integerVarIndices,
            bool isMax)
        {
            _relax = relax;
            _A = A; _b = b; _c = c;
            _isMax = isMax;
            _intVars = integerVarIndices?.ToArray() ?? Enumerable.Range(0, c.Length).ToArray();
            IncumbentZ = _isMax ? double.NegativeInfinity : double.PositiveInfinity;
            _m = A.GetLength(0);
            _n = A.GetLength(1);
        }

        public void Initialize()
        {
            _open.Clear();
            _history.Clear();
            _nextId = 0;
            IsFinished = false;
            IncumbentX = null;
            IncumbentZ = _isMax ? double.NegativeInfinity : double.PositiveInfinity;

            var root = new BnBNode(_nextId++, depth: 0);
            _open.Add(root);
        }

        public IEnumerable<string> Step()
        {
            var log = new List<string>();
            if (IsFinished) { log.Add("BnB: finished."); return log; }
            if (_open.Count == 0) { IsFinished = true; log.Add("BnB: no more nodes. Done."); return log; }

            var node = _open[_open.Count - 1];
            _open.RemoveAt(_open.Count - 1);
            Current = node;

            log.Add($"— Solve relaxation at Node {node.Id} (depth {node.Depth})");

            
            var rr = _relax.Solve(_A, _b, _c, node.Bounds, _isMax);
            if (!rr.Feasible)
            {
                node.Status = NodeStatus.Infeasible;
                log.Add($"   Infeasible relaxation ⇒ fathom.");
                _history.Add(node);
                return log;
            }

            node.X = rr.X;
            node.Z = rr.Z;

            if (HasWorseThanIncumbent(node.Z))
            {
                node.Status = NodeStatus.Pruned;
                log.Add($"   Bound {node.Z:F3} not better than incumbent {IncumbentZ:F3} ⇒ prune.");
                _history.Add(node);
                return log;
            }

           
            int fracIndex = FirstFractionalInteger(node.X);
            if (fracIndex < 0)
            {
                node.Status = NodeStatus.IntegerFeasible;
                UpdateIncumbent(node);
                log.Add($"   Integer feasible at z = {node.Z:F3} ⇒ update incumbent.");
                _history.Add(node);
                return log;
            }

            node.Status = NodeStatus.Fractional;
            node.BranchedVar = fracIndex;
            double val = node.X[fracIndex];
            double floor = Math.Floor(val);
            double ceil = Math.Ceiling(val);
            log.Add($"   Fractional x{fracIndex + 1} = {val:F3} ⇒ branch: x{fracIndex + 1} ≤ {floor}  and  x{fracIndex + 1} ≥ {ceil}");

           
            var left = node.CloneChild(_nextId++);
            left.Bounds.Add(new VarBound(fracIndex, upper: floor));

            var right = node.CloneChild(_nextId++);
            right.Bounds.Add(new VarBound(fracIndex, lower: ceil));

            _open.Add(right);
            _open.Add(left);

            _history.Add(node);

          
            if (rr.TableauList != null && rr.TableauList.Count > 0)
            {
                int k = 1;
                foreach (var t in rr.TableauList)
                {
                    log.Add($"      Tableau {k++}:");
                    log.Add(RenderTableau(t));
                }
                log.Add("      Optimal Tableau:");
                log.Add(RenderTableau(rr.OptimalTableau));
            }

            
            if (_open.Count == 0) IsFinished = true;
            return log;
        }

        private string RenderTableau(double[,] T)
        {
            if (T == null) return "(no tableau)";

            int rows = T.GetLength(0);
            int cols = T.GetLength(1);
            int rhsCol = cols - 1;
            int slackCount = Math.Max(0, cols - _n - 1);

            // Build column labels: t-*, x1..xn, s1..sK, rhs
            var colLabels = new List<string>();
            colLabels.Add("t-*");
            for (int j = 0; j < _n && 1 + j < cols; j++) colLabels.Add($"x{j + 1}");
            for (int k = 0; k < slackCount; k++) colLabels.Add($"s{k + 1}");
            colLabels.Add("rhs");

            // Row label function
            string RowLabel(int r)
            {
                if (r == 0) return "z";
                int idx = r - 1;
                if (idx < _m) return $"c{idx + 1}";
                return $"b{idx - _m + 1}";
            }

            // --- compute widths per column (including header)
            int[] widths = new int[colLabels.Count];
            for (int c = 0; c < colLabels.Count; c++)
                widths[c] = Math.Max(widths[c], colLabels[c].Length);

            void Fit(int col, string s) => widths[col] = Math.Max(widths[col], s.Length);

            // data widths: col 0 = row labels, others = numbers
            for (int r = 0; r < rows; r++)
            {
                Fit(0, RowLabel(r));
                for (int c = 0; c < cols; c++)
                {
                    string num = T[r, c].ToString("0.###");
                    Fit(c + 1, num);
                }
            }

            // helpers to draw lines/cells
            string H(char left, char mid, char right)
            {
                var parts = new List<string>();
                for (int c = 0; c < widths.Length; c++)
                {
                    // use box-drawing; if your font doesn't show it, swap '─' for '-'
                    parts.Add(new string('─', widths[c] + 2));
                }

                return $"{left}{string.Join(mid.ToString(), parts)}{right}";
            }

            string Row(params string[] cells)
            {
                var items = new List<string>();
                for (int c = 0; c < cells.Length; c++)
                {
                    // right-align numbers (cols >= 1), left-align label col (0)
                    bool numberCol = c >= 1;
                    string s = cells[c];
                    if (numberCol) s = s.PadLeft(widths[c]); else s = s.PadRight(widths[c]);
                    items.Add(" " + s + " ");
                }
                return "│" + string.Join("│", items) + "│";
            }

            var lines = new List<string>();
            lines.Add(H('┌', '┬', '┐'));                             // top
            lines.Add(Row(colLabels.ToArray()));
            lines.Add(H('├', '┼', '┤'));                             // header sep

            for (int r = 0; r < rows; r++)
            {
                var cells = new List<string> { RowLabel(r) };
                for (int c = 0; c < cols; c++)
                    cells.Add(T[r, c].ToString("0.###"));
                lines.Add(Row(cells.ToArray()));

                if (r == 0) lines.Add(H('├', '┼', '┤'));             // horizontal line after z-row (optional)
            }

            lines.Add(H('└', '┴', '┘'));                             // bottom
            return string.Join(Environment.NewLine, lines);
        }

        private int FirstFractionalInteger(double[] x)
        {
            foreach (var i in _intVars)
            {
                double v = x[i];
                if (double.IsNaN(v) || double.IsInfinity(v)) continue;
                double frac = Math.Abs(v - Math.Round(v));
                if (frac > 1e-7) return i;
            }
            return -1;
        }

        private bool HasWorseThanIncumbent(double z)
        {
            if (IncumbentX == null) return false;
            return _isMax ? z <= IncumbentZ + 1e-9 : z >= IncumbentZ - 1e-9;
        }

        private void UpdateIncumbent(BnBNode node)
        {
            if (IncumbentX == null)
            {
                IncumbentX = (double[])node.X.Clone();
                IncumbentZ = node.Z;
                return;
            }
            if (_isMax ? node.Z > IncumbentZ + 1e-9 : node.Z < IncumbentZ - 1e-9)
            {
                IncumbentX = (double[])node.X.Clone();
                IncumbentZ = node.Z;
            }
        }
    }
}
