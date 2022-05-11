using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;

namespace ConsoleCalculator.Tests
{
    [TestClass]
    public class ConsoleInteractionTests
    {        
        [TestMethod]
        public void ConsoleWriteLine_String_Pass()
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

        [TestMethod]
        public void ConsoleReadLine_String_Pass()
        {
            //arrange
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("test");

            var stringReader = new StringReader(stringBuilder.ToString());
            Console.SetIn(stringReader);

            ConsoleInteraction wc = new ConsoleInteraction();

            var expected = "test";

            //act            
            var line = wc.ConsoleReadLine();

            //assert
            Assert.AreEqual(expected, line);
        }
    }
}
