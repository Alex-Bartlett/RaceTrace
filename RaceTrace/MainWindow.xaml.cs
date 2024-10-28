using System.Windows;
using Microsoft.Win32;
using OxyPlot;
using OxyPlot.Series;
using RaceLibrary.Models;
using RaceLibrary.Services;
using RaceTrace.ViewModels;

namespace RaceTrace
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public PlotModel PlotModel { get; private set; }

        //private IRaceService _raceService;

        public MainWindow(MainViewModel viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }

        private async void btnBrowse_Clicked(object sender, RoutedEventArgs e)
        {
            var folderDialog = new OpenFolderDialog();
            folderDialog.Multiselect = false;
            // This needs folder content validation and error handling, but I have not got enough time for this.
            if (folderDialog.ShowDialog() == true)
            {
                var folderPath = folderDialog.FolderName ?? "No folder selected"; // This MUST be changed for an actual implementation
                var viewModel = DataContext as MainViewModel;
                txt_SelectedFolder.Text = folderPath.Substring(folderPath.LastIndexOf('\\')); // Another compromise due to time constraints
                await viewModel!.LoadRacesFromFolderAsync(folderPath);
            }
        }

        private void lstFiles_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Load the new plot
            var viewModel = DataContext as MainViewModel;
            var race = lst_Files.SelectedItem as Race;
            if (race != null)
            {
                viewModel!.UpdateRaceTrace(race.Name);
                plot_RaceTrace.Model = viewModel.PlotModel;
            }
        }
    }
}