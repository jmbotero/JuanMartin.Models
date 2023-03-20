using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace JuanMartin.Models.Music
{
    public enum PitchType {
        eigth = 0,
        sisteenth = 1,
        quarter = 2,
        half = 4,
        whole = 8
    }
    public enum CurveType
    {
        tie = 0,
        slur = 1,
        none = 2
    }
    public enum AccidentalType
    {
        doubleFlat = -2,
        flat =-1,
        natural = 0,
        sharp = 1,
        doubleSharp = 2
    }
    public class Note : IStaffPlaceHolder
    {
        public AccidentalType HasAccidental { get; set; } = AccidentalType.natural;
        public PitchType Type { get; set; } = PitchType.quarter;
        public bool IsRest { get; set; } = false;
        public bool IsDotted { get; set; } = false;
        public string Name { get; set; } = "A";   // pitch: A,B,C,D,E,F,G rests: Q,H,W
        public int LgderCount { get; set; } = 0;
        public bool InCurve { get; set; } = false; // tie or slur
        public CurveType TypeOfCurve { get; set; } = CurveType.none;
        public bool InBeam { get; set; } = false;
        
        public override string ToString() 
        {
            var isDotted = (IsDotted) ? "." : "";
            if (IsRest)
                return $"[]{Type}{isDotted}";

            else
                return $"{HasAccidental}{Name}{Type}{isDotted}";
        }
    }
}
