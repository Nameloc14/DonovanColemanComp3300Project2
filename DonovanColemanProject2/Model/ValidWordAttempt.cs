using System.Text.Json.Serialization;

namespace DonovanColemanProject2.Model
{
    /// <summary>
    /// A submitted word that was found in the dictionary and scored.
    /// </summary>
    public class ValidWordAttempt : WordAttempt
    {
        /// <summary>
        /// Creates a valid word attempt.
        /// </summary>
        /// <param name="word">The word that was submitted.</param>
        /// <param name="gameTimeSeconds">The seconds left on the timer when it was submitted.</param>
        /// <param name="points">The points earned for the word.</param>
        /// <param name="roundMaxTimeSeconds">The full length of the round the word was played in.</param>
        /// <exception cref="ArgumentException">
        /// The points do not match the word length, the round length is not an allowed
        /// timer setting, or the game time is longer than the round.
        /// </exception>
        [JsonConstructor]
        public ValidWordAttempt(string word, int gameTimeSeconds, int points, int roundMaxTimeSeconds)
            : base(word, gameTimeSeconds)
        {
            int expectedPoints = WordRules.CalculatePoints(Word.Length);
            if (points != expectedPoints)
            {
                throw new ArgumentException(
                    $"The word \"{Word}\" should be worth {expectedPoints} points, not {points}.",
                    nameof(points));
            }

            if (!RoundTimerOptions.IsValid(roundMaxTimeSeconds))
            {
                throw new ArgumentException(
                    $"The round max time for \"{Word}\" ({roundMaxTimeSeconds}) must be 60, 120, or 180 seconds.",
                    nameof(roundMaxTimeSeconds));
            }

            if (GameTimeSeconds > roundMaxTimeSeconds)
            {
                throw new ArgumentException(
                    $"The game time for \"{Word}\" cannot be longer than the round max time.",
                    nameof(gameTimeSeconds));
            }

            Points = points;
            RoundMaxTimeSeconds = roundMaxTimeSeconds;
        }
        /// <summary>Gets the points earned for the word.</summary>
        public int Points { get; }
        /// <summary>Gets the full length, in seconds, of the round the word was played in.</summary>
        public int RoundMaxTimeSeconds { get; }
    }
}
