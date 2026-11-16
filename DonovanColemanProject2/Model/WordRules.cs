namespace DonovanColemanProject2.Model
{
    /// <summary>
    /// Holds the rules that every word in the game has to follow: how long a word
    /// may be, how many points a word is worth, and whether a word can be built
    /// from a given set of letters.
    /// </summary>
    public static class WordRules
    {
        /// <summary>The fewest letters a word may have before it can be submitted.</summary>
        public const int MinimumWordLength = 3;
        /// <summary>The most letters a word may have (one for every letter on the board).</summary>
        public const int MaximumWordLength = 7;
        /// <summary>
        /// Each letter is worth ten points times the length of the word, so longer
        /// words are worth more per letter (3 letters = 30 each, 7 letters = 70 each).
        /// </summary>
        private const int PointsPerLetterMultiplier = 10;
        /// <summary>
        /// Checks that a word is made of only the lowercase letters a-z and is an
        /// allowed length.
        /// </summary>
        /// <param name="word">The word to check.</param>
        /// <returns>true if the word is well formed; otherwise false.</returns>
        public static bool IsWellFormed(string? word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return false;
            }

            if (word.Length < MinimumWordLength || word.Length > MaximumWordLength)
            {
                return false;
            }

            return word.All(IsGameLetter);
        }
        /// <summary>
        /// Checks that a character is one of the lowercase letters a-z.
        /// </summary>
        /// <param name="letter">The character to check.</param>
        /// <returns>true if the character is a-z; otherwise false.</returns>
        public static bool IsGameLetter(char letter)
        {
            return letter >= 'a' && letter <= 'z';
        }
        /// <summary>
        /// Calculates the points earned for a valid word of the given length.
        /// </summary>
        /// <param name="wordLength">The number of letters in the word (3 to 7).</param>
        /// <returns>90, 160, 250, 360, or 490 points for lengths 3 through 7.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The length is not 3 to 7.</exception>
        public static int CalculatePoints(int wordLength)
        {
            if (wordLength < MinimumWordLength || wordLength > MaximumWordLength)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(wordLength),
                    $"Word length must be between {MinimumWordLength} and {MaximumWordLength}.");
            }

            int pointsPerLetter = wordLength * PointsPerLetterMultiplier;
            return wordLength * pointsPerLetter;
        }
        /// <summary>
        /// Checks whether a word can be spelled from a set of letters when each
        /// letter in the set may be used only once.
        /// </summary>
        /// <param name="word">The word to spell.</param>
        /// <param name="availableLetters">The letters that may be used.</param>
        /// <returns>true if the word can be spelled; otherwise false.</returns>
        public static bool CanBeFormedFrom(string word, string availableLetters)
        {
            ArgumentNullException.ThrowIfNull(word);
            ArgumentNullException.ThrowIfNull(availableLetters);

            List<char> remainingLetters = availableLetters.ToList();
            foreach (char letter in word)
            {
                // Remove returns false when the letter is not left in the list.
                if (!remainingLetters.Remove(letter))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
