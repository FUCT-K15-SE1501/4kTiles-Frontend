using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace _4kTiles_Frontend.Core.Functions
{
    internal class ObservableObject: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
