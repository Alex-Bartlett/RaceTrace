using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using RaceLibrary.Models;
using RaceLibrary.Services;
using RaceTrace.PlotBuilders;

namespace RaceTrace.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<Race> Races { get; set; }
        public PlotModel PlotModel { get; set; }
        private readonly IRaceService _raceService;
        public MainViewModel(IRaceService raceService)
        {
            _raceService = raceService;
            Races = [];
            PlotModel = new PlotModel();
        }

        public async Task LoadRacesFromFolderAsync(string folderPath)
        {
            var races = await _raceService.ReadAllFilesAsync(folderPath);
            if (races.Any())
            {
                Races.Clear();
            }
            foreach (var race in races)
            {
                Races.Add(race);
            }
        }
        public void UpdateRaceTrace(string raceName)
        {
            CreatePlotForRace(Races.Where(r => r.Name == raceName).First());
        }

        private void CreatePlotForRace(Race race)
        {
            PlotModel = RaceTracePlotBuilder.CreateRaceTrace(race);
            PlotModel.InvalidatePlot(true); // Refresh the plot
        }
    }
}
