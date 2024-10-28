using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using RaceLibrary.Models;

namespace RaceTrace.PlotBuilders
{
    public static class RaceTracePlotBuilder
    {
        public static PlotModel CreateRaceTrace(Race race)
        {
            var model = new PlotModel()
            {
                Title = race.Name
            };
            model.Axes.Add(new TimeSpanAxis() { Title = "Lap Time", Position = AxisPosition.Left });
            model.Axes.Add(new LinearAxis() { Title = "Lap", Position = AxisPosition.Bottom });
            AddLapTimesToRace(model, race);
            model.Legends.Add(new Legend()
            {
                LegendPosition = LegendPosition.TopLeft
            });
            return model;
        }

        private static void AddLapTimesToRace(PlotModel model, Race race)
        {
            var driverLaps = GetDriverLapsFromRace(race);
            foreach (var driverLap in driverLaps)
            {
                var series = new LineSeries()
                {
                    Title = driverLap.Key
                };
                foreach (var lap in driverLap.Value)
                {
                    series.Points.Add(new DataPoint(lap.LapNumber, TimeSpanAxis.ToDouble(lap.LapTime.Total)));
                }
                model.Series.Add(series);
            }
        }

        private static Dictionary<string, List<Lap>> GetDriverLapsFromRace(Race race)
        {
            var driverLaps = new Dictionary<string, List<Lap>>();
            foreach (var lap in race.Laps)
            {
                // Add driver if missing
                if (!driverLaps.ContainsKey(lap.DriverId))
                {
                    driverLaps.Add(lap.DriverId, []);
                }
                var driverLap = driverLaps[lap.DriverId];
                driverLap.Add(lap);
            }

            return driverLaps;
        }
    }
}
