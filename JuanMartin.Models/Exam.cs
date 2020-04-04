using System;
using System.Collections.Generic;
using System.Linq;

namespace JuanMartin.Models
{
    public class Exam
    {
        public string Name { get; set; }
        public IEnumerable<Problem> Problems { get; set; }
    }
}