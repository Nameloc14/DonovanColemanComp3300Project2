using DonovanColemanProject2.Model;

namespace DonovanColemanProject2.Views
{
    /// <summary>
    /// The window shown at the end of a round. It lists every valid word the
    /// player entered with its points and shows the overall score for the round.
    /// </summary>
    public partial class RoundSummaryForm : Form
    {
        /// <summary>
        /// Creates the round summary window.
        /// </summary>
        /// <param name="round">The round that just ended.</param>
        public RoundSummaryForm(GameRound round)
        {
            ArgumentNullException.ThrowIfNull(round);

            InitializeComponent();
            ShowRound(round);
        }
        /// <summary>
        /// Fills the window with the details of the round.
        /// </summary>
        /// <param name="round">The round to show.</param>
        private void ShowRound(GameRound round)
        {
            headingLabel.Text = $"Round {round.RoundNumber} Results";
            // Letters are shown in capitals with spaces between them, like the tiles.
            string spacedLetters = string.Join(
                " ", round.Letters.ToUpperInvariant().Select(letter => letter.ToString()));
            detailsLabel.Text = $"Letters: {spacedLetters}     Round time: {round.MaxTimeSeconds} seconds";

            wordsListView.Items.Clear();
            foreach (ValidWordAttempt validWord in round.ValidWords)
            {
                DisplayHelper.AddWordRow(wordsListView, validWord);
            }

            if (round.ValidWords.Count == 0)
            {
                wordCountLabel.Text = "No valid words were entered this round.";
            }
            else
            {
                wordCountLabel.Text = $"Valid words entered: {round.ValidWords.Count}";
            }

            totalScoreLabel.Text = $"Round score: {round.TotalScore}";
        }
        /// <summary>
        /// Closes the window.
        /// </summary>
        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}