using RaceLibrary.Converters;
using RaceLibrary.Helpers;
using RaceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RaceLibrary.Repositories
{
    public class RaceRepository : IRaceRepository
    {
        private readonly JsonSerializerOptions serializerOptions = new()
        {
            Converters = { new RaceConverter() }
        };

        public async Task<IEnumerable<Race?>> GetAllRacesAsync(string raceDataDirectory)
        {
            var files = FileHelper.GetFilesWithExtension(raceDataDirectory, ".json");
            var raceReadTasks = files.Select(f => DeserializeFileAsync(f)).ToArray();
            return await Task.WhenAll(raceReadTasks);
        }

        /// <summary>
        /// Asynchronously deserializes a JSON file into a race
        /// </summary>
        private async Task<Race?> DeserializeFileAsync(string filePath)
        {
            // https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/deserialization
            using FileStream fileStream = File.OpenRead(filePath);
            return await JsonSerializer.DeserializeAsync<Race>(fileStream, serializerOptions);
        }
    }
}
