using System.Text.Json.Serialization;

namespace DonovanColemanProject2.Model
{
    /// <summary>
    /// The base class for every word a player submits. Valid and invalid words both
    /// have a word and a game time, so those shared pieces live here and the
    /// <see cref="ValidWordAttempt"/> and <see cref="InvalidWordAttempt"/> classes
    /// inherit them and add their own details.
    /// </summary>
    public abstract class WordAttempt
    {
        /// <summary>
        /// Sets up the parts shared by valid and invalid attempts.
        /// </summary>
        /// <param name="word">The word that was submitted (stored in lowercase).</param>
        /// <param name="gameTimeSeconds">
        /// The seconds left on the count-down timer when the word was submitted.
        /// </param>
        /// <exception cref="ArgumentException">The word is not 3 to 7 letters a-z.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The game time is negative.</exception>
        protected WordAttempt(string word, int gameTimeSeconds)
        {
            string normalizedWord = (word ?? string.Empty).Trim().ToLowerInvariant();
            if (!WordRules.IsWellFormed(normalizedWord))
            {
                throw new ArgumentException(
                    $"The word \"{word}\" must be {WordRules.MinimumWordLength} to "
                    + $"{WordRules.MaximumWordLength} letters and contain only the letters a-z.",
                    nameof(word));
            }

            if (gameTimeSeconds < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(gameTimeSeconds),
                    $"The game time for \"{normalizedWord}\" cannot be negative.");
            }

            Word = normalizedWord;
            GameTimeSeconds = gameTimeSeconds;
        }
        /// <summary>Gets the word that was submitted, in lowercase.</summary>
        [JsonPropertyOrder(-2)]
        public string Word { get; }
        /// <summary>
        /// Gets the game time of the attempt: the seconds that were left on the
        /// count-down timer when the word was submitted.
        /// </summary>
        [JsonPropertyOrder(-1)]
        public int GameTimeSeconds { get; }
    }
}
