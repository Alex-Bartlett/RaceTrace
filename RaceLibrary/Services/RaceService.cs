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
using RaceLibrary.Repositories;
using System.Diagnostics;

namespace RaceLibrary.Services
{
    public class RaceService : IRaceService
    {
        /// <summary>
        /// Path for a directory containing race data JSON files
        /// </summary>
        public string DataDirectory { get; set; }

        private readonly IRaceRepository _raceRepository;
        private readonly ILogger<RaceService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RaceService"/> class.
        /// </summary>
        /// <param name="dataDirectory">Path for a directory containing race data JSON files</param>
        /// <param name="logger">Logger to log messages to</param>
        public RaceService(string dataDirectory, IRaceRepository raceRepository, ILogger<RaceService> logger)
        {
            DataDirectory = dataDirectory;
            _raceRepository = raceRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Race?>> ReadAllFilesAsync()
        {
            try
            {
                return await _raceRepository.GetAllRacesAsync(DataDirectory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get race data.");
                return [];
            }
    }
}
