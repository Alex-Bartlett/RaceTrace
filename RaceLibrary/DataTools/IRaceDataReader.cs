﻿using RaceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceLibrary.DataTools
{
    public interface IRaceDataReader
    {
        /// <summary>
        /// Path for a directory containing race data json files
        /// </summary>
        public string DataDirectory { get; set; }
        /// <summary>
        /// Reads all JSON files in the DataDirectory and maps them to Races
        /// </summary>
        /// <returns>All races found in the directory</returns>
        public Task<IEnumerable<Race>> ReadAllFiles();

    }
}
