using DonovanColemanProject2.Model;

namespace DonovanColemanProject2.Views
{
    /// <summary>
    /// Small helpers for showing game details on the screen. They are shared by
    /// more than one window so that times and word lists look the same everywhere.
    /// </summary>
    internal static class DisplayHelper
    {
        private const int SecondsPerMinute = 60;
        /// <summary>
        /// Formats a number of seconds as minutes and seconds, such as 1:05.
        /// </summary>
        /// <param name="totalSeconds">The number of seconds (negative values are shown as 0:00).</param>
        /// <returns>The time written as m:ss.</returns>
        public static string FormatTime(int totalSeconds)
        {
            int safeSeconds = Math.Max(0, totalSeconds);
            int minutes = safeSeconds / SecondsPerMinute;
            int seconds = safeSeconds % SecondsPerMinute;
            return $"{minutes}:{seconds:00}";
        }
        /// <summary>
        /// Adds a valid word to a list view as a row showing the word, its points,
        /// and the game time it was entered at.
        /// </summary>
        /// <param name="listView">The list view to add the row to.</param>
        /// <param name="validAttempt">The valid word to show.</param>
        public static void AddWordRow(ListView listView, ValidWordAttempt validAttempt)
        {
            ArgumentNullException.ThrowIfNull(listView);
            ArgumentNullException.ThrowIfNull(validAttempt);

            ListViewItem row = new ListViewItem(validAttempt.Word.ToUpperInvariant());
            row.SubItems.Add(validAttempt.Points.ToString());
            row.SubItems.Add(FormatTime(validAttempt.GameTimeSeconds));
            listView.Items.Add(row);
            row.EnsureVisible();
        }
    }
}