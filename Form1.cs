// Form1.cs
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Linear_Programming_Algorithms
{
    public partial class Form1 : Form
    {
        private string _currentFilePath = null;
        // per-log step counters (keyed by ListBox.Name)
        private readonly Dictionary<string, int> _stepIndices = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            openFileDialog.Filter = "LP or text files (*.lp;*.txt)|*.lp;*.txt|All files (*.*)|*.*";

            // Keep copy button nicely aligned
            btnCopyPreview.Left = Math.Max(8, previewTopPanel.ClientSize.Width - btnCopyPreview.Width - 8);
            btnCopyPreview.Top = Math.Max(4, (previewTopPanel.ClientSize.Height - btnCopyPreview.Height) / 2);
            previewTopPanel.Resize += (s, ev) =>
            {
                btnCopyPreview.Left = Math.Max(8, previewTopPanel.ClientSize.Width - btnCopyPreview.Width - 8);
                btnCopyPreview.Top = Math.Max(4, (previewTopPanel.ClientSize.Height - btnCopyPreview.Height) / 2);
            };
        }

        // ---------------- File controls ----------------
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            LoadFile(openFileDialog.FileName);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            _currentFilePath = null;
            txtPreview.Clear();
            lblDropHint.Text = "Drag & drop an LP file here or click Browse";
            statusLabel.Text = "Cleared";
        }

        private void btnCopyPreview_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPreview.Text))
            {
                MessageBox.Show("No preview to copy.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Clipboard.SetText(txtPreview.Text);
            statusLabel.Text = "Preview copied to clipboard";
        }

        private void panelDropZone_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
            else e.Effect = DragDropEffects.None;
        }

        private void panelDropZone_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files == null || files.Length == 0) return;
            LoadFile(files[0]);
        }

        private void LoadFile(string path)
        {
            try
            {
                var lp = LPData.Parse(path);

                _currentFilePath = path;
                var text = File.ReadAllText(path);
                txtPreview.Text = text;
                lblDropHint.Text = $"Loaded: {Path.GetFileName(path)}";
                statusLabel.Text = $"Loaded {Path.GetFileName(path)} • Variables: {lp.VariableCount} • Constraints: {lp.Constraints.Count}";

                // Example: show objective coefficients as comma list (optional)
                // MessageBox.Show(string.Join(", ", lp.Objective.Coefficients.Select(c => c.ToString(CultureInfo.InvariantCulture))));
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Invalid file format:\n" + ex.Message, "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                statusLabel.Text = "Error: invalid format";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load file:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error loading file";
            }
        }


        private bool IsSignedNumber(string input)
        {
            double val;
            return (input.StartsWith("+") || input.StartsWith("-")) && double.TryParse(input, out val);
        }


        private static string Truncate(string value, int maxChars)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxChars ? value : value.Substring(0, maxChars - 3) + "...";
        }

        // ---------------- Run methods (unique per tab) ----------------

        // Primal Simplex: Run
        private void RunPrimal_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentFilePath))
            {
                MessageBox.Show("Load an LP file first (Browse or drag & drop).", "No file", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            lstPrimalLog.Items.Clear();
            try
            {
                var lp = LPData.Parse(_currentFilePath); // may throw FormatException / NotSupportedException
                lstPrimalLog.Items.Add("LP parsed successfully.");
                lstPrimalLog.Items.Add($"Vars: {lp.VariableCount}, Constraints: {lp.Constraints.Count}");
                lstPrimalLog.Items.Add("Running  Primal simplex)...");

                double[] c = lp.Objective.Coefficients;

                int m = lp.Constraints.Count;        // number of constraints
                int n = lp.VariableCount;            // number of variables

                double[,] A = new double[m, n];
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        A[i, j] = lp.Constraints[i].Coefficients[j];
                    }
                }

                double[] b = new double[m];
                for (int i = 0; i < m; i++)
                {
                    b[i] = lp.Constraints[i].Rhs;
                }

                var solver = new Primal(A, b, c);

                solver.Solve();

                string header = "";
                for (int j = 0; j < lp.VariableCount; j++)
                    header += $"x{j + 1}".PadLeft(10);
                for (int j = 0; j < lp.VariableCount; j++)
                    header += $"s{j + 1}".PadLeft(10);
                header += "RHS".PadLeft(11);

                int matrixIndex = 1;
                foreach (var matrix in solver.TableauList) // listOf2DArrays = List<double[,]>
                {
                    lstPrimalLog.Items.Add($"Tableau {matrixIndex}:");

                    int rows = matrix.GetLength(0);
                    int cols = matrix.GetLength(1);

  

                    lstPrimalLog.Items.Add(header);

                    for (int i = 0; i < rows; i++)
                    {
                        string row = "";
                        for (int j = 0; j < cols; j++)
                        {
                            // Format each number to 3 decimals, pad for alignment
                            row += matrix[i, j].ToString("F3").PadLeft(12);
                        }
                        lstPrimalLog.Items.Add(row);
                    }

                    lstPrimalLog.Items.Add(""); // blank line
                    matrixIndex++;
                }

                lstPrimalLog.Items.Add("Optimal Table");
                lstPrimalLog.Items.Add(""); // blank line
                lstPrimalLog.Items.Add(header);

                for (int i = 0; i < solver.OptimalTableau.GetLength(0); i++) // rows
                {
                    string row = "";
                    for (int j = 0; j < solver.OptimalTableau.GetLength(1); j++) // columns
                    {
                        row += solver.OptimalTableau[i, j].ToString("F3").PadLeft(12);  // add each number with space
                    }
                   
                    lstPrimalLog.Items.Add(row);
                    lstPrimalLog.Items.Add(""); // blank line
                }

                var (x, z) = solver.GetSolution();

                lstPrimalLog.Items.Add($"Optimal Value = {z:F3}");
                for (int i = 0; i < x.Length; i++)
                    lstPrimalLog.Items.Add($"x{i + 1} = {x[i]:F3}");

            }
            catch (FormatException fex)
            {
                MessageBox.Show("Invalid file format:\n" + fex.Message, "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                statusLabel.Text = "Error: invalid format";
            }
            catch (NotSupportedException nsx)
            {
                MessageBox.Show("Unsupported LP for this quick solver:\n" + nsx.Message, "Not supported", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                statusLabel.Text = "Error: unsupported LP";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to run primal simplex:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error running primal";
            }
        }


        // Revised Primal Simplex: Run
        private void RunRevised_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentFilePath)) { MessageBox.Show("Load an LP file first."); return; }

            lstRevisedLog.Items.Clear();
            lstRevisedLog.Items.Add("Revised Primal Simplex — Run started.");
            lstRevisedLog.Items.Add("Computing basis inverse updates (B^-1) incrementally... (stub)");
            lstRevisedLog.Items.Add("Updating reduced costs and selecting entering variable.");
            lstRevisedLog.Items.Add("Basis changed 3 times. Final objective = 40.75 (stub)");
            _stepIndices[lstRevisedLog.Name] = 0;
            statusLabel.Text = "Revised Primal run completed (stub)";
        }

        // Data Sensitivity: Run
        private void RunSensitivity_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentFilePath)) { MessageBox.Show("Load an LP file first."); return; }

            lstSensitivityLog.Items.Clear();
            lstSensitivityLog.Items.Add("Data Sensitivity — Run started.");
            lstSensitivityLog.Items.Add("Computing shadow prices and allowable ranges... (stub)");
            lstSensitivityLog.Items.Add("Variable x2 has allowable increase +3.5, decrease -1.0 (stub)");
            lstSensitivityLog.Items.Add("Objective sensitivity table generated.");
            _stepIndices[lstSensitivityLog.Name] = 0;
            statusLabel.Text = "Data Sensitivity run completed (stub)";
        }

        // Cutting Plane: Run
        private void RunCutting_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentFilePath)) { MessageBox.Show("Load an LP file first."); return; }

            lstCuttingLog.Items.Clear();
            lstCuttingLog.Items.Add("Cutting Plane — Run started.");
            lstCuttingLog.Items.Add("Solve LP relaxation (root) -> fractional solution found (stub)");
            lstCuttingLog.Items.Add("Generate Gomory cut #1, add to model.");
            lstCuttingLog.Items.Add("Re-solve -> new fractional solution -> add cut #2.");
            lstCuttingLog.Items.Add("Integer-feasible solution found. Objective = 38.00 (stub)");
            _stepIndices[lstCuttingLog.Name] = 0;
            statusLabel.Text = "Cutting Plane run completed (stub)";
        }

        // Branch & Bound: Run
        private void RunBranch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentFilePath)) { MessageBox.Show("Load an LP file first."); return; }

            lstBranchLog.Items.Clear();
            lstBranchLog.Items.Add("Branch & Bound — Run started.");
            lstBranchLog.Items.Add("Solve relaxation at root -> objective = 45.3 (stub)");
            lstBranchLog.Items.Add("Branch on x1 (floor/ceil): create two children.");
            lstBranchLog.Items.Add("Found integer incumbent obj = 44.50 (stub), prune nodes by bound.");
            lstBranchLog.Items.Add("Finished. Best integer solution obj = 44.50 (stub)");
            _stepIndices[lstBranchLog.Name] = 0;
            statusLabel.Text = "Branch & Bound run completed (stub)";
        }

        // ---------------- Step methods (unique per tab) ----------------

        private void StepPrimal_Click(object sender, EventArgs e)
        {
            var key = lstPrimalLog.Name;
            if (!_stepIndices.TryGetValue(key, out var i)) i = 0;
            i++;
            _stepIndices[key] = i;

            switch (i)
            {
                case 1:
                    lstPrimalLog.Items.Add("Pivot 1: entering x3, leaving s2. Updated tableau (stub).");
                    break;
                case 2:
                    lstPrimalLog.Items.Add("Pivot 2: entering x1, leaving s1. Updated tableau (stub).");
                    break;
                case 3:
                    lstPrimalLog.Items.Add("Pivot 3: optimality reached (stub).");
                    break;
                default:
                    lstPrimalLog.Items.Add("No more primal steps (stub).");
                    break;
            }

            statusLabel.Text = $"Primal Step {i}";
        }

        private void StepRevised_Click(object sender, EventArgs e)
        {
            var key = lstRevisedLog.Name;
            if (!_stepIndices.TryGetValue(key, out var i)) i = 0;
            i++;
            _stepIndices[key] = i;

            switch (i)
            {
                case 1:
                    lstRevisedLog.Items.Add("Step 1: computed new B^-1 * a_j for entering variable (stub).");
                    break;
                case 2:
                    lstRevisedLog.Items.Add("Step 2: updated basis, recalculated reduced costs (stub).");
                    break;
                case 3:
                    lstRevisedLog.Items.Add("Step 3: found optimal basis (stub).");
                    break;
                default:
                    lstRevisedLog.Items.Add("No more revised-simplex steps (stub).");
                    break;
            }

            statusLabel.Text = $"Revised Step {i}";
        }

        private void StepSensitivity_Click(object sender, EventArgs e)
        {
            var key = lstSensitivityLog.Name;
            if (!_stepIndices.TryGetValue(key, out var i)) i = 0;
            i++;
            _stepIndices[key] = i;

            switch (i)
            {
                case 1:
                    lstSensitivityLog.Items.Add("Step 1: computed dual prices (shadow prices) for constraints (stub).");
                    break;
                case 2:
                    lstSensitivityLog.Items.Add("Step 2: computed allowable ranges for objective coefficients (stub).");
                    break;
                default:
                    lstSensitivityLog.Items.Add("No more sensitivity steps (stub).");
                    break;
            }

            statusLabel.Text = $"Sensitivity Step {i}";
        }

        private void StepCutting_Click(object sender, EventArgs e)
        {
            var key = lstCuttingLog.Name;
            if (!_stepIndices.TryGetValue(key, out var i)) i = 0;
            i++;
            _stepIndices[key] = i;

            switch (i)
            {
                case 1:
                    lstCuttingLog.Items.Add("Step 1: solved relaxation and generated Gomory cut #1 (stub).");
                    break;
                case 2:
                    lstCuttingLog.Items.Add("Step 2: re-solved with cut #1; generated cut #2 (stub).");
                    break;
                case 3:
                    lstCuttingLog.Items.Add("Step 3: integer solution found (stub).");
                    break;
                default:
                    lstCuttingLog.Items.Add("No more cutting-plane steps (stub).");
                    break;
            }

            statusLabel.Text = $"Cutting Step {i}";
        }

        private void StepBranch_Click(object sender, EventArgs e)
        {
            var key = lstBranchLog.Name;
            if (!_stepIndices.TryGetValue(key, out var i)) i = 0;
            i++;
            _stepIndices[key] = i;

            switch (i)
            {
                case 1:
                    lstBranchLog.Items.Add("Step 1: solved root relaxation (stub).");
                    break;
                case 2:
                    lstBranchLog.Items.Add("Step 2: branched on fractional variable x2 -> created two child nodes (stub).");
                    break;
                case 3:
                    lstBranchLog.Items.Add("Step 3: inspected left child -> found feasible integer solution (stub).");
                    break;
                case 4:
                    lstBranchLog.Items.Add("Step 4: pruned right child due to bound < incumbent (stub).");
                    break;
                default:
                    lstBranchLog.Items.Add("No more branch & bound steps (stub).");
                    break;
            }

            statusLabel.Text = $"Branch Step {i}";
        }

        // ---------------- Reset (single shared handler) ----------------
        // Wired as the Click handler for ALL reset buttons.
        private void AlgorithmReset_Click(object sender, EventArgs e)
        {
            if (!(sender is Button btn))
            {
                statusLabel.Text = "Reset invoked";
                return;
            }

            // Designer should set Tag on each reset button to the listbox name (e.g. "lstPrimalLog")
            var tag = btn.Tag as string;
            if (string.IsNullOrEmpty(tag))
            {
                statusLabel.Text = "Reset could not find target";
                return;
            }

            // find the ListBox control by name and clear it
            var ctrl = this.Controls.Find(tag, true).FirstOrDefault();
            if (ctrl is ListBox lb)
            {
                _stepIndices[lb.Name] = 0;
                lb.Items.Clear();          // <-- only clear, do NOT add any text
                statusLabel.Text = $"Reset {lb.Name}";
                return;
            }

            statusLabel.Text = "Reset target not found";
        }
    }
}
