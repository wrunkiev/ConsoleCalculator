using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;

namespace ConsoleCalculator.Tests
{
    [TestClass]
    public class StringServiceTests
    {
        [TestMethod]
        public void ClearExpression_String_Pass()
        {
            //arrange
            string stringInput = " t e s t ";            
            string expected = "test";
            StringService ss = new StringService();

            //act            
            var result = ss.ClearExpression(stringInput);

            //assert
            Assert.AreEqual(expected, result);
        }
    }
}
