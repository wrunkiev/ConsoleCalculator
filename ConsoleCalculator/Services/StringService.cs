using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleCalculator
{
    public class StringService : IStringService
    {
        public string ClearExpression(string expression)
        {
            StringBuilder clearExpression = new StringBuilder();
            foreach (char ch in expression.Trim())
            {
                if (!Char.IsWhiteSpace(ch))
                {
                    clearExpression.Append(ch);
                }
            }

            return clearExpression.ToString();
        }
    }
}
