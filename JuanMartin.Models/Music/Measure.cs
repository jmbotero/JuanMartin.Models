using JuanMartin.Models.Music;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
        public List<Beam> BeamSets { get; set; }
        public List<List<Note>> CurveSets { get; set; }
        public DynamicsType Dynamics { get; set; } = DynamicsType.neutral;
        public VolumeLoudness Volume { get; set; } = VolumeLoudness.none;
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
    }
}
