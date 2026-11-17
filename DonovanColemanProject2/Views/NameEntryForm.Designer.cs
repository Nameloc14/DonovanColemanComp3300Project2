namespace DonovanColemanProject2.Views
{
    partial class NameEntryForm
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
            congratulationsLabel = new Label();
            promptLabel = new Label();
            nameTextBox = new TextBox();
            errorLabel = new Label();
            okButton = new Button();
            SuspendLayout();
            //
            // congratulationsLabel
            //
            congratulationsLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            congratulationsLabel.Location = new Point(20, 14);
            congratulationsLabel.Name = "congratulationsLabel";
            congratulationsLabel.Size = new Size(360, 44);
            congratulationsLabel.TabIndex = 0;
            congratulationsLabel.Text = "You made the high score board!";
            congratulationsLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // promptLabel
            //
            promptLabel.AutoSize = true;
            promptLabel.Location = new Point(20, 66);
            promptLabel.Name = "promptLabel";
            promptLabel.Size = new Size(97, 15);
            promptLabel.TabIndex = 1;
            promptLabel.Text = "Enter your &name:";
            //
            // nameTextBox
            //
            nameTextBox.Location = new Point(20, 86);
            nameTextBox.MaxLength = 20;
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(360, 23);
            nameTextBox.TabIndex = 2;
            nameTextBox.TextChanged += nameTextBox_TextChanged;
            //
            // errorLabel
            //
            errorLabel.ForeColor = Color.Firebrick;
            errorLabel.Location = new Point(20, 114);
            errorLabel.Name = "errorLabel";
            errorLabel.Size = new Size(360, 34);
            errorLabel.TabIndex = 3;
            //
            // okButton
            //
            okButton.Location = new Point(280, 152);
            okButton.Name = "okButton";
            okButton.Size = new Size(100, 32);
            okButton.TabIndex = 4;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            //
            // NameEntryForm
            //
            AcceptButton = okButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 198);
            ControlBox = false;
            Controls.Add(okButton);
            Controls.Add(errorLabel);
            Controls.Add(nameTextBox);
            Controls.Add(promptLabel);
            Controls.Add(congratulationsLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "NameEntryForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "New High Score";
            FormClosing += NameEntryForm_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label congratulationsLabel;
        private Label promptLabel;
        private TextBox nameTextBox;
        private Label errorLabel;
        private Button okButton;
    }
}