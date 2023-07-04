using JuanMartin.Kernel.Extesions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JuanMartin.Models.Music
{
    public class Score
    {

        public string Name { get; set; }
        public string Clef { get; set; }
        public int Tempo { get; set; } = -1;
        public string  TimeSignature { get; set; }
        public List<Measure> Measures { get; set; }

        public string SetStaccato()
        {
            string aux = (Clef != "") ? $"[{Clef}" : "";
            StringBuilder staccato = new StringBuilder($"{aux}");

            aux = (Tempo != -1) ? $"T{Tempo}" : "";
            if (aux != "") staccato.Append(aux);

            aux = (TimeSignature != "") ? $"[{TimeSignature}" : "";
            if (aux != "") staccato.Append(aux);

            if (Measures != null && Measures.Count > 0)
            {
                foreach (var (measure,index) in Measures.Enumerate())
                {
                    staccato.Append("||");
                    staccato.Append(measure.SetStaccato(false));

                    if (index == Measures.Count - 1) staccato.Append('|');
                }
            }

            return staccato.ToString();
        }

    }
}
