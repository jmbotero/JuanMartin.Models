using Newtonsoft.Json;
using System.Collections.Generic;

namespace JuanMartin.Models
{
    public class Problem
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }
        public long LongNumber { get; set; }
        public int IntNumber { get; set; }
        public ulong BigNumber { get; set; }
        public long[] Numbers { get; set; }
        public string Sequence { get; set; }
        public List<int> ListOfNumbers { get; set; }
        public EulerProblem Script { get; set; }

        public Problem()
        {
            LongNumber = 0;
            BigNumber = 0;
            Numbers = null;
            Sequence = string.Empty;
            Script = null;
        }

        public Problem(int argument1, EulerProblem argument2)
        {
            Id = argument1;
            Script = argument2;
        }

        public Problem(int argument1, EulerProblem argument2, long[] argument3)
            : this(argument1,argument2)
        {
            Numbers = argument3;
        }

        public Problem(int argument1, EulerProblem argument2, long argument3)
            : this(argument1, argument2)
        {
            LongNumber = argument3;
        }

        public Problem(int argument1, EulerProblem argument2, int argument3)
            : this(argument1, argument2)
        {
            IntNumber = argument3;
        }

        public Problem(int argument1, EulerProblem argument2, int argument3, List<int> argument4)
            : this(argument1,argument2,argument3)
        {
            ListOfNumbers = argument4;
        }
        public Problem(int argument1, EulerProblem argument2, long argument3,int argument4)
            : this(argument1,argument2,argument3)
        {
            IntNumber = argument4;
        }

        public Problem(int argument1, EulerProblem argument2, string argument3)
            : this(argument1,argument2)
        {
            Sequence = argument3;
        }

        public Problem(int argument1, EulerProblem argument2, ulong argument3)
            : this(argument1,argument2)
        {
            BigNumber = argument3;
        }

        public Problem(int argument1, EulerProblem argument2, long argument3, string argument4)
            : this(argument1, argument2,argument3)
        {
            Sequence = argument4;
        }

        public  Problem(int argument1, EulerProblem argument2, long argument3, string argument4, long[] argument5)
            : this(argument1, argument2,argument3,argument4)
        {
            Numbers = argument5;
        }
    }

    public delegate Result EulerProblem(Problem args);
}