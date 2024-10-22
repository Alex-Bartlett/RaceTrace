using RaceLibrary.Models;
using System.IO;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using RaceLibrary.Converters;

namespace RaceLibrary.DataTools
{
    public class RaceDataReader : IRaceDataReader
    {
        public string DataDirectory { get; set; }

        public RaceDataReader(string dataPath)
        {
            ValidateDirectory(dataPath);
            DataDirectory = dataPath;
        }

        public async Task<IEnumerable<Race?>> ReadAllFiles()
        {
            var files = GetFiles();
            var raceReadTasks = files.Select(f => DeserializeFileAsync(f)).ToArray();
            return await Task.WhenAll(raceReadTasks);
        }

        private async Task<Race?> DeserializeFileAsync(string filePath)
        {
            var serializerOptions = new JsonSerializerOptions()
            {
                Converters = { new RaceConverter() }
            };

            // https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/deserialization
            using FileStream fileStream = File.OpenRead(filePath);
            var race = await JsonSerializer.DeserializeAsync<Race>(fileStream, serializerOptions);
            if (race is null)
            {
                Console.Error.WriteLine("Failed to deserialize file at {0}", filePath);
            }
            return race;
        }

        private List<string> GetFiles()
        {
            return Directory.EnumerateFiles(DataDirectory).ToList();
        }

        private void ValidateDirectory(string dataPath)
        {
            if (string.IsNullOrWhiteSpace(dataPath))
            {
                throw new ArgumentException("Directory cannot be null or empty", nameof(dataPath));
            }
            if (!Directory.Exists(dataPath))
            {
                throw new ArgumentException("Directory does not exist", nameof(dataPath));
            }
            var files = Directory.EnumerateFiles(dataPath);
            if (!files.Any())
            {
                throw new ArgumentException("Directory is empty", nameof(dataPath));
            }
            if (!files.Any(f => f.EndsWith(".json")))
            {
                throw new ArgumentException("No valid files found in directory", nameof(dataPath));
            }
        }
    }
}
