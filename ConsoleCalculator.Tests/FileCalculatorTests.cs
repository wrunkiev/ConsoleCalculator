using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;

namespace ConsoleCalculator.Tests
{
    [TestClass]
    public class FileCalculatorTests
    {
        [TestMethod]
        [DataRow(1, "(1)")]
        [DataRow(3, "1+(1)+1")]
        [DataRow(-5, "(-5)")]
        [DataRow(-5, "-(5)")]
        [DataRow(30, "33+(2-5)")]
        [DataRow(0, "12+(-12)")]                    //From Eugen
        [DataRow(11, "1+2*(3+2)")]                  //From Task
        [DataRow(-2, "(2+3)-(4+3)")]
        [DataRow(-5, "1+2-(3+(4+5)-6)-2")]
        [DataRow(45, "12+((((12)+8)+7)+6)")]        //From Eugen
        [DataRow(3, "12+(3-(8+5))+1")]              //From Eugen
        public void Calculate_String_Pass(double result, string input)
        {
            //arrange
            FileCalculator fc = new FileCalculator();
            var tempResult = result.ToString().Replace(',', '.');

            //act            
            string expected = fc.Calculate(input);

            //assert
            Assert.AreEqual(expected, tempResult);
        }
    }
}
