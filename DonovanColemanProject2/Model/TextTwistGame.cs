namespace DonovanColemanProject2.Model
{
    /// <summary>
    /// Runs the Text Twist game. This class holds all of the game rules and state
    /// (the letters, the word being built, the clock, and the stats) and knows
    /// nothing about the screen, so the form only has to show what this class
    /// reports and pass along what the player does.
    /// </summary>
    public class TextTwistGame
    {
        /// <summary>
        /// How many times to redraw when a set of letters cannot make any word,
        /// before giving up and using the letters anyway.
        /// </summary>
        private const int MaxDrawAttempts = 100;
        /// <summary>How many times to reshuffle while trying to get a new letter order.</summary>
        private const int MaxTwistAttempts = 25;

        private readonly WordDictionary _dictionary;
        private readonly LetterBag _letterBag;
        private readonly Random _random;
        private readonly List<LetterTile> _tiles;
        private readonly List<LetterTile> _selectedTiles;

        /// <summary>
        /// Creates a game that starts with a brand new session and no round running.
        /// </summary>
        /// <param name="dictionary">The words the game accepts.</param>
        /// <param name="random">The random number generator used to deal and twist letters.</param>
        public TextTwistGame(WordDictionary dictionary, Random random)
        {
            ArgumentNullException.ThrowIfNull(dictionary);
            ArgumentNullException.ThrowIfNull(random);

            _dictionary = dictionary;
            _random = random;
            _letterBag = new LetterBag(random);
            _tiles = new List<LetterTile>();
            _selectedTiles = new List<LetterTile>();
            Session = new GameSession();
        }
        /// <summary>Gets the stats for every round played this session.</summary>
        public GameSession Session { get; private set; }
        /// <summary>Gets the round being played, or null when no round is running.</summary>
        public GameRound? CurrentRound { get; private set; }
        /// <summary>Gets the seconds left on the count-down timer.</summary>
        public int TimeRemainingSeconds { get; private set; }
        /// <summary>Gets a value that is true while a round is being played.</summary>
        public bool IsRoundActive
        {
            get { return CurrentRound != null; }
        }
        /// <summary>Gets the seven tiles on the board in the order they are displayed.</summary>
        public IReadOnlyList<LetterTile> Tiles
        {
            get { return _tiles; }
        }
        /// <summary>Gets the word the player is currently building, in lowercase.</summary>
        public string CurrentWord
        {
            get { return new string(_selectedTiles.Select(tile => tile.Letter).ToArray()); }
        }
        /// <summary>
        /// Gets a value that is true when the word being built is long enough to submit.
        /// </summary>
        public bool CanSubmit
        {
            get { return IsRoundActive && _selectedTiles.Count >= WordRules.MinimumWordLength; }
        }
        /// <summary>
        /// Changes the timer length used for the next round.
        /// </summary>
        /// <param name="seconds">60, 120, or 180.</param>
        /// <exception cref="InvalidOperationException">A round is being played.</exception>
        /// <exception cref="ArgumentException">The value is not an allowed timer length.</exception>
        public void SetTimerSeconds(int seconds)
        {
            EnsureNoRoundActive("The timer cannot be changed while a round is being played.");
            Session.TimerSeconds = seconds;
        }
        /// <summary>
        /// Deals seven new letters, resets the clock, and starts a new round.
        /// </summary>
        /// <exception cref="InvalidOperationException">A round is already being played.</exception>
        public void StartNewRound()
        {
            EnsureNoRoundActive("The current round must end before a new one can start.");

            List<char> letters = DrawPlayableLetters();

            _tiles.Clear();
            _selectedTiles.Clear();
            foreach (char letter in letters)
            {
                _tiles.Add(new LetterTile(letter));
            }

            CurrentRound = new GameRound(
                Session.NextRoundNumber, Session.TimerSeconds, new string(letters.ToArray()));
            TimeRemainingSeconds = Session.TimerSeconds;
        }
        /// <summary>
        /// Adds the tile at a board position to the word being built.
        /// </summary>
        /// <param name="tileIndex">The position of the tile on the board (0 to 6).</param>
        /// <returns>true if the tile was added; false if it was already used.</returns>
        /// <exception cref="InvalidOperationException">No round is being played.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The position is not on the board.</exception>
        public bool SelectTile(int tileIndex)
        {
            EnsureRoundActive();
            if (tileIndex < 0 || tileIndex >= _tiles.Count)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(tileIndex), "The tile position is not on the board.");
            }

            LetterTile tile = _tiles[tileIndex];
            if (tile.IsSelected)
            {
                return false;
            }

            tile.IsSelected = true;
            _selectedTiles.Add(tile);
            return true;
        }
        /// <summary>
        /// Adds the first unused tile showing the given letter to the word being
        /// built. This is what lets the player type letters on the keyboard.
        /// </summary>
        /// <param name="letter">The letter that was typed (upper or lower case).</param>
        /// <returns>true if a tile was added; false if no unused tile has that letter.</returns>
        /// <exception cref="InvalidOperationException">No round is being played.</exception>
        public bool SelectLetter(char letter)
        {
            EnsureRoundActive();

            char lowercaseLetter = char.ToLowerInvariant(letter);
            int tileIndex = _tiles.FindIndex(tile => !tile.IsSelected && tile.Letter == lowercaseLetter);
            if (tileIndex < 0)
            {
                return false;
            }

            return SelectTile(tileIndex);
        }
        /// <summary>
        /// Takes the last letter off the word being built and frees its tile.
        /// </summary>
        /// <returns>true if a letter was removed; false if the word was already empty.</returns>
        /// <exception cref="InvalidOperationException">No round is being played.</exception>
        public bool RemoveLastLetter()
        {
            EnsureRoundActive();
            if (_selectedTiles.Count == 0)
            {
                return false;
            }

            LetterTile lastTile = _selectedTiles[_selectedTiles.Count - 1];
            lastTile.IsSelected = false;
            _selectedTiles.RemoveAt(_selectedTiles.Count - 1);
            return true;
        }
        /// <summary>
        /// Empties the word being built and frees every tile.
        /// </summary>
        public void ClearSelection()
        {
            foreach (LetterTile tile in _selectedTiles)
            {
                tile.IsSelected = false;
            }

            _selectedTiles.Clear();
        }
        /// <summary>
        /// Puts the tiles on the board in a different order. Each tile keeps its
        /// selected state, so letters already used in the current word stay used.
        /// </summary>
        /// <exception cref="InvalidOperationException">No round is being played.</exception>
        public void Twist()
        {
            EnsureRoundActive();

            string orderBefore = GetBoardLetters();

            // If every tile shows the same letter no shuffle can look different.
            if (orderBefore.Distinct().Count() < 2)
            {
                return;
            }

            int attempts = 0;
            do
            {
                ShuffleTiles();
                attempts++;
            }
            while (GetBoardLetters() == orderBefore && attempts < MaxTwistAttempts);
        }
        /// <summary>
        /// Checks the word being built, records it as valid or invalid, and clears
        /// the board so the next word can be built.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidWordAttempt"/> if the word scored, or an
        /// <see cref="InvalidWordAttempt"/> that says why it did not.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// No round is being played, or the word is shorter than three letters.
        /// </exception>
        public WordAttempt SubmitWord()
        {
            GameRound round = EnsureRoundActive();
            if (!CanSubmit)
            {
                throw new InvalidOperationException(
                    $"A word must have at least {WordRules.MinimumWordLength} letters to be submitted.");
            }

            string word = CurrentWord;
            WordAttempt attempt;

            if (!_dictionary.Contains(word))
            {
                attempt = round.AddInvalidWord(word, TimeRemainingSeconds, InvalidWordReason.NotInDictionary);
            }
            else if (round.HasValidWord(word))
            {
                attempt = round.AddInvalidWord(word, TimeRemainingSeconds, InvalidWordReason.Duplicate);
            }
            else
            {
                attempt = round.AddValidWord(word, TimeRemainingSeconds);
            }

            ClearSelection();
            return attempt;
        }
        /// <summary>
        /// Takes one second off the clock. When the clock reaches zero the round is
        /// ended and saved to the session.
        /// </summary>
        /// <returns>The round that just ended, or null if the round is still going.</returns>
        /// <exception cref="InvalidOperationException">No round is being played.</exception>
        public GameRound? Tick()
        {
            EnsureRoundActive();

            if (TimeRemainingSeconds > 0)
            {
                TimeRemainingSeconds--;
            }

            if (TimeRemainingSeconds > 0)
            {
                return null;
            }

            return EndRound(true);
        }
        /// <summary>
        /// Ends the round before the clock runs out. The round is still saved to the
        /// session so that every attempt is tracked, but it is marked as not completed.
        /// </summary>
        /// <returns>The round that was ended.</returns>
        /// <exception cref="InvalidOperationException">No round is being played.</exception>
        public GameRound AbandonRound()
        {
            EnsureRoundActive();
            return EndRound(false);
        }
        /// <summary>
        /// Replaces the current session with one loaded from a file so the player can
        /// keep adding to their earlier stats.
        /// </summary>
        /// <param name="session">The session that was loaded.</param>
        /// <exception cref="InvalidOperationException">A round is being played.</exception>
        /// <exception cref="InvalidDataException">
        /// The session lists a word as valid that is not in the dictionary.
        /// </exception>
        public void LoadSession(GameSession session)
        {
            ArgumentNullException.ThrowIfNull(session);
            EnsureNoRoundActive("A session cannot be loaded while a round is being played.");

            foreach (GameRound round in session.Rounds)
            {
                foreach (ValidWordAttempt validWord in round.ValidWords)
                {
                    if (!_dictionary.Contains(validWord.Word))
                    {
                        throw new InvalidDataException(
                            $"Round {round.RoundNumber} lists \"{validWord.Word}\" as a valid word, "
                            + "but it is not in the dictionary.");
                    }
                }
            }

            Session = session;
            ResetBoard();
        }
        /// <summary>
        /// Throws away the current stats and starts over as a brand new game with
        /// the default one minute timer.
        /// </summary>
        /// <exception cref="InvalidOperationException">A round is being played.</exception>
        public void ResetSession()
        {
            EnsureNoRoundActive("The session cannot be reset while a round is being played.");

            Session = new GameSession();
            ResetBoard();
        }
        /// <summary>
        /// Draws seven letters, drawing again if no dictionary word can be made from
        /// them so the player is never dealt a round that cannot be scored.
        /// </summary>
        /// <returns>The seven letters for the round.</returns>
        private List<char> DrawPlayableLetters()
        {
            List<char> letters = _letterBag.DrawLetters();
            int attempts = 1;

            while (!_dictionary.HasPlayableWord(new string(letters.ToArray())) && attempts < MaxDrawAttempts)
            {
                letters = _letterBag.DrawLetters();
                attempts++;
            }

            return letters;
        }
        /// <summary>
        /// Finishes the current round and adds it to the session.
        /// </summary>
        /// <param name="completed">true if the clock ran out; false if the player quit early.</param>
        /// <returns>The round that was ended.</returns>
        private GameRound EndRound(bool completed)
        {
            GameRound round = EnsureRoundActive();

            if (completed)
            {
                round.MarkCompleted();
            }

            ClearSelection();
            Session.AddRound(round);
            CurrentRound = null;
            return round;
        }
        /// <summary>
        /// Clears the tiles, the word being built, and the clock.
        /// </summary>
        private void ResetBoard()
        {
            _tiles.Clear();
            _selectedTiles.Clear();
            TimeRemainingSeconds = 0;
        }
        /// <summary>
        /// Gets the letters on the board, in display order, as one string.
        /// </summary>
        /// <returns>The board letters.</returns>
        private string GetBoardLetters()
        {
            return new string(_tiles.Select(tile => tile.Letter).ToArray());
        }
        /// <summary>
        /// Shuffles the tiles using the Fisher-Yates shuffle: walk backward through
        /// the list and swap each tile with a randomly chosen tile at or before it.
        /// </summary>
        private void ShuffleTiles()
        {
            for (int index = _tiles.Count - 1; index > 0; index--)
            {
                int swapIndex = _random.Next(index + 1);
                LetterTile temporary = _tiles[index];
                _tiles[index] = _tiles[swapIndex];
                _tiles[swapIndex] = temporary;
            }
        }
        /// <summary>
        /// Makes sure a round is being played and hands it back.
        /// </summary>
        /// <returns>The round being played.</returns>
        /// <exception cref="InvalidOperationException">No round is being played.</exception>
        private GameRound EnsureRoundActive()
        {
            if (CurrentRound == null)
            {
                throw new InvalidOperationException("There is no round being played.");
            }

            return CurrentRound;
        }
        /// <summary>
        /// Makes sure no round is being played.
        /// </summary>
        /// <param name="message">The message to use if a round is being played.</param>
        /// <exception cref="InvalidOperationException">A round is being played.</exception>
        private void EnsureNoRoundActive(string message)
        {
            if (CurrentRound != null)
            {
                throw new InvalidOperationException(message);
            }
        }
    }
}