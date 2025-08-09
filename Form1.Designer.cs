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

        private System.Windows.Forms.TabControl tabControlAlgorithms;
        private System.Windows.Forms.TabPage tabPrimal;
        private System.Windows.Forms.TabPage tabRevised;
        private System.Windows.Forms.TabPage tabSensitivity;
        private System.Windows.Forms.TabPage tabCutting;
        private System.Windows.Forms.TabPage tabBranchBound;

        private System.Windows.Forms.FlowLayoutPanel flowPrimalButtons;
        private System.Windows.Forms.Button btnRunPrimal;
        private System.Windows.Forms.Button btnStepPrimal;
        private System.Windows.Forms.Button btnResetPrimal;
        private System.Windows.Forms.ListBox lstPrimalLog;

        private System.Windows.Forms.FlowLayoutPanel flowRevisedButtons;
        private System.Windows.Forms.Button btnRunRevised;
        private System.Windows.Forms.Button btnStepRevised;
        private System.Windows.Forms.Button btnResetRevised;
        private System.Windows.Forms.ListBox lstRevisedLog;

        private System.Windows.Forms.FlowLayoutPanel flowSensitivityButtons;
        private System.Windows.Forms.Button btnRunSensitivity;
        private System.Windows.Forms.Button btnStepSensitivity;
        private System.Windows.Forms.Button btnResetSensitivity;
        private System.Windows.Forms.ListBox lstSensitivityLog;

        private System.Windows.Forms.FlowLayoutPanel flowCuttingButtons;
        private System.Windows.Forms.Button btnRunCutting;
        private System.Windows.Forms.Button btnStepCutting;
        private System.Windows.Forms.Button btnResetCutting;
        private System.Windows.Forms.ListBox lstCuttingLog;

        private System.Windows.Forms.FlowLayoutPanel flowBranchButtons;
        private System.Windows.Forms.Button btnRunBranch;
        private System.Windows.Forms.Button btnStepBranch;
        private System.Windows.Forms.Button btnResetBranch;
        private System.Windows.Forms.ListBox lstBranchLog;

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
            this.lblPrimal = new System.Windows.Forms.Label();
            this.flowPrimalButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRunPrimal = new System.Windows.Forms.Button();
            this.btnStepPrimal = new System.Windows.Forms.Button();
            this.btnResetPrimal = new System.Windows.Forms.Button();
            this.tabRevised = new System.Windows.Forms.TabPage();
            this.panelRevised = new System.Windows.Forms.Panel();
            this.lstRevisedLog = new System.Windows.Forms.ListBox();
            this.lblRevised = new System.Windows.Forms.Label();
            this.flowRevisedButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRunRevised = new System.Windows.Forms.Button();
            this.btnStepRevised = new System.Windows.Forms.Button();
            this.btnResetRevised = new System.Windows.Forms.Button();
            this.tabSensitivity = new System.Windows.Forms.TabPage();
            this.panelSensitivity = new System.Windows.Forms.Panel();
            this.lstSensitivityLog = new System.Windows.Forms.ListBox();
            this.lblSensitivity = new System.Windows.Forms.Label();
            this.flowSensitivityButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRunSensitivity = new System.Windows.Forms.Button();
            this.btnStepSensitivity = new System.Windows.Forms.Button();
            this.btnResetSensitivity = new System.Windows.Forms.Button();
            this.tabCutting = new System.Windows.Forms.TabPage();
            this.panelCutting = new System.Windows.Forms.Panel();
            this.lstCuttingLog = new System.Windows.Forms.ListBox();
            this.lblCutting = new System.Windows.Forms.Label();
            this.flowCuttingButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRunCutting = new System.Windows.Forms.Button();
            this.btnStepCutting = new System.Windows.Forms.Button();
            this.btnResetCutting = new System.Windows.Forms.Button();
            this.tabBranchBound = new System.Windows.Forms.TabPage();
            this.panelBranch = new System.Windows.Forms.Panel();
            this.lstBranchLog = new System.Windows.Forms.ListBox();
            this.lblBranch = new System.Windows.Forms.Label();
            this.flowBranchButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRunBranch = new System.Windows.Forms.Button();
            this.btnStepBranch = new System.Windows.Forms.Button();
            this.btnResetBranch = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.statusStrip.SuspendLayout();
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
            this.tableLayoutMain.Size = new System.Drawing.Size(2510, 1142);
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
            this.leftColumn.Size = new System.Drawing.Size(888, 1112);
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
            this.groupFileInput.Size = new System.Drawing.Size(882, 416);
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
            this.panelDropZone.Location = new System.Drawing.Point(12, 304);
            this.panelDropZone.Name = "panelDropZone";
            this.panelDropZone.Size = new System.Drawing.Size(858, 100);
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
            this.lblDropHint.Size = new System.Drawing.Size(856, 98);
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
            this.flowFileButtons.Size = new System.Drawing.Size(858, 57);
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
            this.groupPreview.Location = new System.Drawing.Point(3, 425);
            this.groupPreview.Name = "groupPreview";
            this.groupPreview.Padding = new System.Windows.Forms.Padding(12);
            this.groupPreview.Size = new System.Drawing.Size(882, 684);
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
            this.txtPreview.Size = new System.Drawing.Size(858, 588);
            this.txtPreview.TabIndex = 0;
            // 
            // previewTopPanel
            // 
            this.previewTopPanel.Controls.Add(this.btnCopyPreview);
            this.previewTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.previewTopPanel.Location = new System.Drawing.Point(12, 40);
            this.previewTopPanel.Name = "previewTopPanel";
            this.previewTopPanel.Size = new System.Drawing.Size(858, 44);
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
            this.tabControlAlgorithms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlAlgorithms.Location = new System.Drawing.Point(909, 15);
            this.tabControlAlgorithms.Name = "tabControlAlgorithms";
            this.tabControlAlgorithms.Padding = new System.Drawing.Point(8, 6);
            this.tabControlAlgorithms.SelectedIndex = 0;
            this.tabControlAlgorithms.Size = new System.Drawing.Size(1586, 1112);
            this.tabControlAlgorithms.TabIndex = 1;
            // 
            // tabPrimal
            // 
            this.tabPrimal.Controls.Add(this.panelPrimal);
            this.tabPrimal.Location = new System.Drawing.Point(10, 49);
            this.tabPrimal.Name = "tabPrimal";
            this.tabPrimal.Size = new System.Drawing.Size(1566, 1053);
            this.tabPrimal.TabIndex = 0;
            this.tabPrimal.Text = "Primal Simplex";
            // 
            // panelPrimal
            // 
            this.panelPrimal.Controls.Add(this.lstPrimalLog);
            this.panelPrimal.Controls.Add(this.lblPrimal);
            this.panelPrimal.Controls.Add(this.flowPrimalButtons);
            this.panelPrimal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrimal.Location = new System.Drawing.Point(0, 0);
            this.panelPrimal.Name = "panelPrimal";
            this.panelPrimal.Padding = new System.Windows.Forms.Padding(12);
            this.panelPrimal.Size = new System.Drawing.Size(1566, 1053);
            this.panelPrimal.TabIndex = 0;
            // 
            // lstPrimalLog
            // 
            this.lstPrimalLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this.lstPrimalLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstPrimalLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstPrimalLog.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lstPrimalLog.ItemHeight = 37;
            this.lstPrimalLog.Location = new System.Drawing.Point(12, 94);
            this.lstPrimalLog.Name = "lstPrimalLog";
            this.lstPrimalLog.Size = new System.Drawing.Size(1542, 947);
            this.lstPrimalLog.TabIndex = 0;
            // 
            // lblPrimal
            // 
            this.lblPrimal.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPrimal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(85)))), ((int)(((byte)(101)))));
            this.lblPrimal.Location = new System.Drawing.Point(12, 68);
            this.lblPrimal.Name = "lblPrimal";
            this.lblPrimal.Padding = new System.Windows.Forms.Padding(8, 6, 8, 0);
            this.lblPrimal.Size = new System.Drawing.Size(1542, 26);
            this.lblPrimal.TabIndex = 1;
            this.lblPrimal.Text = "Control branching strategy and explore nodes (UI stub).";
            // 
            // flowPrimalButtons
            // 
            this.flowPrimalButtons.Controls.Add(this.btnRunPrimal);
            this.flowPrimalButtons.Controls.Add(this.btnStepPrimal);
            this.flowPrimalButtons.Controls.Add(this.btnResetPrimal);
            this.flowPrimalButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowPrimalButtons.Location = new System.Drawing.Point(12, 12);
            this.flowPrimalButtons.Name = "flowPrimalButtons";
            this.flowPrimalButtons.Padding = new System.Windows.Forms.Padding(8);
            this.flowPrimalButtons.Size = new System.Drawing.Size(1542, 56);
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
       
            // 
            // btnStepPrimal
            // 
            this.btnStepPrimal.AutoSize = true;
            this.btnStepPrimal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(239)))), ((int)(((byte)(244)))));
            this.btnStepPrimal.FlatAppearance.BorderSize = 0;
            this.btnStepPrimal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStepPrimal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnStepPrimal.Location = new System.Drawing.Point(103, 11);
            this.btnStepPrimal.Name = "btnStepPrimal";
            this.btnStepPrimal.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.btnStepPrimal.Size = new System.Drawing.Size(93, 51);
            this.btnStepPrimal.TabIndex = 1;
            this.btnStepPrimal.Tag = "lstPrimalLog";
            this.btnStepPrimal.Text = "Step";
            this.btnStepPrimal.UseVisualStyleBackColor = false;
          
            // 
            // btnResetPrimal
            // 
            this.btnResetPrimal.AutoSize = true;
            this.btnResetPrimal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(239)))), ((int)(((byte)(244)))));
            this.btnResetPrimal.FlatAppearance.BorderSize = 0;
            this.btnResetPrimal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetPrimal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnResetPrimal.Location = new System.Drawing.Point(202, 11);
            this.btnResetPrimal.Name = "btnResetPrimal";
            this.btnResetPrimal.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.btnResetPrimal.Size = new System.Drawing.Size(106, 51);
            this.btnResetPrimal.TabIndex = 2;
            this.btnResetPrimal.Tag = "lstPrimalLog";
            this.btnResetPrimal.Text = "Reset";
            this.btnResetPrimal.UseVisualStyleBackColor = false;
            
            // 
            // tabRevised
            // 
            this.tabRevised.Controls.Add(this.panelRevised);
            this.tabRevised.Location = new System.Drawing.Point(10, 49);
            this.tabRevised.Name = "tabRevised";
            this.tabRevised.Size = new System.Drawing.Size(1566, 1053);
            this.tabRevised.TabIndex = 1;
            this.tabRevised.Text = "Revised Primal Simplex";
            // 
            // panelRevised
            // 
            this.panelRevised.Controls.Add(this.lstRevisedLog);
            this.panelRevised.Controls.Add(this.lblRevised);
            this.panelRevised.Controls.Add(this.flowRevisedButtons);
            this.panelRevised.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRevised.Location = new System.Drawing.Point(0, 0);
            this.panelRevised.Name = "panelRevised";
            this.panelRevised.Padding = new System.Windows.Forms.Padding(12);
            this.panelRevised.Size = new System.Drawing.Size(1566, 1053);
            this.panelRevised.TabIndex = 0;
            // 
            // lstRevisedLog
            // 
            this.lstRevisedLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this.lstRevisedLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstRevisedLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRevisedLog.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lstRevisedLog.ItemHeight = 37;
            this.lstRevisedLog.Location = new System.Drawing.Point(12, 94);
            this.lstRevisedLog.Name = "lstRevisedLog";
            this.lstRevisedLog.Size = new System.Drawing.Size(1542, 947);
            this.lstRevisedLog.TabIndex = 0;
            // 
            // lblRevised
            // 
            this.lblRevised.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRevised.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(85)))), ((int)(((byte)(101)))));
            this.lblRevised.Location = new System.Drawing.Point(12, 68);
            this.lblRevised.Name = "lblRevised";
            this.lblRevised.Padding = new System.Windows.Forms.Padding(8, 6, 8, 0);
            this.lblRevised.Size = new System.Drawing.Size(1542, 26);
            this.lblRevised.TabIndex = 1;
            this.lblRevised.Text = "Control branching strategy and explore nodes (UI stub).";
            // 
            // flowRevisedButtons
            // 
            this.flowRevisedButtons.Controls.Add(this.btnRunRevised);
            this.flowRevisedButtons.Controls.Add(this.btnStepRevised);
            this.flowRevisedButtons.Controls.Add(this.btnResetRevised);
            this.flowRevisedButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowRevisedButtons.Location = new System.Drawing.Point(12, 12);
            this.flowRevisedButtons.Name = "flowRevisedButtons";
            this.flowRevisedButtons.Padding = new System.Windows.Forms.Padding(8);
            this.flowRevisedButtons.Size = new System.Drawing.Size(1542, 56);
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
            
            // 
            // btnStepRevised
            // 
            this.btnStepRevised.AutoSize = true;
            this.btnStepRevised.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(239)))), ((int)(((byte)(244)))));
            this.btnStepRevised.FlatAppearance.BorderSize = 0;
            this.btnStepRevised.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStepRevised.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnStepRevised.Location = new System.Drawing.Point(103, 11);
            this.btnStepRevised.Name = "btnStepRevised";
            this.btnStepRevised.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.btnStepRevised.Size = new System.Drawing.Size(93, 51);
            this.btnStepRevised.TabIndex = 1;
            this.btnStepRevised.Tag = "lstRevisedLog";
            this.btnStepRevised.Text = "Step";
            this.btnStepRevised.UseVisualStyleBackColor = false;
           
            // 
            // btnResetRevised
            // 
            this.btnResetRevised.AutoSize = true;
            this.btnResetRevised.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(239)))), ((int)(((byte)(244)))));
            this.btnResetRevised.FlatAppearance.BorderSize = 0;
            this.btnResetRevised.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetRevised.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnResetRevised.Location = new System.Drawing.Point(202, 11);
            this.btnResetRevised.Name = "btnResetRevised";
            this.btnResetRevised.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.btnResetRevised.Size = new System.Drawing.Size(106, 51);
            this.btnResetRevised.TabIndex = 2;
            this.btnResetRevised.Tag = "lstRevisedLog";
            this.btnResetRevised.Text = "Reset";
            this.btnResetRevised.UseVisualStyleBackColor = false;
            
            // 
            // tabSensitivity
            // 
            this.tabSensitivity.Controls.Add(this.panelSensitivity);
            this.tabSensitivity.Location = new System.Drawing.Point(10, 49);
            this.tabSensitivity.Name = "tabSensitivity";
            this.tabSensitivity.Size = new System.Drawing.Size(1566, 1053);
            this.tabSensitivity.TabIndex = 2;
            this.tabSensitivity.Text = "Data Sensitivity";
            // 
            // panelSensitivity
            // 
            this.panelSensitivity.Controls.Add(this.lstSensitivityLog);
            this.panelSensitivity.Controls.Add(this.lblSensitivity);
            this.panelSensitivity.Controls.Add(this.flowSensitivityButtons);
            this.panelSensitivity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSensitivity.Location = new System.Drawing.Point(0, 0);
            this.panelSensitivity.Name = "panelSensitivity";
            this.panelSensitivity.Padding = new System.Windows.Forms.Padding(12);
            this.panelSensitivity.Size = new System.Drawing.Size(1566, 1053);
            this.panelSensitivity.TabIndex = 0;
            // 
            // lstSensitivityLog
            // 
            this.lstSensitivityLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this.lstSensitivityLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstSensitivityLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSensitivityLog.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lstSensitivityLog.ItemHeight = 37;
            this.lstSensitivityLog.Location = new System.Drawing.Point(12, 94);
            this.lstSensitivityLog.Name = "lstSensitivityLog";
            this.lstSensitivityLog.Size = new System.Drawing.Size(1542, 947);
            this.lstSensitivityLog.TabIndex = 0;
            // 
            // lblSensitivity
            // 
            this.lblSensitivity.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSensitivity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(85)))), ((int)(((byte)(101)))));
            this.lblSensitivity.Location = new System.Drawing.Point(12, 68);
            this.lblSensitivity.Name = "lblSensitivity";
            this.lblSensitivity.Padding = new System.Windows.Forms.Padding(8, 6, 8, 0);
            this.lblSensitivity.Size = new System.Drawing.Size(1542, 26);
            this.lblSensitivity.TabIndex = 1;
            this.lblSensitivity.Text = "Control branching strategy and explore nodes (UI stub).";
            // 
            // flowSensitivityButtons
            // 
            this.flowSensitivityButtons.Controls.Add(this.btnRunSensitivity);
            this.flowSensitivityButtons.Controls.Add(this.btnStepSensitivity);
            this.flowSensitivityButtons.Controls.Add(this.btnResetSensitivity);
            this.flowSensitivityButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowSensitivityButtons.Location = new System.Drawing.Point(12, 12);
            this.flowSensitivityButtons.Name = "flowSensitivityButtons";
            this.flowSensitivityButtons.Padding = new System.Windows.Forms.Padding(8);
            this.flowSensitivityButtons.Size = new System.Drawing.Size(1542, 56);
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
         
            // 
            // btnStepSensitivity
            // 
            this.btnStepSensitivity.AutoSize = true;
            this.btnStepSensitivity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(239)))), ((int)(((byte)(244)))));
            this.btnStepSensitivity.FlatAppearance.BorderSize = 0;
            this.btnStepSensitivity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStepSensitivity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnStepSensitivity.Location = new System.Drawing.Point(103, 11);
            this.btnStepSensitivity.Name = "btnStepSensitivity";
            this.btnStepSensitivity.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.btnStepSensitivity.Size = new System.Drawing.Size(93, 51);
            this.btnStepSensitivity.TabIndex = 1;
            this.btnStepSensitivity.Tag = "lstSensitivityLog";
            this.btnStepSensitivity.Text = "Step";
            this.btnStepSensitivity.UseVisualStyleBackColor = false;
          
            // 
            // btnResetSensitivity
            // 
            this.btnResetSensitivity.AutoSize = true;
            this.btnResetSensitivity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(239)))), ((int)(((byte)(244)))));
            this.btnResetSensitivity.FlatAppearance.BorderSize = 0;
            this.btnResetSensitivity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetSensitivity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnResetSensitivity.Location = new System.Drawing.Point(202, 11);
            this.btnResetSensitivity.Name = "btnResetSensitivity";
            this.btnResetSensitivity.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.btnResetSensitivity.Size = new System.Drawing.Size(106, 51);
            this.btnResetSensitivity.TabIndex = 2;
            this.btnResetSensitivity.Tag = "lstSensitivityLog";
            this.btnResetSensitivity.Text = "Reset";
            this.btnResetSensitivity.UseVisualStyleBackColor = false;
            
            // 
            // tabCutting
            // 
            this.tabCutting.Controls.Add(this.panelCutting);
            this.tabCutting.Location = new System.Drawing.Point(10, 49);
            this.tabCutting.Name = "tabCutting";
            this.tabCutting.Size = new System.Drawing.Size(1566, 1053);
            this.tabCutting.TabIndex = 3;
            this.tabCutting.Text = "Cutting Plane";
            // 
            // panelCutting
            // 
            this.panelCutting.Controls.Add(this.lstCuttingLog);
            this.panelCutting.Controls.Add(this.lblCutting);
            this.panelCutting.Controls.Add(this.flowCuttingButtons);
            this.panelCutting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCutting.Location = new System.Drawing.Point(0, 0);
            this.panelCutting.Name = "panelCutting";
            this.panelCutting.Padding = new System.Windows.Forms.Padding(12);
            this.panelCutting.Size = new System.Drawing.Size(1566, 1053);
            this.panelCutting.TabIndex = 0;
            // 
            // lstCuttingLog
            // 
            this.lstCuttingLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this.lstCuttingLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstCuttingLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstCuttingLog.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lstCuttingLog.ItemHeight = 37;
            this.lstCuttingLog.Location = new System.Drawing.Point(12, 94);
            this.lstCuttingLog.Name = "lstCuttingLog";
            this.lstCuttingLog.Size = new System.Drawing.Size(1542, 947);
            this.lstCuttingLog.TabIndex = 0;
            // 
            // lblCutting
            // 
            this.lblCutting.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCutting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(85)))), ((int)(((byte)(101)))));
            this.lblCutting.Location = new System.Drawing.Point(12, 68);
            this.lblCutting.Name = "lblCutting";
            this.lblCutting.Padding = new System.Windows.Forms.Padding(8, 6, 8, 0);
            this.lblCutting.Size = new System.Drawing.Size(1542, 26);
            this.lblCutting.TabIndex = 1;
            this.lblCutting.Text = "Control branching strategy and explore nodes (UI stub).";
            // 
            // flowCuttingButtons
            // 
            this.flowCuttingButtons.Controls.Add(this.btnRunCutting);
            this.flowCuttingButtons.Controls.Add(this.btnStepCutting);
            this.flowCuttingButtons.Controls.Add(this.btnResetCutting);
            this.flowCuttingButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowCuttingButtons.Location = new System.Drawing.Point(12, 12);
            this.flowCuttingButtons.Name = "flowCuttingButtons";
            this.flowCuttingButtons.Padding = new System.Windows.Forms.Padding(8);
            this.flowCuttingButtons.Size = new System.Drawing.Size(1542, 56);
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
          
            // 
            // tabBranchBound
            // 
            this.tabBranchBound.Controls.Add(this.panelBranch);
            this.tabBranchBound.Location = new System.Drawing.Point(10, 49);
            this.tabBranchBound.Name = "tabBranchBound";
            this.tabBranchBound.Size = new System.Drawing.Size(1566, 1053);
            this.tabBranchBound.TabIndex = 4;
            this.tabBranchBound.Text = "Branch & Bound";
            // 
            // panelBranch
            // 
            this.panelBranch.Controls.Add(this.lstBranchLog);
            this.panelBranch.Controls.Add(this.lblBranch);
            this.panelBranch.Controls.Add(this.flowBranchButtons);
            this.panelBranch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBranch.Location = new System.Drawing.Point(0, 0);
            this.panelBranch.Name = "panelBranch";
            this.panelBranch.Padding = new System.Windows.Forms.Padding(12);
            this.panelBranch.Size = new System.Drawing.Size(1566, 1053);
            this.panelBranch.TabIndex = 0;
            // 
            // lstBranchLog
            // 
            this.lstBranchLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this.lstBranchLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstBranchLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBranchLog.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lstBranchLog.ItemHeight = 37;
            this.lstBranchLog.Location = new System.Drawing.Point(12, 94);
            this.lstBranchLog.Name = "lstBranchLog";
            this.lstBranchLog.Size = new System.Drawing.Size(1542, 947);
            this.lstBranchLog.TabIndex = 0;
            // 
            // lblBranch
            // 
            this.lblBranch.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblBranch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(85)))), ((int)(((byte)(101)))));
            this.lblBranch.Location = new System.Drawing.Point(12, 68);
            this.lblBranch.Name = "lblBranch";
            this.lblBranch.Padding = new System.Windows.Forms.Padding(8, 6, 8, 0);
            this.lblBranch.Size = new System.Drawing.Size(1542, 26);
            this.lblBranch.TabIndex = 1;
            this.lblBranch.Text = "Control branching strategy and explore nodes (UI stub).";
            // 
            // flowBranchButtons
            // 
            this.flowBranchButtons.Controls.Add(this.btnRunBranch);
            this.flowBranchButtons.Controls.Add(this.btnStepBranch);
            this.flowBranchButtons.Controls.Add(this.btnResetBranch);
            this.flowBranchButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowBranchButtons.Location = new System.Drawing.Point(12, 12);
            this.flowBranchButtons.Name = "flowBranchButtons";
            this.flowBranchButtons.Padding = new System.Windows.Forms.Padding(8);
            this.flowBranchButtons.Size = new System.Drawing.Size(1542, 56);
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
 
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(36, 36);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 1142);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(2510, 48);
            this.statusStrip.TabIndex = 1;
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(89, 37);
            this.statusLabel.Text = "Ready";



            // Primal
            this.btnRunPrimal.Click += new System.EventHandler(this.RunPrimal_Click);
            this.btnStepPrimal.Click += new System.EventHandler(this.StepPrimal_Click);
            this.btnResetPrimal.Click += new System.EventHandler(this.AlgorithmReset_Click);

            // Revised
            this.btnRunRevised.Click += new System.EventHandler(this.RunRevised_Click);
            this.btnStepRevised.Click += new System.EventHandler(this.StepRevised_Click);
            this.btnResetRevised.Click += new System.EventHandler(this.AlgorithmReset_Click);

            // Sensitivity
            this.btnRunSensitivity.Click += new System.EventHandler(this.RunSensitivity_Click);
            this.btnStepSensitivity.Click += new System.EventHandler(this.StepSensitivity_Click);
            this.btnResetSensitivity.Click += new System.EventHandler(this.AlgorithmReset_Click);

            // Cutting
            this.btnRunCutting.Click += new System.EventHandler(this.RunCutting_Click);
            this.btnStepCutting.Click += new System.EventHandler(this.StepCutting_Click);
            this.btnResetCutting.Click += new System.EventHandler(this.AlgorithmReset_Click);

            // Branch & Bound
            this.btnRunBranch.Click += new System.EventHandler(this.RunBranch_Click);
            this.btnStepBranch.Click += new System.EventHandler(this.StepBranch_Click);
            this.btnResetBranch.Click += new System.EventHandler(this.AlgorithmReset_Click);

            // 
            // Form1
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(2510, 1190);
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
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Panel panelPrimal;
        private System.Windows.Forms.Label lblPrimal;
        private System.Windows.Forms.Panel panelRevised;
        private System.Windows.Forms.Label lblRevised;
        private System.Windows.Forms.Panel panelSensitivity;
        private System.Windows.Forms.Label lblSensitivity;
        private System.Windows.Forms.Panel panelCutting;
        private System.Windows.Forms.Label lblCutting;
        private System.Windows.Forms.Panel panelBranch;
        private System.Windows.Forms.Label lblBranch;
    }
}
