using System.IO;

namespace RaceLibrary.Import
{
    public class DataReader
    {
        public string DirectoryPath { get; set; }

        public DataReader(string directoryPath)
        {
            if (String.IsNullOrWhiteSpace(directoryPath))
            {
                throw new ArgumentException("Directory cannot be null or empty", nameof(directoryPath));
            }
            DirectoryPath = directoryPath;
        }

        private void ValidateDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                throw new ArgumentException("Directory does not exist", nameof(directoryPath));
            }
            var files = Directory.EnumerateFiles(directoryPath);
            if (!files.Any())
            {
                throw new ArgumentException("Directory is empty", nameof(directoryPath));
            }
            if (!files.Any(f => f.EndsWith(".json")))
            {
                throw new ArgumentException("No valid files found in directory", nameof(directoryPath));
            }
        }
    }
}
