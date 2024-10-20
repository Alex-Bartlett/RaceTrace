using RaceLibrary.Models;
using System.IO;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceLibrary.Import
{
    public class RaceDataReader : IRaceDataReader
    {
        public string DataPath { get; set; }

        public RaceDataReader(string dataPath)
        {
            DataPath = dataPath;
        }

        public async Task<IEnumerable<Race>> ReadAllRaceData()
        {
            throw new NotImplementedException();
        }

    }
}
