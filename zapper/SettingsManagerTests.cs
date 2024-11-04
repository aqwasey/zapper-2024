namespace SettingsManagerTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SettingsManager;
    using System.IO;

    [TestClass]
    public class SettingsManagerTests
    {
        [TestMethod]
        public void IsFeatureEnabled_ValidInput_ReturnsCorrectResult()
        {
            string settings = "10101010";
            bool result = SettingsManager.IsFeatureEnabled(settings, 3);
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IsFeatureEnabled_InvalidIndex_ThrowsException()
        {
            SettingsManager.IsFeatureEnabled("10101010", 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsFeatureEnabled_InvalidSettingsLength_ThrowsException()
        {
            SettingsManager.IsFeatureEnabled("10101", 3);
        }

        [TestMethod]
        public void ReadSettingsFromFile_ValidFile_ReturnsCorrectSettings()
        {
            string tempFilePath = Path.GetTempFileName();
            File.WriteAllText(tempFilePath, "11001100");

            string settings = SettingsManager.ReadSettingsFromFile(tempFilePath);

            Assert.AreEqual("11001100", settings);

            File.Delete(tempFilePath);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ReadSettingsFromFile_InvalidFile_ThrowsException()
        {
            string invalidFilePath = "nonexistent_file.txt";
            SettingsManager.ReadSettingsFromFile(invalidFilePath);
        }

        [TestMethod]
        public void WriteSettingsToFile_ValidInput_WritesCorrectly()
        {
            string tempFilePath = Path.GetTempFileName();
            string settings = "01010101";

            SettingsManager.WriteSettingsToFile(tempFilePath, settings);

            string readSettings = File.ReadAllText(tempFilePath);

            Assert.AreEqual(settings, readSettings);

            File.Delete(tempFilePath);
        }

        [TestMethod]
        [ExpectedException(typeof(IOException))]
        public void WriteSettingsToFile_InvalidFile_ThrowsException()
        {
            string invalidFilePath = "C:\\SystemVolumeInformation\\settings.txt"; // Assuming restricted access
            string settings = "01010101";

            SettingsManager.WriteSettingsToFile(invalidFilePath, settings);
        }
    }
}