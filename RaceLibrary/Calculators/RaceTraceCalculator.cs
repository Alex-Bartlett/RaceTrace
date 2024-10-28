using RaceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceLibrary.Calculators
{
    public static class RaceTraceCalculator
    {
        public static (string driver, TimeSpan[] lapTimes)[] CalculatorRaceTrace(Race race)
        {
            Dictionary<string, TimeSpan[]> driverLapTimes = new();
            var laps = race.Laps.OrderBy(l => l.LapNumber);
            return [];
        }
    }
}
