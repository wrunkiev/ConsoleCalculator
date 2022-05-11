using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;

namespace ConsoleCalculator.Tests
{
    [TestClass]
    public class UserInputValidatorTests
    {
        private string filePath = @"test.txt";

        [TestMethod]        
        public void ValidateInputExpressionInConsole_StringAndBool_Pass()
        {
            //arrange
            string stringInput = "1+2+3*4+5+6";
            UserInputValidator uiv = new UserInputValidator();
            string expected = "";

            //act            
            var result = uiv.ValidateInputExpression(stringInput, true);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ValidateInputExpressionInConsole_StringAndBool_Error()
        {
            //arrange
            string stringInput = "1+2+3*4+(5+6)";
            UserInputValidator uiv = new UserInputValidator();
            string expected = UserMessageConstant.ErrorExpressionMsg;

            //act            
            var result = uiv.ValidateInputExpression(stringInput, true);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ValidateInputExpressionInFile_StringAndBool_Pass()
        {
            //arrange
            string stringInput = "1+2+3*4+(5+6)";
            UserInputValidator uiv = new UserInputValidator();
            string expected = "";

            //act            
            var result = uiv.ValidateInputExpression(stringInput, false);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ValidateInputExpressionInFile_StringAndBool_Error()
        {
            //arrange
            string stringInput = "1+2+3*4+(5+6)*f";
            UserInputValidator uiv = new UserInputValidator();
            string expected = UserMessageConstant.ErrorExpressionMsg;

            //act            
            var result = uiv.ValidateInputExpression(stringInput, false);

            //assert
            Assert.AreEqual(expected, result);
        }        

        [TestInitialize]
        public void TestInit()
        {            
            File.Create(filePath).Close();                        
        }

        [TestMethod]
        public void ValidateFilePath_String_Pass()
        {
            //arrange            
            UserInputValidator uiv = new UserInputValidator();
            string expected = "";

            //act            
            var result = uiv.ValidateFilePath(filePath);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ValidateFilePath_String_Error()
        {
            //arrange            
            UserInputValidator uiv = new UserInputValidator();
            string expected = UserMessageConstant.ErrorFoundFilePathMsg;
            string fileStringPath = @"C:\Soft\Sources for Projects\test.txt";

            //act            
            var result = uiv.ValidateFilePath(fileStringPath);

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
