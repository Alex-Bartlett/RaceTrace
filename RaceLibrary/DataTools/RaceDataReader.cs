using RaceLibrary.Models;
using System.IO;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using RaceLibrary.Converters;
using Microsoft.Extensions.Logging;

namespace RaceLibrary.DataTools
{
    public class RaceDataReader : IRaceDataReader
    {
        /// <summary>
        /// Path for a directory containing race data JSON files
        /// </summary>
        public string DataDirectory { get; set; }

        private ILogger<RaceDataReader> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RaceDataReader"/> class.
        /// </summary>
        /// <param name="dataDirectory">Path for a directory containing race data JSON files</param>
        /// <param name="logger">Logger to log messages to</param>
        public RaceDataReader(string dataDirectory, ILogger<RaceDataReader> logger)
        {
            ValidateDirectory(dataDirectory);
            DataDirectory = dataDirectory;
            _logger = logger;
        }

        public async Task<IEnumerable<Race?>> ReadAllFilesAsync()
        {
            var files = GetFiles();
            var raceReadTasks = files.Select(f => DeserializeFileAsync(f)).ToArray();
            return await Task.WhenAll(raceReadTasks);
        }

        /// <summary>
        /// Asynchronously deserializes a JSON file into a race
        /// </summary>
        private async Task<Race?> DeserializeFileAsync(string filePath)
        {
            var serializerOptions = new JsonSerializerOptions()
            {
                Converters = { new RaceConverter() }
            };

            // https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/deserialization
            using FileStream fileStream = File.OpenRead(filePath);
            try
            {
                var race = await JsonSerializer.DeserializeAsync<Race>(fileStream, serializerOptions);
                if (race is null)
                {
                    _logger.LogWarning("Failed to deserialize file at {filePath}", filePath);
                }
                return race;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to deserialize file at {filePath}", filePath);
                return null;
            }
        }

        private List<string> GetFiles()
        {
            return Directory.EnumerateFiles(DataDirectory).ToList();
        }

        /// <summary>
        /// Performs checks on a directory path to verify it exists and contains JSON files
        /// </summary>
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
