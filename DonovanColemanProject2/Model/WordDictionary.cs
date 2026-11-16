namespace DonovanColemanProject2.Model
{
    /// <summary>
    /// The list of words the game accepts. Words are kept in a HashSet so that
    /// checking a word is fast no matter how many words there are.
    /// </summary>
    public class WordDictionary
    {
        private readonly HashSet<string> _words;
        /// <summary>
        /// Creates a dictionary from a collection of words.
        /// </summary>
        /// <param name="words">The words to accept. Blank entries are skipped.</param>
        /// <exception cref="ArgumentException">The collection holds no usable words.</exception>
        public WordDictionary(IEnumerable<string> words)
        {
            ArgumentNullException.ThrowIfNull(words);

            _words = new HashSet<string>();
            foreach (string word in words)
            {
                if (!string.IsNullOrWhiteSpace(word))
                {
                    _words.Add(word.Trim().ToLowerInvariant());
                }
            }

            if (_words.Count == 0)
            {
                throw new ArgumentException("The dictionary does not contain any words.", nameof(words));
            }
        }
        public int Count
        {
            get { return _words.Count; }
        }
        /// <summary>
        /// Checks whether a word is in the dictionary. Upper and lower case do not matter.
        /// </summary>
        /// <param name="word">The word to look up.</param>
        /// <returns>true if the word is in the dictionary; otherwise false.</returns>
        public bool Contains(string? word)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                return false;
            }

            return _words.Contains(word.Trim().ToLowerInvariant());
        }
        /// <summary>
        /// Checks whether at least one dictionary word can be spelled from a set of letters.
        /// </summary>
        /// <param name="letters">The letters available, each usable once.</param>
        /// <returns>true if any playable word can be made; otherwise false.</returns>
        public bool HasPlayableWord(string letters)
        {
            ArgumentNullException.ThrowIfNull(letters);

            string normalizedLetters = letters.ToLowerInvariant();
            return _words.Any(word => WordRules.IsWellFormed(word)
                && WordRules.CanBeFormedFrom(word, normalizedLetters));
        }
    }
}
