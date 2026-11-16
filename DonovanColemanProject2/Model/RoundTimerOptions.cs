namespace DonovanColemanProject2.Model
{
    /// <summary>
    /// Holds the count-down timer lengths the player is allowed to choose from.
    /// </summary>
    public static class RoundTimerOptions
    {
        /// <summary>One minute, which is the default round length.</summary>
        public const int OneMinuteSeconds = 60;
        /// <summary>Two minutes.</summary>
        public const int TwoMinutesSeconds = 120;
        /// <summary>Three minutes.</summary>
        public const int ThreeMinutesSeconds = 180;
        /// <summary>The round length used when the player has not picked one.</summary>
        public const int DefaultSeconds = OneMinuteSeconds;
        /// <summary>
        /// Checks whether a number of seconds is one of the allowed round lengths.
        /// </summary>
        /// <param name="seconds">The round length to check.</param>
        /// <returns>true for 60, 120, or 180; otherwise false.</returns>
        public static bool IsValid(int seconds)
        {
            return seconds == OneMinuteSeconds
                || seconds == TwoMinutesSeconds
                || seconds == ThreeMinutesSeconds;
        }
    }
}
