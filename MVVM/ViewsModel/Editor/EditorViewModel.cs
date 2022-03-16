using _4kTiles_Frontend.Core.Functions;
using _4kTiles_Frontend.MVVM.Models.Library.Items;
using _4kTiles_Frontend.MVVM.Models.SongNote;
using _4kTiles_Frontend.Services.ApiClient;

using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace _4kTiles_Frontend.MVVM.ViewsModel.Editor
{
    internal class EditorViewModel : ObservableObject
    {
        #region fields
        private SongModel _songModel;
        private Song _songNote;
        private ObservableCollection<NoteRowViewModel> _rows;
        private string _selectedGenre;
        private ObservableCollection<string> _genreList;
        #endregion

        #region properties
        public IFkTilesClient Client { get; set; }
        public SongModel SongModel
        {
            get => _songModel;
            set
            {
                _songModel = value;
                OnPropertyChanged();
            }
        }
        public Song SongNote
        {
            get => _songNote;
            set
            {
                _songNote = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<NoteRowViewModel> Rows
        {
            get => _rows;
            set
            {
                _rows = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> GenreList
        {
            get
            {
                if (_genreList == null)
                    _genreList = new();
                return _genreList;
            }
            set
            {
                _genreList = value;
                OnPropertyChanged();
            }
        }

        public string Genres { get; set; } = "#";

        public string SelectedGenre
        {
            get
            {
                if (_selectedGenre == null)
                    _selectedGenre = GenreList.FirstOrDefault();
                return _selectedGenre;
            }
            set
            {
                _selectedGenre = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand ShowValue { get; set; }

        public RelayCommand UploadToServer { get; set; }

        public RelayCommand AddRowCommand { get; set; }

        public RelayCommand RemoveRowCommand { get; set; }

        #endregion

        public EditorViewModel()
        {
            Client = FkTilesClient.Client;
            SongNote = new();

            SongModel = new();

            ShowValue = new(o =>
            {
                SongNote.Rows = new();
                for (var i = 0; i < Rows.Count; i++)
                {
                    var newRow = Rows[Rows.Count - i - 1].Row;
                    newRow.Position = i;
                    SongNote.Rows.Add(newRow);
                }
                SongModel.Notes = Client.GetJsonString(SongNote);
                MessageBox.Show(SongModel.ToString());
            });

            Rows = new();

            AddRowCommand = new(o =>
            {
                AddNewRow();
            });

            RemoveRowCommand = new(o =>
            {
                if (Rows.Count >= 1)
                    Rows.RemoveAt(0);
            });

            UploadToServer = new(o =>
            {
                SongNote.Rows = new();
                for (var i = 0; i < Rows.Count; i++)
                {
                    var newRow = Rows[Rows.Count - i - 1].Row;
                    newRow.Position = i;
                    SongNote.Rows.Add(newRow);
                }
                SongModel.Notes = Client.GetJsonString(SongNote);
                SongModel.Genres = new()
                {
                    SelectedGenre
                };

                _ = Task.Run(async () =>
                  {
                      await CreateSong();
                  });
            });

            Task.Run(async () =>
            {
                _ = await GetSongGenres();
            });
        }

        private async Task<bool> GetSongGenres()
        {
            GenreList.Clear();
            var list = new ObservableCollection<string>();
            var response = await Client.RequestAsync<Collection<string>>("library/genre");
            if (response == null)
                return false;

            foreach (var item in response.Data)
                list.Add(item);
            GenreList = list;
            return true;
        }

        private async Task<bool> CreateSong()
        {
            var response = await Client.RequestAsync<SongOverviewModel>("Song/Create", RequestMethod.POST, SongModel);

            if (response == null || response.IsError)
            {
                _ = MessageBox.Show(response?.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            MessageBox.Show("Song created!");
            return true;
        }

        private void AddNewRow()
        {
            Rows.Insert(0, new());
        }
    }

    public class SongModel
    {
        public string SongName { get; set; }
        public string Author { get; set; }
        public int Bpm { get; set; }
        public string Notes { get; set; }
        public bool IsPublic { get; set; }
        public Collection<string> Genres { get; set; }
    }
}
