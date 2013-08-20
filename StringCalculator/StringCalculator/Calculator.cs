using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    public class Calculator : ICalculator
    {
        List<char> _delimiter = new List<char>{',','\n'};
        public int Add(string values)
        {
            if (string.IsNullOrEmpty(values))
            {
                return 0;
            }
            int sum = EvaluateStringToInt(values);

            return sum;
        }

        private int EvaluateStringToInt(string values)
        {
            return values.Split(_delimiter.ToArray())
                .Select(x=>int.Parse(x))
                .Sum();
        }
    }
}
