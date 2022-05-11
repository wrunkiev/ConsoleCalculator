using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleCalculator
{
    public class ConsoleCalculator : BaseCalculator
    {
        public override string Calculate(string input)
        {
            MathParser mathParser = new MathParser();
            return mathParser.GetExprInBreckets(input).ToString();
        }
    }
}
