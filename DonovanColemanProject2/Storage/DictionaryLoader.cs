using System.Text.Json;
using System.Text.Json.Serialization;
using DonovanColemanProject2.Model;

namespace DonovanColemanProject2.Storage
{
    /// <summary>
    /// Reads the provided dictionary.json file and turns it into a
    /// <see cref="WordDictionary"/> the game can use.
    /// </summary>
    public static class DictionaryLoader
    {
        /// <summary>The name of the dictionary file that ships with the game.</summary>
        public const string DefaultFileName = "dictionary.json";
        /// <summary>
        /// Gets the full path of the dictionary file that sits next to the program.
        /// </summary>
        public static string DefaultFilePath
        {
            get { return Path.Combine(AppContext.BaseDirectory, DefaultFileName); }
        }
        /// <summary>
        /// Loads the dictionary from a JSON file. The file is a list of groups, one
        /// for each starting letter, and each group has a list of words.
        /// </summary>
        /// <param name="filePath">The path of the dictionary file.</param>
        /// <returns>The dictionary holding every word in the file.</returns>
        /// <exception cref="FileNotFoundException">The file does not exist.</exception>
        /// <exception cref="InvalidDataException">The file could not be read or is not in the expected format.</exception>
        public static WordDictionary Load(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("A dictionary file path is required.", nameof(filePath));
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The dictionary file could not be found.", filePath);
            }

            try
            {
                string json = File.ReadAllText(filePath);
                List<DictionaryLetterGroup?>? letterGroups =
                    JsonSerializer.Deserialize<List<DictionaryLetterGroup?>>(json);

                if (letterGroups == null)
                {
                    throw new InvalidDataException("The dictionary file is empty.");
                }
                List<string> words = new List<string>();
                foreach (DictionaryLetterGroup? letterGroup in letterGroups)
                {
                    if (letterGroup?.Words != null)
                    {
                        words.AddRange(letterGroup.Words.Where(word => word != null));
                    }
                }
                return new WordDictionary(words);
            }
            catch (Exception exception) when (exception is JsonException
                || exception is IOException
                || exception is UnauthorizedAccessException
                || exception is ArgumentException)
            {
                throw new InvalidDataException(
                    "The dictionary file could not be read: " + exception.Message, exception);
            }
        }
        /// <summary>
        /// Matches the shape of one group in dictionary.json, for example
        /// { "letter": "a", "words": [ "aah", "aahed" ] }.
        /// </summary>
        private sealed class DictionaryLetterGroup
        {
            /// <summary>Gets or sets the letter every word in the group starts with.</summary>
            [JsonPropertyName("letter")]
            public string? Letter { get; set; }

            /// <summary>Gets or sets the words in the group.</summary>
            [JsonPropertyName("words")]
            public List<string>? Words { get; set; }
        }
    }
}
