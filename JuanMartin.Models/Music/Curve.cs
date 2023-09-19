
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuanMartin.Models.Music
{
    public enum CurveType
    {
        tie = 0,
        slur = 1,
        none = 2
    }
    public class Curve : IStaffPlaceHolder
    {
        public List<Note> Notes { get; set; }
        public int index { get; set; }
        public int Count { get; set; } = 0;
        public Curve()
        {
            Notes = new List<Note>();
            Count = Notes.Count;
        }
        public Curve(Note note):this()
        {
            Notes.Add(note);
            Count = Notes.Count;
        }
        public CurveType GetCurveType()
        {
            if (Notes == null) return CurveType.none;
            if (Notes.Count <= 1) return CurveType.none;

            return (Notes.Any(note => note.Name != Notes.First().Name)) ? CurveType.slur : CurveType.tie;
        }
        public void Add(Note note)
        { 
            Notes.Add(note);
            Count=Notes.Count;
        }
        public int AddNote(Note note)
        {
            if(Notes==null) Notes=new List<Note>();
            
            Notes.Add(note);

            return Notes.Count - 1;
        }
        public override string ToString()
        {
            StringBuilder curve = new StringBuilder();
            curve.Append("(");
            foreach (var note in Notes)
            {
                curve.Append(" ");
                curve.Append(note.ToString());
            }
            curve.Append(" )");
            return curve.ToString();
        }

        public string SetStaccato(Dictionary<string, string> additionalSettings = null)
        {
            string staccato = "";

            return staccato;
        }

    }
}
