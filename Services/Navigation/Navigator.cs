using _4kTiles_Frontend.Core.Functions;

namespace _4kTiles_Frontend.Services.Navigation
{
    internal class Navigator : ObservableObject
    {
        private object _currentView;
        private string _currentTitle;

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public string CurrentTitle
        {
            get => _currentTitle;
            set
            {
                _currentTitle = value;
                OnPropertyChanged();
            }
        }

        public static Navigator Instance { get; set; }

        static Navigator()
        {
            Instance = new Navigator();
        }

    }
}
