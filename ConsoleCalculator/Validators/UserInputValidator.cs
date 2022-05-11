using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace ConsoleCalculator
{
    public class UserInputValidator : IValidator
    {
        //validate input expression. Expression can contains numbers, brackets, operation '+','-','/','*' 
        public string ValidateInputExpression(string expression, bool isConsoleFlow)
        {
            if (String.IsNullOrWhiteSpace(expression))
            {
                return UserMessageConstant.ErrorEmptyExpressionMsg;
            }

            string regExp;

            if (isConsoleFlow)
            {
                regExp = @"[^0123456789\.\-\+\*\/]";
            }
            else
            {
                regExp = @"[^0123456789\.\-\+\*\/\(\)]";
            }
            //regExp = @"[^0123456789\.\-\+\*\/\(\)]";// for tests, not delete

            Regex regex = new Regex(regExp);
            MatchCollection matches = regex.Matches(expression);
            if (matches.Count > 0)            
            {
                return UserMessageConstant.ErrorExpressionMsg;
            }                

            return "";
        }        

        //validate filepath
        public string ValidateFilePath(string path)
        {            
            FileInfo fileInfo = new FileInfo(@path);

            if (fileInfo.Exists)
            {
                return "";
            }
            
            return UserMessageConstant.ErrorFoundFilePathMsg;                                  
        }
    }
}
