using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleCalculator
{
    public class FileCalculator : BaseCalculator
    {
        public override string Calculate(string input)
        {
            try
            {
                MathParser mathParser = new MathParser();
                return mathParser.GetExprInBreckets(input).ToString();
            }
            catch
            {
                return UserMessageConstant.ErrorExpressionMsg;
            }                        
        }        
    }
}
