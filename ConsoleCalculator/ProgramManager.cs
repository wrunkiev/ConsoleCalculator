using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleCalculator
{
    public class ProgramManager
    {
        private readonly IConsoleInteraction _wc;
        private BaseCalculator _baseCalculator;
        private readonly IValidator _userInputValidator;
        private readonly IFileInteraction _fileInteraction;
        private readonly IStringService _stringService;


        public ProgramManager(IValidator userInputValidator,
            IFileInteraction fileInteraction, 
            IStringService stringService,
            IConsoleInteraction consoleInteraction)
        {            
            _userInputValidator = userInputValidator ?? throw new ArgumentNullException(nameof(userInputValidator));
            _fileInteraction = fileInteraction ?? throw new ArgumentNullException(nameof(fileInteraction));
            _stringService = stringService ?? throw new ArgumentNullException(nameof(stringService));
            _wc = consoleInteraction ?? throw new ArgumentNullException(nameof(consoleInteraction));
        }

        public void Start(string[] args)
        {
            //welcome in program
            PrintWelcome();

            //logic of choice mode of calculator
            if (args == null || args.Length == 0)
            {
                InvokeModeConsole();
            }
            else
            {
                InvokeModeFile(args[0]);
            }           
        }

        private void PrintWelcome()
        {
            _wc.ConsoleWriteLine(UserMessageConstant.WelcomeMsg);            
        }        

        private void InvokeModeConsole()
        {
            //read from console expression
            _wc.ConsoleWriteLine(UserMessageConstant.UserInputExpressionMsg);
            var userInputConsole = _wc.ConsoleReadLine();  
            
            //validate input date            
            var resultValidator = _userInputValidator.ValidateInputExpression(userInputConsole, true);
           
            //if we get errors then restart mode for console calculator
            if (resultValidator != "")
            {
                _wc.ConsoleWriteLine(resultValidator);
                InvokeModeConsole();
            }
            
            //clear whitespaces
            var tempExpression = _stringService.ClearExpression(userInputConsole);
            
            //pass line for parsing and get result
            _baseCalculator = new ConsoleCalculator();
            var result =_baseCalculator.Calculate(tempExpression);
            
            //write to console
            _wc.ConsoleWriteLine(tempExpression + " = " + result);            

            //restart program
            RestartConsoleMode();
        }

        private void RestartConsoleMode()
        {
            _wc.ConsoleWriteLine(UserMessageConstant.RestartMsg);
            _wc.ConsoleWriteLine(UserMessageConstant.UserExitMsg);
            string[] args = null;
            var restartValue = _wc.ConsoleReadLine();
            if (!restartValue.Equals(""))
            {
                Start(args);
            }

            //exit program
            Environment.Exit(0);
        }

        private void InvokeModeFile(string path)
        {
            //validate path to file
            UserInputValidator userInputValidator = new UserInputValidator();
            var resultValidator = userInputValidator.ValidateFilePath(path);
            
            //if we get errors then send message to user and exit program
            if (resultValidator != "")
            {
                _wc.ConsoleWriteLine(resultValidator);
                //exit program
                Environment.Exit(0);
            }

            string[] resultRead = _fileInteraction.ReadFromFile(path);
            string[] resultCalculator = new string[resultRead.Length];
            string[] resultTemp = new string[resultRead.Length];
            _baseCalculator = new FileCalculator();

            for (int i = 0; i < resultRead.Length; i++)
            {
                //validate input date            
                var resultFileValidator = _userInputValidator.ValidateInputExpression(resultRead[i], false);

                //clear whitespaces
                resultTemp[i] = _stringService.ClearExpression(resultRead[i]);

                //if we get errors then restart mode for console calculator
                if (resultFileValidator != "")
                {
                    resultCalculator[i] = resultTemp[i] + " = " + resultFileValidator;
                }
                else
                {                    
                    resultCalculator[i] = resultTemp[i] + " = " + _baseCalculator.Calculate(resultTemp[i]);
                }                
            }

            //путь куда писать результат
            var resultFile = _fileInteraction.GetFilePathOutput(path);            

            //write to file
            _fileInteraction.WriteToFile(resultFile, resultCalculator);

            //exit program
            Environment.Exit(0);
        }
    }
}
