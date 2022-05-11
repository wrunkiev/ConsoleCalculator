using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculator.Tests
{
    [TestClass]
    class ProgramManagerTests
    {
        [TestMethod]
        [DataRow(0, "0")]
        public void Start_ArrString_Pass()
        {
            //arrange
            string stringInput = "test";
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            ConsoleInteraction wc = new ConsoleInteraction();
            string expected = "test" + Environment.NewLine;

            //act            
            wc.ConsoleWriteLine(stringInput);

            //assert
            Assert.AreEqual(expected, stringWriter.ToString());
        }
    }
}
