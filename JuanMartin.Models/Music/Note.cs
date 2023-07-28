using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JuanMartin.Kernel.Extesions;

namespace JuanMartin.Models.Music
{
    public enum PitchType {
        [Description("o")]
        one_twenty_eighth = 128,
        [Description("x")]
        sixty_fourth = 64,
        [Description("t")]
        thirty_second = 32,
        [Description("s")]
        sisteenth = 16,
        [Description("i")]
        eigth = 8,
        [Description("q")]
        quarter = 4,
        [Description("h")]
        half = 2,
        [Description("w")]  
        whole = 1
    }
    public enum CurveType
    {
        tie = 0,
        slur = 1,
        none = 2
    }
    public enum AccidentalType
    {
        [Description("-")]
        none = -3,
        [Description("bb")]
        doubleFlat = -2,
        [Description("b")]
        flat = -1,
        [Description("n")]
        natural = 0,
        [Description("#")]
        sharp = 1,
        [Description("x")]
        doubleSharp = 2
    }
    public class Note : IStaffPlaceHolder
    {
        public AccidentalType Accidental { get; set; } = AccidentalType.none;
        public PitchType Type { get; set; } = PitchType.quarter;
        public bool IsRest { get; set; } = false;
        public bool IsDotted { get; set; } = false;
        public string Name { get; set; } = "A";   // pitch: A,B,C,D,E,F,G rests: R
        public int LedgerCount { get; set; } = 0;
        public bool LastInCurve { get; set; } = false;
        public bool FirstInCurve { get; set; } = false;
        public bool LastInBeam { get; set; } = false;
        public bool FirstInBeam { get; set; } = false;
        public bool InCurve { get; set; } = false; // tie or slur
        public CurveType TypeOfCurve { get; set; } = CurveType.none;
        public bool InBeam { get; set; } = false;
        public int Octave { get; set; } = 5;
        public Measure Measure { get; set; } = null;

        public override string ToString() 
        {
            var isDotted = (IsDotted) ? "." : "";
            if (IsRest)
                return $"R:{Type}";
            else
                return $"{Accidental}:{Name}{isDotted}:{Octave}:{Type}";
        }

        public string SetStaccato(Dictionary<string, string> additionalSettings = null)
        {
            string volume = "";
            if (additionalSettings != null && !IsRest)
            {
                if (additionalSettings.ContainsKey(Measure.MeasureNoteVolumeSetting))
                    volume = additionalSettings[Measure.MeasureNoteVolumeSetting];
                else if (additionalSettings.ContainsKey(Measure.MeasureDynamicsSetting))
                    volume = additionalSettings[Measure.MeasureDynamicsSetting];
            }

            string duration = EnumExtensions.GetDescription(Type);
            if (IsRest)
            {
                return $"R{duration}";
            }

            string octave = "";
            if (FirstInCurve || InCurve) { duration = duration + "-"; }
            if (LastInCurve || InCurve) { duration += "-"; }

            string accidental = (Accidental != AccidentalType.natural) ? EnumExtensions.GetDescription(Accidental) : "";
            if (Measure != null && Measure.Score != null && (Measure.Score.Clef == CllefType.bass && Octave != 4) || (Measure.Score.Clef == CllefType.treble && Octave != 5)) octave = Octave.ToString();

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
            return $"{Name}{accidental}{octave}{duration}{volume}";

        }
    }
}
