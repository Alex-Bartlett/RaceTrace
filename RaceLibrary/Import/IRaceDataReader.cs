using RaceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceLibrary.Import
{
    public interface IRaceDataReader
    {
        public string DataPath { get; set; }
        public Task<IEnumerable<Race>> ReadAllRaceData();

    }
}
