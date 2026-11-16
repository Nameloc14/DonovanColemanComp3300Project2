using System.Text.Json.Serialization;

namespace DonovanColemanProject2.Model
{
    /// <summary>
    /// The player's stats for one sitting: the timer setting last chosen and every
    /// round that has been played. This is the object that is written out when the
    /// player exports their stats and read back in when a previous session is loaded.
    /// </summary>
    public class GameSession
    {
        private readonly List<GameRound> _rounds;
        private int _timerSeconds;
        /// <summary>
        /// Creates a brand new session with no rounds and the default one minute timer.
        /// </summary>
        public GameSession()
            : this(RoundTimerOptions.DefaultSeconds, new List<GameRound>())
        {
        }
        /// <summary>
        /// Creates a session from saved details. This is the constructor used when a
        /// previous session is loaded from a file, so every value is checked.
        /// </summary>
        /// <param name="timerSeconds">The timer setting last chosen by the player.</param>
        /// <param name="rounds">The rounds played, in order.</param>
        /// <exception cref="ArgumentException">The timer or the rounds are not valid.</exception>
        [JsonConstructor]
        public GameSession(int timerSeconds, IReadOnlyList<GameRound> rounds)
        {
            if (rounds == null)
            {
                throw new ArgumentNullException(nameof(rounds), "The list of rounds is missing.");
            }

            TimerSeconds = timerSeconds;
            _rounds = new List<GameRound>();

            foreach (GameRound round in rounds)
            {
                AddRound(round);
            }
        }
        /// <summary>
        /// Gets or sets the count-down timer length, in seconds, used for new rounds.
        /// </summary>
        /// <exception cref="ArgumentException">The value is not 60, 120, or 180.</exception>
        public int TimerSeconds
        {
            get
            {
                return _timerSeconds;
            }

            set
            {
                if (!RoundTimerOptions.IsValid(value))
                {
                    throw new ArgumentException(
                        $"The timer setting ({value}) must be 60, 120, or 180 seconds.", nameof(value));
                }

                _timerSeconds = value;
            }
        }
        /// <summary>Gets the rounds that have been played, in the order played.</summary>
        public IReadOnlyList<GameRound> Rounds
        {
            get { return _rounds; }
        }
        /// <summary>
        /// Gets a value that is true once at least one round has been played, which
        /// is when there are stats worth exporting.
        /// </summary>
        [JsonIgnore]
        public bool HasRounds
        {
            get { return _rounds.Count > 0; }
        }
        /// <summary>
        /// Gets the number the next round should be given.
        /// </summary>
        [JsonIgnore]
        public int NextRoundNumber
        {
            get { return _rounds.Count + 1; }
        }
        /// <summary>
        /// Adds a finished round to the session.
        /// </summary>
        /// <param name="round">The round to add.</param>
        /// <exception cref="ArgumentException">The round number is not the next one in order.</exception>
        public void AddRound(GameRound round)
        {
            if (round == null)
            {
                throw new ArgumentNullException(nameof(round), "The session contains an empty round entry.");
            }

            if (round.RoundNumber != NextRoundNumber)
            {
                throw new ArgumentException(
                    $"Round numbers must count up from 1 in order. Expected round {NextRoundNumber} "
                    + $"but found round {round.RoundNumber}.",
                    nameof(round));
            }

            _rounds.Add(round);
        }
    }
}