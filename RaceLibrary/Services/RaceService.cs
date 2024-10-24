using RaceLibrary.Models;
using RaceLibrary.Helpers;
using RaceLibrary.Repositories;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace RaceLibrary.Services
{
    public class RaceService : IRaceService
    {
        private readonly IRaceRepository _raceRepository;
        private readonly ILogger<RaceService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RaceService"/> class.
        /// </summary>
        public RaceService(IRaceRepository raceRepository, ILogger<RaceService> logger)
        {
            _raceRepository = raceRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Race>> ReadAllFilesAsync(string directoryPath)
        {
            if (ValidateDirectoryPath(directoryPath))
            {
                var results = await _raceRepository.GetAllRacesAsync(directoryPath);
                foreach (var (filePath, exception) in results.Errors)
                {
                    _logger.LogError(exception, "Failed to read race data for file {filePath}", filePath);
                }
                return results.Races.Where(r => r is not null)!;
            }
            else
            {
                return [];
            }
        }

        private bool ValidateDirectoryPath(string directoryPath)
        {
            if (!FileHelper.IsValidDirectory(directoryPath, out var invalidReason))
            {
                _logger.LogError("Directory invalid - {invalidReason}", invalidReason);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
