using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;

namespace ConsoleCalculator.Tests
{
    [TestClass]
    public class MathParserTests
    {
        [TestMethod]
        [DataRow(0, "0")]
        [DataRow(-3, "-3")]
        [DataRow(12, "3*4")]
        [DataRow(26, "1+2+3*4+5+6")]
        [DataRow(2, "1+2-3*4+5+6")]
        [DataRow(-4, "-8/2")]
        [DataRow(12, "2+3+4+2+1")]
        [DataRow(2, "2+3-4+2-1")]
        [DataRow(-4, "2+3-4*2-1")]
        [DataRow(2, "2+3-4/2-1")]
        [DataRow(15, "2+15/3+4*2")]
        [DataRow(4, "+8/2")]
        [DataRow(4.5, "9/2")]
        [DataRow(8, "+16/2")]
        [DataRow(22, "2*11")]
        [DataRow(33.5, "2+35/2+14")]
        [DataRow(10, "2-36/9-3+15")]
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
        public void GetExprInBreckets_String_Pass(double result, string input)
        {
            //arrange
            MathParser mp = new MathParser();            
            var expected = result.ToString().Replace(',', '.');

            //act            
            var tempResult = mp.GetExprInBreckets(input);

            //assert
            Assert.AreEqual(expected, tempResult);
        }
    }
}
