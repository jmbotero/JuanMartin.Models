using JuanMartin.Kernel.Extesions;
using JuanMartin.Models.Music;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JuanMartin.Models.Music
{
    public enum VolumeLoudness
    {
        none = 0,
        crescendo = 1,
        decrescendo = 2,
        diminuendo = 3
    }

    public enum DynamicsType
    {
        [Description("n")]
        neutral=0,
        [Description("fff")]
        fortississimo = 1,
        [Description("ff")]
        fortissimo = 2,
        [Description("f")]
        forte = 3,
        [Description("mf")]
        mezzo_forte = 4,
        [Description("mp")]
        mezzo_piano = 5,
        [Description("p")]
        piano = 6, 
        [Description("pp")]
        pianissimo = 7,
        [Description("ppp")]
        pianississimo = 8
    }

    public class Measure
    {
        public List<IStaffPlaceHolder> Notes { get; set; }
        public List<Beam> BeamSets { get; set; }
        public List<List<Note>> CurveSets { get; set; }
        public DynamicsType Dynamics { get; set; } = DynamicsType.neutral;
        public VolumeLoudness Volume { get; set; } = VolumeLoudness.none;
        public string Instrument { get; set; } = "";
        public int Voice { get; set; } = -1;
        public void AddCurve(List<Note> curve = null)
        {
            List<Note> newCurve = (curve == null) ? new List<Note>() : curve;
            if (CurveSets == null)
                CurveSets = new List<List<Note>>();

            CurveSets.Add(newCurve);
        }

        public void AddBeam(Beam beam = null)
        {
            Beam newBeam = (beam == null) ? new Beam() : beam;
            if (BeamSets == null)
                BeamSets = new List<Beam>();

            BeamSets.Add(newBeam);
        }

        public string SetStaccato(bool addMeasureCloseDelimiter = true)
        {
            string aux = (Dynamics != DynamicsType.neutral) ? $"{Dynamics}" : "";
            StringBuilder staccato = new StringBuilder($"{aux}");

            aux = (Volume != VolumeLoudness.none) ? $"{Volume}" : "";
            if (aux != "") staccato.Append(aux);
            aux = (Voice != -1) ? $"V{Voice}" : "";
            if (aux != "") staccato.Append(aux);
            aux = (Instrument != "") ? $"[{Instrument}]" : "";
            if (aux != "") staccato.Append(aux);

            if (Notes != null && Notes.Count > 0)
            {
                if(staccato.ToString().Last() != '|')  staccato.Append("| ");
                foreach (var note in Notes)
                {
                    staccato.Append($" {note.SetStaccato()} ");
                }
                if (addMeasureCloseDelimiter) staccato.Append("|");
            }

            return staccato.ToString();
        }
    }
}
