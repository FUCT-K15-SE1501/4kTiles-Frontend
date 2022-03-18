using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4kTiles_Frontend.MVVM.Models.SongNote
{
    public class NoteType
    {
        public float Volume { get; set; } = 1;
        public float Delay { get; set; } = 0;
        public int MidiKey { get; set; } = 72;
        public float Length { get; set; } = 0;
    }
}
