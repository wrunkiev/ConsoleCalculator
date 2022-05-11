using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;

namespace ConsoleCalculator.Tests
{
    [TestClass]
    public class ConsoleCalculatorTests
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
        public void Calculate_String_Pass(double result, string input)
        {
            //arrange
            ConsoleCalculator cc = new ConsoleCalculator();
            var tempResult = result.ToString().Replace(',', '.');

            //act            
            string expected = cc.Calculate(input);

            //assert
            Assert.AreEqual(expected, tempResult);
        }
    }
}
