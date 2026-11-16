namespace DonovanColemanProject2.Model
{
    /// <summary>
    /// The reasons a submitted word can be turned down.
    /// </summary>
    public enum InvalidWordReason
    {
        /// <summary>The word is not in the provided dictionary.</summary>
        NotInDictionary,
        /// <summary>The word was already entered and scored earlier in the round.</summary>
        Duplicate
    }
}
