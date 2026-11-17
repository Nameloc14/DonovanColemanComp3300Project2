namespace DonovanColemanProject2.Views
{
    partial class RoundSummaryForm
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
            headingLabel = new Label();
            detailsLabel = new Label();
            wordsListView = new ListView();
            wordColumnHeader = new ColumnHeader();
            pointsColumnHeader = new ColumnHeader();
            timeColumnHeader = new ColumnHeader();
            wordCountLabel = new Label();
            totalScoreLabel = new Label();
            okButton = new Button();
            SuspendLayout();
            //
            // headingLabel
            //
            headingLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            headingLabel.Location = new Point(20, 14);
            headingLabel.Name = "headingLabel";
            headingLabel.Size = new Size(400, 30);
            headingLabel.TabIndex = 0;
            headingLabel.Text = "Round Results";
            headingLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // detailsLabel
            //
            detailsLabel.Location = new Point(22, 48);
            detailsLabel.Name = "detailsLabel";
            detailsLabel.Size = new Size(398, 20);
            detailsLabel.TabIndex = 1;
            detailsLabel.Text = "Letters:";
            detailsLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // wordsListView
            //
            wordsListView.Columns.AddRange(new ColumnHeader[] { wordColumnHeader, pointsColumnHeader, timeColumnHeader });
            wordsListView.FullRowSelect = true;
            wordsListView.GridLines = true;
            wordsListView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            wordsListView.Location = new Point(20, 76);
            wordsListView.MultiSelect = false;
            wordsListView.Name = "wordsListView";
            wordsListView.Size = new Size(400, 220);
            wordsListView.TabIndex = 2;
            wordsListView.UseCompatibleStateImageBehavior = false;
            wordsListView.View = View.Details;
            //
            // wordColumnHeader
            //
            wordColumnHeader.Text = "Word";
            wordColumnHeader.Width = 170;
            //
            // pointsColumnHeader
            //
            pointsColumnHeader.Text = "Points";
            pointsColumnHeader.Width = 100;
            //
            // timeColumnHeader
            //
            timeColumnHeader.Text = "Time Left";
            timeColumnHeader.Width = 100;
            //
            // wordCountLabel
            //
            wordCountLabel.Location = new Point(22, 304);
            wordCountLabel.Name = "wordCountLabel";
            wordCountLabel.Size = new Size(398, 20);
            wordCountLabel.TabIndex = 3;
            wordCountLabel.Text = "Valid words entered: 0";
            wordCountLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // totalScoreLabel
            //
            totalScoreLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            totalScoreLabel.Location = new Point(20, 328);
            totalScoreLabel.Name = "totalScoreLabel";
            totalScoreLabel.Size = new Size(280, 34);
            totalScoreLabel.TabIndex = 4;
            totalScoreLabel.Text = "Round score: 0";
            totalScoreLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // okButton
            //
            okButton.Location = new Point(320, 330);
            okButton.Name = "okButton";
            okButton.Size = new Size(100, 32);
            okButton.TabIndex = 5;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            //
            // RoundSummaryForm
            //
            AcceptButton = okButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = okButton;
            ClientSize = new Size(440, 378);
            Controls.Add(okButton);
            Controls.Add(totalScoreLabel);
            Controls.Add(wordCountLabel);
            Controls.Add(wordsListView);
            Controls.Add(detailsLabel);
            Controls.Add(headingLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RoundSummaryForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Round Over";
            ResumeLayout(false);
        }

        #endregion

        private Label headingLabel;
        private Label detailsLabel;
        private ListView wordsListView;
        private ColumnHeader wordColumnHeader;
        private ColumnHeader pointsColumnHeader;
        private ColumnHeader timeColumnHeader;
        private Label wordCountLabel;
        private Label totalScoreLabel;
        private Button okButton;
    }
}