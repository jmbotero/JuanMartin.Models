using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuanMartin.Models.Music
{
    public enum pitchType {
        eigth = 0,
        sisteenth = 1,
        quarter = 2,
        half = 4,
        whole = 8
    }

    public enum accidentalType
    {
        doubleFlat = -2,
        flat =-1,
        natural = 0,
        sharp = 1,
        doubleSharp = 2
    }
    public class Note : IStaffPlaceHolder
    {
        public accidentalType HasAccidental { get; set; } = accidentalType.natural;
        public pitchType Type { get; set; } = pitchType.quarter;
        public bool IsRest { get; set; } = false;
        public bool IsDotted { get; set; } = false;
        public string Name { get; set; } = "A";   // pitch: A,B,C,D,E,F,G rests: Q,H,W
        public int LgderCount { get; set; } = 0;
        public bool InBeam { get; set; } = false;
        public Beam BeamSet { get; set; }
        public void Play() { }
    }
}
