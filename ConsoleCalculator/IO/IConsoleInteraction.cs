using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleCalculator
{
    public interface IConsoleInteraction
    {
        public string ConsoleReadLine();

        public void ConsoleWriteLine(object message);
    }
}
