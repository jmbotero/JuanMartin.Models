using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JuanMartin.Models.Music {
    public class Beam : IStaffPlaceHolder
    {
        public Beam()
        {
            Notes = new List<Note>();
        }
        public List<Note> Notes { get; set; }
        public int index { get; set; }
        public string SetStaccato(Dictionary<string, string> additionalSettings = null)
        {
            int index = 0;
            string staccato = "";
            foreach (var note in Notes)
            {
                note.Type = PitchType.eigth;
                staccato += note.SetStaccato();
                if (index < Notes.Count - 1)
                    staccato = staccato + "+";
                index++;
            }
            return staccato;
        }

    }
}
