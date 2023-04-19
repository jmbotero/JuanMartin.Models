using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using JuanMartin.Kernel.Extesions;

namespace JuanMartin.Models.Music
{
    public enum PitchType {
        [Description("o")]
        one_twenty_eighth = 0,
        [Description("x")]
        sixty_fourth = 1,
        [Description("t")]
        thirty_second = 2,
        [Description("s")]
        sisteenth = 3,
        [Description("i")]
        eigth = 4,
        [Description("q")]
        quarter = 5,
        [Description("h")]
        half = 6,
        [Description("w")]
        whole = 7
    }
    public enum CurveType
    {
        tie = 0,
        slur = 1,
        none = 2
    }
    public enum AccidentalType
    {
        [Description("x")]
        none = -3,
        [Description("bb")]
        doubleFlat = -2,
        [Description("b")]
        flat = -1,
        [Description("n")]
        natural = 0,
        [Description("#")]
        sharp = 1,
        [Description("##")]
        doubleSharp = 2
    }
    public class Note : IStaffPlaceHolder
    {
        public AccidentalType HasAccidental { get; set; } = AccidentalType.none;
        public PitchType Type { get; set; } = PitchType.quarter;
        public bool IsRest { get; set; } = false;
        public bool IsDotted { get; set; } = false;
        public string Name { get; set; } = "A";   // pitch: A,B,C,D,E,F,G rests: Q,H,W
        public string Staccato { get; set; } = "";
        public int LgderCount { get; set; } = 0;
        public bool LastInCurve { get; set; } = false;
        public bool LastInBeam { get; set; } = false;
        public bool InCurve { get; set; } = false; // tie or slur
        public CurveType TypeOfCurve { get; set; } = CurveType.none;
        public bool InBeam { get; set; } = false;
        public int Octave { get; set; } = 5;
        public override string ToString() 
        {
            var isDotted = (IsDotted) ? "." : "";
            if (IsRest)
                return $"[]{Type}{isDotted}";

            else
                return $"{HasAccidental}:{Name}{isDotted}:{Type}";
        }

        public void SetStaccato()
        {
            string octave = "";
            string duration = EnumExtensions.GetDescription(Type);
            string accidental = (HasAccidental != AccidentalType.natural) ? EnumExtensions.GetDescription(HasAccidental) : "";
            if (Octave != 4) octave = Octave.ToString();

            // Staccato supports dotted durations (for example, q.), in which case the 
            // note is played for the original duration plus half of the original duration.
            // A dotted quarter note is the same as combining a quarter note with an
            // eighth note.
            if (IsDotted)
            {
                switch (Type)
                {
                    case PitchType.one_twenty_eighth:
                        break;
                    case PitchType.sixty_fourth:
                        duration += EnumExtensions.GetDescription(PitchType.one_twenty_eighth);
                        break;
                    case PitchType.thirty_second:
                        duration += EnumExtensions.GetDescription(PitchType.sixty_fourth);
                        break;
                    case PitchType.sisteenth:
                        duration += EnumExtensions.GetDescription(PitchType.thirty_second);
                        break;
                    case PitchType.eigth:
                        duration += EnumExtensions.GetDescription(PitchType.sisteenth);
                        break;
                    case PitchType.quarter:
                        duration += EnumExtensions.GetDescription(PitchType.eigth);
                        break;
                    case PitchType.half:
                        duration += EnumExtensions.GetDescription(PitchType.quarter);
                        break;
                    case PitchType.whole:
                        duration += EnumExtensions.GetDescription(PitchType.half);
                        break;
                }
            }
            Staccato = $"{Name}{accidental}{octave}{duration}";
        }

    }
}
