using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleCalculator
{
    public class MathParser
    {
        private NumberStyles styles = NumberStyles.AllowDecimalPoint | NumberStyles.Integer;
        private CultureInfo provider = CultureInfo.InvariantCulture;

        public string GetExprInBreckets(string input)
        {
            var regExp = @"\([0123456789\.\-\+\*\/]+?\)";
            Regex regex = new Regex(regExp);
            MatchCollection matches = regex.Matches(input);
            string result = input;

            if (matches.Count > 0) 
            {                
                string left = input.Substring(0, matches[0].Index);
                string inner = matches[0].Value;
                string right = input.Substring(matches[0].Index + matches[0].Value.Length, input.Length - left.Length - inner.Length);

                //делаем проверку inner это выражение или число. Если число идем дальше. Если выражение рассчитаем его
                result = ValidateExpressionOrNumber(left, inner, right, input);
                
                return GetExprInBreckets(result);                
            }
            return GetResultInBreckets(result);
        }

        private string ValidateExpressionOrNumber(string left, string inner, string right, string input)
        {
            var tempResult = inner.Substring(1, inner.Length - 2);
            string result;

            if (double.TryParse(tempResult, styles, provider, out double resultTempInBreckets))
            {                              
                var sign = inner[1];
                if (resultTempInBreckets >= 0)
                {
                    if (sign.Equals('+'))
                    {
                        var tempLeft = left.Substring(0, left.Length);
                        result = tempLeft + resultTempInBreckets + right;
                    }
                    else
                    {
                        result = left + tempResult + right;
                    }
                }
                else
                {
                    if (left.Equals(""))
                    {
                        result = left + tempResult + right;
                    }
                    else
                    {
                        if (left[left.Length - 1].Equals('-'))
                        {
                            var tempLeft = left.Substring(0, left.Length);
                            double tempInner = resultTempInBreckets * (-1);
                            result = tempLeft + tempInner.ToString() + right;
                        }
                        else
                        {
                            var tempLeft = left.Substring(0, left.Length);
                            double tempInner = resultTempInBreckets * (1);
                            result = tempLeft + tempInner.ToString() + right;
                        }                        
                    }                    
                }
                //else
                //{
                 //   result = GetResultInBreckets(input);
                //}
                
                /*if (sign.Equals('+') || sign.Equals('-'))
                {
                    if (resultTempInBreckets > 0)
                    {
                        result = left + tempResult + right;
                    }
                    else if (sign.Equals('+'))
                    {
                        var tempLeft = left.Substring(0, left.Length - 1);
                        result = tempLeft + tempResult + right;
                    }
                    else
                    {
                        var tempLeft = left.Substring(0, left.Length - 1);
                        double tempInner = resultTempInBreckets * (-1);
                        result = tempLeft + tempInner.ToString() + right;
                    }
                }                                
                else
                {
                    result = GetResultInBreckets(input);
                }*/
            }
            else
            {
                inner = GetResultInBreckets(tempResult);
                result = left + inner + right;
            }

            return result;
        }

        private string GetResultInBreckets(string input)
        {
            string resultMultiDiv = OperationMultiDiv(input);
            string result = OperationPlusMinus(resultMultiDiv);

            return result;
        }

        //парсинг выражения с операциями '+' и '-' и упрощение его
        private string OperationPlusMinus(string input)
        {
            var regExp = @"(\(?\-?[0-9]+\.?[0-9]*\)?)[\+\-](\(?\-?[0-9]+\.?[0-9]*\)?)";
            Regex regex = new Regex(regExp);
            MatchCollection matches = regex.Matches(input);

            string result = input;

            if (matches.Count > 0)
            {                
                string left = input.Substring(0, matches[0].Index);
                string inner = matches[0].Value;
                string right = input.Substring(matches[0].Index + matches[0].Value.Length, input.Length - left.Length - inner.Length);

                GroupCollection groups = matches[0].Groups;
                var index = inner.Length - groups[2].Length - 1;
                var sign = inner[index];

                inner = GetResultPlusMinus(sign, groups, left);
                string tempResult = left + inner + right;
                result = tempResult.Replace(',', '.');
                return OperationPlusMinus(result);                
            }
            return result;
        }

        private string GetResultPlusMinus(char sign, GroupCollection groups, string left)
        {
            double firstNumber;
            double secondNumber;
            double resultOperation = 0;

            if (sign.Equals('+'))
            {
                firstNumber = GetNumberFromString(groups[1].Value);

                secondNumber = GetNumberFromString(groups[2].Value);

                resultOperation = firstNumber + secondNumber;
            }
            else if (sign.Equals('-'))
            {
                firstNumber = GetNumberFromString(groups[1].Value);

                secondNumber = GetNumberFromString(groups[2].Value);

                resultOperation = firstNumber - secondNumber;
            }

            if (resultOperation < 0)
            {
                if (left.Equals(""))
                {
                    return resultOperation.ToString();
                }
                return "(" + resultOperation.ToString() + ")";
            }
            
            return resultOperation.ToString();            
        }

        private double GetNumberFromString(string input)
        {
            string stringNumber;

            if (input.Contains('('))
            {
                stringNumber = input.Substring(1, input.Length - 2);
            }
            else
            {
                stringNumber = input;
            }

            return double.Parse(stringNumber, styles, provider);
        }

        private string GetResultMultiDiv(string left, string inner)
        {
            string[] temp;
            double firstNumber;
            double secondNumber;
            double resultOperation = 0;

            if (inner.Contains('/'))
            {
                temp = inner.Split('/');

                firstNumber = GetNumberFromString(temp[0]);

                secondNumber = GetNumberFromString(temp[1]);

                resultOperation = firstNumber / secondNumber;
            }
            else if (inner.Contains('*'))
            {
                temp = inner.Split('*');

                firstNumber = GetNumberFromString(temp[0]);

                secondNumber = GetNumberFromString(temp[1]);

                resultOperation = firstNumber * secondNumber;
            }

            if (resultOperation > 0)
            {
                if (left.Equals(""))
                {
                    return resultOperation.ToString();
                }
                return "+" + resultOperation.ToString();
            }            
            
            return resultOperation.ToString();            
        }

        // парсинг выражения с операциями умножить и разделить
        private string OperationMultiDiv(string input)
        {            
            var regExp = @"\(?[+-]?[0-9]+\.?[0-9]*\)?[\*\/]\(?\-?[0-9]+\.?[0-9]*\)?";
            Regex regex = new Regex(regExp);
            MatchCollection matches = regex.Matches(input);

            string result = input;

            if (matches.Count > 0)
            {                
                string left = input.Substring(0, matches[0].Index);
                string inner = matches[0].Value;
                string right = input.Substring(matches[0].Index + matches[0].Value.Length, input.Length - left.Length - inner.Length);

                inner = GetResultMultiDiv(left, inner);
                string tempResult = left + inner + right;
                result = tempResult.Replace(',', '.');
                return OperationMultiDiv(result);                
            }
            return result;
        }
    }
}
