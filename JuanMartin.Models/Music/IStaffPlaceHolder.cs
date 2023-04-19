using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuanMartin.Models.Music {
    public interface IStaffPlaceHolder {
        string Staccato { get; set; }
        string ToString();
        void SetStaccato();
    }
}
