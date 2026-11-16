namespace DonovanColemanProject2.Model
{
    /// <summary>
    /// One of the seven letters on the board. A tile remembers whether it has been
    /// used in the word the player is currently building, which is what lets a
    /// letter stay selected when the board is twisted.
    /// </summary>
    public class LetterTile
    {
        /// <summary>
        /// Creates a tile that has not been selected yet.
        /// </summary>
        /// <param name="letter">The lowercase letter (a-z) shown on the tile.</param>
        /// <exception cref="ArgumentException">The character is not a letter from a-z.</exception>
        public LetterTile(char letter)
        {
            if (!WordRules.IsGameLetter(letter))
            {
                throw new ArgumentException("A tile must hold a lowercase letter from a-z.", nameof(letter));
            }

            Letter = letter;
            IsSelected = false;
        }

        /// <summary>Gets the lowercase letter on the tile.</summary>
        public char Letter { get; }

        /// <summary>
        /// Gets or sets a value that is true while the tile is being used in the
        /// word the player is currently building.
        /// </summary>
        public bool IsSelected { get; set; }
    }
}
