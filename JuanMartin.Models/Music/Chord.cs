using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using JuanMartin.Kernel.Extesions;

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
        [Description("fix")]
        fixed_notes = -1,
        [Description("maj")]
        major = 0,
        [Description("min")]
        minor = 1,
        [Description("dim")]
        diminished = 2,
        [Description("maj7")]
        major_seventh = 3,
        [Description("min7")]
        minor_seventh = 4,
        [Description("dim7")]
        diminished_seventh = 5,
        [Description("dom7")]
        dominant_seventh = 6,
        [Description("sus2")]
        suspended_two = 7,
        [Description("sus4")]
        suspended_four =  8,
        [Description("aug")]
        augmented = 9,
        [Description("aug7")]
        augmented_seventh = 10
    }
    public class Chord : IStaffPlaceHolder
    {
        public Dictionary<string, List<string>> ChordIntervals = new Dictionary<string, List<string>>()
        {
            {"fix", null },
            {"maj", new List<string> {"1","3","5"} },
            {"min", new List<string> {"1","b3","5"} },
            {"dim", new List<string> {"1","b3","b5"} },
            {"aug", new List<string> {"1","3","#5"} },
            {"dom7", new List<string> {"1","3","5", "b7"} },
            {"maj7", new List<string> {"1","3","5", "7"} },
            {"min7", new List<string> {"1","b3","5", "b7"} },
            {"dim7", new List<string> {"1","b3","b5", "6"} },
            {"aug7", new List<string> {"1","3","#5", "b7"} },
            {"sus2", new List<string> {"1","2","5"} },
            {"sus4", new List<string> {"1","4","5"} }
        };
        public string[] ChordNotesScale = { "[A", "", "C#", "", "E","","G#", "[B", "C#", "D#", "E", "F#", "Ab", "A#", "[C", "D", "E", "F", "G", "A", "B", "[D", "E", "F#", "G", "A", "B", "C#","[E", "F#", "G#", "A", "B", "C#", "D", "[F", "G", "A", "Bb", "C", "D", "E" };
        public string[] NotesOctave  = { "C", "C#", "D", "Eb", "E", "F", "F#", "G", "G#", "A", "Bb", "B" };
        public Note Root { get; set; }
        public ChordType Type { get; set; } = ChordType.none;
        public QualityType Quality { get; set; }
        public int Octave { get; set; } = 4;
        public int Inversions { get; set; } = 0;
        private List<Note> Notes { get; set; } = new List<Note>();
        private List<string> Intervals { get; set; } = new List<string>();
        public List<Note> GetListOfChordNotes()
        {
            return Notes; 
        }

        public void SetChordNotes(List<Note> notes)
        {
            Notes = notes;
        }
        public void AddChordNoteToList(Note note)
        {
            Notes.Add(note);
        }

        public string[] GetPitchNamesBasedOnNoteScales(string rootNote, string[] intervals, string[] scales)
        {
            int GetChordScaleRootIndex()
            {
                var idx = Array.IndexOf(scales,  $"[{rootNote}");

                return idx;
            }
            int GetMaxInterval()
            {
                int max = 0;
                foreach(var interval in intervals)
                {
                    int i = int.Parse((interval.Length > 1) ? interval[1].ToString() : interval[0].ToString()) - 1;
                    if(i > max) max = i;
                }
                return max; 
            }

            List<string> notes = new List<string>();
            int[] indices = new int[intervals.Length];
            int previousPosition = 0;

            foreach (var (interval,i)  in intervals.Enumerate())
            {
                int position = int.Parse((interval.Length > 1) ? interval[1].ToString() : interval[0].ToString()) - 1;
                if (i == 0)
                {
                    var idx = GetChordScaleRootIndex();

                    indices[i] = idx;
                }
                else
                {
                    indices[i] = indices[i - 1] + (position - previousPosition); 
                    previousPosition = position;
                }
                notes.Add(scales[indices[i]].TrimStart('['));
            }
            return notes.ToArray();
        }                    

        /// <summary>
        // / Define intervals for fixed notes using  scales array
        /// </summary>
        public void SetIntervalsFromNoteScales()
        {
            (string, int) GetChordIntervalFromNote(Note note ,  int indexPreviousNote = 0)
            {
                int i = Array.IndexOf(ChordNotesScale, note.Name, indexPreviousNote) + 1;
                if(i== -1) i = Array.IndexOf(ChordNotesScale, $"[{note.Name}", indexPreviousNote) + 1;
                if(i== -1)
                {
                    throw new ArgumentOutOfRangeException($"Note {note.Name} not in scale.");
                }

                indexPreviousNote = i - 1;
                string index = i.ToString();

                if (note.HasAccidental != AccidentalType.none) index += EnumExtensions.GetDescription(note.HasAccidental);

                return (index,indexPreviousNote);
            }
            int idx = 0;
            string interval;
            (interval, _) = GetChordIntervalFromNote(Root, idx);
            Intervals.Add(interval);

            idx = 0;
            foreach(Note note in Notes)
            {
                (interval, idx) = GetChordIntervalFromNote(note, idx);
                Intervals.Add(interval);
            }
        }

        public string[] GetIntervals()
        {
            return Intervals.ToArray();
        }

        /// <summary>
        /// Define intervals from chord quality type
        /// </summary>
        /// <param name="QualityTypeDescription"></param>
         public void SetChordIntervals(string QualityTypeDescription)
        {
            if(QualityTypeDescription != "") 
            {
                List<string> chordIntervals = ChordIntervals[QualityTypeDescription.ToLower()];

                if (chordIntervals != null) 
                {
                    Intervals = chordIntervals;
                }
            }
        }
        public string SetStaccato()
        {
            string accidental = "";
            string staccato = "";

            switch(Type)
            {
                case ChordType.quality_based:
                    accidental = (Root.HasAccidental != AccidentalType.none) ? EnumExtensions.GetDescription(Root.HasAccidental) : "";
                    StringBuilder stringBuilder = null;
                    if (Inversions>0)
                    {
                        stringBuilder = new StringBuilder(Inversions);
                        stringBuilder.Insert(0, "^", Inversions);
                    }
                    string inversions = (stringBuilder != null) ? stringBuilder.ToString() : "";
                    staccato = $"{Root.Name}{accidental}{Root.Octave}{Quality}{inversions}";
                    break;
                case ChordType.fixed_notes:
                    accidental = (Root.HasAccidental != AccidentalType.none) ? EnumExtensions.GetDescription(Root.HasAccidental) : "";
                    staccato = $"{Root.Name}{accidental}{Root.Octave}";
                    foreach (Note note in Notes)
                    {
                        accidental = (note.HasAccidental != AccidentalType.none) ? EnumExtensions.GetDescription(note.HasAccidental) : "";
                        staccato += $"+{note.Name}{accidental}{Root.Octave}";
                    }
                    break;
            }
            return staccato;
        }
    }
}
