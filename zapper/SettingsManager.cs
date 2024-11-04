namespace SettingsManager
{
    /// <summary>
    /// Manages user settings and provides methods to check feature enablement and read/write settings to a file.
    /// </summary>
    public class SettingsManager
    {
        /// <summary>
        /// Checks if a specific feature is enabled based on the given settings string and index.
        ///
        /// <param name="settings">The settings string, where '1' indicates enabled and '0' indicates disabled.</param>
        /// <param name="settingIndex">The index of the feature to check (1-8).</param>
        /// <returns>True if the feature is enabled, false otherwise.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the setting index is invalid.</exception>
        /// <exception cref="ArgumentException">Thrown if the settings string is not 8 characters long.</exception>
        /// </summary>
        public static bool IsFeatureEnabled(string settings, int settingIndex)
        {
            // Validate the setting index
            if (settingIndex < 1 || settingIndex > 8)
            {
                throw new ArgumentOutOfRangeException(nameof(settingIndex), "Setting index must be between 1 and 8.");
            }

            // Validate the settings string length
            if (settings.Length != 8)
            {
                throw new ArgumentException("Settings string must be 8 characters long.");
            }

            // Check if the feature is enabled at the specified index
            return settings[settingIndex - 1] == '1';
        }

        /// <summary>
        /// Reads the settings from a specified file.
        ///
        /// <param name="filePath">The path to the settings file.</param>
        /// <returns>The settings string read from the file.</returns>
        /// </summary>
        public static string ReadSettingsFromFile(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Writes the settings to a specified file.
        ///
        /// <param name="filePath">The path to the settings file.</param>
        /// <param name="settings">The settings string to write to the file.</param>
        /// </summary>
        public static void WriteSettingsToFile(string filePath, string settings)
        {
            using (var writer = new StreamWriter(filePath))
            {
                writer.Write(settings);
            }
        }
    }
}