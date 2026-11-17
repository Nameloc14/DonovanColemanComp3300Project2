using DonovanColemanProject2.Model;

namespace DonovanColemanProject2.Views
{
    /// <summary>
    /// The window that asks a player for their name when their score earns a
    /// place on the high score board. It does not close until a valid name is
    /// entered.
    /// </summary>
    public partial class NameEntryForm : Form
    {
        private string _playerName;
        private bool _nameAccepted;
        /// <summary>
        /// Creates the name entry window.
        /// </summary>
        /// <param name="score">The score that made the high score board.</param>
        public NameEntryForm(int score)
        {
            InitializeComponent();

            _playerName = string.Empty;
            _nameAccepted = false;
            nameTextBox.MaxLength = HighScoreEntry.MaxNameLength;
            congratulationsLabel.Text = $"You made the high score board with {score} points!";
            errorLabel.Text = string.Empty;
        }
        /// <summary>
        /// Gets the name the player entered. It is empty until a valid name is accepted.
        /// </summary>
        public string PlayerName
        {
            get { return _playerName; }
        }
        /// <summary>
        /// Checks the name and closes the window if it is valid. If it is not, the
        /// player is told why and the window stays open.
        /// </summary>
        private void okButton_Click(object sender, EventArgs e)
        {
            string enteredName = nameTextBox.Text.Trim();

            if (enteredName.Length == 0)
            {
                ShowNameError("Please enter your name.");
                return;
            }

            if (!HighScoreEntry.IsValidName(enteredName))
            {
                ShowNameError("Names can only use letters, spaces, hyphens, apostrophes, and periods.");
                return;
            }

            _playerName = enteredName;
            _nameAccepted = true;
            DialogResult = DialogResult.OK;
            Close();
        }
        /// <summary>
        /// Clears the error message as soon as the player starts fixing the name.
        /// </summary>
        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            errorLabel.Text = string.Empty;
        }
        /// <summary>
        /// Keeps the window open if the player tries to close it (for example with
        /// Alt+F4) before entering a valid name.
        /// </summary>
        private void NameEntryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_nameAccepted && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                ShowNameError("Please enter your name to save your high score.");
            }
        }
        /// <summary>
        /// Shows a problem with the name and puts the cursor back in the text box.
        /// </summary>
        /// <param name="message">The problem to show.</param>
        private void ShowNameError(string message)
        {
            errorLabel.Text = message;
            nameTextBox.Focus();
            nameTextBox.SelectAll();
        }
    }
}