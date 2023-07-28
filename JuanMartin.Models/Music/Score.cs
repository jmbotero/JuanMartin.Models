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
    public enum CllefType
    {
        [Description("G")]
        treble = 0,
        [Description("F")]
        bass = 1,
        [Description("C")]
        alto = 2,
        [Description("T")]
        tenor = 3
    } 
    public class Score
    {

        public string Name { get; set; }
        public CllefType Clef { get; set; }
        public int Tempo { get; set; } = 120;
        public string TimeSignature { get; set; } = "4/4";
        public List<Measure> Measures { get; set; }
        public string TranslatedSheet { get; set; } = String.Empty;
        public string SetStaccato(Dictionary<string, string> additionalSettings = null)
        {
            bool scoreHasHeader = false;

             string aux = (Tempo != -1) ? $"T{Tempo} " : "";
            StringBuilder staccato = new StringBuilder($"{aux}");

            aux = (TimeSignature != "") ? $"TIME:{TimeSignature} " : "";
            if (aux != "") staccato.Append(aux);

            if (staccato.Length > 0)
                scoreHasHeader = true;

            if (Measures != null && Measures.Count > 0)
            {
                int previousMeasureVoice = -1;
                string previousMeasureInstrument = "";
                DynamicsType previousMeasureDynamic = DynamicsType.neutral;

                if(scoreHasHeader)
                     staccato.Append($"{Measure.MeasureDelimiter} ");

                foreach (var (measure, index) in Measures.Enumerate())
                {
                    // TODO: do not repeat meaure header
                    bool addNewHeader = false;
                    bool addNoteDynamics = false;
                    if (measure.Instrument != previousMeasureInstrument && measure.Dynamics != previousMeasureDynamic && measure.Voice != previousMeasureVoice)
                        addNewHeader = true;
                    if(measure.Dynamics!=DynamicsType.neutral) 
                        addNoteDynamics = true;
                    
                    if (addNewHeader || addNoteDynamics)
                    {
                        additionalSettings = new Dictionary<string, string>();
                        if (addNewHeader) { additionalSettings.Add(Measure.MeasureHeaderSetting, ""); }
                        if (addNoteDynamics) { additionalSettings.Add(Measure.MeasureDynamicsSetting, ""); }
                    }
                    else
                        additionalSettings = null;

                    // remove closing measure delimiter if next measure has NO header
                    if (!addNewHeader && staccato.Length > 1 && staccato.ToString().Last() == Measure.MeasureDelimiter)
                        staccato.Length -= 1;   

                    staccato.Append(measure.SetStaccato(additionalSettings));


                    previousMeasureVoice = measure.Voice;
                    previousMeasureInstrument = measure.Instrument;
                    previousMeasureDynamic = measure.Dynamics;
                }
            }

            return staccato.ToString();
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