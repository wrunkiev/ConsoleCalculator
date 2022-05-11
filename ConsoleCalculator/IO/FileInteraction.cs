using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ConsoleCalculator
{
    public class FileInteraction : IFileInteraction
    {
        public string[] ReadFromFile(string path)
        {
            return File.ReadAllLines(path);
        }

        public void WriteToFile(string path, string[] data)
        {
            File.WriteAllLines(path, data);
        }        

        public string GetFilePathOutput(string filePathInput)
        {
            string path = @filePathInput;
            var fileExt = Path.GetExtension(path);
            var fileName = Path.GetFileNameWithoutExtension(path)+"-result";
            var directoryName = Path.GetDirectoryName(path);
            var filePathOutput = directoryName + "\\" + fileName + fileExt;
            
            return filePathOutput;
        }
    }
}
