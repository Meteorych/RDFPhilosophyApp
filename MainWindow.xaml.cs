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
        }

        private void GetPhilosophersByBranch_Click(object sender, RoutedEventArgs e)
        {
            
            InputBoxGetPhilosopherByBranch.Visibility = Visibility.Visible;
        }

        private void GetPhilosopherYesButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NoButton_Click(Object sender, RoutedEventArgs e)
        {
            var parentGrid = (sender as FrameworkElement)?.Parent as Grid;
            if ( parentGrid is not null)
            {
                parentGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void GetMethodsByBranch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeBirthYearButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void InsertNewPhilosopherButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}