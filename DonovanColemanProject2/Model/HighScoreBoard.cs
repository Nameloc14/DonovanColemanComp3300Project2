namespace DonovanColemanProject2.Model
{
    /// <summary>
    /// The high score board. It keeps the best scores, decides whether a new score
    /// earns a place, and can hand the scores back sorted in different ways.
    /// </summary>
    public class HighScoreBoard
    {
        /// <summary>The most scores the board holds.</summary>
        public const int MaxEntries = 10;

        private readonly List<HighScoreEntry> _entries;
        /// <summary>
        /// Creates an empty high score board.
        /// </summary>
        public HighScoreBoard()
        {
            _entries = new List<HighScoreEntry>();
        }
        /// <summary>
        /// Creates a high score board from saved scores. If more scores are given
        /// than the board holds, only the best ones are kept.
        /// </summary>
        /// <param name="entries">The saved scores.</param>
        /// <exception cref="ArgumentException">The collection contains an empty entry.</exception>
        public HighScoreBoard(IEnumerable<HighScoreEntry> entries)
            : this()
        {
            ArgumentNullException.ThrowIfNull(entries);

            foreach (HighScoreEntry entry in entries)
            {
                if (entry == null)
                {
                    throw new ArgumentException("The high score list contains an empty entry.", nameof(entries));
                }

                _entries.Add(entry);
            }

            KeepBestEntries();
        }
        /// <summary>Gets the scores on the board, highest score first.</summary>
        public IReadOnlyList<HighScoreEntry> Entries
        {
            get { return _entries; }
        }
        /// <summary>
        /// Checks whether a score is good enough to go on the board. A score of zero
        /// never qualifies. When the board is full the score has to beat the lowest
        /// score already on it.
        /// </summary>
        /// <param name="score">The score to check.</param>
        /// <returns>true if the score earns a place; otherwise false.</returns>
        public bool Qualifies(int score)
        {
            if (score <= 0)
            {
                return false;
            }

            if (_entries.Count < MaxEntries)
            {
                return true;
            }

            int lowestScore = _entries.Min(entry => entry.Score);
            return score > lowestScore;
        }
        /// <summary>
        /// Adds a score to the board and drops the lowest score if the board is over full.
        /// </summary>
        /// <param name="entry">The score to add.</param>
        /// <exception cref="InvalidOperationException">The score is not high enough for the board.</exception>
        public void Add(HighScoreEntry entry)
        {
            ArgumentNullException.ThrowIfNull(entry);

            if (!Qualifies(entry.Score))
            {
                throw new InvalidOperationException("That score is not high enough for the high score board.");
            }

            _entries.Add(entry);
            KeepBestEntries();
        }
        /// <summary>
        /// Removes every score from the board.
        /// </summary>
        public void Reset()
        {
            _entries.Clear();
        }
        /// <summary>
        /// Gets the scores sorted by overall score, highest first.
        /// </summary>
        /// <returns>A new sorted list of the scores.</returns>
        public List<HighScoreEntry> GetSortedByScore()
        {
            return _entries
                .OrderByDescending(entry => entry.Score)
                .ToList();
        }
        /// <summary>
        /// Gets the scores with a two-level sort: first by the timer setting
        /// (shortest first), then by score (highest first) within each timer setting.
        /// </summary>
        /// <returns>A new sorted list of the scores.</returns>
        public List<HighScoreEntry> GetSortedByTimeThenScore()
        {
            return _entries
                .OrderBy(entry => entry.MaxTimeSeconds)
                .ThenByDescending(entry => entry.Score)
                .ToList();
        }
        /// <summary>
        /// Sorts the board highest score first and trims it down to the best scores.
        /// OrderByDescending keeps tied scores in the order they were added, so an
        /// older score stays ahead of a newer score with the same points.
        /// </summary>
        private void KeepBestEntries()
        {
            List<HighScoreEntry> bestEntries = _entries
                .OrderByDescending(entry => entry.Score)
                .Take(MaxEntries)
                .ToList();

            _entries.Clear();
            _entries.AddRange(bestEntries);
        }
    }
}
