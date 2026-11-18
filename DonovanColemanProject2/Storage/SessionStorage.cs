using System.Text.Json;
using System.Text.Json.Serialization;
using DonovanColemanProject2.Model;
namespace DonovanColemanProject2.Storage
{
    /// <summary>
    /// Exports the player's stats to a JSON file.
    /// </summary>
    public static class SessionStorage
    {
        /// <summary>The file name suggested when exporting.</summary>
        public const string DefaultFileName = "TextTwistStats.json";

        /// <summary>
        /// The settings used when writing the file. Indenting makes the file
        /// easy to read, and the enum converter writes reasons as words such as
        /// "Duplicate" instead of numbers.
        /// </summary>
        private static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };
        /// <summary>
        /// Writes every detail of the session (the timer setting and, for each round,
        /// all valid and invalid words) to a JSON file.
        /// </summary>
        /// <param name="session">The session to export.</param>
        /// <param name="filePath">The path of the file to write.</param>
        /// <exception cref="InvalidOperationException">There are no rounds to export.</exception>
        /// <exception cref="IOException">The file could not be written.</exception>
        public static void Export(GameSession session, string filePath)
        {
            ArgumentNullException.ThrowIfNull(session);
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("A file path is required to export stats.", nameof(filePath));
            }

            if (!session.HasRounds)
            {
                throw new InvalidOperationException("There are no rounds to export yet.");
            }

            try
            {
                string json = JsonSerializer.Serialize(session, SerializerOptions);
                File.WriteAllText(filePath, json);
            }
            catch (UnauthorizedAccessException exception)
            {
                // Rethrown as an IOException so callers only have one kind of failure to handle.
                throw new IOException("The stats file could not be written: " + exception.Message, exception);
            }
        }
    }
}