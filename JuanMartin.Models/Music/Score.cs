using JuanMartin.Kernel.Extesions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JuanMartin.Models.Music
{
    public enum ClefType
    {
        [Description("G")]
        treble = 0,
        [Description("F")]
        bass = 1,
        [Description("C")]
        alto = 2,
        [Description("T")]
        tenor = 3,
        [Description("X")]
        none = -1
    }

    public enum TempoType
    {
        [Description("[Prestissimo]")]
        prestissimo = 300, // very very fast >200bpm
        [Description("[Presto]")]
        presto = 168, // very fast 168-200bpm
        [Description("[Allegro]")]
        allegro = 120, // fast 120-s168bpm
        [Description("[Moderato]")]
        moderato = 108, // mderately  108-120pm
        [Description("[Andante]")]
        andannte = 76, //  walking pace 76-108bpm
        [Description("[Adagio]")]
        adagio = 66, // slow and stately 66-76bpm
        [Description("[Lento]")]
        lento = 40, // very slow 40-66bpm
        [Description("[Grave]")]
        grave = 20, // slow and solemn 20-40pm
        numeric = -1

    }

    public class Score
    {
        public const int ScoreDefaultTempoValueSetting = 120;
        public const string ScoreDefaultTimeSignatureSetting = "4/4";
        public const string ScoreClefValuesSetting = "(G|C|F|T)";
        public string Name { get; set; }
        public List<ClefType> Clefs { get; set; } = new List<ClefType>();
        public int TempoValue { get; set; } = ScoreDefaultTempoValueSetting;
        public string TimeSignature { get; set; } = ScoreDefaultTimeSignatureSetting;
        public List<Measure> Measures { get; set; }
        public string TranslatedSheet { get; set; } = String.Empty;
        public bool PlaySingleNotes { get; set; } = false;

        public int AddClef(string value)
        {
            ClefType clef = EnumExtensions.GetValueFromDescription<ClefType>(value);
            int index = 0;

            if (!Clefs.Contains(clef))
            {
                Clefs.Add(clef);
                index = Clefs.Count - 1;
            }
            else
                index = Clefs.IndexOf(clef);

            return index;
        }

        public string SetStaccato()
        {
            bool scoreHasHeader = false;

            StringBuilder staccato = new StringBuilder(StringScoreHeader(false));

            if (staccato.Length > 0)
                scoreHasHeader = true;

            if (Measures != null && Measures.Count > 0)
            {
                int previousMeasureVoice = -1;

                if(scoreHasHeader)
                     staccato.Append($"{Measure.MeasureDelimiter} ");

                foreach (var (measure, index) in Measures.Enumerate())
                {
                    // TODO: do not repeat meaure header
                    bool addNewHeader = false;
                    bool addNoteDynamics = false;
                    if (measure.Voice != previousMeasureVoice)
                        addNewHeader = true;
                    if(measure.Dynamics!=DynamicsType.neutral) 
                        addNoteDynamics = true;

                    Dictionary<string, string> additionalSettings;
                    if (addNewHeader || addNoteDynamics)
                    {
                        additionalSettings = new Dictionary<string, string>();
                        if (addNewHeader) { additionalSettings.Add(Measure.MeasureHeaderSetting, ""); }
                        if (addNoteDynamics) { additionalSettings.Add(Measure.MeasureDynamicsSetting, ""); }
                    }
                    else
                        additionalSettings = null;

                    // remove closing score delimiter if next score has NO header
                    if (!addNewHeader && staccato.Length > 1 && staccato.ToString().Last() == Measure.MeasureDelimiter)
                        staccato.Length -= 1;

                    string m = measure.SetStaccato(additionalSettings);
                    staccato.Append(m);


                    previousMeasureVoice = measure.Voice;
                }
            }

            return staccato.ToString();
        }

        public TempoType GetTempoName(int  value)
        {
            TempoType tempo = TempoType.numeric;
            TempoType[] tempoRanges = (TempoType[])Enum.GetValues(typeof(TempoType));
            int lastTempoIndex = tempoRanges.Length - 1;

            if (value > 200) 
            {
                return tempoRanges[lastTempoIndex - 1]; 
            }
            else if (value < 20)
            {
                return tempoRanges[0];
            }

            for (int i = 1; i < tempoRanges.Length; i++)
            {
                int lower = (int)tempoRanges[i - 1];
                int upper = (int)tempoRanges[i];

                if (value >= lower && value <= upper)
                {
                    tempo = tempoRanges[i];
                    break;
                }
            }

            return tempo;
        }
        
        public string StringScoreHeader(bool includeScoreName = false)
        {
            string aux = (includeScoreName) ? $"Playing; {Name}" : "";
            StringBuilder header = new StringBuilder($"{aux}\n\r");

            aux = (TempoValue != -1) ? $"T{TempoValue} " : "";
            if (aux != "") header.Append(aux);

            aux = (TimeSignature != "") ? $"TIME:{TimeSignature} " : "";
            if (aux != "") header.Append(aux);

            return header.ToString();
        }

        public override string ToString()
        {
            int previousMeasureVoice = -1;
            StringBuilder score = new StringBuilder();

            score.Append(StringScoreHeader(true));

            if (Measures != null && Measures.Count > 0)
            {
                string measureHeader = "";
                bool removeHeader, hasHeader = true;

                foreach (var measure in Measures)
                {
                    if (measure.Voice != previousMeasureVoice)
                    {
                        measureHeader = measure.StringMeasureHeader(true);
                        if (measureHeader != "")
                            measureHeader = $"{Measure.MeasureDelimiter}{measureHeader}";
                        removeHeader = false;
                    }
                    else
                        removeHeader = true;

                    string m = measure.ToString();
                    if (removeHeader && m != "" && measureHeader != "")
                    {
                        m = m.Replace(measureHeader, "");
                        hasHeader = false;
                    }
                    
                    score.Append(m);
                    if (hasHeader && m != "" && measure != Measures.Last()) score.Length -= 2; // remove closing delimiter

                    previousMeasureVoice = measure.Voice;
                }
            }

            return score.ToString();
        }

        public List<int> GetMeasuresTiming()
        {
            List<int> times = new List<int>();

            foreach (var measure in Measures)
            {
                int total = 0;
                foreach(var note in measure.Notes) 
                {
                    int d = 0;
                    if( note is Note) 
                            d = 1 / (int)(((Note)note).Type);
                    total += d;
                }
                times.Add(total);
            }
            return times;
        }
    }
}