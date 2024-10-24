using RaceLibrary.Common.Results;
using RaceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceLibrary.Repositories
{
    public interface IRaceRepository
    {
        /// <summary>
        /// Reads all JSON files in the <paramref name="raceDataDirectory"/> and maps them to Races./>
        /// </summary>
        /// <param name="raceDataDirectory">The directory to search within</param>
        /// <returns><see cref="RaceLoadResult"/> containing the successfully mapped races, and file paths and exceptions of those that failed.</returns>
        public Task<RaceLoadResult> GetAllRacesAsync(string raceDataDirectory);
    }
}
