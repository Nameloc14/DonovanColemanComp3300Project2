using System.Text.Json.Serialization;

namespace DonovanColemanProject2.Model
{
    /// <summary>
    /// A submitted word that did not score, along with the reason why.
    /// </summary>
    public class InvalidWordAttempt : WordAttempt
    {
        /// <summary>
        /// Creates an invalid word attempt.
        /// </summary>
        /// <param name="word">The word that was submitted.</param>
        /// <param name="gameTimeSeconds">The seconds left on the timer when it was submitted.</param>
        /// <param name="reason">Why the word was not accepted.</param>
        /// <exception cref="ArgumentException">The reason is not a known reason.</exception>
        [JsonConstructor]
        public InvalidWordAttempt(string word, int gameTimeSeconds, InvalidWordReason reason)
            : base(word, gameTimeSeconds)
        {
            if (!Enum.IsDefined(reason))
            {
                throw new ArgumentException(
                    $"The reason given for \"{Word}\" is not a known reason.",
                    nameof(reason));
            }

            Reason = reason;
        }
        /// <summary>Gets the reason the word was not accepted.</summary>
        public InvalidWordReason Reason { get; }
        /// <summary>
        /// Gets the reason written out as a sentence that can be shown to the player.
        /// It is left out of the exported file because <see cref="Reason"/> already says it.
        /// </summary>
        [JsonIgnore]
        public string ReasonDescription
        {
            get
            {
                switch (Reason)
                {
                    case InvalidWordReason.Duplicate:
                        return "already entered this round";
                    case InvalidWordReason.NotInDictionary:
                    default:
                        return "not found in the dictionary";
                }
            }
        }
    }
}
