using System;
using System.Collections.Generic;
using System.Linq;

namespace JuanMartin.Models.Euler
{
    public class Exam
    {
        public string Name { get; set; }
        public IEnumerable<Problem> Problems { get; set; }
    }
}