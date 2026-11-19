using System.Text.Json;
using System.Text.Json.Serialization;
using DonovanColemanProject2.Model;

namespace DonovanColemanProject2.Storage
{
    /// <summary>
    /// Exports the player's stats to a JSON file and loads a previously exported
    /// file back in so the player can continue where they left off.
    /// </summary>
    public static class SessionStorage
    {
        /// <summary>The file name suggested when exporting.</summary>
        public const string DefaultFileName = "TextTwistStats.json";
        /// <summary>
        /// The settings used for both writing and reading. Indenting makes the file
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
        /// <summary>
        /// Reads a previously exported stats file. Every value in the file is checked
        /// as it is read, so a file that was damaged or edited by hand is rejected
        /// with a message that says what is wrong.
        /// </summary>
        /// <param name="filePath">The path of the file to read.</param>
        /// <returns>The session stored in the file.</returns>
        /// <exception cref="InvalidDataException">
        /// The file is missing, cannot be read, is not valid JSON, or holds values
        /// that do not make sense. The message describes the problem.
        /// </exception>
        public static GameSession Load(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("A file path is required to load stats.", nameof(filePath));
            }

            if (!File.Exists(filePath))
            {
                throw new InvalidDataException("The stats file could not be found.");
            }

            try
            {
                string json = File.ReadAllText(filePath);
                if (string.IsNullOrWhiteSpace(json))
                {
                    throw new InvalidDataException("The stats file is empty.");
                }

                GameSession? session = JsonSerializer.Deserialize<GameSession>(json, SerializerOptions);
                if (session == null)
                {
                    throw new InvalidDataException("The stats file does not contain a session.");
                }

                return session;
            }
            catch (JsonException exception)
            {
                throw new InvalidDataException(
                    "The file is not a stats file in the expected JSON format. " + exception.Message, exception);
            }
            catch (ArgumentException exception)
            {
                // Thrown by the model classes when a value in the file fails their checks.
                throw new InvalidDataException(
                    "The stats file contains bad data. " + RemoveParameterSuffix(exception.Message), exception);
            }
            catch (Exception exception) when (exception is IOException
                || exception is UnauthorizedAccessException
                || exception is NotSupportedException)
            {
                throw new InvalidDataException("The stats file could not be opened. " + exception.Message, exception);
            }
        }
        /// <summary>
        /// Removes the " (Parameter 'name')" text that .NET adds to the end of argument
        /// exception messages, since it would not mean anything to the player.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <returns>The message without the parameter text.</returns>
        private static string RemoveParameterSuffix(string message)
        {
            int suffixStart = message.IndexOf(" (Parameter '", StringComparison.Ordinal);
            if (suffixStart < 0)
            {
                return message;
            }

            return message.Substring(0, suffixStart);
        }
    }
}