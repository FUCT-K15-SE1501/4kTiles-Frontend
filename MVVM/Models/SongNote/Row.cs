using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4kTiles_Frontend.MVVM.Models.SongNote
{
    public class Row
    {
        // row index (start from 0)
        public int Position { get; set; }
        public List<Note> Notes { get; set; }
    }
}
