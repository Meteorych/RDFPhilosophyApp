using System.Windows;
using System.Windows.Controls;

namespace RDFPhilosophyApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _viewModel;
        public MainWindow()
        {
            _viewModel = new MainWindowViewModel("bolt://localhost:7687");
            DataContext = _viewModel;
            InitializeComponent();
        }

        private void ShowDataButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.GetData();
        }

        private void ShowPhilosophersButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.GetPhilosophersOnlyData();
            DataView.UpdateLayout();
        }
    }
}