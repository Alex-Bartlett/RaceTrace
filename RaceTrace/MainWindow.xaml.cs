using Microsoft.Win32;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OxyPlot;
using OxyPlot.Series;

namespace RaceTrace
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string? FolderPath { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChangeText(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog openFolderDialog = new();
            openFolderDialog.ShowDialog();
            openFolderDialog.FolderOk += (s,e) => DoThing(s,e);

        }

        private void DoThing(object? sender, CancelEventArgs e)
        {
            if (sender is null) return;
            BindingExpression binding = txtFolderPath.GetBindingExpression(TextBox.TextProperty);
            FolderPath = sender.ToString() ?? "No folder selected";
        }
    }
}