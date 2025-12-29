namespace APATA_NEA_Project.Forms;

partial class StartScreen
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        btnGenerateMaze = new Button();
        txtWidth = new TextBox();
        txtHeight = new TextBox();
        grpMazeSize = new GroupBox();
        lblMilliseconds = new Label();
        txtGenerationDelay = new TextBox();
        lblGenerationDelay = new Label();
        lblPercentage = new Label();
        lblRemoveDeadends = new Label();
        txtRemoveDeadends = new TextBox();
        lblCellsHeight = new Label();
        lblCellsWidth = new Label();
        lblHeight = new Label();
        lblWidth = new Label();
        lblTitle = new Label();
        grpMazeSize.SuspendLayout();
        SuspendLayout();
        // 
        // btnGenerateMaze
        // 
        btnGenerateMaze.Location = new Point(128, 401);
        btnGenerateMaze.Name = "btnGenerateMaze";
        btnGenerateMaze.Size = new Size(256, 32);
        btnGenerateMaze.TabIndex = 0;
        btnGenerateMaze.Text = "Generate Maze";
        btnGenerateMaze.UseVisualStyleBackColor = true;
        btnGenerateMaze.Click += btnGenerateMaze_Click;
        // 
        // txtWidth
        // 
        txtWidth.Location = new Point(63, 34);
        txtWidth.Name = "txtWidth";
        txtWidth.Size = new Size(64, 24);
        txtWidth.TabIndex = 1;
        txtWidth.TextAlign = HorizontalAlignment.Right;
        // 
        // txtHeight
        // 
        txtHeight.Location = new Point(63, 62);
        txtHeight.Name = "txtHeight";
        txtHeight.Size = new Size(64, 24);
        txtHeight.TabIndex = 2;
        txtHeight.TextAlign = HorizontalAlignment.Right;
        // 
        // grpMazeSize
        // 
        grpMazeSize.Controls.Add(lblMilliseconds);
        grpMazeSize.Controls.Add(txtGenerationDelay);
        grpMazeSize.Controls.Add(lblGenerationDelay);
        grpMazeSize.Controls.Add(lblPercentage);
        grpMazeSize.Controls.Add(lblRemoveDeadends);
        grpMazeSize.Controls.Add(txtRemoveDeadends);
        grpMazeSize.Controls.Add(lblCellsHeight);
        grpMazeSize.Controls.Add(lblCellsWidth);
        grpMazeSize.Controls.Add(txtWidth);
        grpMazeSize.Controls.Add(lblHeight);
        grpMazeSize.Controls.Add(txtHeight);
        grpMazeSize.Controls.Add(lblWidth);
        grpMazeSize.Font = new Font("Microsoft Sans Serif", 9F);
        grpMazeSize.Location = new Point(128, 235);
        grpMazeSize.Name = "grpMazeSize";
        grpMazeSize.Size = new Size(256, 160);
        grpMazeSize.TabIndex = 3;
        grpMazeSize.TabStop = false;
        grpMazeSize.Text = "Enter Maze Size:";
        // 
        // lblMilliseconds
        // 
        lblMilliseconds.AutoSize = true;
        lblMilliseconds.Location = new Point(204, 89);
        lblMilliseconds.Name = "lblMilliseconds";
        lblMilliseconds.Size = new Size(29, 18);
        lblMilliseconds.TabIndex = 5;
        lblMilliseconds.Text = "ms";
        // 
        // txtGenerationDelay
        // 
        txtGenerationDelay.Location = new Point(138, 86);
        txtGenerationDelay.Name = "txtGenerationDelay";
        txtGenerationDelay.Size = new Size(64, 24);
        txtGenerationDelay.TabIndex = 5;
        // 
        // lblGenerationDelay
        // 
        lblGenerationDelay.AutoSize = true;
        lblGenerationDelay.Location = new Point(6, 89);
        lblGenerationDelay.Name = "lblGenerationDelay";
        lblGenerationDelay.Size = new Size(126, 18);
        lblGenerationDelay.TabIndex = 5;
        lblGenerationDelay.Text = "Generation Delay:";
        // 
        // lblPercentage
        // 
        lblPercentage.Location = new Point(219, 117);
        lblPercentage.Name = "lblPercentage";
        lblPercentage.Size = new Size(21, 18);
        lblPercentage.TabIndex = 5;
        lblPercentage.Text = "%";
        // 
        // lblRemoveDeadends
        // 
        lblRemoveDeadends.Location = new Point(6, 117);
        lblRemoveDeadends.Name = "lblRemoveDeadends";
        lblRemoveDeadends.Size = new Size(139, 18);
        lblRemoveDeadends.TabIndex = 5;
        lblRemoveDeadends.Text = "Remove Deadends:";
        // 
        // txtRemoveDeadends
        // 
        txtRemoveDeadends.Location = new Point(151, 114);
        txtRemoveDeadends.Name = "txtRemoveDeadends";
        txtRemoveDeadends.Size = new Size(64, 24);
        txtRemoveDeadends.TabIndex = 5;
        txtRemoveDeadends.Text = "50";
        txtRemoveDeadends.TextAlign = HorizontalAlignment.Right;
        // 
        // lblCellsHeight
        // 
        lblCellsHeight.Location = new Point(129, 65);
        lblCellsHeight.Name = "lblCellsHeight";
        lblCellsHeight.Size = new Size(38, 18);
        lblCellsHeight.TabIndex = 6;
        lblCellsHeight.Text = "cells";
        // 
        // lblCellsWidth
        // 
        lblCellsWidth.Location = new Point(129, 37);
        lblCellsWidth.Name = "lblCellsWidth";
        lblCellsWidth.Size = new Size(38, 18);
        lblCellsWidth.TabIndex = 5;
        lblCellsWidth.Text = "cells";
        // 
        // lblHeight
        // 
        lblHeight.Location = new Point(6, 65);
        lblHeight.Name = "lblHeight";
        lblHeight.Size = new Size(54, 18);
        lblHeight.TabIndex = 4;
        lblHeight.Text = "Height:";
        // 
        // lblWidth
        // 
        lblWidth.Location = new Point(6, 37);
        lblWidth.Name = "lblWidth";
        lblWidth.Size = new Size(50, 18);
        lblWidth.TabIndex = 4;
        lblWidth.Text = "Width:";
        // 
        // lblTitle
        // 
        lblTitle.Font = new Font("Calibri", 31.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
        lblTitle.Location = new Point(175, 16);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(163, 66);
        lblTitle.TabIndex = 4;
        lblTitle.Text = "APATA";
        // 
        // StartScreen
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(255, 128, 0);
        ClientSize = new Size(490, 461);
        Controls.Add(lblTitle);
        Controls.Add(grpMazeSize);
        Controls.Add(btnGenerateMaze);
        FormBorderStyle = FormBorderStyle.Fixed3D;
        MaximizeBox = false;
        Name = "StartScreen";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "APATA";
        grpMazeSize.ResumeLayout(false);
        grpMazeSize.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private Button btnGenerateMaze;
    private TextBox txtWidth;
    private TextBox txtHeight;
    private GroupBox grpMazeSize;
    private Label lblWidth;
    private Label lblHeight;
    private Label lblTitle;
    private Label lblCellsHeight;
    private Label lblCellsWidth;
    private Label lblRemoveDeadends;
    private TextBox txtRemoveDeadends;
    private Label lblPercentage;
    private Label lblMilliseconds;
    private TextBox txtGenerationDelay;
    private Label lblGenerationDelay;
}