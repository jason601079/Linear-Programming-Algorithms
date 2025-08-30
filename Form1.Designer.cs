// Form1.Designer.cs
namespace Linear_Programming_Algorithms
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel tableLayoutMain;
        private System.Windows.Forms.TableLayoutPanel leftColumn;
        private System.Windows.Forms.GroupBox groupFileInput;
        private System.Windows.Forms.FlowLayoutPanel flowFileButtons;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Panel panelDropZone;
        private System.Windows.Forms.Label lblDropHint;

        private System.Windows.Forms.GroupBox groupPreview;
        private System.Windows.Forms.Panel previewTopPanel;
        private System.Windows.Forms.Button btnCopyPreview;
        private System.Windows.Forms.TextBox txtPreview;

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tableLayoutMain = new System.Windows.Forms.TableLayoutPanel();
            this.leftColumn = new System.Windows.Forms.TableLayoutPanel();
            this.groupFileInput = new System.Windows.Forms.GroupBox();
            this.panelDropZone = new System.Windows.Forms.Panel();
            this.lblDropHint = new System.Windows.Forms.Label();
            this.flowFileButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.groupPreview = new System.Windows.Forms.GroupBox();
            this.txtPreview = new System.Windows.Forms.TextBox();
            this.previewTopPanel = new System.Windows.Forms.Panel();
            this.btnCopyPreview = new System.Windows.Forms.Button();
            this.tabControlAlgorithms = new System.Windows.Forms.TabControl();
            this.tabPrimal = new System.Windows.Forms.TabPage();
            this.panelPrimal = new System.Windows.Forms.Panel();
            this.lstPrimalLog = new System.Windows.Forms.ListBox();
            this.flowPrimalButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRunPrimal = new System.Windows.Forms.Button();
            this.btnResetPrimal = new System.Windows.Forms.Button();
            this.btnPrimalExport = new System.Windows.Forms.Button();
            this.tabRevised = new System.Windows.Forms.TabPage();
            this.panelRevised = new System.Windows.Forms.Panel();
            this.lstRevisedLog = new System.Windows.Forms.ListBox();
            this.flowRevisedButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRunRevised = new System.Windows.Forms.Button();
            this.btnResetRevised = new System.Windows.Forms.Button();
            this.btnRevisedExport = new System.Windows.Forms.Button();
            this.tabSensitivity = new System.Windows.Forms.TabPage();
            this.panelSensitivity = new System.Windows.Forms.Panel();
            this.btnAnalyzeNewVar = new MaterialSkin.Controls.MaterialFloatingActionButton();
            this.txtNewVarCoeffs = new MaterialSkin.Controls.MaterialTextBox2();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.txtNewVarObjective = new MaterialSkin.Controls.MaterialTextBox2();
            this.lstSensitivityLog = new System.Windows.Forms.ListBox();
            this.flowSensitivityButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRunSensitivity = new System.Windows.Forms.Button();
            this.btnResetSensitivity = new System.Windows.Forms.Button();
            this.btnDSExport = new System.Windows.Forms.Button();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.tabCutting = new System.Windows.Forms.TabPage();
            this.panelCutting = new System.Windows.Forms.Panel();
            this.lstCuttingLog = new System.Windows.Forms.ListBox();
            this.flowCuttingButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRunCutting = new System.Windows.Forms.Button();
            this.btnStepCutting = new System.Windows.Forms.Button();
            this.btnResetCutting = new System.Windows.Forms.Button();
            this.btnCuttingExport = new System.Windows.Forms.Button();
            this.tabBranchBound = new System.Windows.Forms.TabPage();
            this.panelBranch = new System.Windows.Forms.Panel();
            this.lstBranchLog = new System.Windows.Forms.ListBox();
            this.flowBranchButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRunBranch = new System.Windows.Forms.Button();
            this.btnStepBranch = new System.Windows.Forms.Button();
            this.btnResetBranch = new System.Windows.Forms.Button();
            this.btnBBExport = new System.Windows.Forms.Button();
            this.tabBBKnapsack = new System.Windows.Forms.TabPage();
            this.rtbKnapsack = new System.Windows.Forms.RichTextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnKnapsackRun = new System.Windows.Forms.Button();
            this.btnKnapsackStep = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnKnapsackExport = new System.Windows.Forms.Button();
            this.tabNonLinear = new System.Windows.Forms.TabPage();
            this.rtbNL = new System.Windows.Forms.RichTextBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnNLReset = new System.Windows.Forms.Button();
            this.btnNLExport = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtNewRHSvar = new MaterialSkin.Controls.MaterialTextBox2();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.tableLayoutMain.SuspendLayout();
            this.leftColumn.SuspendLayout();
            this.groupFileInput.SuspendLayout();
            this.panelDropZone.SuspendLayout();
            this.flowFileButtons.SuspendLayout();
            this.groupPreview.SuspendLayout();
            this.previewTopPanel.SuspendLayout();
            this.tabControlAlgorithms.SuspendLayout();
            this.tabPrimal.SuspendLayout();
            this.panelPrimal.SuspendLayout();
            this.flowPrimalButtons.SuspendLayout();
            this.tabRevised.SuspendLayout();
            this.panelRevised.SuspendLayout();
            this.flowRevisedButtons.SuspendLayout();
            this.tabSensitivity.SuspendLayout();
            this.panelSensitivity.SuspendLayout();
            this.flowSensitivityButtons.SuspendLayout();
            this.tabCutting.SuspendLayout();
            this.panelCutting.SuspendLayout();
            this.flowCuttingButtons.SuspendLayout();
            this.tabBranchBound.SuspendLayout();
            this.panelBranch.SuspendLayout();
            this.flowBranchButtons.SuspendLayout();
            this.tabBBKnapsack.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabNonLinear.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutMain
            // 
            this.tableLayoutMain.ColumnCount = 2;
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36F));
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64F));
            this.tableLayoutMain.Controls.Add(this.leftColumn, 0, 0);
            this.tableLayoutMain.Controls.Add(this.tabControlAlgorithms, 1, 0);
            this.tableLayoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutMain.Name = "tableLayoutMain";
            this.tableLayoutMain.Padding = new System.Windows.Forms.Padding(12);
            this.tableLayoutMain.RowCount = 1;
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutMain.Size = new System.Drawing.Size(1924, 1007);
            this.tableLayoutMain.TabIndex = 0;
            // 
            // leftColumn
            // 
            this.leftColumn.ColumnCount = 1;
            this.leftColumn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.leftColumn.Controls.Add(this.groupFileInput, 0, 0);
            this.leftColumn.Controls.Add(this.groupPreview, 0, 1);
            this.leftColumn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftColumn.Location = new System.Drawing.Point(15, 15);
            this.leftColumn.Name = "leftColumn";
            this.leftColumn.RowCount = 2;
            this.leftColumn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 38F));
            this.leftColumn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62F));
            this.leftColumn.Size = new System.Drawing.Size(678, 977);
            this.leftColumn.TabIndex = 0;
            // 
            // groupFileInput
            // 
            this.groupFileInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.groupFileInput.Controls.Add(this.panelDropZone);
            this.groupFileInput.Controls.Add(this.flowFileButtons);
            this.groupFileInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupFileInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.groupFileInput.Location = new System.Drawing.Point(3, 3);
            this.groupFileInput.Name = "groupFileInput";
            this.groupFileInput.Padding = new System.Windows.Forms.Padding(12);
            this.groupFileInput.Size = new System.Drawing.Size(672, 365);
            this.groupFileInput.TabIndex = 0;
            this.groupFileInput.TabStop = false;
            this.groupFileInput.Text = "File Input";
            // 
            // panelDropZone
            // 
            this.panelDropZone.AllowDrop = true;
            this.panelDropZone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.panelDropZone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDropZone.Controls.Add(this.lblDropHint);
            this.panelDropZone.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelDropZone.Location = new System.Drawing.Point(12, 253);
            this.panelDropZone.Name = "panelDropZone";
            this.panelDropZone.Size = new System.Drawing.Size(648, 100);
            this.panelDropZone.TabIndex = 0;
            this.panelDropZone.DragDrop += new System.Windows.Forms.DragEventHandler(this.panelDropZone_DragDrop);
            this.panelDropZone.DragEnter += new System.Windows.Forms.DragEventHandler(this.panelDropZone_DragEnter);
            // 
            // lblDropHint
            // 
            this.lblDropHint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDropHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(112)))), ((int)(((byte)(133)))));
            this.lblDropHint.Location = new System.Drawing.Point(0, 0);
            this.lblDropHint.Name = "lblDropHint";
            this.lblDropHint.Size = new System.Drawing.Size(646, 98);
            this.lblDropHint.TabIndex = 0;
            this.lblDropHint.Text = "Drag & drop an LP file here or click Browse";
            this.lblDropHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flowFileButtons
            // 
            this.flowFileButtons.AutoSize = true;
            this.flowFileButtons.Controls.Add(this.btnBrowse);
            this.flowFileButtons.Controls.Add(this.btnClear);
            this.flowFileButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowFileButtons.Location = new System.Drawing.Point(12, 40);
            this.flowFileButtons.Name = "flowFileButtons";
            this.flowFileButtons.Size = new System.Drawing.Size(648, 57);
            this.flowFileButtons.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.AutoSize = true;
            this.btnBrowse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnBrowse.FlatAppearance.BorderSize = 0;
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.ForeColor = System.Drawing.Color.White;
            this.btnBrowse.Location = new System.Drawing.Point(3, 3);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.btnBrowse.Size = new System.Drawing.Size(143, 51);
            this.btnBrowse.TabIndex = 0;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnClear
            // 
            this.btnClear.AutoSize = true;
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(239)))), ((int)(((byte)(244)))));
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnClear.Location = new System.Drawing.Point(152, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.btnClear.Size = new System.Drawing.Size(101, 51);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // groupPreview
            // 
            this.groupPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.groupPreview.Controls.Add(this.txtPreview);
            this.groupPreview.Controls.Add(this.previewTopPanel);
            this.groupPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPreview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.groupPreview.Location = new System.Drawing.Point(3, 374);
            this.groupPreview.Name = "groupPreview";
            this.groupPreview.Padding = new System.Windows.Forms.Padding(12);
            this.groupPreview.Size = new System.Drawing.Size(672, 600);
            this.groupPreview.TabIndex = 1;
            this.groupPreview.TabStop = false;
            this.groupPreview.Text = "Preview";
            // 
            // txtPreview
            // 
            this.txtPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this.txtPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPreview.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtPreview.Location = new System.Drawing.Point(12, 84);
            this.txtPreview.Multiline = true;
            this.txtPreview.Name = "txtPreview";
            this.txtPreview.ReadOnly = true;
            this.txtPreview.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPreview.Size = new System.Drawing.Size(648, 504);
            this.txtPreview.TabIndex = 0;
            this.txtPreview.TextChanged += new System.EventHandler(this.txtPreview_TextChanged);
            // 
            // previewTopPanel
            // 
            this.previewTopPanel.Controls.Add(this.btnCopyPreview);
            this.previewTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.previewTopPanel.Location = new System.Drawing.Point(12, 40);
            this.previewTopPanel.Name = "previewTopPanel";
            this.previewTopPanel.Size = new System.Drawing.Size(648, 44);
            this.previewTopPanel.TabIndex = 1;
            // 
            // btnCopyPreview
            // 
            this.btnCopyPreview.AutoSize = true;
            this.btnCopyPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(239)))), ((int)(((byte)(244)))));
            this.btnCopyPreview.FlatAppearance.BorderSize = 0;
            this.btnCopyPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopyPreview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnCopyPreview.Location = new System.Drawing.Point(0, 0);
            this.btnCopyPreview.Name = "btnCopyPreview";
            this.btnCopyPreview.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.btnCopyPreview.Size = new System.Drawing.Size(99, 51);
            this.btnCopyPreview.TabIndex = 0;
            this.btnCopyPreview.Text = "Copy";
            this.btnCopyPreview.UseVisualStyleBackColor = false;
            this.btnCopyPreview.Click += new System.EventHandler(this.btnCopyPreview_Click);
            // 
            // tabControlAlgorithms
            // 
            this.tabControlAlgorithms.Controls.Add(this.tabPrimal);
            this.tabControlAlgorithms.Controls.Add(this.tabRevised);
            this.tabControlAlgorithms.Controls.Add(this.tabSensitivity);
            this.tabControlAlgorithms.Controls.Add(this.tabCutting);
            this.tabControlAlgorithms.Controls.Add(this.tabBranchBound);
            this.tabControlAlgorithms.Controls.Add(this.tabBBKnapsack);
            this.tabControlAlgorithms.Controls.Add(this.tabNonLinear);
            this.tabControlAlgorithms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlAlgorithms.Location = new System.Drawing.Point(699, 15);
            this.tabControlAlgorithms.Name = "tabControlAlgorithms";
            this.tabControlAlgorithms.Padding = new System.Drawing.Point(8, 6);
            this.tabControlAlgorithms.SelectedIndex = 0;
            this.tabControlAlgorithms.Size = new System.Drawing.Size(1210, 977);
            this.tabControlAlgorithms.TabIndex = 1;
            // 
            // tabPrimal
            // 
            this.tabPrimal.Controls.Add(this.panelPrimal);
            this.tabPrimal.Location = new System.Drawing.Point(10, 49);
            this.tabPrimal.Name = "tabPrimal";
            this.tabPrimal.Size = new System.Drawing.Size(1190, 918);
            this.tabPrimal.TabIndex = 0;
            this.tabPrimal.Text = "Primal Simplex";
            // 
            // panelPrimal
            // 
            this.panelPrimal.Controls.Add(this.lstPrimalLog);
            this.panelPrimal.Controls.Add(this.flowPrimalButtons);
            this.panelPrimal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrimal.Location = new System.Drawing.Point(0, 0);
            this.panelPrimal.Name = "panelPrimal";
            this.panelPrimal.Padding = new System.Windows.Forms.Padding(12);
            this.panelPrimal.Size = new System.Drawing.Size(1190, 918);
            this.panelPrimal.TabIndex = 0;
            // 
            // lstPrimalLog
            // 
            this.lstPrimalLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this.lstPrimalLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstPrimalLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstPrimalLog.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lstPrimalLog.ItemHeight = 37;
            this.lstPrimalLog.Location = new System.Drawing.Point(12, 82);
            this.lstPrimalLog.Name = "lstPrimalLog";
            this.lstPrimalLog.Size = new System.Drawing.Size(1166, 824);
            this.lstPrimalLog.TabIndex = 0;
            // 
            // flowPrimalButtons
            // 
            this.flowPrimalButtons.Controls.Add(this.btnRunPrimal);
            this.flowPrimalButtons.Controls.Add(this.btnResetPrimal);
            this.flowPrimalButtons.Controls.Add(this.btnPrimalExport);
            this.flowPrimalButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowPrimalButtons.Location = new System.Drawing.Point(12, 12);
            this.flowPrimalButtons.Name = "flowPrimalButtons";
            this.flowPrimalButtons.Padding = new System.Windows.Forms.Padding(8);
            this.flowPrimalButtons.Size = new System.Drawing.Size(1166, 70);
            this.flowPrimalButtons.TabIndex = 2;
            // 
            // btnRunPrimal
            // 
            this.btnRunPrimal.AutoSize = true;
            this.btnRunPrimal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnRunPrimal.FlatAppearance.BorderSize = 0;
            this.btnRunPrimal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRunPrimal.ForeColor = System.Drawing.Color.White;
            this.btnRunPrimal.Location = new System.Drawing.Point(11, 11);
            this.btnRunPrimal.Name = "btnRunPrimal";
            this.btnRunPrimal.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.btnRunPrimal.Size = new System.Drawing.Size(86, 51);
            this.btnRunPrimal.TabIndex = 0;
            this.btnRunPrimal.Tag = "lstPrimalLog";
            this.btnRunPrimal.Text = "Run";
            this.btnRunPrimal.UseVisualStyleBackColor = false;
            this.btnRunPrimal.Click += new System.EventHandler(this.RunPrimal_Click);
            // 
            // btnResetPrimal
            // 
            this.btnResetPrimal.AutoSize = true;
            this.btnResetPrimal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(239)))), ((int)(((byte)(244)))));
            this.btnResetPrimal.FlatAppearance.BorderSize = 0;
            this.btnResetPrimal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetPrimal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnResetPrimal.Location = new System.Drawing.Point(103, 11);
            this.btnResetPrimal.Name = "btnResetPrimal";
            this.btnResetPrimal.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.btnResetPrimal.Size = new System.Drawing.Size(106, 51);
            this.btnResetPrimal.TabIndex = 2;
            this.btnResetPrimal.Tag = "lstPrimalLog";
            this.btnResetPrimal.Text = "Reset";
            this.btnResetPrimal.UseVisualStyleBackColor = false;
            this.btnResetPrimal.Click += new System.EventHandler(this.AlgorithmReset_Click);
            // 
            // btnPrimalExport
            // 
            this.btnPrimalExport.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPrimalExport.AutoSize = true;
            this.btnPrimalExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnPrimalExport.FlatAppearance.BorderSize = 0;
            this.btnPrimalExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrimalExport.ForeColor = System.Drawing.Color.White;
            this.btnPrimalExport.Location = new System.Drawing.Point(215, 11);
            this.btnPrimalExport.Name = "btnPrimalExport";
            this.btnPrimalExport.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.btnPrimalExport.Size = new System.Drawing.Size(112, 51);
            this.btnPrimalExport.TabIndex = 4;
            this.btnPrimalExport.Tag = "lstBranchLog";
            this.btnPrimalExport.Text = "Export";
            this.btnPrimalExport.UseVisualStyleBackColor = false;
            // 
            // tabRevised
            // 
            this.tabRevised.Controls.Add(this.panelRevised);
            this.tabRevised.Location = new System.Drawing.Point(10, 49);
            this.tabRevised.Name = "tabRevised";
            this.tabRevised.Size = new System.Drawing.Size(1190, 940);
            this.tabRevised.TabIndex = 1;
            this.tabRevised.Text = "Revised Primal Simplex";
            // 
            // panelRevised
            // 
            this.panelRevised.Controls.Add(this.lstRevisedLog);
            this.panelRevised.Controls.Add(this.flowRevisedButtons);
            this.panelRevised.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRevised.Location = new System.Drawing.Point(0, 0);
            this.panelRevised.Name = "panelRevised";
            this.panelRevised.Padding = new System.Windows.Forms.Padding(12);
            this.panelRevised.Size = new System.Drawing.Size(1190, 940);
            this.panelRevised.TabIndex = 0;
            // 
            // lstRevisedLog
            // 
            this.lstRevisedLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this.lstRevisedLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstRevisedLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRevisedLog.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lstRevisedLog.ItemHeight = 37;
            this.lstRevisedLog.Location = new System.Drawing.Point(12, 82);
            this.lstRevisedLog.Name = "lstRevisedLog";
            this.lstRevisedLog.Size = new System.Drawing.Size(1166, 846);
            this.lstRevisedLog.TabIndex = 0;
            // 
            // flowRevisedButtons
            // 
            this.flowRevisedButtons.Controls.Add(this.btnRunRevised);
            this.flowRevisedButtons.Controls.Add(this.btnResetRevised);
            this.flowRevisedButtons.Controls.Add(this.btnRevisedExport);
            this.flowRevisedButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowRevisedButtons.Location = new System.Drawing.Point(12, 12);
            this.flowRevisedButtons.Name = "flowRevisedButtons";
            this.flowRevisedButtons.Padding = new System.Windows.Forms.Padding(8);
            this.flowRevisedButtons.Size = new System.Drawing.Size(1166, 70);
            this.flowRevisedButtons.TabIndex = 2;
            // 
            // btnRunRevised
            // 
            this.btnRunRevised.AutoSize = true;
            this.btnRunRevised.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnRunRevised.FlatAppearance.BorderSize = 0;
            this.btnRunRevised.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRunRevised.ForeColor = System.Drawing.Color.White;
            this.btnRunRevised.Location = new System.Drawing.Point(11, 11);
            this.btnRunRevised.Name = "btnRunRevised";
            this.btnRunRevised.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.btnRunRevised.Size = new System.Drawing.Size(86, 51);
            this.btnRunRevised.TabIndex = 0;
            this.btnRunRevised.Tag = "lstRevisedLog";
            this.btnRunRevised.Text = "Run";
            this.btnRunRevised.UseVisualStyleBackColor = false;
            this.btnRunRevised.Click += new System.EventHandler(this.RunRevised_Click);
            // 
            // btnResetRevised
            // 
            this.btnResetRevised.AutoSize = true;
            this.btnResetRevised.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(239)))), ((int)(((byte)(244)))));
            this.btnResetRevised.FlatAppearance.BorderSize = 0;
            this.btnResetRevised.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetRevised.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnResetRevised.Location = new System.Drawing.Point(103, 11);
            this.btnResetRevised.Name = "btnResetRevised";
            this.btnResetRevised.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.btnResetRevised.Size = new System.Drawing.Size(106, 51);
            this.btnResetRevised.TabIndex = 2;
            this.btnResetRevised.Tag = "lstRevisedLog";
            this.btnResetRevised.Text = "Reset";
            this.btnResetRevised.UseVisualStyleBackColor = false;
            this.btnResetRevised.Click += new System.EventHandler(this.AlgorithmReset_Click);
            // 
            // btnRevisedExport
            // 
            this.btnRevisedExport.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRevisedExport.AutoSize = true;
            this.btnRevisedExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnRevisedExport.FlatAppearance.BorderSize = 0;
            this.btnRevisedExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRevisedExport.ForeColor = System.Drawing.Color.White;
            this.btnRevisedExport.Location = new System.Drawing.Point(215, 11);
            this.btnRevisedExport.Name = "btnRevisedExport";
            this.btnRevisedExport.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.btnRevisedExport.Size = new System.Drawing.Size(112, 51);
            this.btnRevisedExport.TabIndex = 4;
            this.btnRevisedExport.Tag = "lstBranchLog";
            this.btnRevisedExport.Text = "Export";
            this.btnRevisedExport.UseVisualStyleBackColor = false;
            // 
            // tabSensitivity
            // 
            this.tabSensitivity.Controls.Add(this.panelSensitivity);
            this.tabSensitivity.Location = new System.Drawing.Point(10, 49);
            this.tabSensitivity.Name = "tabSensitivity";
            this.tabSensitivity.Size = new System.Drawing.Size(1190, 918);
            this.tabSensitivity.TabIndex = 2;
            this.tabSensitivity.Text = "Data Sensitivity";
            // 
            // panelSensitivity
            // 
            this.panelSensitivity.Controls.Add(this.panel1);
            this.panelSensitivity.Controls.Add(this.lstSensitivityLog);
            this.panelSensitivity.Controls.Add(this.flowSensitivityButtons);
            this.panelSensitivity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSensitivity.Location = new System.Drawing.Point(0, 0);
            this.panelSensitivity.Name = "panelSensitivity";
            this.panelSensitivity.Padding = new System.Windows.Forms.Padding(12);
            this.panelSensitivity.Size = new System.Drawing.Size(1190, 918);
            this.panelSensitivity.TabIndex = 0;
            // 
            // btnAnalyzeNewVar
            // 
            this.btnAnalyzeNewVar.Depth = 0;
            this.btnAnalyzeNewVar.Icon = null;
            this.btnAnalyzeNewVar.Location = new System.Drawing.Point(959, 23);
            this.btnAnalyzeNewVar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnAnalyzeNewVar.Name = "btnAnalyzeNewVar";
            this.btnAnalyzeNewVar.Size = new System.Drawing.Size(56, 56);
            this.btnAnalyzeNewVar.TabIndex = 8;
            this.btnAnalyzeNewVar.Text = "Run";
            this.btnAnalyzeNewVar.UseVisualStyleBackColor = true;
            // 
            // txtNewVarCoeffs
            // 
            this.txtNewVarCoeffs.AnimateReadOnly = false;
            this.txtNewVarCoeffs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtNewVarCoeffs.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtNewVarCoeffs.Depth = 0;
            this.txtNewVarCoeffs.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtNewVarCoeffs.HideSelection = true;
            this.txtNewVarCoeffs.LeadingIcon = null;
            this.txtNewVarCoeffs.Location = new System.Drawing.Point(369, 31);
            this.txtNewVarCoeffs.MaxLength = 32767;
            this.txtNewVarCoeffs.MouseState = MaterialSkin.MouseState.OUT;
            this.txtNewVarCoeffs.Name = "txtNewVarCoeffs";
            this.txtNewVarCoeffs.PasswordChar = '\0';
            this.txtNewVarCoeffs.PrefixSuffixText = null;
            this.txtNewVarCoeffs.ReadOnly = false;
            this.txtNewVarCoeffs.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtNewVarCoeffs.SelectedText = "";
            this.txtNewVarCoeffs.SelectionLength = 0;
            this.txtNewVarCoeffs.SelectionStart = 0;
            this.txtNewVarCoeffs.ShortcutsEnabled = true;
            this.txtNewVarCoeffs.Size = new System.Drawing.Size(250, 48);
            this.txtNewVarCoeffs.TabIndex = 6;
            this.txtNewVarCoeffs.TabStop = false;
            this.txtNewVarCoeffs.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtNewVarCoeffs.TrailingIcon = null;
            this.txtNewVarCoeffs.UseSystemPasswordChar = false;
            // 
            // materialLabel2
            // 
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel2.Location = new System.Drawing.Point(414, 7);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(153, 35);
            this.materialLabel2.TabIndex = 7;
            this.materialLabel2.Text = "Add new coefficents";
            // 
            // txtNewVarObjective
            // 
            this.txtNewVarObjective.AnimateReadOnly = false;
            this.txtNewVarObjective.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtNewVarObjective.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtNewVarObjective.Depth = 0;
            this.txtNewVarObjective.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtNewVarObjective.HideSelection = true;
            this.txtNewVarObjective.LeadingIcon = null;
            this.txtNewVarObjective.Location = new System.Drawing.Point(21, 31);
            this.txtNewVarObjective.MaxLength = 32767;
            this.txtNewVarObjective.MouseState = MaterialSkin.MouseState.OUT;
            this.txtNewVarObjective.Name = "txtNewVarObjective";
            this.txtNewVarObjective.PasswordChar = '\0';
            this.txtNewVarObjective.PrefixSuffixText = null;
            this.txtNewVarObjective.ReadOnly = false;
            this.txtNewVarObjective.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtNewVarObjective.SelectedText = "";
            this.txtNewVarObjective.SelectionLength = 0;
            this.txtNewVarObjective.SelectionStart = 0;
            this.txtNewVarObjective.ShortcutsEnabled = true;
            this.txtNewVarObjective.Size = new System.Drawing.Size(250, 48);
            this.txtNewVarObjective.TabIndex = 4;
            this.txtNewVarObjective.TabStop = false;
            this.txtNewVarObjective.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtNewVarObjective.TrailingIcon = null;
            this.txtNewVarObjective.UseSystemPasswordChar = false;
            // 
            // lstSensitivityLog
            // 
            this.lstSensitivityLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstSensitivityLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this.lstSensitivityLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstSensitivityLog.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lstSensitivityLog.ItemHeight = 37;
            this.lstSensitivityLog.Location = new System.Drawing.Point(15, 214);
            this.lstSensitivityLog.Name = "lstSensitivityLog";
            this.lstSensitivityLog.Size = new System.Drawing.Size(1166, 666);
            this.lstSensitivityLog.TabIndex = 0;
            // 
            // flowSensitivityButtons
            // 
            this.flowSensitivityButtons.Controls.Add(this.btnRunSensitivity);
            this.flowSensitivityButtons.Controls.Add(this.btnResetSensitivity);
            this.flowSensitivityButtons.Controls.Add(this.btnDSExport);
            this.flowSensitivityButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowSensitivityButtons.Location = new System.Drawing.Point(12, 12);
            this.flowSensitivityButtons.Name = "flowSensitivityButtons";
            this.flowSensitivityButtons.Padding = new System.Windows.Forms.Padding(8);
            this.flowSensitivityButtons.Size = new System.Drawing.Size(1166, 70);
            this.flowSensitivityButtons.TabIndex = 2;
            // 
            // btnRunSensitivity
            // 
            this.btnRunSensitivity.AutoSize = true;
            this.btnRunSensitivity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnRunSensitivity.FlatAppearance.BorderSize = 0;
            this.btnRunSensitivity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRunSensitivity.ForeColor = System.Drawing.Color.White;
            this.btnRunSensitivity.Location = new System.Drawing.Point(11, 11);
            this.btnRunSensitivity.Name = "btnRunSensitivity";
            this.btnRunSensitivity.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.btnRunSensitivity.Size = new System.Drawing.Size(86, 51);
            this.btnRunSensitivity.TabIndex = 0;
            this.btnRunSensitivity.Tag = "lstSensitivityLog";
            this.btnRunSensitivity.Text = "Run";
            this.btnRunSensitivity.UseVisualStyleBackColor = false;
            this.btnRunSensitivity.Click += new System.EventHandler(this.RunSensitivity_Click);
            // 
            // btnResetSensitivity
            // 
            this.btnResetSensitivity.AutoSize = true;
            this.btnResetSensitivity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(239)))), ((int)(((byte)(244)))));
            this.btnResetSensitivity.FlatAppearance.BorderSize = 0;
            this.btnResetSensitivity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetSensitivity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnResetSensitivity.Location = new System.Drawing.Point(103, 11);
            this.btnResetSensitivity.Name = "btnResetSensitivity";
            this.btnResetSensitivity.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.btnResetSensitivity.Size = new System.Drawing.Size(106, 51);
            this.btnResetSensitivity.TabIndex = 2;
            this.btnResetSensitivity.Tag = "lstSensitivityLog";
            this.btnResetSensitivity.Text = "Reset";
            this.btnResetSensitivity.UseVisualStyleBackColor = false;
            this.btnResetSensitivity.Click += new System.EventHandler(this.AlgorithmReset_Click);
            // 
            // btnDSExport
            // 
            this.btnDSExport.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDSExport.AutoSize = true;
            this.btnDSExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnDSExport.FlatAppearance.BorderSize = 0;
            this.btnDSExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDSExport.ForeColor = System.Drawing.Color.White;
            this.btnDSExport.Location = new System.Drawing.Point(215, 11);
            this.btnDSExport.Name = "btnDSExport";
            this.btnDSExport.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.btnDSExport.Size = new System.Drawing.Size(112, 51);
            this.btnDSExport.TabIndex = 4;
            this.btnDSExport.Tag = "lstBranchLog";
            this.btnDSExport.Text = "Export";
            this.btnDSExport.UseVisualStyleBackColor = false;
            // 
            // materialLabel1
            // 
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel1.Location = new System.Drawing.Point(49, 7);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(196, 35);
            this.materialLabel1.TabIndex = 5;
            this.materialLabel1.Text = "Add new variable objective";
            // 
            // tabCutting
            // 
            this.tabCutting.Controls.Add(this.panelCutting);
            this.tabCutting.Location = new System.Drawing.Point(10, 49);
            this.tabCutting.Name = "tabCutting";
            this.tabCutting.Size = new System.Drawing.Size(1190, 940);
            this.tabCutting.TabIndex = 3;
            this.tabCutting.Text = "Cutting Plane";
            // 
            // panelCutting
            // 
            this.panelCutting.Controls.Add(this.lstCuttingLog);
            this.panelCutting.Controls.Add(this.flowCuttingButtons);
            this.panelCutting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCutting.Location = new System.Drawing.Point(0, 0);
            this.panelCutting.Name = "panelCutting";
            this.panelCutting.Padding = new System.Windows.Forms.Padding(12);
            this.panelCutting.Size = new System.Drawing.Size(1190, 940);
            this.panelCutting.TabIndex = 0;
            // 
            // lstCuttingLog
            // 
            this.lstCuttingLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this.lstCuttingLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstCuttingLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstCuttingLog.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lstCuttingLog.ItemHeight = 37;
            this.lstCuttingLog.Location = new System.Drawing.Point(12, 82);
            this.lstCuttingLog.Name = "lstCuttingLog";
            this.lstCuttingLog.Size = new System.Drawing.Size(1166, 846);
            this.lstCuttingLog.TabIndex = 0;
            // 
            // flowCuttingButtons
            // 
            this.flowCuttingButtons.Controls.Add(this.btnRunCutting);
            this.flowCuttingButtons.Controls.Add(this.btnStepCutting);
            this.flowCuttingButtons.Controls.Add(this.btnResetCutting);
            this.flowCuttingButtons.Controls.Add(this.btnCuttingExport);
            this.flowCuttingButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowCuttingButtons.Location = new System.Drawing.Point(12, 12);
            this.flowCuttingButtons.Name = "flowCuttingButtons";
            this.flowCuttingButtons.Padding = new System.Windows.Forms.Padding(8);
            this.flowCuttingButtons.Size = new System.Drawing.Size(1166, 70);
            this.flowCuttingButtons.TabIndex = 2;
            // 
            // btnRunCutting
            // 
            this.btnRunCutting.AutoSize = true;
            this.btnRunCutting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnRunCutting.FlatAppearance.BorderSize = 0;
            this.btnRunCutting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRunCutting.ForeColor = System.Drawing.Color.White;
            this.btnRunCutting.Location = new System.Drawing.Point(11, 11);
            this.btnRunCutting.Name = "btnRunCutting";
            this.btnRunCutting.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.btnRunCutting.Size = new System.Drawing.Size(86, 51);
            this.btnRunCutting.TabIndex = 0;
            this.btnRunCutting.Tag = "lstCuttingLog";
            this.btnRunCutting.Text = "Run";
            this.btnRunCutting.UseVisualStyleBackColor = false;
            this.btnRunCutting.Click += new System.EventHandler(this.RunCutting_Click);
            // 
            // btnStepCutting
            // 
            this.btnStepCutting.AutoSize = true;
            this.btnStepCutting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(239)))), ((int)(((byte)(244)))));
            this.btnStepCutting.FlatAppearance.BorderSize = 0;
            this.btnStepCutting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStepCutting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnStepCutting.Location = new System.Drawing.Point(103, 11);
            this.btnStepCutting.Name = "btnStepCutting";
            this.btnStepCutting.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.btnStepCutting.Size = new System.Drawing.Size(93, 51);
            this.btnStepCutting.TabIndex = 1;
            this.btnStepCutting.Tag = "lstCuttingLog";
            this.btnStepCutting.Text = "Step";
            this.btnStepCutting.UseVisualStyleBackColor = false;
            this.btnStepCutting.Click += new System.EventHandler(this.StepCutting_Click);
            // 
            // btnResetCutting
            // 
            this.btnResetCutting.AutoSize = true;
            this.btnResetCutting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(239)))), ((int)(((byte)(244)))));
            this.btnResetCutting.FlatAppearance.BorderSize = 0;
            this.btnResetCutting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetCutting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnResetCutting.Location = new System.Drawing.Point(202, 11);
            this.btnResetCutting.Name = "btnResetCutting";
            this.btnResetCutting.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.btnResetCutting.Size = new System.Drawing.Size(106, 51);
            this.btnResetCutting.TabIndex = 2;
            this.btnResetCutting.Tag = "lstCuttingLog";
            this.btnResetCutting.Text = "Reset";
            this.btnResetCutting.UseVisualStyleBackColor = false;
            this.btnResetCutting.Click += new System.EventHandler(this.AlgorithmReset_Click);
            // 
            // btnCuttingExport
            // 
            this.btnCuttingExport.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCuttingExport.AutoSize = true;
            this.btnCuttingExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnCuttingExport.FlatAppearance.BorderSize = 0;
            this.btnCuttingExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCuttingExport.ForeColor = System.Drawing.Color.White;
            this.btnCuttingExport.Location = new System.Drawing.Point(314, 11);
            this.btnCuttingExport.Name = "btnCuttingExport";
            this.btnCuttingExport.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.btnCuttingExport.Size = new System.Drawing.Size(112, 51);
            this.btnCuttingExport.TabIndex = 4;
            this.btnCuttingExport.Tag = "lstBranchLog";
            this.btnCuttingExport.Text = "Export";
            this.btnCuttingExport.UseVisualStyleBackColor = false;
            // 
            // tabBranchBound
            // 
            this.tabBranchBound.Controls.Add(this.panelBranch);
            this.tabBranchBound.Location = new System.Drawing.Point(10, 49);
            this.tabBranchBound.Name = "tabBranchBound";
            this.tabBranchBound.Size = new System.Drawing.Size(1190, 940);
            this.tabBranchBound.TabIndex = 4;
            this.tabBranchBound.Text = "Branch & Bound";
            // 
            // panelBranch
            // 
            this.panelBranch.Controls.Add(this.lstBranchLog);
            this.panelBranch.Controls.Add(this.flowBranchButtons);
            this.panelBranch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBranch.Location = new System.Drawing.Point(0, 0);
            this.panelBranch.Name = "panelBranch";
            this.panelBranch.Padding = new System.Windows.Forms.Padding(12);
            this.panelBranch.Size = new System.Drawing.Size(1190, 940);
            this.panelBranch.TabIndex = 0;
            // 
            // lstBranchLog
            // 
            this.lstBranchLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this.lstBranchLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstBranchLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBranchLog.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lstBranchLog.ItemHeight = 37;
            this.lstBranchLog.Location = new System.Drawing.Point(12, 82);
            this.lstBranchLog.Name = "lstBranchLog";
            this.lstBranchLog.Size = new System.Drawing.Size(1166, 846);
            this.lstBranchLog.TabIndex = 0;
            // 
            // flowBranchButtons
            // 
            this.flowBranchButtons.Controls.Add(this.btnRunBranch);
            this.flowBranchButtons.Controls.Add(this.btnStepBranch);
            this.flowBranchButtons.Controls.Add(this.btnResetBranch);
            this.flowBranchButtons.Controls.Add(this.btnBBExport);
            this.flowBranchButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowBranchButtons.Location = new System.Drawing.Point(12, 12);
            this.flowBranchButtons.Name = "flowBranchButtons";
            this.flowBranchButtons.Padding = new System.Windows.Forms.Padding(8);
            this.flowBranchButtons.Size = new System.Drawing.Size(1166, 70);
            this.flowBranchButtons.TabIndex = 2;
            // 
            // btnRunBranch
            // 
            this.btnRunBranch.AutoSize = true;
            this.btnRunBranch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnRunBranch.FlatAppearance.BorderSize = 0;
            this.btnRunBranch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRunBranch.ForeColor = System.Drawing.Color.White;
            this.btnRunBranch.Location = new System.Drawing.Point(11, 11);
            this.btnRunBranch.Name = "btnRunBranch";
            this.btnRunBranch.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.btnRunBranch.Size = new System.Drawing.Size(86, 51);
            this.btnRunBranch.TabIndex = 0;
            this.btnRunBranch.Tag = "lstBranchLog";
            this.btnRunBranch.Text = "Run";
            this.btnRunBranch.UseVisualStyleBackColor = false;
            this.btnRunBranch.Click += new System.EventHandler(this.RunBranch_Click);
            // 
            // btnStepBranch
            // 
            this.btnStepBranch.AutoSize = true;
            this.btnStepBranch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(239)))), ((int)(((byte)(244)))));
            this.btnStepBranch.FlatAppearance.BorderSize = 0;
            this.btnStepBranch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStepBranch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnStepBranch.Location = new System.Drawing.Point(103, 11);
            this.btnStepBranch.Name = "btnStepBranch";
            this.btnStepBranch.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.btnStepBranch.Size = new System.Drawing.Size(93, 51);
            this.btnStepBranch.TabIndex = 1;
            this.btnStepBranch.Tag = "lstBranchLog";
            this.btnStepBranch.Text = "Step";
            this.btnStepBranch.UseVisualStyleBackColor = false;
            this.btnStepBranch.Click += new System.EventHandler(this.StepBranch_Click);
            // 
            // btnResetBranch
            // 
            this.btnResetBranch.AutoSize = true;
            this.btnResetBranch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(239)))), ((int)(((byte)(244)))));
            this.btnResetBranch.FlatAppearance.BorderSize = 0;
            this.btnResetBranch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetBranch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnResetBranch.Location = new System.Drawing.Point(202, 11);
            this.btnResetBranch.Name = "btnResetBranch";
            this.btnResetBranch.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.btnResetBranch.Size = new System.Drawing.Size(106, 51);
            this.btnResetBranch.TabIndex = 2;
            this.btnResetBranch.Tag = "lstBranchLog";
            this.btnResetBranch.Text = "Reset";
            this.btnResetBranch.UseVisualStyleBackColor = false;
            this.btnResetBranch.Click += new System.EventHandler(this.AlgorithmReset_Click);
            // 
            // btnBBExport
            // 
            this.btnBBExport.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnBBExport.AutoSize = true;
            this.btnBBExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnBBExport.FlatAppearance.BorderSize = 0;
            this.btnBBExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBBExport.ForeColor = System.Drawing.Color.White;
            this.btnBBExport.Location = new System.Drawing.Point(314, 11);
            this.btnBBExport.Name = "btnBBExport";
            this.btnBBExport.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.btnBBExport.Size = new System.Drawing.Size(112, 51);
            this.btnBBExport.TabIndex = 3;
            this.btnBBExport.Tag = "lstBranchLog";
            this.btnBBExport.Text = "Export";
            this.btnBBExport.UseVisualStyleBackColor = false;
            // 
            // tabBBKnapsack
            // 
            this.tabBBKnapsack.BackColor = System.Drawing.SystemColors.Control;
            this.tabBBKnapsack.Controls.Add(this.rtbKnapsack);
            this.tabBBKnapsack.Controls.Add(this.flowLayoutPanel1);
            this.tabBBKnapsack.Location = new System.Drawing.Point(10, 49);
            this.tabBBKnapsack.Name = "tabBBKnapsack";
            this.tabBBKnapsack.Size = new System.Drawing.Size(1190, 918);
            this.tabBBKnapsack.TabIndex = 5;
            this.tabBBKnapsack.Text = "B&B Knapsack";
            // 
            // rtbKnapsack
            // 
            this.rtbKnapsack.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbKnapsack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this.rtbKnapsack.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbKnapsack.Location = new System.Drawing.Point(11, 83);
            this.rtbKnapsack.Name = "rtbKnapsack";
            this.rtbKnapsack.Size = new System.Drawing.Size(1164, 830);
            this.rtbKnapsack.TabIndex = 4;
            this.rtbKnapsack.Text = "";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnKnapsackRun);
            this.flowLayoutPanel1.Controls.Add(this.btnKnapsackStep);
            this.flowLayoutPanel1.Controls.Add(this.button3);
            this.flowLayoutPanel1.Controls.Add(this.btnKnapsackExport);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(10);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(8);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1190, 70);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // btnKnapsackRun
            // 
            this.btnKnapsackRun.AutoSize = true;
            this.btnKnapsackRun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnKnapsackRun.FlatAppearance.BorderSize = 0;
            this.btnKnapsackRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKnapsackRun.ForeColor = System.Drawing.Color.White;
            this.btnKnapsackRun.Location = new System.Drawing.Point(11, 11);
            this.btnKnapsackRun.Name = "btnKnapsackRun";
            this.btnKnapsackRun.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.btnKnapsackRun.Size = new System.Drawing.Size(86, 51);
            this.btnKnapsackRun.TabIndex = 0;
            this.btnKnapsackRun.Tag = "lstBranchLog";
            this.btnKnapsackRun.Text = "Run";
            this.btnKnapsackRun.UseVisualStyleBackColor = false;
            this.btnKnapsackRun.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnKnapsackStep
            // 
            this.btnKnapsackStep.AutoSize = true;
            this.btnKnapsackStep.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(239)))), ((int)(((byte)(244)))));
            this.btnKnapsackStep.FlatAppearance.BorderSize = 0;
            this.btnKnapsackStep.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKnapsackStep.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnKnapsackStep.Location = new System.Drawing.Point(103, 11);
            this.btnKnapsackStep.Name = "btnKnapsackStep";
            this.btnKnapsackStep.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.btnKnapsackStep.Size = new System.Drawing.Size(93, 51);
            this.btnKnapsackStep.TabIndex = 1;
            this.btnKnapsackStep.Tag = "lstBranchLog";
            this.btnKnapsackStep.Text = "Step";
            this.btnKnapsackStep.UseVisualStyleBackColor = false;
            this.btnKnapsackStep.Click += new System.EventHandler(this.btnKnapsackStep_Click);
            // 
            // button3
            // 
            this.button3.AutoSize = true;
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(239)))), ((int)(((byte)(244)))));
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.button3.Location = new System.Drawing.Point(202, 11);
            this.button3.Name = "button3";
            this.button3.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.button3.Size = new System.Drawing.Size(106, 51);
            this.button3.TabIndex = 2;
            this.button3.Tag = "lstBranchLog";
            this.button3.Text = "Reset";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnKnapsackExport
            // 
            this.btnKnapsackExport.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnKnapsackExport.AutoSize = true;
            this.btnKnapsackExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnKnapsackExport.FlatAppearance.BorderSize = 0;
            this.btnKnapsackExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKnapsackExport.ForeColor = System.Drawing.Color.White;
            this.btnKnapsackExport.Location = new System.Drawing.Point(314, 11);
            this.btnKnapsackExport.Name = "btnKnapsackExport";
            this.btnKnapsackExport.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.btnKnapsackExport.Size = new System.Drawing.Size(112, 51);
            this.btnKnapsackExport.TabIndex = 4;
            this.btnKnapsackExport.Tag = "lstBranchLog";
            this.btnKnapsackExport.Text = "Export";
            this.btnKnapsackExport.UseVisualStyleBackColor = false;
            // 
            // tabNonLinear
            // 
            this.tabNonLinear.BackColor = System.Drawing.SystemColors.Control;
            this.tabNonLinear.Controls.Add(this.rtbNL);
            this.tabNonLinear.Controls.Add(this.flowLayoutPanel2);
            this.tabNonLinear.Location = new System.Drawing.Point(10, 49);
            this.tabNonLinear.Name = "tabNonLinear";
            this.tabNonLinear.Size = new System.Drawing.Size(1190, 918);
            this.tabNonLinear.TabIndex = 6;
            this.tabNonLinear.Text = "Non Linear";
            // 
            // rtbNL
            // 
            this.rtbNL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbNL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this.rtbNL.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbNL.Location = new System.Drawing.Point(11, 76);
            this.rtbNL.Name = "rtbNL";
            this.rtbNL.Size = new System.Drawing.Size(1164, 830);
            this.rtbNL.TabIndex = 5;
            this.rtbNL.Text = "This is a test";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.button1);
            this.flowLayoutPanel2.Controls.Add(this.btnNLReset);
            this.flowLayoutPanel2.Controls.Add(this.btnNLExport);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(8);
            this.flowLayoutPanel2.Size = new System.Drawing.Size(1190, 70);
            this.flowLayoutPanel2.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(11, 11);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.button1.Size = new System.Drawing.Size(86, 51);
            this.button1.TabIndex = 0;
            this.button1.Tag = "lstPrimalLog";
            this.button1.Text = "Run";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnNLReset
            // 
            this.btnNLReset.AutoSize = true;
            this.btnNLReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(239)))), ((int)(((byte)(244)))));
            this.btnNLReset.FlatAppearance.BorderSize = 0;
            this.btnNLReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNLReset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnNLReset.Location = new System.Drawing.Point(103, 11);
            this.btnNLReset.Name = "btnNLReset";
            this.btnNLReset.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.btnNLReset.Size = new System.Drawing.Size(106, 51);
            this.btnNLReset.TabIndex = 2;
            this.btnNLReset.Tag = "lstPrimalLog";
            this.btnNLReset.Text = "Reset";
            this.btnNLReset.UseVisualStyleBackColor = false;
            this.btnNLReset.Click += new System.EventHandler(this.btnNLReset_Click);
            // 
            // btnNLExport
            // 
            this.btnNLExport.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnNLExport.AutoSize = true;
            this.btnNLExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnNLExport.FlatAppearance.BorderSize = 0;
            this.btnNLExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNLExport.ForeColor = System.Drawing.Color.White;
            this.btnNLExport.Location = new System.Drawing.Point(215, 11);
            this.btnNLExport.Name = "btnNLExport";
            this.btnNLExport.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.btnNLExport.Size = new System.Drawing.Size(112, 51);
            this.btnNLExport.TabIndex = 4;
            this.btnNLExport.Tag = "lstBranchLog";
            this.btnNLExport.Text = "Export";
            this.btnNLExport.UseVisualStyleBackColor = false;
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(36, 36);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 1007);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1924, 48);
            this.statusStrip.TabIndex = 1;
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(89, 37);
            this.statusLabel.Text = "Ready";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtNewRHSvar);
            this.panel1.Controls.Add(this.txtNewVarCoeffs);
            this.panel1.Controls.Add(this.txtNewVarObjective);
            this.panel1.Controls.Add(this.btnAnalyzeNewVar);
            this.panel1.Controls.Add(this.materialLabel3);
            this.panel1.Controls.Add(this.materialLabel1);
            this.panel1.Controls.Add(this.materialLabel2);
            this.panel1.Location = new System.Drawing.Point(12, 88);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1159, 92);
            this.panel1.TabIndex = 9;
            // 
            // txtNewRHSvar
            // 
            this.txtNewRHSvar.AnimateReadOnly = false;
            this.txtNewRHSvar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtNewRHSvar.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtNewRHSvar.Depth = 0;
            this.txtNewRHSvar.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtNewRHSvar.HideSelection = true;
            this.txtNewRHSvar.LeadingIcon = null;
            this.txtNewRHSvar.Location = new System.Drawing.Point(651, 31);
            this.txtNewRHSvar.MaxLength = 32767;
            this.txtNewRHSvar.MouseState = MaterialSkin.MouseState.OUT;
            this.txtNewRHSvar.Name = "txtNewRHSvar";
            this.txtNewRHSvar.PasswordChar = '\0';
            this.txtNewRHSvar.PrefixSuffixText = null;
            this.txtNewRHSvar.ReadOnly = false;
            this.txtNewRHSvar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtNewRHSvar.SelectedText = "";
            this.txtNewRHSvar.SelectionLength = 0;
            this.txtNewRHSvar.SelectionStart = 0;
            this.txtNewRHSvar.ShortcutsEnabled = true;
            this.txtNewRHSvar.Size = new System.Drawing.Size(250, 48);
            this.txtNewRHSvar.TabIndex = 8;
            this.txtNewRHSvar.TabStop = false;
            this.txtNewRHSvar.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtNewRHSvar.TrailingIcon = null;
            this.txtNewRHSvar.UseSystemPasswordChar = false;
            // 
            // materialLabel3
            // 
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel3.Location = new System.Drawing.Point(696, 7);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(153, 35);
            this.materialLabel3.TabIndex = 9;
            this.materialLabel3.Text = "Add new RHS";
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1924, 1055);
            this.Controls.Add(this.tableLayoutMain);
            this.Controls.Add(this.statusStrip);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Linear Programming Algorithms";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutMain.ResumeLayout(false);
            this.leftColumn.ResumeLayout(false);
            this.groupFileInput.ResumeLayout(false);
            this.groupFileInput.PerformLayout();
            this.panelDropZone.ResumeLayout(false);
            this.flowFileButtons.ResumeLayout(false);
            this.flowFileButtons.PerformLayout();
            this.groupPreview.ResumeLayout(false);
            this.groupPreview.PerformLayout();
            this.previewTopPanel.ResumeLayout(false);
            this.previewTopPanel.PerformLayout();
            this.tabControlAlgorithms.ResumeLayout(false);
            this.tabPrimal.ResumeLayout(false);
            this.panelPrimal.ResumeLayout(false);
            this.flowPrimalButtons.ResumeLayout(false);
            this.flowPrimalButtons.PerformLayout();
            this.tabRevised.ResumeLayout(false);
            this.panelRevised.ResumeLayout(false);
            this.flowRevisedButtons.ResumeLayout(false);
            this.flowRevisedButtons.PerformLayout();
            this.tabSensitivity.ResumeLayout(false);
            this.panelSensitivity.ResumeLayout(false);
            this.flowSensitivityButtons.ResumeLayout(false);
            this.flowSensitivityButtons.PerformLayout();
            this.tabCutting.ResumeLayout(false);
            this.panelCutting.ResumeLayout(false);
            this.flowCuttingButtons.ResumeLayout(false);
            this.flowCuttingButtons.PerformLayout();
            this.tabBranchBound.ResumeLayout(false);
            this.panelBranch.ResumeLayout(false);
            this.flowBranchButtons.ResumeLayout(false);
            this.flowBranchButtons.PerformLayout();
            this.tabBBKnapsack.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tabNonLinear.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.TabControl tabControlAlgorithms;
        private System.Windows.Forms.TabPage tabPrimal;
        private System.Windows.Forms.Panel panelPrimal;
        private System.Windows.Forms.ListBox lstPrimalLog;
        private System.Windows.Forms.FlowLayoutPanel flowPrimalButtons;
        private System.Windows.Forms.Button btnRunPrimal;
        private System.Windows.Forms.Button btnResetPrimal;
        private System.Windows.Forms.TabPage tabRevised;
        private System.Windows.Forms.Panel panelRevised;
        private System.Windows.Forms.ListBox lstRevisedLog;
        private System.Windows.Forms.FlowLayoutPanel flowRevisedButtons;
        private System.Windows.Forms.Button btnRunRevised;
        private System.Windows.Forms.Button btnResetRevised;
        private System.Windows.Forms.TabPage tabSensitivity;
        private System.Windows.Forms.Panel panelSensitivity;
        private System.Windows.Forms.ListBox lstSensitivityLog;
        private System.Windows.Forms.FlowLayoutPanel flowSensitivityButtons;
        private System.Windows.Forms.Button btnRunSensitivity;
        private System.Windows.Forms.Button btnResetSensitivity;
        private System.Windows.Forms.TabPage tabCutting;
        private System.Windows.Forms.Panel panelCutting;
        private System.Windows.Forms.ListBox lstCuttingLog;
        private System.Windows.Forms.FlowLayoutPanel flowCuttingButtons;
        private System.Windows.Forms.Button btnRunCutting;
        private System.Windows.Forms.Button btnStepCutting;
        private System.Windows.Forms.Button btnResetCutting;
        private System.Windows.Forms.TabPage tabBranchBound;
        private System.Windows.Forms.Panel panelBranch;
        private System.Windows.Forms.ListBox lstBranchLog;
        private System.Windows.Forms.FlowLayoutPanel flowBranchButtons;
        private System.Windows.Forms.Button btnRunBranch;
        private System.Windows.Forms.Button btnStepBranch;
        private System.Windows.Forms.Button btnResetBranch;
        private System.Windows.Forms.TabPage tabBBKnapsack;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnKnapsackRun;
        private System.Windows.Forms.Button btnKnapsackStep;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnPrimalExport;
        private System.Windows.Forms.Button btnRevisedExport;
        private System.Windows.Forms.Button btnDSExport;
        private System.Windows.Forms.Button btnCuttingExport;
        private System.Windows.Forms.Button btnBBExport;
        private System.Windows.Forms.Button btnKnapsackExport;
        private System.Windows.Forms.RichTextBox rtbKnapsack;
        private System.Windows.Forms.TabPage tabNonLinear;
        private System.Windows.Forms.RichTextBox rtbNL;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnNLReset;
        private System.Windows.Forms.Button btnNLExport;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialTextBox2 txtNewVarObjective;
        private MaterialSkin.Controls.MaterialTextBox2 txtNewVarCoeffs;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialFloatingActionButton btnAnalyzeNewVar;
        private System.Windows.Forms.Panel panel1;
        private MaterialSkin.Controls.MaterialTextBox2 txtNewRHSvar;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
    }
}
