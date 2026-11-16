namespace DonovanColemanProject2.Model
{
    /// <summary>
    /// The bag of letters that the seven letters for each round are drawn from.
    /// Common letters are in the bag more times than rare ones, so they are drawn
    /// more often.
    /// </summary>
    public class LetterBag
    {
        /// <summary>The number of letters dealt for each round.</summary>
        public const int LettersPerRound = 7;

        /// <summary>
        /// How many copies of each letter go into the bag, taken from the project
        /// instructions.
        /// </summary>
        private static readonly Dictionary<char, int> LetterFrequencies = new Dictionary<char, int>
        {
            { 'e', 11 },
            { 't', 9 },
            { 'o', 8 },
            { 'a', 6 }, { 'i', 6 }, { 'n', 6 }, { 's', 6 },
            { 'h', 5 }, { 'r', 5 },
            { 'l', 4 },
            { 'd', 3 }, { 'u', 3 }, { 'w', 3 }, { 'y', 3 },
            { 'b', 2 }, { 'c', 2 }, { 'f', 2 }, { 'g', 2 }, { 'm', 2 }, { 'p', 2 }, { 'v', 2 },
            { 'j', 1 }, { 'k', 1 }, { 'q', 1 }, { 'x', 1 }, { 'z', 1 }
        };

        private readonly Random _random;

        /// <summary>
        /// Creates a letter bag.
        /// </summary>
        /// <param name="random">The random number generator used to draw letters.</param>
        public LetterBag(Random random)
        {
            ArgumentNullException.ThrowIfNull(random);
            _random = random;
        }

        /// <summary>
        /// Gets how many letters are in a full bag.
        /// </summary>
        public static int FullBagCount
        {
            get { return LetterFrequencies.Values.Sum(); }
        }

        /// <summary>
        /// Fills a fresh bag and draws seven letters from it. A letter that is drawn
        /// is taken out of the bag, so a letter can never be drawn more times than
        /// it appears in the bag.
        /// </summary>
        /// <returns>The seven letters that were drawn, in the order drawn.</returns>
        public List<char> DrawLetters()
        {
            List<char> bag = FillBag();
            List<char> drawnLetters = new List<char>();

            for (int count = 0; count < LettersPerRound; count++)
            {
                int position = _random.Next(bag.Count);
                drawnLetters.Add(bag[position]);
                bag.RemoveAt(position);
            }

            return drawnLetters;
        }

        /// <summary>
        /// Builds a full bag with every letter repeated as many times as its frequency.
        /// </summary>
        /// <returns>A list holding every letter in the bag.</returns>
        private static List<char> FillBag()
        {
            List<char> bag = new List<char>();
            foreach (KeyValuePair<char, int> letterFrequency in LetterFrequencies)
            {
                for (int copy = 0; copy < letterFrequency.Value; copy++)
                {
                    bag.Add(letterFrequency.Key);
                }
            }

            return bag;
        }
    }
}
