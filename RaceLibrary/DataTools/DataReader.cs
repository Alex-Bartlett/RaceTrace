using System.IO;
using System.Text.Json;
using RaceLibrary.Models;

namespace RaceLibrary.DataTools
{
    public class DataReader
    {
        public string DirectoryPath { get; set; }

        public DataReader(string directoryPath)
        {
            ValidateDirectory(directoryPath);
            DirectoryPath = directoryPath;
        }

        public async Task<IEnumerable<Race?>> ReadAllFiles()
        {
            var files = GetFiles();
            var raceReadTasks = files.Select(f => DeserializeFileAsync(f)).ToArray();
            return await Task.WhenAll(raceReadTasks);
        }

        private async Task<Race?> DeserializeFileAsync(string filePath)
        {
            // https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/deserialization
            using FileStream fileStream = File.OpenRead(filePath);
            var race = await JsonSerializer.DeserializeAsync<Race>(fileStream);
            if (race is null)
            {
                Console.Error.WriteLine("Failed to deserialize file at {0}", filePath);
            }
            return race;
        }

        private List<string> GetFiles()
        {
            return Directory.EnumerateFiles(DirectoryPath).ToList();
        }

        private void ValidateDirectory(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
            {
                throw new ArgumentException("Directory cannot be null or empty", nameof(directoryPath));
            }
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
