using System.Text.Json.Serialization;

namespace DonovanColemanProject2.Model
{
    /// <summary>
    /// Everything that happened in one round of the game: the letters that were
    /// dealt, how long the round was, and every valid and invalid word the player
    /// submitted.
    /// </summary>
    public class GameRound
    {
        private readonly List<ValidWordAttempt> _validWords;
        private readonly List<InvalidWordAttempt> _invalidWords;
        /// <summary>
        /// Creates a brand new round that has no words submitted yet.
        /// </summary>
        /// <param name="roundNumber">The number of the round, starting at 1.</param>
        /// <param name="maxTimeSeconds">The full length of the round in seconds.</param>
        /// <param name="letters">The seven letters dealt for the round.</param>
        public GameRound(int roundNumber, int maxTimeSeconds, string letters)
            : this(roundNumber, maxTimeSeconds, letters, false,
                  new List<ValidWordAttempt>(), new List<InvalidWordAttempt>())
        {
        }
        /// <summary>
        /// Creates a round from saved details. This is the constructor used when a
        /// previous session is loaded from a file, so every value is checked.
        /// </summary>
        /// <param name="roundNumber">The number of the round, starting at 1.</param>
        /// <param name="maxTimeSeconds">The full length of the round in seconds.</param>
        /// <param name="letters">The seven letters dealt for the round.</param>
        /// <param name="isCompleted">true if the round was played until the timer ran out.</param>
        /// <param name="validWords">The valid words submitted in the round.</param>
        /// <param name="invalidWords">The invalid words submitted in the round.</param>
        /// <exception cref="ArgumentException">Any of the values do not make sense for a round.</exception>
        [JsonConstructor]
        public GameRound(
            int roundNumber,
            int maxTimeSeconds,
            string letters,
            bool isCompleted,
            IReadOnlyList<ValidWordAttempt> validWords,
            IReadOnlyList<InvalidWordAttempt> invalidWords)
        {
            if (roundNumber < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(roundNumber), "A round number must be 1 or higher.");
            }

            if (!RoundTimerOptions.IsValid(maxTimeSeconds))
            {
                throw new ArgumentException(
                    $"Round {roundNumber} has a max time of {maxTimeSeconds}, but it must be 60, 120, or 180 seconds.",
                    nameof(maxTimeSeconds));
            }

            string normalizedLetters = (letters ?? string.Empty).Trim().ToLowerInvariant();
            if (normalizedLetters.Length != LetterBag.LettersPerRound
                || !normalizedLetters.All(WordRules.IsGameLetter))
            {
                throw new ArgumentException(
                    $"Round {roundNumber} must have exactly {LetterBag.LettersPerRound} letters from a-z.",
                    nameof(letters));
            }

            if (validWords == null)
            {
                throw new ArgumentNullException(
                    nameof(validWords), $"Round {roundNumber} is missing its list of valid words.");
            }

            if (invalidWords == null)
            {
                throw new ArgumentNullException(
                    nameof(invalidWords), $"Round {roundNumber} is missing its list of invalid words.");
            }

            RoundNumber = roundNumber;
            MaxTimeSeconds = maxTimeSeconds;
            Letters = normalizedLetters;
            IsCompleted = isCompleted;
            _validWords = new List<ValidWordAttempt>();
            _invalidWords = new List<InvalidWordAttempt>();

            foreach (ValidWordAttempt validWord in validWords)
            {
                AddCheckedValidWord(validWord);
            }

            foreach (InvalidWordAttempt invalidWord in invalidWords)
            {
                AddCheckedInvalidWord(invalidWord);
            }
        }
        /// <summary>Gets the number of the round, starting at 1.</summary>
        public int RoundNumber { get; }

        /// <summary>Gets the full length of the round in seconds (the timer setting).</summary>
        public int MaxTimeSeconds { get; }
        /// <summary>Gets the seven letters that were dealt for the round, in lowercase.</summary>
        public string Letters { get; }
        /// <summary>
        /// Gets a value that is true if the round was played until the timer ran out
        /// and false if the player ended it early.
        /// </summary>
        public bool IsCompleted { get; private set; }
        /// <summary>Gets the total points earned in the round.</summary>
        public int TotalScore
        {
            get { return _validWords.Sum(validWord => validWord.Points); }
        }
        /// <summary>Gets the valid words submitted in the round, in the order entered.</summary>
        public IReadOnlyList<ValidWordAttempt> ValidWords
        {
            get { return _validWords; }
        }
        /// <summary>Gets the invalid words submitted in the round, in the order entered.</summary>
        public IReadOnlyList<InvalidWordAttempt> InvalidWords
        {
            get { return _invalidWords; }
        }
        /// <summary>
        /// Checks whether a word has already been scored in this round.
        /// </summary>
        /// <param name="word">The word to look for.</param>
        /// <returns>true if the word is already in the valid word list; otherwise false.</returns>
        public bool HasValidWord(string word)
        {
            ArgumentNullException.ThrowIfNull(word);

            string normalizedWord = word.Trim().ToLowerInvariant();
            return _validWords.Any(validWord => validWord.Word == normalizedWord);
        }
        /// <summary>
        /// Records a valid word and scores it.
        /// </summary>
        /// <param name="word">The word that was submitted.</param>
        /// <param name="gameTimeSeconds">The seconds left on the timer when it was submitted.</param>
        /// <returns>The attempt that was recorded, including the points earned.</returns>
        /// <exception cref="ArgumentException">The word cannot be made from the round letters.</exception>
        /// <exception cref="InvalidOperationException">The word was already scored this round.</exception>
        public ValidWordAttempt AddValidWord(string word, int gameTimeSeconds)
        {
            ArgumentNullException.ThrowIfNull(word);

            int points = WordRules.CalculatePoints(word.Trim().Length);
            ValidWordAttempt attempt = new ValidWordAttempt(word, gameTimeSeconds, points, MaxTimeSeconds);
            if (HasValidWord(attempt.Word))
            {
                throw new InvalidOperationException(
                    $"The word \"{attempt.Word}\" has already been scored this round.");
            }

            AddCheckedValidWord(attempt);
            return attempt;
        }
        /// <summary>
        /// Records a word that did not score.
        /// </summary>
        /// <param name="word">The word that was submitted.</param>
        /// <param name="gameTimeSeconds">The seconds left on the timer when it was submitted.</param>
        /// <param name="reason">Why the word was not accepted.</param>
        /// <returns>The attempt that was recorded.</returns>
        /// <exception cref="ArgumentException">The word cannot be made from the round letters.</exception>
        public InvalidWordAttempt AddInvalidWord(string word, int gameTimeSeconds, InvalidWordReason reason)
        {
            InvalidWordAttempt attempt = new InvalidWordAttempt(word, gameTimeSeconds, reason);
            AddCheckedInvalidWord(attempt);
            return attempt;
        }
        /// <summary>
        /// Marks the round as having been played until the timer ran out.
        /// </summary>
        public void MarkCompleted()
        {
            IsCompleted = true;
        }
        /// <summary>
        /// Makes sure a valid word belongs in this round before storing it.
        /// </summary>
        /// <param name="attempt">The valid word to check and store.</param>
        private void AddCheckedValidWord(ValidWordAttempt attempt)
        {
            CheckAttemptFitsRound(attempt);

            if (attempt.RoundMaxTimeSeconds != MaxTimeSeconds)
            {
                throw new ArgumentException(
                    $"The word \"{attempt.Word}\" in round {RoundNumber} has a round max time of "
                    + $"{attempt.RoundMaxTimeSeconds}, but the round is {MaxTimeSeconds} seconds long.");
            }

            if (HasValidWord(attempt.Word))
            {
                throw new ArgumentException(
                    $"The word \"{attempt.Word}\" is listed as valid more than once in round {RoundNumber}.");
            }

            _validWords.Add(attempt);
        }
        /// <summary>
        /// Makes sure an invalid word belongs in this round before storing it.
        /// </summary>
        /// <param name="attempt">The invalid word to check and store.</param>
        private void AddCheckedInvalidWord(InvalidWordAttempt attempt)
        {
            CheckAttemptFitsRound(attempt);
            _invalidWords.Add(attempt);
        }
        /// <summary>
        /// Runs the checks that apply to both valid and invalid attempts. Because both
        /// kinds inherit from <see cref="WordAttempt"/>, one method can check either.
        /// </summary>
        /// <param name="attempt">The attempt to check.</param>
        private void CheckAttemptFitsRound(WordAttempt attempt)
        {
            if (attempt == null)
            {
                throw new ArgumentNullException(
                    nameof(attempt), $"Round {RoundNumber} contains an empty word entry.");
            }

            if (!WordRules.CanBeFormedFrom(attempt.Word, Letters))
            {
                throw new ArgumentException(
                    $"The word \"{attempt.Word}\" cannot be made from the letters of round {RoundNumber}.");
            }

            if (attempt.GameTimeSeconds > MaxTimeSeconds)
            {
                throw new ArgumentException(
                    $"The word \"{attempt.Word}\" in round {RoundNumber} has a game time longer than the round.");
            }
        }
    }
}