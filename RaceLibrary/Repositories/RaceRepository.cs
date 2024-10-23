using RaceLibrary.Common.Results;
using RaceLibrary.Converters;
using RaceLibrary.Helpers;
using RaceLibrary.Models;
using System;
using System.Collections.Concurrent;
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

        public async Task<RaceLoadResult> GetAllRacesAsync(string raceDataDirectory)
        {
            var files = FileHelper.GetFilesWithExtension(raceDataDirectory, ".json");
            // Organise results into successful results and failures for transparency
            ConcurrentBag<Race?> races = [];
            ConcurrentBag<(string, Exception)> failures = [];
            var raceReadTasks = files.Select(
                async file => {
                    try
                    {
                        var race = await DeserializeFileAsync(file);
                        races.Add(race);
                    }
                    catch (Exception ex)
                    {
                        failures.Add((file, ex));
                    }
                }
            );

            await Task.WhenAll(raceReadTasks);

            return new RaceLoadResult(races.ToArray(), failures.ToArray());
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
