using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleCalculator
{
    public interface IValidator
    {
        public string ValidateInputExpression(string expression, bool flagConsole);

        public string ValidateFilePath(string path);        
    }
}
