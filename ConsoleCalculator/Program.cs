using Microsoft.Extensions.DependencyInjection;
using System;

namespace ConsoleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IValidator, UserInputValidator>()
                .AddSingleton<IFileInteraction, FileInteraction>()
                .AddSingleton<IStringService, StringService>()
                .AddSingleton<IConsoleInteraction, ConsoleInteraction>()
                .AddSingleton<ProgramManager>();

            //logic of choice mode of calculator
            if (args == null || args.Length == 0)
            {
                serviceProvider.AddSingleton<BaseCalculator, ConsoleCalculator>();                
            }
            else
            {
                serviceProvider.AddSingleton<BaseCalculator, FileCalculator>();                
            }

            serviceProvider.BuildServiceProvider().GetService<ProgramManager>().Start(args);
        }
    }
}
