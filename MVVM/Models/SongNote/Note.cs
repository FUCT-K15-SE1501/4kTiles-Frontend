using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4kTiles_Frontend.MVVM.Models.SongNote
{
    public class Note
    {
        // relative to current row [0,1,2,3]
        public int Position { get; set; } = -1;
        public List<NoteType> noteType { get; set; } = new()
        {
            new()
        };
        public bool touchOptional { get; set; } = false;
    }
}
