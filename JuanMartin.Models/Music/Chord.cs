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
        quality_based = 0,
        fixed_notes  = 1,
        none  = 2
    }
    public enum QualityType
    {
        [Description("maj")]
        major  = 0,
        [Description("min")]
        minor = 1,
        [Description("dim")]
        diminished = 2,
        [Description("maj7")]
        major_seventh = 3,
        [Description("min7")]
        minor_seventh = 4,
        [Description("dom7")]
        dominant_seventh = 5,
        [Description("sus2")]
        suspended_two = 6,
        [Description("sus4")]
        susprnded_four = 7,
        [Description("aug")]
        augmented = 8,
        [Description("ext")]
        extended = 9
    }
    public class Chord : IStaffPlaceHolder
    {
        public Dictionary<string, List<string>> ChordIntervals = new Dictionary<string, List<string>>()
        {
            {"maj", new List<string> {"1","3","5"} },
            {"min", new List<string> {"1","b3","5"} }
        };
        public string[] NoteScale = { "C", "C#", "D", "Eb", "E", "F", "F#", "G", "G#", "A", "Bb", "B" };
        public Note Root { get; set; }
        public ChordType Type { get; set; } = ChordType.none;
        public QualityType Quality { get; set; }
        public string Staccato { get; set; } = "";
        public string Pattern { get; set; } = "";
        public int Octave { get; set; } = 4;
        private List<Note> Notes { get; set; } = new List<Note>();
        private List<string> Intervals { get; set; } = new List<string>();

        public Note[] GetNotes()
        {
            throw new NotImplementedException();
        }

        public string[] GetIntervals()
        {
            throw new NotImplementedException();
        }

        public void SetStaccato()
        {
            throw new NotImplementedException();
        }
    }
}
