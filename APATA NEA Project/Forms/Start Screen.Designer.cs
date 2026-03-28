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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartScreen));
        btnOpenMazeScreen = new Button();
        txtWidth = new TextBox();
        txtHeight = new TextBox();
        grpMazeSize = new GroupBox();
        lblPercentage = new Label();
        lblRemoveDeadends = new Label();
        lblHeight = new Label();
        lblCellsHeight = new Label();
        lblWidth = new Label();
        lblCellsWidth = new Label();
        txtRemoveDeadends = new TextBox();
        lblTitle = new Label();
        picMazeLogo = new PictureBox();
        lblAPATAMeaning = new Label();
        grpMazeSize.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)picMazeLogo).BeginInit();
        SuspendLayout();
        // 
        // btnOpenMazeScreen
        // 
        btnOpenMazeScreen.Location = new Point(128, 417);
        btnOpenMazeScreen.Name = "btnOpenMazeScreen";
        btnOpenMazeScreen.Size = new Size(256, 32);
        btnOpenMazeScreen.TabIndex = 12;
        btnOpenMazeScreen.Text = "Open Maze Screen";
        btnOpenMazeScreen.UseVisualStyleBackColor = true;
        btnOpenMazeScreen.Click += btnOpenMazeScreen_Click;
        // 
        // txtWidth
        // 
        txtWidth.Location = new Point(66, 20);
        txtWidth.Name = "txtWidth";
        txtWidth.Size = new Size(64, 24);
        txtWidth.TabIndex = 4;
        txtWidth.Text = "16";
        txtWidth.TextAlign = HorizontalAlignment.Right;
        // 
        // txtHeight
        // 
        txtHeight.Location = new Point(66, 48);
        txtHeight.Name = "txtHeight";
        txtHeight.Size = new Size(64, 24);
        txtHeight.TabIndex = 7;
        txtHeight.Text = "16";
        txtHeight.TextAlign = HorizontalAlignment.Right;
        // 
        // grpMazeSize
        // 
        grpMazeSize.Controls.Add(lblPercentage);
        grpMazeSize.Controls.Add(lblRemoveDeadends);
        grpMazeSize.Controls.Add(lblHeight);
        grpMazeSize.Controls.Add(lblCellsHeight);
        grpMazeSize.Controls.Add(lblWidth);
        grpMazeSize.Controls.Add(lblCellsWidth);
        grpMazeSize.Controls.Add(txtRemoveDeadends);
        grpMazeSize.Controls.Add(txtWidth);
        grpMazeSize.Controls.Add(txtHeight);
        grpMazeSize.Font = new Font("Microsoft Sans Serif", 9F);
        grpMazeSize.Location = new Point(128, 299);
        grpMazeSize.Name = "grpMazeSize";
        grpMazeSize.Size = new Size(256, 112);
        grpMazeSize.TabIndex = 2;
        grpMazeSize.TabStop = false;
        grpMazeSize.Text = "Enter Maze Configurations:";
        // 
        // lblPercentage
        // 
        lblPercentage.Location = new Point(216, 75);
        lblPercentage.Name = "lblPercentage";
        lblPercentage.Size = new Size(21, 27);
        lblPercentage.TabIndex = 11;
        lblPercentage.Text = "%";
        lblPercentage.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblRemoveDeadends
        // 
        lblRemoveDeadends.Location = new Point(6, 75);
        lblRemoveDeadends.Name = "lblRemoveDeadends";
        lblRemoveDeadends.Size = new Size(139, 27);
        lblRemoveDeadends.TabIndex = 9;
        lblRemoveDeadends.Text = "Remove Deadends:";
        lblRemoveDeadends.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblHeight
        // 
        lblHeight.Location = new Point(6, 48);
        lblHeight.Name = "lblHeight";
        lblHeight.Size = new Size(54, 27);
        lblHeight.TabIndex = 6;
        lblHeight.Text = "Height:";
        lblHeight.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblCellsHeight
        // 
        lblCellsHeight.Location = new Point(131, 48);
        lblCellsHeight.Name = "lblCellsHeight";
        lblCellsHeight.Size = new Size(38, 27);
        lblCellsHeight.TabIndex = 8;
        lblCellsHeight.Text = "cells";
        lblCellsHeight.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblWidth
        // 
        lblWidth.Location = new Point(6, 20);
        lblWidth.Name = "lblWidth";
        lblWidth.Size = new Size(54, 27);
        lblWidth.TabIndex = 3;
        lblWidth.Text = "Width:";
        lblWidth.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblCellsWidth
        // 
        lblCellsWidth.Location = new Point(131, 19);
        lblCellsWidth.Name = "lblCellsWidth";
        lblCellsWidth.Size = new Size(38, 27);
        lblCellsWidth.TabIndex = 5;
        lblCellsWidth.Text = "cells";
        lblCellsWidth.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // txtRemoveDeadends
        // 
        txtRemoveDeadends.Location = new Point(151, 78);
        txtRemoveDeadends.Name = "txtRemoveDeadends";
        txtRemoveDeadends.Size = new Size(64, 24);
        txtRemoveDeadends.TabIndex = 10;
        txtRemoveDeadends.Text = "50";
        txtRemoveDeadends.TextAlign = HorizontalAlignment.Right;
        // 
        // lblTitle
        // 
        lblTitle.Font = new Font("Calibri", 31.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
        lblTitle.Location = new Point(174, 9);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(164, 66);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "APATA";
        lblTitle.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // picMazeLogo
        // 
        picMazeLogo.BackgroundImageLayout = ImageLayout.None;
        picMazeLogo.Image = (Image)resources.GetObject("picMazeLogo.Image");
        picMazeLogo.Location = new Point(128, 120);
        picMazeLogo.Name = "picMazeLogo";
        picMazeLogo.Size = new Size(256, 173);
        picMazeLogo.SizeMode = PictureBoxSizeMode.Zoom;
        picMazeLogo.TabIndex = 0;
        picMazeLogo.TabStop = false;
        picMazeLogo.WaitOnLoad = true;
        // 
        // lblAPATAMeaning
        // 
        lblAPATAMeaning.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
        lblAPATAMeaning.Location = new Point(76, 75);
        lblAPATAMeaning.Name = "lblAPATAMeaning";
        lblAPATAMeaning.Size = new Size(360, 28);
        lblAPATAMeaning.TabIndex = 13;
        lblAPATAMeaning.Text = "(A Pathfinding Algorithms Teaching Aid)";
        lblAPATAMeaning.TextAlign = ContentAlignment.MiddleCenter;
        lblAPATAMeaning.UseWaitCursor = true;
        // 
        // StartScreen
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.LightGreen;
        ClientSize = new Size(490, 461);
        Controls.Add(lblAPATAMeaning);
        Controls.Add(picMazeLogo);
        Controls.Add(lblTitle);
        Controls.Add(grpMazeSize);
        Controls.Add(btnOpenMazeScreen);
        FormBorderStyle = FormBorderStyle.Fixed3D;
        Icon = (Icon)resources.GetObject("$this.Icon");
        MaximizeBox = false;
        Name = "StartScreen";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "APATA";
        grpMazeSize.ResumeLayout(false);
        grpMazeSize.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)picMazeLogo).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Button btnOpenMazeScreen;
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
    private PictureBox picMazeLogo;
    private Label lblAPATAMeaning;
}