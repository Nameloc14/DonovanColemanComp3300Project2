using System.Text.Json;
using DonovanColemanProject2.Model;

namespace DonovanColemanProject2.Storage
{
    /// <summary>
    /// Saves the high score board to a JSON file and reads it back, which is what
    /// keeps the high scores around after the game is closed and opened again.
    /// </summary>
    public class HighScoreStorage
    {
        private const string ApplicationFolderName = "DonovanColemanProject2";
        private const string HighScoreFileName = "highscores.json";

        private static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        private readonly string _filePath;
        /// <summary>
        /// Creates storage that uses the default high score file in the current
        /// user's local application data folder.
        /// </summary>
        public HighScoreStorage()
            : this(DefaultFilePath)
        {
        }
        /// <summary>
        /// Creates storage that uses a specific file.
        /// </summary>
        /// <param name="filePath">The path of the high score file.</param>
        public HighScoreStorage(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("A high score file path is required.", nameof(filePath));
            }

            _filePath = filePath;
        }
        /// <summary>
        /// Gets the default path of the high score file. The local application data
        /// folder is used because a program is always allowed to write there.
        /// </summary>
        public static string DefaultFilePath
        {
            get
            {
                string localDataFolder = Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(localDataFolder, ApplicationFolderName, HighScoreFileName);
            }
        }
        /// <summary>
        /// Loads the high score board. If no scores have been saved yet an empty
        /// board is returned.
        /// </summary>
        /// <returns>The saved high score board.</returns>
        /// <exception cref="InvalidDataException">The file exists but could not be read.</exception>
        public HighScoreBoard Load()
        {
            if (!File.Exists(_filePath))
            {
                return new HighScoreBoard();
            }

            try
            {
                string json = File.ReadAllText(_filePath);
                List<HighScoreEntry>? entries =
                    JsonSerializer.Deserialize<List<HighScoreEntry>>(json, SerializerOptions);

                if (entries == null)
                {
                    return new HighScoreBoard();
                }

                return new HighScoreBoard(entries);
            }
            catch (Exception exception) when (exception is JsonException
                || exception is IOException
                || exception is UnauthorizedAccessException
                || exception is ArgumentException)
            {
                throw new InvalidDataException(
                    "The high score file could not be read: " + exception.Message, exception);
            }
        }
        /// <summary>
        /// Saves the high score board, replacing what was saved before.
        /// </summary>
        /// <param name="board">The board to save.</param>
        /// <exception cref="IOException">The file could not be written.</exception>
        public void Save(HighScoreBoard board)
        {
            ArgumentNullException.ThrowIfNull(board);

            try
            {
                string? folder = Path.GetDirectoryName(_filePath);
                if (!string.IsNullOrEmpty(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string json = JsonSerializer.Serialize(board.Entries, SerializerOptions);
                File.WriteAllText(_filePath, json);
            }
            catch (UnauthorizedAccessException exception)
            {
                // Rethrown as an IOException so callers only have one kind of failure to handle.
                throw new IOException("The high score file could not be written: " + exception.Message, exception);
            }
        }
    }
}
