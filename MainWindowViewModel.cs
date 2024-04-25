using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RDFPhilosophyApp
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        

        public MainWindowViewModel()
        {
            
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
