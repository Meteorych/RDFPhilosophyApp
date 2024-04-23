using System.ComponentModel;
using System.Runtime.CompilerServices;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace RDFPhilosophyApp
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly ScriptEngine _queriesEngine = Python.CreateEngine();

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
