using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleCalculator
{
    public interface IFileInteraction
    {
        public string[] ReadFromFile(string path);

        public void WriteToFile(string path, string[] data);

        public string GetFilePathOutput(string filePathInput);
    }
}
