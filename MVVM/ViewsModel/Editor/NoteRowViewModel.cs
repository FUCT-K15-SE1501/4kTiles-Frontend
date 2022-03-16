using _4kTiles_Frontend.Core.Functions;
using _4kTiles_Frontend.MVVM.Models.SongNote;

namespace _4kTiles_Frontend.MVVM.ViewsModel.Editor
{
    internal class NoteRowViewModel : ObservableObject
    {
        private Note _note;

        public Note SelectedNote
        {
            get
            {
                if (_note == null)
                    _note = new();
                return _note;
            }
            set
            {
                _note = value;
                OnPropertyChanged();
            }
        }

        public Row Row
        {
            get => new()
            {
                Notes = new()
                {
                    SelectedNote
                }
            };
        }

        public string Genres { get; set; }

        public RelayCommand Note1Command { get; set; }
        public RelayCommand Note2Command { get; set; }
        public RelayCommand Note3Command { get; set; }
        public RelayCommand Note4Command { get; set; }

        public NoteRowViewModel()
        {
            Note1Command = new(o =>
           {
               SelectedNote.Position = 0;
           });
            Note2Command = new(o =>
           {
               SelectedNote.Position = 1;
           });
            Note3Command = new(o =>
            {
                SelectedNote.Position = 2;
            });
            Note4Command = new(o =>
            {
                SelectedNote.Position = 3;
            });

        }
    }
}
