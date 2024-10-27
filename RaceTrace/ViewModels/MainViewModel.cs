using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;
using RaceLibrary.Services;

namespace RaceTrace
{
    public class MainViewModel
    {
        public PlotModel MyModel { get; set; }
        private readonly IRaceService _raceService;
        public MainViewModel(IRaceService raceService)
        {
            _raceService = raceService;
            var races = _raceService.ReadAllFilesAsync("C:\\Users\\nitro\\Documents\\Coding\\RaceTrace\\Data").Result;
            Console.WriteLine(races.ToString());
            this.MyModel = new PlotModel { Title = "Example 1" };
            this.MyModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
        }
    }
}
