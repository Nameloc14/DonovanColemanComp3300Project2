using DonovanColemanProject2.Model;
using DonovanColemanProject2.Storage;
using Timer = System.Windows.Forms.Timer;

namespace DonovanColemanProject2.Views
{
    /// <summary>
    /// The main game window. It shows what the <see cref="TextTwistGame"/> reports
    /// and passes along what the player does. All of the game rules live in the
    /// model classes, so this class only deals with the screen.
    /// </summary>
    public partial class MainForm : Form
    {
        private const string GameTitle = "Text Twist by Coleman";
        private const string StatsFileFilter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
        private const int OneSecondInMilliseconds = 1000;
        /// <summary>The clock turns red when this many seconds or fewer are left.</summary>
        private const int LowTimeWarningSeconds = 10;

        private static readonly Color AvailableTileColor = Color.White;
        private static readonly Color UsedTileColor = Color.Silver;
        private static readonly Color ValidFeedbackColor = Color.DarkGreen;
        private static readonly Color InvalidFeedbackColor = Color.Firebrick;
        private static readonly Color NeutralFeedbackColor = SystemColors.ControlText;

        private readonly TextTwistGame _game;
        private readonly HighScoreStorage _highScoreStorage;
        private readonly Timer _roundTimer;
        private readonly Button[] _letterButtons;
        private HighScoreBoard _highScoreBoard;
        /// <summary>
        /// Creates the main game window.
        /// </summary>
        /// <param name="dictionary">The words the game accepts.</param>
        public MainForm(WordDictionary dictionary)
        {
            ArgumentNullException.ThrowIfNull(dictionary);

            InitializeComponent();

            _game = new TextTwistGame(dictionary, new Random());
            _highScoreStorage = new HighScoreStorage();
            _highScoreBoard = new HighScoreBoard();
            // The seven letter buttons are kept in an array so they can be matched
            // to the seven tiles in the game by position.
            _letterButtons = new Button[]
            {
                letterButton1, letterButton2, letterButton3, letterButton4,
                letterButton5, letterButton6, letterButton7
            };

            _roundTimer = new Timer();
            _roundTimer.Interval = OneSecondInMilliseconds;
            _roundTimer.Tick += RoundTimer_Tick;

            ShowFeedback("Choose New Game to start playing.", NeutralFeedbackColor);
            UpdateDisplay();
        }
        /// <summary>
        /// Lets the player use the keyboard during a round: letter keys pick tiles,
        /// Enter submits, Backspace removes a letter, and Escape clears the word.
        /// This runs before any button sees the key, so Enter always submits the
        /// word no matter which button has the focus.
        /// </summary>
        /// <param name="msg">The Windows message for the key press.</param>
        /// <param name="keyData">The key that was pressed.</param>
        /// <returns>true if the key was used by the game; otherwise the normal result.</returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (_game.IsRoundActive && HandleGameKey(keyData))
            {
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        /// <summary>
        /// Stops and releases the timer once the window has closed.
        /// </summary>
        /// <param name="e">The event details.</param>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _roundTimer.Stop();
            _roundTimer.Dispose();
            base.OnFormClosed(e);
        }
        /// <summary>
        /// Loads the saved high scores when the window first opens.
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                _highScoreBoard = _highScoreStorage.Load();
            }
            catch (InvalidDataException exception)
            {
                _highScoreBoard = new HighScoreBoard();
                MessageBox.Show(
                    this,
                    exception.Message + Environment.NewLine + Environment.NewLine
                    + "The high score board will start out empty.",
                    GameTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
        /// <summary>
        /// Asks before closing if a round is still being played.
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_game.IsRoundActive)
            {
                return;
            }
            // The clock is paused while the question is on the screen.
            _roundTimer.Stop();
            DialogResult answer = MessageBox.Show(
                this,
                "A round is still being played. Do you want to exit anyway?",
                GameTitle,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (answer != DialogResult.Yes)
            {
                e.Cancel = true;
                _roundTimer.Start();
            }
        }
        /// <summary>
        /// Counts the clock down once a second and ends the round at zero.
        /// </summary>
        private void RoundTimer_Tick(object? sender, EventArgs e)
        {
            if (!_game.IsRoundActive)
            {
                _roundTimer.Stop();
                return;
            }

            GameRound? finishedRound = _game.Tick();
            UpdateTimeDisplay();

            if (finishedRound != null)
            {
                _roundTimer.Stop();
                FinishRound(finishedRound);
            }
        }
        /// <summary>
        /// Adds the clicked letter to the word being built. All seven letter buttons
        /// share this one handler.
        /// </summary>
        private void letterButton_Click(object sender, EventArgs e)
        {
            int tileIndex = Array.IndexOf(_letterButtons, sender);
            if (tileIndex < 0 || !_game.IsRoundActive)
            {
                return;
            }

            _game.SelectTile(tileIndex);
            ClearFeedback();
            UpdateBoard();
        }
        /// <summary>
        /// Shows the letters in a different order.
        /// </summary>
        private void twistButton_Click(object sender, EventArgs e)
        {
            if (!_game.IsRoundActive)
            {
                return;
            }

            _game.Twist();
            UpdateBoard();
        }
        /// <summary>
        /// Takes the last letter off the word being built.
        /// </summary>
        private void undoLetterButton_Click(object sender, EventArgs e)
        {
            RemoveLastLetter();
        }
        /// <summary>
        /// Empties the word being built.
        /// </summary>
        private void clearButton_Click(object sender, EventArgs e)
        {
            ClearCurrentWord();
        }
        /// <summary>
        /// Submits the word being built so it can be checked and scored.
        /// </summary>
        private void submitButton_Click(object sender, EventArgs e)
        {
            SubmitCurrentWord();
        }
        /// <summary>
        /// Starts a new game from the button on the form.
        /// </summary>
        private void newGameButton_Click(object sender, EventArgs e)
        {
            StartNewGame();
        }
        /// <summary>
        /// Starts a new game from the File menu.
        /// </summary>
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartNewGame();
        }
        /// <summary>
        /// Closes the application from the File menu.
        /// </summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// Changes the timer length. The three timer menu items share this handler.
        /// </summary>
        private void timerLengthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_game.IsRoundActive)
            {
                return;
            }

            int seconds;
            if (sender == twoMinutesToolStripMenuItem)
            {
                seconds = RoundTimerOptions.TwoMinutesSeconds;
            }
            else if (sender == threeMinutesToolStripMenuItem)
            {
                seconds = RoundTimerOptions.ThreeMinutesSeconds;
            }
            else
            {
                seconds = RoundTimerOptions.OneMinuteSeconds;
            }

            _game.SetTimerSeconds(seconds);
            UpdateDisplay();
        }
        /// <summary>
        /// Opens the high score board.
        /// </summary>
        private void viewHighScoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_game.IsRoundActive)
            {
                return;
            }

            ShowHighScoreBoard();
        }
        /// <summary>
        /// Exports every round played this session to a JSON file the player picks.
        /// </summary>
        private void exportStatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_game.IsRoundActive || !_game.Session.HasRounds)
            {
                return;
            }

            using SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "Export Stats";
            saveDialog.Filter = StatsFileFilter;
            saveDialog.FileName = SessionStorage.DefaultFileName;
            saveDialog.DefaultExt = "json";
            saveDialog.AddExtension = true;
            saveDialog.OverwritePrompt = true;

            if (saveDialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                SessionStorage.Export(_game.Session, saveDialog.FileName);
                MessageBox.Show(
                    this,
                    $"Stats for {_game.Session.Rounds.Count} round(s) were exported to:"
                    + Environment.NewLine + saveDialog.FileName,
                    GameTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (IOException exception)
            {
                MessageBox.Show(
                    this,
                    "The stats could not be exported." + Environment.NewLine + Environment.NewLine
                    + exception.Message,
                    GameTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Shows the rules of the game.
        /// </summary>
        private void howToPlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_game.IsRoundActive)
            {
                return;
            }

            string instructions =
                "Make as many words as you can from the seven letters before the clock runs out."
                + Environment.NewLine + Environment.NewLine
                + "- Click a letter (or type it) to add it to your word. Each letter can be used once per word."
                + Environment.NewLine
                + "- Words need at least 3 letters. Press Submit or Enter to check a word."
                + Environment.NewLine
                + "- Twist shows the letters in a new order. Undo Letter (Backspace) removes the last "
                + "letter and Clear (Esc) removes them all."
                + Environment.NewLine
                + "- A word only scores the first time you enter it in a round."
                + Environment.NewLine + Environment.NewLine
                + "Points: 3 letters = 90, 4 = 160, 5 = 250, 6 = 360, 7 = 490."
                + Environment.NewLine + Environment.NewLine
                + "Use Options > Timer to play 1, 2, or 3 minute rounds.";

            MessageBox.Show(this, instructions, "How to Play", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Uses a key press for the game if it is one of the game keys.
        /// </summary>
        /// <param name="keyData">The key that was pressed.</param>
        /// <returns>true if the key was a game key; otherwise false.</returns>
        private bool HandleGameKey(Keys keyData)
        {
            if (keyData >= Keys.A && keyData <= Keys.Z)
            {
                char letter = (char)('a' + (keyData - Keys.A));
                if (_game.SelectLetter(letter))
                {
                    ClearFeedback();
                }
                else
                {
                    ShowFeedback(
                        $"There is no unused \"{char.ToUpperInvariant(letter)}\" to pick.",
                        InvalidFeedbackColor);
                }

                UpdateBoard();
                return true;
            }

            switch (keyData)
            {
                case Keys.Enter:
                    SubmitCurrentWord();
                    return true;
                case Keys.Back:
                    RemoveLastLetter();
                    return true;
                case Keys.Escape:
                    ClearCurrentWord();
                    return true;
                default:
                    return false;
            }
        }
        /// <summary>
        /// Starts a new round. If a round is already being played the player is
        /// asked first, because starting over ends that round early.
        /// </summary>
        private void StartNewGame()
        {
            if (_game.IsRoundActive)
            {
                // The clock is paused while the question is on the screen.
                _roundTimer.Stop();
                DialogResult answer = MessageBox.Show(
                    this,
                    "A round is still being played. Do you want to end it and start a new game?"
                    + Environment.NewLine + Environment.NewLine
                    + "The round will be kept in your stats, but it will not go on the high score board.",
                    GameTitle,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (answer != DialogResult.Yes)
                {
                    _roundTimer.Start();
                    return;
                }

                _game.AbandonRound();
            }

            _game.StartNewRound();
            wordsListView.Items.Clear();
            ShowFeedback("Go! Make words from the letters above.", NeutralFeedbackColor);
            UpdateDisplay();
            _roundTimer.Start();
        }
        /// <summary>
        /// Sends the word being built to the game and shows whether it scored.
        /// </summary>
        private void SubmitCurrentWord()
        {
            if (!_game.IsRoundActive)
            {
                return;
            }

            if (!_game.CanSubmit)
            {
                ShowFeedback(
                    $"A word needs at least {WordRules.MinimumWordLength} letters.",
                    InvalidFeedbackColor);
                return;
            }

            WordAttempt attempt = _game.SubmitWord();
            string displayWord = attempt.Word.ToUpperInvariant();

            if (attempt is ValidWordAttempt validAttempt)
            {
                DisplayHelper.AddWordRow(wordsListView, validAttempt);
                ShowFeedback(
                    $"\"{displayWord}\" is a valid word!  +{validAttempt.Points} points",
                    ValidFeedbackColor);
            }
            else if (attempt is InvalidWordAttempt invalidAttempt)
            {
                ShowFeedback(
                    $"\"{displayWord}\" is not valid: {invalidAttempt.ReasonDescription}.  No points.",
                    InvalidFeedbackColor);
            }

            UpdateBoard();
        }
        /// <summary>
        /// Takes the last letter off the word being built.
        /// </summary>
        private void RemoveLastLetter()
        {
            if (!_game.IsRoundActive)
            {
                return;
            }

            _game.RemoveLastLetter();
            ClearFeedback();
            UpdateBoard();
        }
        /// <summary>
        /// Empties the word being built.
        /// </summary>
        private void ClearCurrentWord()
        {
            if (!_game.IsRoundActive)
            {
                return;
            }

            _game.ClearSelection();
            ClearFeedback();
            UpdateBoard();
        }
        /// <summary>
        /// Wraps up a round whose clock ran out: shows the results and then checks
        /// whether the score belongs on the high score board.
        /// </summary>
        /// <param name="round">The round that just ended.</param>
        private void FinishRound(GameRound round)
        {
            ShowFeedback("Time is up!", NeutralFeedbackColor);
            UpdateDisplay();

            using (RoundSummaryForm summaryForm = new RoundSummaryForm(round))
            {
                summaryForm.ShowDialog(this);
            }

            RecordHighScore(round);
            ShowFeedback("Choose New Game to play again.", NeutralFeedbackColor);
        }
        /// <summary>
        /// If the round's score earns a place on the high score board, asks the
        /// player for their name, saves the score, and shows the board.
        /// </summary>
        /// <param name="round">The round that just ended.</param>
        private void RecordHighScore(GameRound round)
        {
            if (!_highScoreBoard.Qualifies(round.TotalScore))
            {
                return;
            }

            string playerName;
            using (NameEntryForm nameForm = new NameEntryForm(round.TotalScore))
            {
                nameForm.ShowDialog(this);
                playerName = nameForm.PlayerName;
            }
            // The name form does not close without a valid name, but the check stays
            // here so a bad name can never reach the board.
            if (!HighScoreEntry.IsValidName(playerName))
            {
                return;
            }

            _highScoreBoard.Add(new HighScoreEntry(playerName, round.TotalScore, round.MaxTimeSeconds));
            SaveHighScores();
            ShowHighScoreBoard();
        }
        /// <summary>
        /// Opens the high score board and saves it if the player reset it.
        /// </summary>
        private void ShowHighScoreBoard()
        {
            using HighScoreForm highScoreForm = new HighScoreForm(_highScoreBoard);
            highScoreForm.ShowDialog(this);

            if (highScoreForm.WasReset)
            {
                SaveHighScores();
            }
        }
        /// <summary>
        /// Writes the high score board to its file and tells the player if that fails.
        /// </summary>
        private void SaveHighScores()
        {
            try
            {
                _highScoreStorage.Save(_highScoreBoard);
            }
            catch (IOException exception)
            {
                MessageBox.Show(
                    this,
                    "The high scores could not be saved, so they will be lost when the game closes."
                    + Environment.NewLine + Environment.NewLine + exception.Message,
                    GameTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
        /// <summary>
        /// Shows a message under the buttons.
        /// </summary>
        /// <param name="message">The message to show.</param>
        /// <param name="color">The color to show it in.</param>
        private void ShowFeedback(string message, Color color)
        {
            feedbackLabel.ForeColor = color;
            feedbackLabel.Text = message;
        }
        /// <summary>
        /// Removes the message under the buttons.
        /// </summary>
        private void ClearFeedback()
        {
            feedbackLabel.Text = string.Empty;
        }
        /// <summary>
        /// Refreshes every part of the window from the current state of the game.
        /// </summary>
        private void UpdateDisplay()
        {
            UpdateBoard();
            UpdateTimeDisplay();
            UpdateRoundDisplay();
            UpdateMenus();
        }
        /// <summary>
        /// Refreshes the letter buttons, the word being built, the score, and the
        /// buttons that act on the word. A letter that is already used in the word is
        /// disabled and shaded so it is clear it cannot be picked again.
        /// </summary>
        private void UpdateBoard()
        {
            bool isRoundActive = _game.IsRoundActive;
            IReadOnlyList<LetterTile> tiles = _game.Tiles;

            for (int index = 0; index < _letterButtons.Length; index++)
            {
                Button letterButton = _letterButtons[index];
                if (index < tiles.Count)
                {
                    LetterTile tile = tiles[index];
                    bool isAvailable = isRoundActive && !tile.IsSelected;
                    letterButton.Text = char.ToUpperInvariant(tile.Letter).ToString();
                    letterButton.Enabled = isAvailable;
                    letterButton.BackColor = isAvailable ? AvailableTileColor : UsedTileColor;
                }
                else
                {
                    letterButton.Text = string.Empty;
                    letterButton.Enabled = false;
                    letterButton.BackColor = UsedTileColor;
                }
            }

            string currentWord = isRoundActive ? _game.CurrentWord : string.Empty;
            currentWordLabel.Text = currentWord.ToUpperInvariant();

            twistButton.Enabled = isRoundActive;
            undoLetterButton.Enabled = isRoundActive && currentWord.Length > 0;
            clearButton.Enabled = isRoundActive && currentWord.Length > 0;
            submitButton.Enabled = _game.CanSubmit;

            scoreLabel.Text = GetScoreToShow().ToString();
        }
        /// <summary>
        /// Refreshes the clock. Between rounds it shows the timer setting that the
        /// next round will use.
        /// </summary>
        private void UpdateTimeDisplay()
        {
            bool isRoundActive = _game.IsRoundActive;
            int secondsToShow = isRoundActive ? _game.TimeRemainingSeconds : _game.Session.TimerSeconds;

            timeRemainingLabel.Text = DisplayHelper.FormatTime(secondsToShow);
            timeRemainingLabel.ForeColor = isRoundActive && secondsToShow <= LowTimeWarningSeconds
                ? InvalidFeedbackColor
                : SystemColors.ControlText;
        }
        /// <summary>
        /// Refreshes the round number. Between rounds it shows the last round played.
        /// </summary>
        private void UpdateRoundDisplay()
        {
            GameRound? currentRound = _game.CurrentRound;
            if (currentRound != null)
            {
                roundLabel.Text = currentRound.RoundNumber.ToString();
            }
            else if (_game.Session.HasRounds)
            {
                roundLabel.Text = _game.Session.Rounds.Count.ToString();
            }
            else
            {
                roundLabel.Text = "-";
            }
        }
        /// <summary>
        /// Turns menu items on and off to match what makes sense right now. While a
        /// round is being played only New Game and Exit are available, and exporting
        /// is only available once at least one round has been played.
        /// </summary>
        private void UpdateMenus()
        {
            bool isBetweenRounds = !_game.IsRoundActive;
            int timerSeconds = _game.Session.TimerSeconds;

            timerToolStripMenuItem.Enabled = isBetweenRounds;
            oneMinuteToolStripMenuItem.Checked = timerSeconds == RoundTimerOptions.OneMinuteSeconds;
            twoMinutesToolStripMenuItem.Checked = timerSeconds == RoundTimerOptions.TwoMinutesSeconds;
            threeMinutesToolStripMenuItem.Checked = timerSeconds == RoundTimerOptions.ThreeMinutesSeconds;

            exportStatsToolStripMenuItem.Enabled = isBetweenRounds && _game.Session.HasRounds;
            viewHighScoresToolStripMenuItem.Enabled = isBetweenRounds;
            howToPlayToolStripMenuItem.Enabled = isBetweenRounds;
        }
        /// <summary>
        /// Works out which score to show: the round being played, or the last round
        /// played when the game is between rounds.
        /// </summary>
        /// <returns>The score to show.</returns>
        private int GetScoreToShow()
        {
            GameRound? currentRound = _game.CurrentRound;
            if (currentRound != null)
            {
                return currentRound.TotalScore;
            }

            if (_game.Session.HasRounds)
            {
                return _game.Session.Rounds[_game.Session.Rounds.Count - 1].TotalScore;
            }

            return 0;
        }
    }
}