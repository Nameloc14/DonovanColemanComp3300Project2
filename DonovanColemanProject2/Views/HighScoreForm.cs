using DonovanColemanProject2.Model;

namespace DonovanColemanProject2.Views
{
    /// <summary>
    /// The high score board window. It shows each player's name, score, and timer
    /// setting, lets the player choose how the scores are sorted, and lets the
    /// player reset the board.
    /// </summary>
    public partial class HighScoreForm : Form
    {
        private readonly HighScoreBoard _highScoreBoard;
        private bool _wasReset;

        /// <summary>
        /// Creates the high score board window.
        /// </summary>
        /// <param name="highScoreBoard">The board to show.</param>
        public HighScoreForm(HighScoreBoard highScoreBoard)
        {
            ArgumentNullException.ThrowIfNull(highScoreBoard);

            // The board is stored before the controls are built because the sort radio
            // buttons can raise their CheckedChanged event while they are being set up.
            _highScoreBoard = highScoreBoard;
            _wasReset = false;

            InitializeComponent();

            ShowScores();
        }
        /// <summary>
        /// Gets a value that is true if the player reset the board while the window
        /// was open, which tells the main window the board needs to be saved.
        /// </summary>
        public bool WasReset
        {
            get { return _wasReset; }
        }
        /// <summary>
        /// Re-sorts the list when the player picks a different sort order. Both radio
        /// buttons share this handler.
        /// </summary>
        private void sortRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // CheckedChanged fires for the button being turned off and the one being
            // turned on, so only the one that is now checked needs to refresh the list.
            if (sender is RadioButton radioButton && radioButton.Checked)
            {
                ShowScores();
            }
        }
        /// <summary>
        /// Clears the high score board after the player confirms.
        /// </summary>
        private void resetButton_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show(
                this,
                "This will permanently remove every high score. Do you want to reset the board?",
                "Reset High Scores",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (answer != DialogResult.Yes)
            {
                return;
            }

            _highScoreBoard.Reset();
            _wasReset = true;
            ShowScores();
        }
        /// <summary>
        /// Closes the window.
        /// </summary>
        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// Fills the list with the scores in the sort order the player picked.
        /// </summary>
        private void ShowScores()
        {
            List<HighScoreEntry> sortedEntries = sortByTimeRadioButton.Checked
                ? _highScoreBoard.GetSortedByTimeThenScore()
                : _highScoreBoard.GetSortedByScore();

            scoresListView.BeginUpdate();
            scoresListView.Items.Clear();

            int position = 1;
            foreach (HighScoreEntry entry in sortedEntries)
            {
                ListViewItem row = new ListViewItem(position.ToString());
                row.SubItems.Add(entry.Name);
                row.SubItems.Add(entry.Score.ToString());
                row.SubItems.Add($"{entry.MaxTimeSeconds} seconds");
                scoresListView.Items.Add(row);
                position++;
            }

            scoresListView.EndUpdate();

            bool hasScores = sortedEntries.Count > 0;
            emptyBoardLabel.Visible = !hasScores;
            resetButton.Enabled = hasScores;
        }
    }
}