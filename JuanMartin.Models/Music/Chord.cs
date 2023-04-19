using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuanMartin.Models.Music
{
    public enum ChordType
    {
        [Description("maj")]
        major  = 0,
        [Description("min")]
        minor = 1,
        diminished = 2,
        [Description("maj7")]
        major_seventh = 3,
        [Description("min7")]
        minor_seventh = 4,
        dominant_seventh = 5,
        [Description("sus2")]
        suspended_two = 6,
        [Description("sus4")]
        susprnded_four = 7,
        augmented = 8,
        extended = 9
    }
    public class Chord : IStaffPlaceHolder
    {
        public Note Root { get; set; }
        public ChordType Type { get; set; }
        public List<Note> Notes { get; set; } = new List<Note>();
        public List<int> Intervals { get; set; } = new List<int>();
        public string Staccato { get; set; } = "";
        public int Octave { get; set; } = 4;

        public void SetStaccato()
        {
            throw new NotImplementedException();
        }
    }
}
