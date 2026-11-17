namespace DonovanColemanProject2.Views
{
    partial class HighScoreForm
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
            sortGroupBox = new GroupBox();
            sortByTimeRadioButton = new RadioButton();
            sortByScoreRadioButton = new RadioButton();
            scoresListView = new ListView();
            positionColumnHeader = new ColumnHeader();
            nameColumnHeader = new ColumnHeader();
            scoreColumnHeader = new ColumnHeader();
            timeColumnHeader = new ColumnHeader();
            emptyBoardLabel = new Label();
            resetButton = new Button();
            closeButton = new Button();
            sortGroupBox.SuspendLayout();
            SuspendLayout();
            //
            // sortGroupBox
            //
            sortGroupBox.Controls.Add(sortByTimeRadioButton);
            sortGroupBox.Controls.Add(sortByScoreRadioButton);
            sortGroupBox.Location = new Point(20, 12);
            sortGroupBox.Name = "sortGroupBox";
            sortGroupBox.Size = new Size(440, 56);
            sortGroupBox.TabIndex = 0;
            sortGroupBox.TabStop = false;
            sortGroupBox.Text = "Sort by";
            //
            // sortByTimeRadioButton
            //
            sortByTimeRadioButton.AutoSize = true;
            sortByTimeRadioButton.Location = new Point(190, 24);
            sortByTimeRadioButton.Name = "sortByTimeRadioButton";
            sortByTimeRadioButton.Size = new Size(216, 19);
            sortByTimeRadioButton.TabIndex = 1;
            sortByTimeRadioButton.Text = "&Time (shortest first), then score";
            sortByTimeRadioButton.UseVisualStyleBackColor = true;
            sortByTimeRadioButton.CheckedChanged += sortRadioButton_CheckedChanged;
            //
            // sortByScoreRadioButton
            //
            sortByScoreRadioButton.AutoSize = true;
            sortByScoreRadioButton.Checked = true;
            sortByScoreRadioButton.Location = new Point(16, 24);
            sortByScoreRadioButton.Name = "sortByScoreRadioButton";
            sortByScoreRadioButton.Size = new Size(147, 19);
            sortByScoreRadioButton.TabIndex = 0;
            sortByScoreRadioButton.TabStop = true;
            sortByScoreRadioButton.Text = "&Score (highest first)";
            sortByScoreRadioButton.UseVisualStyleBackColor = true;
            sortByScoreRadioButton.CheckedChanged += sortRadioButton_CheckedChanged;
            //
            // scoresListView
            //
            scoresListView.Columns.AddRange(new ColumnHeader[] { positionColumnHeader, nameColumnHeader, scoreColumnHeader, timeColumnHeader });
            scoresListView.FullRowSelect = true;
            scoresListView.GridLines = true;
            scoresListView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            scoresListView.Location = new Point(20, 80);
            scoresListView.MultiSelect = false;
            scoresListView.Name = "scoresListView";
            scoresListView.Size = new Size(440, 236);
            scoresListView.TabIndex = 1;
            scoresListView.UseCompatibleStateImageBehavior = false;
            scoresListView.View = View.Details;
            //
            // positionColumnHeader
            //
            positionColumnHeader.Text = "#";
            positionColumnHeader.Width = 40;
            //
            // nameColumnHeader
            //
            nameColumnHeader.Text = "Name";
            nameColumnHeader.Width = 180;
            //
            // scoreColumnHeader
            //
            scoreColumnHeader.Text = "Score";
            scoreColumnHeader.Width = 90;
            //
            // timeColumnHeader
            //
            timeColumnHeader.Text = "Round Time";
            timeColumnHeader.Width = 105;
            //
            // emptyBoardLabel
            //
            emptyBoardLabel.ForeColor = SystemColors.GrayText;
            emptyBoardLabel.Location = new Point(20, 322);
            emptyBoardLabel.Name = "emptyBoardLabel";
            emptyBoardLabel.Size = new Size(440, 20);
            emptyBoardLabel.TabIndex = 2;
            emptyBoardLabel.Text = "No high scores yet. Finish a round with points to get on the board.";
            emptyBoardLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // resetButton
            //
            resetButton.Location = new Point(20, 352);
            resetButton.Name = "resetButton";
            resetButton.Size = new Size(150, 32);
            resetButton.TabIndex = 3;
            resetButton.Text = "&Reset High Scores";
            resetButton.UseVisualStyleBackColor = true;
            resetButton.Click += resetButton_Click;
            //
            // closeButton
            //
            closeButton.Location = new Point(360, 352);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(100, 32);
            closeButton.TabIndex = 4;
            closeButton.Text = "Close";
            closeButton.UseVisualStyleBackColor = true;
            closeButton.Click += closeButton_Click;
            //
            // HighScoreForm
            //
            AcceptButton = closeButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = closeButton;
            ClientSize = new Size(480, 400);
            Controls.Add(closeButton);
            Controls.Add(resetButton);
            Controls.Add(emptyBoardLabel);
            Controls.Add(scoresListView);
            Controls.Add(sortGroupBox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "HighScoreForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "High Scores";
            sortGroupBox.ResumeLayout(false);
            sortGroupBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox sortGroupBox;
        private RadioButton sortByTimeRadioButton;
        private RadioButton sortByScoreRadioButton;
        private ListView scoresListView;
        private ColumnHeader positionColumnHeader;
        private ColumnHeader nameColumnHeader;
        private ColumnHeader scoreColumnHeader;
        private ColumnHeader timeColumnHeader;
        private Label emptyBoardLabel;
        private Button resetButton;
        private Button closeButton;
    }
}