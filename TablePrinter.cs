using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linear_Programming_Algorithms
{
    internal static class TablePrinter
    {
        // Prints a box-drawn table for a simplex tableau.
        // T:    tableau [rows x cols]
        // n:    # of original decision variables (x1..xn)
        // m:    # of original constraints (for c1..cm row labels)
        public static string Box(double[,] T, int n, int m)
        {
            if (T == null) return "(no tableau)";
            int rows = T.GetLength(0);
            int cols = T.GetLength(1);
            int rhsCol = cols - 1;
            int slackCount = Math.Max(0, cols - n - 1);

            // Build headers: t-*, x1..xn, s1..sK, rhs
            var headers = new List<string> { "t-*" };
            for (int j = 0; j < n && 1 + j < cols; j++) headers.Add($"x{j + 1}");
            for (int k = 0; k < slackCount; k++) headers.Add($"s{k + 1}");
            headers.Add("rhs");

            string RowLabel(int r)
            {
                if (r == 0) return "z";
                int idx = r - 1;
                if (idx < m) return $"c{idx + 1}";
                return $"b{idx - m + 1}";
            }

            // compute widths
            int[] widths = new int[headers.Count];
            for (int c = 0; c < headers.Count; c++) widths[c] = headers[c].Length;

            void Fit(int col, string s) => widths[col] = Math.Max(widths[col], s.Length);

            for (int r = 0; r < rows; r++)
            {
                Fit(0, RowLabel(r));
                for (int c = 0; c < cols; c++) Fit(c + 1, T[r, c].ToString("0.###"));
            }

            // line/cell helpers
            string H(char left, char mid, char right)
            {
                var parts = new List<string>();
                for (int c = 0; c < widths.Length; c++) parts.Add(new string('─', widths[c] + 2));
                return $"{left}{string.Join(mid.ToString(), parts)}{right}";
            }

            string Row(params string[] cells)
            {
                var items = new List<string>();
                for (int c = 0; c < cells.Length; c++)
                {
                    bool numberCol = c >= 1;
                    string s = numberCol ? cells[c].PadLeft(widths[c]) : cells[c].PadRight(widths[c]);
                    items.Add(" " + s + " ");
                }
                return "│" + string.Join("│", items) + "│";
            }

            var lines = new List<string>();
            lines.Add(H('┌', '┬', '┐'));
            lines.Add(Row(headers.ToArray()));
            lines.Add(H('├', '┼', '┤'));

            for (int r = 0; r < rows; r++)
            {
                var cells = new List<string> { RowLabel(r) };
                for (int c = 0; c < cols; c++) cells.Add(T[r, c].ToString("0.###"));
                lines.Add(Row(cells.ToArray()));
                if (r == 0) lines.Add(H('├', '┼', '┤')); // line after z-row (optional)
            }

            lines.Add(H('└', '┴', '┘'));
            return string.Join(Environment.NewLine, lines);
        }
    }
}
