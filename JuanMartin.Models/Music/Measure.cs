using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using JuanMartin.Kernel.Extesions;

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
        fortississimo = 8,
        [Description("ff")]
        fortissimo = 7,
        [Description("f")]
        forte = 6,
        [Description("mf")]
        mezzo_forte = 5,
        [Description("mp")]
        mezzo_piano = 1,
        [Description("p")]
        piano = 2, 
        [Description("pp")]
        pianissimo = 3,
        [Description("ppp")]
        pianississimo = 4
    }

    public class Measure
    {
        public const char MeasureDelimiter = '|';
        public const string MeasureHeader = "||";
        public const char MeasureHeaderStart = '{';
        public const char MeasureHeaderEnd = '}';
        public const string MeasureHeaderSetting = "Header";
        public const string MeasureDynamicsSetting = "Dynamics";
        public const string MeasureNoteVolumeSetting = "Volume";
        public List<IStaffPlaceHolder> Notes { get; set; }
        public List<Beam> BeamSets { get; set; }
        public List<List<Note>> CurveSets { get; set; }
        public DynamicsType Dynamics { get; set; } = DynamicsType.neutral;
        public VolumeLoudness Volume { get; set; } = VolumeLoudness.none;
        public string Instrument { get; set; } = "";
        public int Voice { get; set; } = -1;
        public Score Score { get; set; } = null;
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

        public override string ToString()
        {
            StringBuilder measure = new StringBuilder();

            if (Notes != null && Notes.Count > 0)
            {
                measure.Append(Measure.MeasureDelimiter);
                foreach (var note in Notes)
                {
                    measure.Append(note.ToString()  );
                }
                measure.Append(Measure.MeasureDelimiter);
            }

            return measure.ToString();
        }

        public string SetStaccato(Dictionary<string, string> additionalSettings = null)
        {
            string aux = "";
            StringBuilder staccato = new StringBuilder(aux);

            if (additionalSettings != null && additionalSettings.ContainsKey(MeasureDynamicsSetting))
            {
                additionalSettings.Remove(MeasureNoteVolumeSetting);
                additionalSettings.Remove(MeasureDynamicsSetting);

                string settingValue = "";
                if (Dynamics != DynamicsType.neutral)
                {
                    int d = (int)Dynamics * 15;
                    int a = 127 - d;
                    settingValue = $"a{a}d{d}";
                }
                additionalSettings.Add(MeasureDynamicsSetting, settingValue);

                settingValue = "";
                if (Volume != VolumeLoudness.none)
                {
                    switch (Volume)
                    {
                        case VolumeLoudness.crescendo:
                            settingValue = "d";//":CE(Volume,VOLUME_COARSE) ";
                            break;
                        case VolumeLoudness.decrescendo:
                            settingValue = "a";// ":CE(Volume,VOLUME_FINE) ";
                            break;
                        case VolumeLoudness.diminuendo:
                            settingValue = "a";// ":CE(Volume,VOLUME_FINE) T50 ";
                            break;
                    }
                    additionalSettings.Add(MeasureNoteVolumeSetting, settingValue);
                }
            }
            
            if(additionalSettings != null && additionalSettings.ContainsKey(MeasureHeaderSetting))
            {
                additionalSettings.Remove(MeasureHeaderSetting);

                staccato.Append(" ");
                aux = (Voice != -1) ? $"V{Voice} " : "";
                if (aux != "") staccato.Append(aux);
                aux = (Instrument != "") ? $"I[{Instrument}] " : "";
                if (aux != "") staccato.Append(aux);
            }

            if (Notes != null && Notes.Count > 0)
            {
                staccato.Append($"{Measure.MeasureDelimiter} ");
                foreach (var note in Notes)
                {
                    staccato.Append($"{note.SetStaccato(additionalSettings)} ");
                }
               staccato.Append(Measure.MeasureDelimiter);
            }

            return staccato.ToString();
        }


    }
}
