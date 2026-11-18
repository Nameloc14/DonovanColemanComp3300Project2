namespace DonovanColemanProject2.Views
{
    partial class MainForm
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
            menuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newGameToolStripMenuItem = new ToolStripMenuItem();
            fileToolStripSeparator1 = new ToolStripSeparator();
            exportStatsToolStripMenuItem = new ToolStripMenuItem();
            fileToolStripSeparator2 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            timerToolStripMenuItem = new ToolStripMenuItem();
            oneMinuteToolStripMenuItem = new ToolStripMenuItem();
            twoMinutesToolStripMenuItem = new ToolStripMenuItem();
            threeMinutesToolStripMenuItem = new ToolStripMenuItem();
            highScoresToolStripMenuItem = new ToolStripMenuItem();
            viewHighScoresToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            howToPlayToolStripMenuItem = new ToolStripMenuItem();
            timeCaptionLabel = new Label();
            timeRemainingLabel = new Label();
            scoreCaptionLabel = new Label();
            scoreLabel = new Label();
            roundCaptionLabel = new Label();
            roundLabel = new Label();
            currentWordLabel = new Label();
            letterButton1 = new Button();
            letterButton2 = new Button();
            letterButton3 = new Button();
            letterButton4 = new Button();
            letterButton5 = new Button();
            letterButton6 = new Button();
            letterButton7 = new Button();
            twistButton = new Button();
            undoLetterButton = new Button();
            clearButton = new Button();
            submitButton = new Button();
            feedbackLabel = new Label();
            wordsCaptionLabel = new Label();
            wordsListView = new ListView();
            wordColumnHeader = new ColumnHeader();
            pointsColumnHeader = new ColumnHeader();
            timeColumnHeader = new ColumnHeader();
            newGameButton = new Button();
            keyboardHintLabel = new Label();
            menuStrip.SuspendLayout();
            SuspendLayout();
            //
            // menuStrip
            //
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, optionsToolStripMenuItem, highScoresToolStripMenuItem, helpToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(600, 24);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip";
            //
            // fileToolStripMenuItem
            //
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newGameToolStripMenuItem, fileToolStripSeparator1, exportStatsToolStripMenuItem, fileToolStripSeparator2, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "&File";
            //
            // newGameToolStripMenuItem
            //
            newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            newGameToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            newGameToolStripMenuItem.Size = new Size(244, 22);
            newGameToolStripMenuItem.Text = "&New Game";
            newGameToolStripMenuItem.Click += newGameToolStripMenuItem_Click;
            //
            // fileToolStripSeparator1
            //
            fileToolStripSeparator1.Name = "fileToolStripSeparator1";
            fileToolStripSeparator1.Size = new Size(241, 6);
            //
            // exportStatsToolStripMenuItem
            //
            exportStatsToolStripMenuItem.Enabled = false;
            exportStatsToolStripMenuItem.Name = "exportStatsToolStripMenuItem";
            exportStatsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.E;
            exportStatsToolStripMenuItem.Size = new Size(244, 22);
            exportStatsToolStripMenuItem.Text = "&Export Stats...";
            exportStatsToolStripMenuItem.Click += exportStatsToolStripMenuItem_Click;
            //
            // fileToolStripSeparator2
            //
            fileToolStripSeparator2.Name = "fileToolStripSeparator2";
            fileToolStripSeparator2.Size = new Size(241, 6);
            //
            // exitToolStripMenuItem
            //
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(244, 22);
            exitToolStripMenuItem.Text = "E&xit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            //
            // optionsToolStripMenuItem
            //
            optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { timerToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(61, 20);
            optionsToolStripMenuItem.Text = "&Options";
            //
            // timerToolStripMenuItem
            //
            timerToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { oneMinuteToolStripMenuItem, twoMinutesToolStripMenuItem, threeMinutesToolStripMenuItem });
            timerToolStripMenuItem.Name = "timerToolStripMenuItem";
            timerToolStripMenuItem.Size = new Size(104, 22);
            timerToolStripMenuItem.Text = "&Timer";
            //
            // oneMinuteToolStripMenuItem
            //
            oneMinuteToolStripMenuItem.Checked = true;
            oneMinuteToolStripMenuItem.CheckState = CheckState.Checked;
            oneMinuteToolStripMenuItem.Name = "oneMinuteToolStripMenuItem";
            oneMinuteToolStripMenuItem.Size = new Size(205, 22);
            oneMinuteToolStripMenuItem.Text = "&1 Minute (60 seconds)";
            oneMinuteToolStripMenuItem.Click += timerLengthToolStripMenuItem_Click;
            //
            // twoMinutesToolStripMenuItem
            //
            twoMinutesToolStripMenuItem.Name = "twoMinutesToolStripMenuItem";
            twoMinutesToolStripMenuItem.Size = new Size(205, 22);
            twoMinutesToolStripMenuItem.Text = "&2 Minutes (120 seconds)";
            twoMinutesToolStripMenuItem.Click += timerLengthToolStripMenuItem_Click;
            //
            // threeMinutesToolStripMenuItem
            //
            threeMinutesToolStripMenuItem.Name = "threeMinutesToolStripMenuItem";
            threeMinutesToolStripMenuItem.Size = new Size(205, 22);
            threeMinutesToolStripMenuItem.Text = "&3 Minutes (180 seconds)";
            threeMinutesToolStripMenuItem.Click += timerLengthToolStripMenuItem_Click;
            //
            // highScoresToolStripMenuItem
            //
            highScoresToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { viewHighScoresToolStripMenuItem });
            highScoresToolStripMenuItem.Name = "highScoresToolStripMenuItem";
            highScoresToolStripMenuItem.Size = new Size(81, 20);
            highScoresToolStripMenuItem.Text = "&High Scores";
            //
            // viewHighScoresToolStripMenuItem
            //
            viewHighScoresToolStripMenuItem.Name = "viewHighScoresToolStripMenuItem";
            viewHighScoresToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.H;
            viewHighScoresToolStripMenuItem.Size = new Size(217, 22);
            viewHighScoresToolStripMenuItem.Text = "&View High Scores...";
            viewHighScoresToolStripMenuItem.Click += viewHighScoresToolStripMenuItem_Click;
            //
            // helpToolStripMenuItem
            //
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { howToPlayToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "H&elp";
            //
            // howToPlayToolStripMenuItem
            //
            howToPlayToolStripMenuItem.Name = "howToPlayToolStripMenuItem";
            howToPlayToolStripMenuItem.ShortcutKeys = Keys.F1;
            howToPlayToolStripMenuItem.Size = new Size(156, 22);
            howToPlayToolStripMenuItem.Text = "How to &Play";
            howToPlayToolStripMenuItem.Click += howToPlayToolStripMenuItem_Click;
            //
            // timeCaptionLabel
            //
            timeCaptionLabel.AutoSize = true;
            timeCaptionLabel.Location = new Point(40, 36);
            timeCaptionLabel.Name = "timeCaptionLabel";
            timeCaptionLabel.Size = new Size(55, 15);
            timeCaptionLabel.TabIndex = 1;
            timeCaptionLabel.Text = "Time Left";
            //
            // timeRemainingLabel
            //
            timeRemainingLabel.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            timeRemainingLabel.Location = new Point(34, 52);
            timeRemainingLabel.Name = "timeRemainingLabel";
            timeRemainingLabel.Size = new Size(170, 46);
            timeRemainingLabel.TabIndex = 2;
            timeRemainingLabel.Text = "1:00";
            timeRemainingLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // scoreCaptionLabel
            //
            scoreCaptionLabel.AutoSize = true;
            scoreCaptionLabel.Location = new Point(240, 36);
            scoreCaptionLabel.Name = "scoreCaptionLabel";
            scoreCaptionLabel.Size = new Size(73, 15);
            scoreCaptionLabel.TabIndex = 3;
            scoreCaptionLabel.Text = "Round Score";
            //
            // scoreLabel
            //
            scoreLabel.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            scoreLabel.Location = new Point(234, 52);
            scoreLabel.Name = "scoreLabel";
            scoreLabel.Size = new Size(170, 46);
            scoreLabel.TabIndex = 4;
            scoreLabel.Text = "0";
            scoreLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // roundCaptionLabel
            //
            roundCaptionLabel.AutoSize = true;
            roundCaptionLabel.Location = new Point(440, 36);
            roundCaptionLabel.Name = "roundCaptionLabel";
            roundCaptionLabel.Size = new Size(42, 15);
            roundCaptionLabel.TabIndex = 5;
            roundCaptionLabel.Text = "Round";
            //
            // roundLabel
            //
            roundLabel.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            roundLabel.Location = new Point(434, 52);
            roundLabel.Name = "roundLabel";
            roundLabel.Size = new Size(126, 46);
            roundLabel.TabIndex = 6;
            roundLabel.Text = "-";
            roundLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // currentWordLabel
            //
            currentWordLabel.BackColor = Color.White;
            currentWordLabel.BorderStyle = BorderStyle.FixedSingle;
            currentWordLabel.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            currentWordLabel.Location = new Point(40, 108);
            currentWordLabel.Name = "currentWordLabel";
            currentWordLabel.Size = new Size(520, 56);
            currentWordLabel.TabIndex = 7;
            currentWordLabel.TextAlign = ContentAlignment.MiddleCenter;
            //
            // letterButton1
            //
            letterButton1.BackColor = Color.Silver;
            letterButton1.Enabled = false;
            letterButton1.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            letterButton1.Location = new Point(40, 180);
            letterButton1.Name = "letterButton1";
            letterButton1.Size = new Size(64, 64);
            letterButton1.TabIndex = 8;
            letterButton1.UseVisualStyleBackColor = false;
            letterButton1.Click += letterButton_Click;
            //
            // letterButton2
            //
            letterButton2.BackColor = Color.Silver;
            letterButton2.Enabled = false;
            letterButton2.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            letterButton2.Location = new Point(116, 180);
            letterButton2.Name = "letterButton2";
            letterButton2.Size = new Size(64, 64);
            letterButton2.TabIndex = 9;
            letterButton2.UseVisualStyleBackColor = false;
            letterButton2.Click += letterButton_Click;
            //
            // letterButton3
            //
            letterButton3.BackColor = Color.Silver;
            letterButton3.Enabled = false;
            letterButton3.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            letterButton3.Location = new Point(192, 180);
            letterButton3.Name = "letterButton3";
            letterButton3.Size = new Size(64, 64);
            letterButton3.TabIndex = 10;
            letterButton3.UseVisualStyleBackColor = false;
            letterButton3.Click += letterButton_Click;
            //
            // letterButton4
            //
            letterButton4.BackColor = Color.Silver;
            letterButton4.Enabled = false;
            letterButton4.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            letterButton4.Location = new Point(268, 180);
            letterButton4.Name = "letterButton4";
            letterButton4.Size = new Size(64, 64);
            letterButton4.TabIndex = 11;
            letterButton4.UseVisualStyleBackColor = false;
            letterButton4.Click += letterButton_Click;
            //
            // letterButton5
            //
            letterButton5.BackColor = Color.Silver;
            letterButton5.Enabled = false;
            letterButton5.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            letterButton5.Location = new Point(344, 180);
            letterButton5.Name = "letterButton5";
            letterButton5.Size = new Size(64, 64);
            letterButton5.TabIndex = 12;
            letterButton5.UseVisualStyleBackColor = false;
            letterButton5.Click += letterButton_Click;
            //
            // letterButton6
            //
            letterButton6.BackColor = Color.Silver;
            letterButton6.Enabled = false;
            letterButton6.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            letterButton6.Location = new Point(420, 180);
            letterButton6.Name = "letterButton6";
            letterButton6.Size = new Size(64, 64);
            letterButton6.TabIndex = 13;
            letterButton6.UseVisualStyleBackColor = false;
            letterButton6.Click += letterButton_Click;
            //
            // letterButton7
            //
            letterButton7.BackColor = Color.Silver;
            letterButton7.Enabled = false;
            letterButton7.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            letterButton7.Location = new Point(496, 180);
            letterButton7.Name = "letterButton7";
            letterButton7.Size = new Size(64, 64);
            letterButton7.TabIndex = 14;
            letterButton7.UseVisualStyleBackColor = false;
            letterButton7.Click += letterButton_Click;
            //
            // twistButton
            //
            twistButton.Enabled = false;
            twistButton.Location = new Point(40, 260);
            twistButton.Name = "twistButton";
            twistButton.Size = new Size(121, 38);
            twistButton.TabIndex = 15;
            twistButton.Text = "Twist";
            twistButton.UseVisualStyleBackColor = true;
            twistButton.Click += twistButton_Click;
            //
            // undoLetterButton
            //
            undoLetterButton.Enabled = false;
            undoLetterButton.Location = new Point(173, 260);
            undoLetterButton.Name = "undoLetterButton";
            undoLetterButton.Size = new Size(121, 38);
            undoLetterButton.TabIndex = 16;
            undoLetterButton.Text = "Undo Letter";
            undoLetterButton.UseVisualStyleBackColor = true;
            undoLetterButton.Click += undoLetterButton_Click;
            //
            // clearButton
            //
            clearButton.Enabled = false;
            clearButton.Location = new Point(306, 260);
            clearButton.Name = "clearButton";
            clearButton.Size = new Size(121, 38);
            clearButton.TabIndex = 17;
            clearButton.Text = "Clear";
            clearButton.UseVisualStyleBackColor = true;
            clearButton.Click += clearButton_Click;
            //
            // submitButton
            //
            submitButton.Enabled = false;
            submitButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            submitButton.Location = new Point(439, 260);
            submitButton.Name = "submitButton";
            submitButton.Size = new Size(121, 38);
            submitButton.TabIndex = 18;
            submitButton.Text = "Submit Word";
            submitButton.UseVisualStyleBackColor = true;
            submitButton.Click += submitButton_Click;
            //
            // feedbackLabel
            //
            feedbackLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            feedbackLabel.Location = new Point(40, 306);
            feedbackLabel.Name = "feedbackLabel";
            feedbackLabel.Size = new Size(520, 42);
            feedbackLabel.TabIndex = 19;
            feedbackLabel.TextAlign = ContentAlignment.MiddleCenter;
            //
            // wordsCaptionLabel
            //
            wordsCaptionLabel.AutoSize = true;
            wordsCaptionLabel.Location = new Point(40, 356);
            wordsCaptionLabel.Name = "wordsCaptionLabel";
            wordsCaptionLabel.Size = new Size(127, 15);
            wordsCaptionLabel.TabIndex = 20;
            wordsCaptionLabel.Text = "Valid words this round:";
            //
            // wordsListView
            //
            wordsListView.Columns.AddRange(new ColumnHeader[] { wordColumnHeader, pointsColumnHeader, timeColumnHeader });
            wordsListView.FullRowSelect = true;
            wordsListView.GridLines = true;
            wordsListView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            wordsListView.Location = new Point(40, 376);
            wordsListView.MultiSelect = false;
            wordsListView.Name = "wordsListView";
            wordsListView.Size = new Size(520, 140);
            wordsListView.TabIndex = 21;
            wordsListView.UseCompatibleStateImageBehavior = false;
            wordsListView.View = View.Details;
            //
            // wordColumnHeader
            //
            wordColumnHeader.Text = "Word";
            wordColumnHeader.Width = 220;
            //
            // pointsColumnHeader
            //
            pointsColumnHeader.Text = "Points";
            pointsColumnHeader.Width = 130;
            //
            // timeColumnHeader
            //
            timeColumnHeader.Text = "Time Left";
            timeColumnHeader.Width = 130;
            //
            // newGameButton
            //
            newGameButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            newGameButton.Location = new Point(40, 528);
            newGameButton.Name = "newGameButton";
            newGameButton.Size = new Size(140, 36);
            newGameButton.TabIndex = 22;
            newGameButton.Text = "New Game";
            newGameButton.UseVisualStyleBackColor = true;
            newGameButton.Click += newGameButton_Click;
            //
            // keyboardHintLabel
            //
            keyboardHintLabel.ForeColor = SystemColors.GrayText;
            keyboardHintLabel.Location = new Point(196, 528);
            keyboardHintLabel.Name = "keyboardHintLabel";
            keyboardHintLabel.Size = new Size(364, 36);
            keyboardHintLabel.TabIndex = 23;
            keyboardHintLabel.Text = "Click the letters or type them. Enter submits, Backspace removes a letter, and Esc clears the word.";
            keyboardHintLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // MainForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 578);
            Controls.Add(keyboardHintLabel);
            Controls.Add(newGameButton);
            Controls.Add(wordsListView);
            Controls.Add(wordsCaptionLabel);
            Controls.Add(feedbackLabel);
            Controls.Add(submitButton);
            Controls.Add(clearButton);
            Controls.Add(undoLetterButton);
            Controls.Add(twistButton);
            Controls.Add(letterButton7);
            Controls.Add(letterButton6);
            Controls.Add(letterButton5);
            Controls.Add(letterButton4);
            Controls.Add(letterButton3);
            Controls.Add(letterButton2);
            Controls.Add(letterButton1);
            Controls.Add(currentWordLabel);
            Controls.Add(roundLabel);
            Controls.Add(roundCaptionLabel);
            Controls.Add(scoreLabel);
            Controls.Add(scoreCaptionLabel);
            Controls.Add(timeRemainingLabel);
            Controls.Add(timeCaptionLabel);
            Controls.Add(menuStrip);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Text Twist by Coleman";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion
        private MenuStrip menuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newGameToolStripMenuItem;
        private ToolStripSeparator fileToolStripSeparator1;
        private ToolStripMenuItem exportStatsToolStripMenuItem;
        private ToolStripSeparator fileToolStripSeparator2;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem timerToolStripMenuItem;
        private ToolStripMenuItem oneMinuteToolStripMenuItem;
        private ToolStripMenuItem twoMinutesToolStripMenuItem;
        private ToolStripMenuItem threeMinutesToolStripMenuItem;
        private ToolStripMenuItem highScoresToolStripMenuItem;
        private ToolStripMenuItem viewHighScoresToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem howToPlayToolStripMenuItem;
        private Label timeCaptionLabel;
        private Label timeRemainingLabel;
        private Label scoreCaptionLabel;
        private Label scoreLabel;
        private Label roundCaptionLabel;
        private Label roundLabel;
        private Label currentWordLabel;
        private Button letterButton1;
        private Button letterButton2;
        private Button letterButton3;
        private Button letterButton4;
        private Button letterButton5;
        private Button letterButton6;
        private Button letterButton7;
        private Button twistButton;
        private Button undoLetterButton;
        private Button clearButton;
        private Button submitButton;
        private Label feedbackLabel;
        private Label wordsCaptionLabel;
        private ListView wordsListView;
        private ColumnHeader wordColumnHeader;
        private ColumnHeader pointsColumnHeader;
        private ColumnHeader timeColumnHeader;
        private Button newGameButton;
        private Label keyboardHintLabel;
    }
}