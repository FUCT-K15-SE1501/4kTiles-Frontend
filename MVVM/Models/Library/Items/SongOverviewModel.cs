using _4kTiles_Frontend.Core.Functions;
using _4kTiles_Frontend.Services.ApiClient;
using System;
using System.Collections.Generic;

namespace _4kTiles_Frontend.MVVM.Models.Library.Items
{
    public class SongOverviewModel
    {
        public int SongId { get; set; }
        public string SongName { get; set; } = null!;
        public string Author { get; set; } = null!;
        public int CreatorId { get; set; }
        public string CreatorName { get; set; } = null!;
        public int Bpm { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public ICollection<string> Genres { get; set; } = null!;

        public override string ToString()
        {
            return $"SongId: {SongId}\nSongName: {SongName}\nAuthor: {Author}\nCreatorId: {CreatorId}\nCreatorName: {CreatorName}\nBpm: {Bpm}\nReleaseDate: {ReleaseDate}";
        }
    }
}
