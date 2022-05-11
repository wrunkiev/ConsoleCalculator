using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;

namespace ConsoleCalculator.Tests
{
    [TestClass]
    public class FileInteractionTests
    {
        private string filePath = @"test.txt";
        private string[] fileLines = {"1+2*(3+2)",
                                      "1+x+4",
                                      "2+15/3+4*2",
                                      "",
                                      "4.45-34*(-3)+45*34/5+4"};

        [TestInitialize]
        public void TestInit()
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            File.WriteAllLines(filePath, fileLines);
        }

        [TestMethod]
        public void ReadFromFile_StringPath_StringArray()
        {
            //arrange
            FileInteraction fs = new FileInteraction();

            //act            
            string[] result = fs.ReadFromFile(filePath);

            //assert
            for (int i = 0; i < fileLines.Length; i++)
            {
                Assert.AreEqual(fileLines[i], result[i]);
            }
        }

        [TestMethod]
        public void WriteToFile_StringPathAndData()
        {
            //arrange
            FileInteraction fs = new FileInteraction();

            //act           
            fs.WriteToFile(filePath, fileLines);
            string[] result = fs.ReadFromFile(filePath);

            //assert
            for (int i = 0; i < fileLines.Length; i++)
            {
                Assert.AreEqual(fileLines[i], result[i]);
            }           
        }

        [TestMethod]
        public void GetFilePathOutput_StringPathIn_StringPathOut()
        {
            //arrange
            FileInteraction fs = new FileInteraction();
            var expected = @"\test-result.txt";

            //act           
            var result = fs.GetFilePathOutput(filePath);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            File.Create(filePath).Close();

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
