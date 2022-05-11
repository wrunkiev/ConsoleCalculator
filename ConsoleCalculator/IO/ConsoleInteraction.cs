using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleCalculator
{
    public class ConsoleInteraction : IConsoleInteraction
    {
        public void ConsoleWriteLine(object message)
        {
            Console.WriteLine(message);
        }        

        public string ConsoleReadLine()
        {
            return Console.ReadLine();
        }
    }
}
