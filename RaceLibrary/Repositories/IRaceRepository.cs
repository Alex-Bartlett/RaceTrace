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
        public Task<IEnumerable<Race?>> GetAllRacesAsync(string raceDataDirectory);
    }
}
