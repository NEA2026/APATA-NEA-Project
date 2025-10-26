namespace APATA_NEA_Project.Forms
{
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
            lblHCells = new Label();
            lblWCells = new Label();
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
            txtWidth.Location = new Point(62, 34);
            txtWidth.Name = "txtWidth";
            txtWidth.Size = new Size(64, 24);
            txtWidth.TabIndex = 1;
            // 
            // txtHeight
            // 
            txtHeight.Location = new Point(62, 70);
            txtHeight.Name = "txtHeight";
            txtHeight.Size = new Size(64, 24);
            txtHeight.TabIndex = 2;
            // 
            // grpMazeSize
            // 
            grpMazeSize.Controls.Add(lblHCells);
            grpMazeSize.Controls.Add(lblWCells);
            grpMazeSize.Controls.Add(txtWidth);
            grpMazeSize.Controls.Add(lblHeight);
            grpMazeSize.Controls.Add(txtHeight);
            grpMazeSize.Controls.Add(lblWidth);
            grpMazeSize.Font = new Font("Microsoft Sans Serif", 9F);
            grpMazeSize.Location = new Point(128, 256);
            grpMazeSize.Name = "grpMazeSize";
            grpMazeSize.Size = new Size(256, 128);
            grpMazeSize.TabIndex = 3;
            grpMazeSize.TabStop = false;
            grpMazeSize.Text = "Enter Maze Size:";
            // 
            // lblHCells
            // 
            lblHCells.AutoSize = true;
            lblHCells.Location = new Point(126, 73);
            lblHCells.Name = "lblHCells";
            lblHCells.Size = new Size(38, 18);
            lblHCells.TabIndex = 6;
            lblHCells.Text = "cells";
            // 
            // lblWCells
            // 
            lblWCells.AutoSize = true;
            lblWCells.Location = new Point(126, 37);
            lblWCells.Name = "lblWCells";
            lblWCells.Size = new Size(38, 18);
            lblWCells.TabIndex = 5;
            lblWCells.Text = "cells";
            // 
            // lblHeight
            // 
            lblHeight.AutoSize = true;
            lblHeight.Location = new Point(6, 73);
            lblHeight.Name = "lblHeight";
            lblHeight.Size = new Size(54, 18);
            lblHeight.TabIndex = 4;
            lblHeight.Text = "Height:";
            // 
            // lblWidth
            // 
            lblWidth.AutoSize = true;
            lblWidth.Location = new Point(6, 37);
            lblWidth.Name = "lblWidth";
            lblWidth.Size = new Size(50, 18);
            lblWidth.TabIndex = 4;
            lblWidth.Text = "Width:";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
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
            Name = "StartScreen";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "APATA";
            grpMazeSize.ResumeLayout(false);
            grpMazeSize.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnGenerateMaze;
        private TextBox txtWidth;
        private TextBox txtHeight;
        private GroupBox grpMazeSize;
        private Label lblWidth;
        private Label lblHeight;
        private Label lblTitle;
        private Label lblHCells;
        private Label lblWCells;
    }
}