using DonovanColemanProject2.Model;
using DonovanColemanProject2.Storage;
using DonovanColemanProject2.Views;

namespace DonovanColemanProject2
{
    /// <summary>
    /// Starts the Text Twist application.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application. The dictionary is loaded before
        /// the game window opens because the game cannot be played without it.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            WordDictionary dictionary;
            try
            {
                dictionary = DictionaryLoader.Load(DictionaryLoader.DefaultFilePath);
            }
            catch (Exception exception) when (exception is FileNotFoundException
                || exception is InvalidDataException)
            {
                MessageBox.Show(
                    "Text Twist cannot start because the word dictionary could not be loaded."
                    + Environment.NewLine + Environment.NewLine
                    + exception.Message
                    + Environment.NewLine + Environment.NewLine
                    + "Expected file: " + DictionaryLoader.DefaultFilePath,
                    "Text Twist by Coleman",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            Application.Run(new MainForm(dictionary));
        }
    }
}