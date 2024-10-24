using RaceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceLibrary.Services
{
    public interface IRaceService
    {
        /// <summary>
        /// Reads all JSON files in the <paramref name="directoryPath"/> and maps them to <see cref="Race"/> objects.
        /// </summary>
        /// <returns>All races found in the directory</returns>
        public Task<IEnumerable<Race>> ReadAllFilesAsync(string directoryPath);

    }
}
