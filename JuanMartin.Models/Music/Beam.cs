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
        public string Staccato { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void SetStaccato()
        {
            throw new NotImplementedException();
        }
    }
}
