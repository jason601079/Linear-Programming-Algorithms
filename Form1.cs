using iTextSharp.text;
using iTextSharp.text.pdf;
using Org.BouncyCastle.Tsp;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        private KnapsackStepper _stepper = new KnapsackStepper();


        private string _currentFilePath = null;      
        private readonly Dictionary<string, int> _stepIndices = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        public Form1()
        {
            InitializeComponent();

            btnPrimalExport.Click += (s, e) => ExportListBox(lstPrimalLog, "PrimalLog.txt");
            btnRevisedExport.Click += (s, e) => ExportListBox(lstRevisedLog, "RevisedLog.txt");
            btnCuttingExport.Click += (s, e) => ExportListBox(lstCuttingLog, "CuttingLog.txt");
            btnDSExport.Click += (s, e) => ExportListBox(lstSensitivityLog, "SensitivityLog.txt");
            btnBBExport.Click += (s, e) => ExportListBox(lstBranchLog, "BranchLog.txt");
            btnKnapsackExport.Click += (s, e) => ExportRichText(rtbKnapsack, "KnapsackLog.txt");
            btnNLExport.Click += (s, e) => ExportRichText(rtbNL, "NonLinearLog.txt");
            
        }

        private void ExportListBox(ListBox listBox, string baseFileName)
        {
            if (listBox.Items.Count == 0)
            {
                MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.FileName = baseFileName;
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string extension = Path.GetExtension(saveFileDialog.FileName).ToLower();

                    if (extension == ".pdf")
                        ExportToPdf(listBox, saveFileDialog.FileName);
                    else
                        ExportToText(listBox, saveFileDialog.FileName);

                    MessageBox.Show("Export successful!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ExportRichText(RichTextBox richTextBox, string baseFileName)
        {
            if (string.IsNullOrWhiteSpace(richTextBox.Text))
            {
                MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.FileName = baseFileName;
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string extension = Path.GetExtension(saveFileDialog.FileName).ToLower();

                    if (extension == ".pdf")
                        ExportToPdf(richTextBox, saveFileDialog.FileName);
                    else
                        ExportToText(richTextBox, saveFileDialog.FileName);

                    MessageBox.Show("Export successful!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ExportToText(RichTextBox richTextBox, string filePath)
        {
            File.WriteAllText(filePath, richTextBox.Text);
        }

        private void ExportToPdf(RichTextBox richTextBox, string filePath)
        {
            Document doc = new Document(PageSize.A4, 20, 20, 20, 20);

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                PdfWriter.GetInstance(doc, stream);
                doc.Open();

                var font = FontFactory.GetFont(FontFactory.COURIER, 11); 
                doc.Add(new Paragraph(richTextBox.Text, font));

                doc.Close();
            }
        }


        private void ExportToText(ListBox listBox, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var item in listBox.Items)
                    writer.WriteLine(item.ToString());
            }
        }

        private void ExportToPdf(ListBox listBox, string filePath)
        {
            Document doc = new Document(PageSize.A4, 20, 20, 20, 20);

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                PdfWriter.GetInstance(doc, stream);
                doc.Open();

                var font = FontFactory.GetFont(FontFactory.HELVETICA, 12);

                foreach (var item in listBox.Items)
                {
                    doc.Add(new Paragraph(item.ToString(), font));
                }

                doc.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            openFileDialog.Filter = "LP or text files (*.lp;*.txt)|*.lp;*.txt|All files (*.*)|*.*";

       
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

        // ---------------- Run methods ---------------

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
                var lp = LPData.Parse(_currentFilePath); 
                lstPrimalLog.Items.Add("LP parsed successfully.");
                lstPrimalLog.Items.Add($"Vars: {lp.VariableCount}, Constraints: {lp.Constraints.Count}");
                lstPrimalLog.Items.Add("Running  Primal simplex)...");

                double[] c = lp.Objective.Coefficients;

                int m = lp.Constraints.Count;        
                int n = lp.VariableCount;            

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
                foreach (var matrix in solver.TableauList) 
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

                    lstPrimalLog.Items.Add(""); 
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
                        row += solver.OptimalTableau[i, j].ToString("F3").PadLeft(12);  
                    }
                   
                    lstPrimalLog.Items.Add(row);
                    lstPrimalLog.Items.Add(""); 
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

            var lp = LPData.Parse(_currentFilePath);

            /*  int m = lp.Constraints.Count;
              int n = lp.VariableCount;

              // 2. Build arrays
              double[,] A = new double[m, n];
              double[] b = new double[m];
              double[] c = (double[])lp.Objective.Coefficients.Clone();

              for (int i = 0; i < m; i++)
              {
                  var con = lp.Constraints[i];
                  for (int j = 0; j < n; j++)
                      A[i, j] = con.Coefficients[j];
                  b[i] = con.Rhs;
              }*/

            ConvertToStandardForm(lp, out double[,] A, out double[] b, out double[] c);


            var solver = new RevisedPrimalSimplex(A, b, c);
            var answer = solver.Solve();

            lstRevisedLog.Items.Add($"Status :{answer.Status}");
            lstRevisedLog.Items.Add($"Optimal value: {answer.z}");
            lstRevisedLog.Items.Add($"x = [{string.Join(", ", answer.x)}]");

            lstRevisedLog.Items.Add("");

            foreach (var iter in solver.Iterations)
                lstRevisedLog.Items.Add(iter.ToPrettyString());

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

        // ---------------- Step methods ----------------

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

        // ---------------- Reset  ----------------
        // Wired as the Click handler for ALL reset buttons.
        private void AlgorithmReset_Click(object sender, EventArgs e)
        {
            if (!(sender is Button btn))
            {
                statusLabel.Text = "Reset invoked";
                return;
            }

            // Designer should set Tag on each reset button to the listbox name 
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
                lb.Items.Clear();          
                statusLabel.Text = $"Reset {lb.Name}";
                return;
            }

            statusLabel.Text = "Reset target not found";
        }
        private void AppendColoredLine(RichTextBox box, string text, Color defaultColor)
        {
            int start = box.TextLength;
            box.SelectionStart = start;
            box.SelectionLength = 0;
            box.SelectionColor = defaultColor;
            box.AppendText(text + Environment.NewLine);

          
            HighlightKeyword(box, "feasible", Color.Goldenrod); 
            HighlightKeyword(box, "infeasible", Color.Red);      
            HighlightKeyword(box, "new best", Color.Green);       
            HighlightKeyword(box, "best", Color.Green);           
        }
        private void HighlightKeyword(RichTextBox box, string keyword, Color color)
        {
            int index = 0;
            while ((index = box.Text.IndexOf(keyword, index, StringComparison.OrdinalIgnoreCase)) != -1)
            {
                box.Select(index, keyword.Length);
                box.SelectionColor = color;
                index += keyword.Length;
            }
            // reset selection
            box.SelectionLength = 0;
            box.SelectionColor = box.ForeColor;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentFilePath))
            {
                MessageBox.Show("Load an LP file first (Browse or drag & drop).", "No file", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!System.IO.File.Exists(_currentFilePath))
            {
                MessageBox.Show("The LP file could not be found. Please re-load the file.", "File not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            rtbKnapsack.Clear();

            try
            {
                var data = LPData.Parse(_currentFilePath);
                var solver = new KnapsackSolver();
                var (maxProfit, taken) = solver.Solve(data);

                AppendColoredLine(rtbKnapsack, $"Max profit: {maxProfit}", Color.Black);
                AppendColoredLine(rtbKnapsack, "Items taken (1-based indices):", Color.Black);

                double totalWeight = 0.0;
                bool any = false;
                for (int i = 0; i < taken.Length; i++)
                {
                    if (taken[i])
                    {
                        any = true;
                        double profit = data.Objective.Coefficients[i];
                        double weight = data.Constraints[0].Coefficients[i];
                        AppendColoredLine(rtbKnapsack, $"  Item {i + 1}: profit={profit}, weight={weight}", Color.Black);
                        totalWeight += weight;
                    }
                }

                if (!any)
                    AppendColoredLine(rtbKnapsack, "  (none)", Color.Gray);

                AppendColoredLine(rtbKnapsack, $"Total weight: {totalWeight} / {data.Constraints[0].Rhs}", Color.Black);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Solve failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnKnapsackStep_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentFilePath))
            {
                MessageBox.Show("Load an LP file first (Browse or drag & drop).", "No file", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                if (!_stepper.IsInitialized)
                {
                    var data = LPData.Parse(_currentFilePath);
                    _stepper.Initialize(data);
                    rtbKnapsack.Clear();
                    AppendColoredLine(rtbKnapsack, "=== Step Trace (press Step repeatedly) ===", Color.Black);
                }

                var step = _stepper.Step();
                foreach (var line in step.Lines)
                {
                    AppendColoredLine(rtbKnapsack, line, Color.Black);
                }

                if (step.IsFinished)
                {
                    rtbKnapsack.AppendText(Environment.NewLine);
                    AppendColoredLine(rtbKnapsack, "Finished. (press Reset to start new trace)", Color.Cyan);
                }

                rtbKnapsack.ScrollToCaret();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Step failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            _stepper.Reset();
            rtbKnapsack.Clear();
        }


        private void btnNLReset_Click(object sender, EventArgs e)
        {
            rtbNL.Clear();

        private void ConvertToStandardForm(LPData lp, out double[,] A, out double[] b, out double[] c)
        {
            int m = lp.Constraints.Count;
            int n = lp.VariableCount;

            // count slack vars (only for <= constraints)
            int slackCount = lp.Constraints.Count(cons => cons.Relation == Relation.LessOrEqual);

            int totalVars = n + slackCount;
            A = new double[m, totalVars];
            b = new double[m];
            c = new double[totalVars];

            // copy objective
            for (int j = 0; j < n; j++)
                c[j] = lp.Objective.Coefficients[j];

            // flip if minimization
            if (lp.Objective.Type == ProblemType.Min)
                for (int j = 0; j < n; j++) c[j] = -c[j];

            // fill A and b
            int slackCol = n;
            for (int i = 0; i < m; i++)
            {
                var cons = lp.Constraints[i];
                for (int j = 0; j < n; j++)
                    A[i, j] = cons.Coefficients[j];
                b[i] = cons.Rhs;

                if (cons.Relation == Relation.LessOrEqual)
                {
                    A[i, slackCol] = 1.0; // add slack
                    slackCol++;
                }
                else
                {
                    throw new NotSupportedException("Only <= constraints are supported in this solver.");
                }
            }

        }
    }
}
