using System.Text.Json.Serialization;

namespace DonovanColemanProject2.Model
{
    /// <summary>
    /// One line on the high score board: who played, what they scored, and the
    /// timer setting they played with.
    /// </summary>
    public class HighScoreEntry
    {
        /// <summary>The most characters a player name may have.</summary>
        public const int MaxNameLength = 20;
        /// <summary>
        /// Creates a high score entry.
        /// </summary>
        /// <param name="name">The player's name.</param>
        /// <param name="score">The round score, which must be above zero.</param>
        /// <param name="maxTimeSeconds">The timer setting the round was played with.</param>
        /// <exception cref="ArgumentException">The name, score, or time is not valid.</exception>
        [JsonConstructor]
        public HighScoreEntry(string name, int score, int maxTimeSeconds)
        {
            string trimmedName = (name ?? string.Empty).Trim();
            if (!IsValidName(trimmedName))
            {
                throw new ArgumentException(
                    $"A high score name must be 1 to {MaxNameLength} characters and may only "
                    + "contain letters, spaces, hyphens, apostrophes, and periods.",
                    nameof(name));
            }

            if (score <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(score), "A high score must be above zero.");
            }

            if (!RoundTimerOptions.IsValid(maxTimeSeconds))
            {
                throw new ArgumentException(
                    "A high score time must be 60, 120, or 180 seconds.", nameof(maxTimeSeconds));
            }

            Name = trimmedName;
            Score = score;
            MaxTimeSeconds = maxTimeSeconds;
        }
        /// <summary>Gets the player's name.</summary>
        public string Name { get; }

        /// <summary>Gets the points scored in the round.</summary>
        public int Score { get; }
        /// <summary>Gets the timer setting, in seconds, the round was played with.</summary>
        public int MaxTimeSeconds { get; }
        /// <summary>
        /// Checks whether a name is allowed on the high score board. A name needs at
        /// least one letter and may only use letters, spaces, hyphens, apostrophes,
        /// and periods. Numbers and other symbols are not part of a name.
        /// </summary>
        /// <param name="name">The name to check.</param>
        /// <returns>true if the name is allowed; otherwise false.</returns>
        public static bool IsValidName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            string trimmedName = name.Trim();
            if (trimmedName.Length > MaxNameLength)
            {
                return false;
            }

            if (!trimmedName.Any(char.IsLetter))
            {
                return false;
            }

            return trimmedName.All(IsNameCharacter);
        }
        /// <summary>
        /// Checks whether a single character may be used in a player name.
        /// </summary>
        /// <param name="character">The character to check.</param>
        /// <returns>true if the character is allowed; otherwise false.</returns>
        private static bool IsNameCharacter(char character)
        {
            return char.IsLetter(character)
                || character == ' '
                || character == '-'
                || character == '\''
                || character == '.';
        }
    }
}
