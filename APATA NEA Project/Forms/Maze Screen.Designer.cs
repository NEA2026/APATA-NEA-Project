namespace APATA_NEA_Project.Forms;

partial class MazeScreen
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MazeScreen));
        btnBack = new Button();
        chkGenerateMaze = new CheckBox();
        tbGenerationDelay = new TrackBar();
        lblGenerationDelayValue = new Label();
        btnResetGeneration = new Button();
        btnStepGeneration = new Button();
        cboPathfindingAlgorithm = new ComboBox();
        chkSolveShortestPath = new CheckBox();
        tbPathfindingDelay = new TrackBar();
        lblPathfindingDelayValue = new Label();
        btnStepPathfinding = new Button();
        btnResetPathfinding = new Button();
        grpGenerationControls = new GroupBox();
        grpPathfindingControls = new GroupBox();
        grpControlPanel = new GroupBox();
        ((System.ComponentModel.ISupportInitialize)tbGenerationDelay).BeginInit();
        ((System.ComponentModel.ISupportInitialize)tbPathfindingDelay).BeginInit();
        grpGenerationControls.SuspendLayout();
        grpPathfindingControls.SuspendLayout();
        grpControlPanel.SuspendLayout();
        SuspendLayout();
        // 
        // btnBack
        // 
        btnBack.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnBack.Location = new Point(12, 675);
        btnBack.Name = "btnBack";
        btnBack.Size = new Size(128, 30);
        btnBack.TabIndex = 0;
        btnBack.Text = "Back";
        btnBack.UseVisualStyleBackColor = true;
        btnBack.Click += btnBack_Click;
        // 
        // chkGenerateMaze
        // 
        chkGenerateMaze.Appearance = Appearance.Button;
        chkGenerateMaze.Location = new Point(6, 26);
        chkGenerateMaze.Name = "chkGenerateMaze";
        chkGenerateMaze.Size = new Size(145, 30);
        chkGenerateMaze.TabIndex = 1;
        chkGenerateMaze.Text = "Generate Maze";
        chkGenerateMaze.TextAlign = ContentAlignment.MiddleCenter;
        chkGenerateMaze.UseVisualStyleBackColor = true;
        chkGenerateMaze.CheckedChanged += chkGenerateMaze_CheckedChanged;
        // 
        // tbGenerationDelay
        // 
        tbGenerationDelay.Location = new Point(157, 72);
        tbGenerationDelay.Maximum = 1000;
        tbGenerationDelay.Name = "tbGenerationDelay";
        tbGenerationDelay.Size = new Size(145, 56);
        tbGenerationDelay.TabIndex = 5;
        tbGenerationDelay.TickStyle = TickStyle.None;
        tbGenerationDelay.Scroll += tbGenerationDelay_Scroll;
        // 
        // lblGenerationDelayValue
        // 
        lblGenerationDelayValue.AutoSize = true;
        lblGenerationDelayValue.Location = new Point(157, 108);
        lblGenerationDelayValue.Name = "lblGenerationDelayValue";
        lblGenerationDelayValue.Size = new Size(40, 20);
        lblGenerationDelayValue.TabIndex = 4;
        lblGenerationDelayValue.Text = "0 ms";
        // 
        // btnResetGeneration
        // 
        btnResetGeneration.Enabled = false;
        btnResetGeneration.Location = new Point(157, 26);
        btnResetGeneration.Name = "btnResetGeneration";
        btnResetGeneration.Size = new Size(145, 30);
        btnResetGeneration.TabIndex = 2;
        btnResetGeneration.Text = "Reset";
        btnResetGeneration.UseVisualStyleBackColor = true;
        btnResetGeneration.Click += btnResetGeneration_Click;
        // 
        // btnStepGeneration
        // 
        btnStepGeneration.Location = new Point(6, 62);
        btnStepGeneration.Name = "btnStepGeneration";
        btnStepGeneration.Size = new Size(145, 30);
        btnStepGeneration.TabIndex = 3;
        btnStepGeneration.Text = "Step";
        btnStepGeneration.UseVisualStyleBackColor = true;
        btnStepGeneration.Click += btnStepGeneration_Click;
        // 
        // cboPathfindingAlgorithm
        // 
        cboPathfindingAlgorithm.DropDownStyle = ComboBoxStyle.DropDownList;
        cboPathfindingAlgorithm.Enabled = false;
        cboPathfindingAlgorithm.FormattingEnabled = true;
        cboPathfindingAlgorithm.Items.AddRange(new object[] { "Dijkstra", "A*" });
        cboPathfindingAlgorithm.Location = new Point(6, 26);
        cboPathfindingAlgorithm.Name = "cboPathfindingAlgorithm";
        cboPathfindingAlgorithm.Size = new Size(145, 28);
        cboPathfindingAlgorithm.TabIndex = 6;
        // 
        // chkSolveShortestPath
        // 
        chkSolveShortestPath.Appearance = Appearance.Button;
        chkSolveShortestPath.AutoSize = true;
        chkSolveShortestPath.Enabled = false;
        chkSolveShortestPath.Location = new Point(6, 60);
        chkSolveShortestPath.Name = "chkSolveShortestPath";
        chkSolveShortestPath.Size = new Size(145, 30);
        chkSolveShortestPath.TabIndex = 7;
        chkSolveShortestPath.Text = "Solve Shortest Path";
        chkSolveShortestPath.UseVisualStyleBackColor = true;
        chkSolveShortestPath.CheckedChanged += chkSolveShortestPath_CheckedChanged;
        // 
        // tbPathfindingDelay
        // 
        tbPathfindingDelay.Location = new Point(157, 70);
        tbPathfindingDelay.Maximum = 1000;
        tbPathfindingDelay.Name = "tbPathfindingDelay";
        tbPathfindingDelay.Size = new Size(145, 56);
        tbPathfindingDelay.TabIndex = 11;
        tbPathfindingDelay.TickStyle = TickStyle.None;
        tbPathfindingDelay.Scroll += tbPathfindingDelay_Scroll;
        // 
        // lblPathfindingDelayValue
        // 
        lblPathfindingDelayValue.AutoSize = true;
        lblPathfindingDelayValue.Location = new Point(157, 106);
        lblPathfindingDelayValue.Name = "lblPathfindingDelayValue";
        lblPathfindingDelayValue.Size = new Size(40, 20);
        lblPathfindingDelayValue.TabIndex = 10;
        lblPathfindingDelayValue.Text = "0 ms";
        // 
        // btnStepPathfinding
        // 
        btnStepPathfinding.Enabled = false;
        btnStepPathfinding.Location = new Point(6, 96);
        btnStepPathfinding.Name = "btnStepPathfinding";
        btnStepPathfinding.Size = new Size(145, 30);
        btnStepPathfinding.TabIndex = 9;
        btnStepPathfinding.Text = "Step";
        btnStepPathfinding.UseVisualStyleBackColor = true;
        btnStepPathfinding.Click += btnStepPathfinding_Click;
        // 
        // btnResetPathfinding
        // 
        btnResetPathfinding.Enabled = false;
        btnResetPathfinding.Location = new Point(157, 26);
        btnResetPathfinding.Name = "btnResetPathfinding";
        btnResetPathfinding.Size = new Size(145, 30);
        btnResetPathfinding.TabIndex = 8;
        btnResetPathfinding.Text = "Reset";
        btnResetPathfinding.UseVisualStyleBackColor = true;
        btnResetPathfinding.Click += btnResetPathfinding_Click;
        // 
        // grpGenerationControls
        // 
        grpGenerationControls.Controls.Add(lblGenerationDelayValue);
        grpGenerationControls.Controls.Add(btnStepGeneration);
        grpGenerationControls.Controls.Add(btnResetGeneration);
        grpGenerationControls.Controls.Add(tbGenerationDelay);
        grpGenerationControls.Controls.Add(chkGenerateMaze);
        grpGenerationControls.Location = new Point(6, 26);
        grpGenerationControls.Name = "grpGenerationControls";
        grpGenerationControls.Size = new Size(308, 144);
        grpGenerationControls.TabIndex = 12;
        grpGenerationControls.TabStop = false;
        grpGenerationControls.Text = "Generation Controls";
        // 
        // grpPathfindingControls
        // 
        grpPathfindingControls.Controls.Add(lblPathfindingDelayValue);
        grpPathfindingControls.Controls.Add(cboPathfindingAlgorithm);
        grpPathfindingControls.Controls.Add(chkSolveShortestPath);
        grpPathfindingControls.Controls.Add(btnResetPathfinding);
        grpPathfindingControls.Controls.Add(tbPathfindingDelay);
        grpPathfindingControls.Controls.Add(btnStepPathfinding);
        grpPathfindingControls.Location = new Point(6, 212);
        grpPathfindingControls.Name = "grpPathfindingControls";
        grpPathfindingControls.Size = new Size(308, 144);
        grpPathfindingControls.TabIndex = 13;
        grpPathfindingControls.TabStop = false;
        grpPathfindingControls.Text = "Pathfinding Controls";
        // 
        // grpControlPanel
        // 
        grpControlPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        grpControlPanel.Controls.Add(grpGenerationControls);
        grpControlPanel.Controls.Add(grpPathfindingControls);
        grpControlPanel.Location = new Point(674, 12);
        grpControlPanel.Name = "grpControlPanel";
        grpControlPanel.Size = new Size(320, 362);
        grpControlPanel.TabIndex = 14;
        grpControlPanel.TabStop = false;
        grpControlPanel.Text = "Control Panel";
        // 
        // MazeScreen
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        ClientSize = new Size(1006, 717);
        Controls.Add(grpControlPanel);
        Controls.Add(btnBack);
        Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
        FormBorderStyle = FormBorderStyle.Fixed3D;
        Icon = (Icon)resources.GetObject("$this.Icon");
        MaximizeBox = false;
        Name = "MazeScreen";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "APATA";
        Paint += MazeScreen_Paint;
        ((System.ComponentModel.ISupportInitialize)tbGenerationDelay).EndInit();
        ((System.ComponentModel.ISupportInitialize)tbPathfindingDelay).EndInit();
        grpGenerationControls.ResumeLayout(false);
        grpGenerationControls.PerformLayout();
        grpPathfindingControls.ResumeLayout(false);
        grpPathfindingControls.PerformLayout();
        grpControlPanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private Button btnBack;
    private TrackBar tbGenerationDelay;
    private Label lblGenerationDelayValue;
    private CheckBox chkGenerateMaze;
    private Button btnResetGeneration;
    private Button btnStepGeneration;
    private ComboBox cboPathfindingAlgorithm;
    private CheckBox chkSolveShortestPath;
    private TrackBar tbPathfindingDelay;
    private Label lblPathfindingDelayValue;
    private Button btnStepPathfinding;
    private Button btnResetPathfinding;
    private GroupBox grpGenerationControls;
    private GroupBox grpPathfindingControls;
    private GroupBox grpControlPanel;
}
