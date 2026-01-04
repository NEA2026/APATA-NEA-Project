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
        btnBack = new Button();
        chkGenerateMaze = new CheckBox();
        tbGenerationDelay = new TrackBar();
        lblGenerationDelayValue = new Label();
        btnReset = new Button();
        btnStep = new Button();
        cboPathfindingAlgorithm = new ComboBox();
        chkSolveShortestPath = new CheckBox();
        tbPathfindingDelay = new TrackBar();
        lblPathfindingDelayValue = new Label();
        ((System.ComponentModel.ISupportInitialize)tbGenerationDelay).BeginInit();
        ((System.ComponentModel.ISupportInitialize)tbPathfindingDelay).BeginInit();
        SuspendLayout();
        // 
        // btnBack
        // 
        btnBack.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnBack.Location = new Point(25, 680);
        btnBack.Name = "btnBack";
        btnBack.Size = new Size(128, 29);
        btnBack.TabIndex = 0;
        btnBack.Text = "Back";
        btnBack.UseVisualStyleBackColor = true;
        btnBack.Click += btnBack_Click;
        // 
        // chkGenerateMaze
        // 
        chkGenerateMaze.Appearance = Appearance.Button;
        chkGenerateMaze.AutoSize = true;
        chkGenerateMaze.Location = new Point(728, 41);
        chkGenerateMaze.Name = "chkGenerateMaze";
        chkGenerateMaze.Size = new Size(119, 30);
        chkGenerateMaze.TabIndex = 2;
        chkGenerateMaze.Text = "Generate Maze";
        chkGenerateMaze.UseVisualStyleBackColor = true;
        chkGenerateMaze.CheckedChanged += chkGenerateMaze_CheckedChanged;
        // 
        // tbGenerationDelay
        // 
        tbGenerationDelay.Location = new Point(853, 115);
        tbGenerationDelay.Maximum = 1000;
        tbGenerationDelay.Name = "tbGenerationDelay";
        tbGenerationDelay.Size = new Size(130, 56);
        tbGenerationDelay.TabIndex = 3;
        tbGenerationDelay.TickStyle = TickStyle.None;
        tbGenerationDelay.Scroll += tbGenerationDelay_Scroll;
        // 
        // lblGenerationDelayValue
        // 
        lblGenerationDelayValue.AutoSize = true;
        lblGenerationDelayValue.Location = new Point(853, 174);
        lblGenerationDelayValue.Name = "lblGenerationDelayValue";
        lblGenerationDelayValue.Size = new Size(40, 20);
        lblGenerationDelayValue.TabIndex = 4;
        lblGenerationDelayValue.Text = "0 ms";
        // 
        // btnReset
        // 
        btnReset.Enabled = false;
        btnReset.Location = new Point(853, 42);
        btnReset.Name = "btnReset";
        btnReset.Size = new Size(107, 29);
        btnReset.TabIndex = 5;
        btnReset.Text = "Reset";
        btnReset.UseVisualStyleBackColor = true;
        btnReset.Click += btnReset_Click;
        // 
        // btnStep
        // 
        btnStep.Location = new Point(728, 77);
        btnStep.Name = "btnStep";
        btnStep.Size = new Size(119, 32);
        btnStep.TabIndex = 6;
        btnStep.Text = "Step";
        btnStep.UseVisualStyleBackColor = true;
        btnStep.Click += btnStep_Click;
        // 
        // cboPathfindingAlgorithm
        // 
        cboPathfindingAlgorithm.DropDownStyle = ComboBoxStyle.DropDownList;
        cboPathfindingAlgorithm.FormattingEnabled = true;
        cboPathfindingAlgorithm.Items.AddRange(new object[] { "Dijkstra", "A*" });
        cboPathfindingAlgorithm.Location = new Point(728, 314);
        cboPathfindingAlgorithm.Name = "cboPathfindingAlgorithm";
        cboPathfindingAlgorithm.Size = new Size(187, 28);
        cboPathfindingAlgorithm.TabIndex = 8;
        // 
        // chkSolveShortestPath
        // 
        chkSolveShortestPath.Appearance = Appearance.Button;
        chkSolveShortestPath.AutoSize = true;
        chkSolveShortestPath.Enabled = false;
        chkSolveShortestPath.Location = new Point(728, 365);
        chkSolveShortestPath.Name = "chkSolveShortestPath";
        chkSolveShortestPath.Size = new Size(145, 30);
        chkSolveShortestPath.TabIndex = 9;
        chkSolveShortestPath.Text = "Solve Shortest Path";
        chkSolveShortestPath.UseVisualStyleBackColor = true;
        chkSolveShortestPath.CheckedChanged += chkSolveShortestPath_CheckedChanged;
        // 
        // tbPathfindingDelay
        // 
        tbPathfindingDelay.Location = new Point(853, 424);
        tbPathfindingDelay.Maximum = 1000;
        tbPathfindingDelay.Name = "tbPathfindingDelay";
        tbPathfindingDelay.Size = new Size(130, 56);
        tbPathfindingDelay.TabIndex = 10;
        tbPathfindingDelay.TickStyle = TickStyle.None;
        tbPathfindingDelay.Scroll += tbPathfindingDelay_Scroll;
        // 
        // lblPathfindingDelayValue
        // 
        lblPathfindingDelayValue.AutoSize = true;
        lblPathfindingDelayValue.Location = new Point(853, 483);
        lblPathfindingDelayValue.Name = "lblPathfindingDelayValue";
        lblPathfindingDelayValue.Size = new Size(40, 20);
        lblPathfindingDelayValue.TabIndex = 11;
        lblPathfindingDelayValue.Text = "0 ms";
        // 
        // MazeScreen
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        ClientSize = new Size(1006, 717);
        Controls.Add(lblPathfindingDelayValue);
        Controls.Add(tbPathfindingDelay);
        Controls.Add(chkSolveShortestPath);
        Controls.Add(cboPathfindingAlgorithm);
        Controls.Add(btnStep);
        Controls.Add(btnReset);
        Controls.Add(lblGenerationDelayValue);
        Controls.Add(tbGenerationDelay);
        Controls.Add(chkGenerateMaze);
        Controls.Add(btnBack);
        Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
        FormBorderStyle = FormBorderStyle.Fixed3D;
        Name = "MazeScreen";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "APATA";
        Paint += MazeScreen_Paint;
        ((System.ComponentModel.ISupportInitialize)tbGenerationDelay).EndInit();
        ((System.ComponentModel.ISupportInitialize)tbPathfindingDelay).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Button btnBack;
    private TrackBar tbGenerationDelay;
    private Label lblGenerationDelayValue;
    private CheckBox chkGenerateMaze;
    private Button btnReset;
    private Button btnStep;
    private ComboBox cboPathfindingAlgorithm;
    private CheckBox chkSolveShortestPath;
    private TrackBar tbPathfindingDelay;
    private Label lblPathfindingDelayValue;
}
