using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Drawing;
using static Org.BouncyCastle.Math.Primes;
using LPR381Project.Algorithms;

namespace Linear_Programming_Algorithms
{

    public partial class Form1 : Form
    {
        private KnapsackStepper _stepper = new KnapsackStepper();


        private string _currentFilePath = null;
        private readonly Dictionary<string, int> _stepIndices = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        private Linear_Programming_Algorithms.BNB.BranchAndBoundSolver _bnb;
        private bool _bnbInitialized = false;

        public Form1()
        {
            InitializeComponent();
            lstBranchLog.Font = new System.Drawing.Font("Consolas", 9f, FontStyle.Regular);
            lstPrimalLog.Font = new System.Drawing.Font("Consolas", 9f, FontStyle.Regular);
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

                bool[] isBinary = lp.SignRestrictions
                        .Select(sr => sr == SignRestriction.Binary)
                        .ToArray();

                var solver = new Primal(A, b, c,isBinary);

                solver.Solve();

                string header = "";
                for (int j = 0; j < lp.VariableCount; j++)
                    header += $"x{j + 1}".PadLeft(10);
                for (int j = 0; j < lp.VariableCount; j++)
                    header += $"s{j + 1}".PadLeft(10);
                header += "RHS".PadLeft(11);

                int matrixIndex = 1;

                int totalRows = 0;
                int totalCols = 0;

                foreach (var matrix in solver.TableauList)
                {
                    lstPrimalLog.Items.Add($"Tableau {matrixIndex}:");
                    lstPrimalLog.Items.Add(""); // spacing

                     totalRows = solver.TableauPublic.GetLength(0);
                     totalCols = solver.TableauPublic.GetLength(1);

                    string box = TablePrinter.Box(
                                    matrix,
                                    n: totalCols - totalRows,                  // x1..xn
                                    m: totalRows - 1);             // c1..cm
                    foreach (var line in box.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
                        lstPrimalLog.Items.Add(line);

                    lstPrimalLog.Items.Add(""); // spacing
                    matrixIndex++;
                }

                // Optimal tableau
                lstPrimalLog.Items.Add("Optimal Table:");
                lstPrimalLog.Items.Add("");

                int optRows = solver.OptimalTableau.GetLength(0);
                int optCols = solver.OptimalTableau.GetLength(1);

                string optBox = TablePrinter.Box(
                                    solver.OptimalTableau,
                                    n: optCols - optRows,
                                    m: optRows - 1);
                foreach (var line in optBox.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
                    lstPrimalLog.Items.Add(line);

                lstPrimalLog.Items.Add(""); // spacing

                var (solution, optimalValue) = solver.GetSolution();
                lstPrimalLog.Items.Add($"Optimal Value = {optimalValue:F3}");
                for (int i = 0; i < solution.Length; i++)
                    lstPrimalLog.Items.Add($"x{i + 1} = {solution[i]:F3}");

                var (x, z) = solver.GetSolution();

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
            try
            {
                var lp = LPData.Parse(_currentFilePath);
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
                lstSensitivityLog.Items.Clear();

                SensitivityAnalysis sa = new SensitivityAnalysis(solver);

                var newConstraintCoeffs = txtNewVarObjective.Text
                    .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(double.Parse)
                    .ToList();
                //Must be numbers separated by commas or spaces. 2,3,4 or 2 3 4

                double newRhs = Double.Parse(txtNewRHSvar.Text); //Must be a single number
               


                double newVarObjective = double.Parse(txtNewVarObjective.Text); //Must be a single number.

                var newVarCoeffs = txtNewVarCoeffs.Text
                    .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(double.Parse)
                    .ToList();
                //Must be numbers separated by commas or spaces. 2,3,4 or 2 3 4

                string result = sa.AnalyzeNewVariable(newVarObjective, newVarCoeffs);

                lstSensitivityLog.Items.Add(result);

                if (result.Contains("Zj-Cj"))
                {
                    lstSensitivityLog.Items.Add("");
                    lstSensitivityLog.Items.Add("Explanation: Zj-Cj (Reduced Cost) tells us if adding this variable improves the solution.");
                    lstSensitivityLog.Items.Add(" - If Zj-Cj < 0 (in maximization), the variable will not improve the objective.");
                    lstSensitivityLog.Items.Add(" - If Zj-Cj > 0, the variable could enter the basis and improve the solution.");
                    lstSensitivityLog.Items.Add("");

                }

                lstSensitivityLog.Items.Add("=== New Constraint Test ===");
                lstSensitivityLog.Items.Add(sa.AnalyzeNewConstraint(newConstraintCoeffs, newRhs));

                if (result.Contains("LHS") && result.Contains("RHS"))
                {
                    lstSensitivityLog.Items.Add("Explanation: LHS is the current value of this constraint under the solution.");
                    lstSensitivityLog.Items.Add(" - If LHS < RHS, the constraint is not binding yet.");
                    lstSensitivityLog.Items.Add(" - If LHS = RHS, the constraint is binding (tight).");
                    lstSensitivityLog.Items.Add(" - If LHS > RHS, the constraint is violated and the solution is infeasible.");
                }
            }
            catch (Exception ex)
            {
                lstSensitivityLog.Items.Add("Error: " + ex.Message);
            }
        }
        


        // Cutting Plane: Run
        private void RunCutting_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentFilePath))
            {
                MessageBox.Show("Load an LP file first (Browse or drag & drop).", "No file", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            lstCuttingLog.Items.Clear(); 

            try
            {
                string[] lpLines = System.IO.File.ReadAllLines(_currentFilePath);
                CuttingPlane solver = new CuttingPlane(lpLines);

                string log = solver.Solve(); 

                foreach (var line in log.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    lstCuttingLog.Items.Add(line);
                }

                statusLabel.Text = "Cutting Plane finished";
            }
            catch (FormatException fex)
            {
                MessageBox.Show("Invalid LP file format:\n" + fex.Message, "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                statusLabel.Text = "Error: invalid format";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to run cutting plane:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error running cutting plane";
            }
        }



        // Branch & Bound: Run
        private void RunBranch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentFilePath)) { MessageBox.Show("Load an LP file first."); return; }

            lstBranchLog.Items.Clear();

            try
            {
                var lp = LPData.Parse(_currentFilePath);

                // Build A, b, c exactly like your Primal tab (consistent with your code)  :contentReference[oaicite:3]{index=3}
                int m = lp.Constraints.Count;
                int n = lp.VariableCount;

                double[,] A = new double[m, n];
                for (int i = 0; i < m; i++)
                    for (int j = 0; j < n; j++)
                        A[i, j] = lp.Constraints[i].Coefficients[j];

                double[] b = new double[m];
                for (int i = 0; i < m; i++) b[i] = lp.Constraints[i].Rhs;

                double[] c = (double[])lp.Objective.Coefficients.Clone();

                bool isMax = lp.Objective.Type != ProblemType.Min;

                var intIdx = Enumerable.Range(0, n).ToList();

                var adapter = new Linear_Programming_Algorithms.BNB.PrimalAdapter();
                _bnb = new Linear_Programming_Algorithms.BNB.BranchAndBoundSolver(
                            adapter, A, b, c, intIdx, isMax);

                _bnb.Initialize();
                _bnbInitialized = true;

                lstBranchLog.Items.Add($"Branch & Bound — initialized. Vars: {n}, Constraints: {m}");
                statusLabel.Text = "BnB: initialized";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to initialize Branch & Bound:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "BnB init error";
            }

        }

        // ---------------- Step methods ----------------               

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

        }




        private void StepBranch_Click(object sender, EventArgs e)
        {
            if (!_bnbInitialized || _bnb == null)
            {
                MessageBox.Show("Click 'Run' on the Branch & Bound tab first.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var lines = _bnb.Step();
            foreach (var line in lines)
            {
                // multi-line entries (like printed tableaus) are split to preserve formatting in ListBox
                if (line?.Contains("\n") == true)
                {
                    foreach (var sub in line.Split(new[] { "\n" }, StringSplitOptions.None))
                        lstBranchLog.Items.Add(sub);
                }
                else lstBranchLog.Items.Add(line);
            }

            if (_bnb.IsFinished)
            {
                lstBranchLog.Items.Add("");
                if (_bnb.IncumbentX != null)
                {
                    lstBranchLog.Items.Add($"Final incumbent z = {_bnb.IncumbentZ:F3}");
                    for (int i = 0; i < _bnb.IncumbentX.Length; i++)
                        lstBranchLog.Items.Add($"x{i + 1} = {_bnb.IncumbentX[i]:F3}");
                }
                else
                {
                    lstBranchLog.Items.Add("No feasible integer solution found.");
                }
                statusLabel.Text = "BnB completed";
            }
            else
            {
                statusLabel.Text = "BnB stepped";
            }
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
        }

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

        private void txtPreview_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAnalyzeNewVar_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            rtbNL.Multiline = true;
            rtbNL.Clear();
            var noLin = new NonLinear();
            noLin.Solve();

            string max = noLin.maximize ? "max" : "min";

            rtbNL.AppendText($"{max}  Function: f(x) = -x² + 4x + 10\r\n");

            int finalIt = noLin.Iterations + 1;
            rtbNL.AppendText(Environment.NewLine);
            rtbNL.AppendText($"x = {noLin.FinalX:F6}" + Environment.NewLine);
            rtbNL.AppendText($"f(x) = {noLin.FinalFx:F6}" + Environment.NewLine);
            rtbNL.AppendText($"Iterations : {finalIt.ToString()} " + Environment.NewLine);

            foreach (var item in noLin.IterationLog)
            {
                rtbNL.AppendText(item + Environment.NewLine);
            }
        }
    }
}
