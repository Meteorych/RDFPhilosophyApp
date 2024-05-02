using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RDFPhilosophyApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string URI1 = @"http://www.semanticweb.org/user/ontologies/2024/3/philosophy#";

        /// <summary>
        /// URI2 is for philosophers (ns1__Philosopher) only.
        /// </summary>
        private const string URI2 = @"http://www.semanticweb.org/user/ontologies/2024/3/philosophy-2#";

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
            _viewModel.GetData(URI1 + InputBranchPhilosophersTextBox.Text, label2: ":ns0__Main_philosophers");
            NoButton_Click(sender, e);
        }

        private void NoButton_Click(Object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var parentGrid = FindParent<Grid>(button);
            if (parentGrid is not null)
            {
                parentGrid.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Techical static method to find type T among parents.
        /// </summary>
        /// <typeparam name="T">Type of parent to find.</typeparam>
        /// <param name="child">Element which parents will be investigated.</param>
        /// <returns>Returns object if found it or null if not.</returns>
        private static T? FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(child);

            while (parent != null && !(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as T;
        }

        private void GetMethodsByBranch_Click(object sender, RoutedEventArgs e)
        {
            InputBoxGetMethodsByBranch.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Get methods of branch "Yes" button processing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetMethodsByBranchYesButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.GetData(URI1 + InputBranchMethodsTextBox.Text, label2: ":ns0__Main_method");
            NoButton_Click(sender, e);
        }

        private void InsertNewPhilosopherButton_Click(object sender, RoutedEventArgs e)
        {
            InputBoxPhilosopherData.Visibility = Visibility.Visible;
        }

        private void ChangeBirthYearButton_Click(object sender, RoutedEventArgs e)
        {
            InputBoxChangePhilosophersYearOfBirth.Visibility = Visibility.Visible;
        }

        private void ChangeBirthYearButtonYesButton_Click(object sender, RoutedEventArgs e)
        {
            if (InputNewYearOfBirthTextBox is not null)
            {
                _viewModel.ChangeData(URI2 + InputPhilosopherTextBox.Text, Convert.ToDouble(InputNewYearOfBirthTextBox.Text));
            }
            NoButton_Click(sender, e);
            _viewModel.GetPhilosophersOnlyData();
            MessageBox.Show("Data is succesfully changed", "Success!");
        }
    }
}