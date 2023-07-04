using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JuanMartin.Models.Music {
    public class Beam : IStaffPlaceHolder {
        public Beam()
        {
            Notes = new List<Note>();
        }
        public List<Note> Notes { get; set; }
        public int index { get; set; }
        public string SetStaccato()
        {
            string staccato = "";
            foreach (var note in Notes)
            {
                note.Type = PitchType.eigth;
                staccato += $"{note.SetStaccato()}+";
            }
            return staccato;
        }
    }
}
