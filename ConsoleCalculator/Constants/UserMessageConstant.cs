using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleCalculator
{
    public class UserMessageConstant
    {
        public static readonly string WelcomeMsg = "Welcome to program 'Calculator'.";
        
        public static readonly string UserInputExpressionMsg = "Enter expression:";
        public static readonly string ErrorExpressionMsg = "This expression has errors";
        public static readonly string ErrorEmptyExpressionMsg = "This expression is empty or null";
        
        public static readonly string ErrorFoundFilePathMsg = "This file wasn't found";
        public static readonly string UserOutputFilePathMsg = "Enter output file path:";

        public static readonly string RestartMsg = "Will you restart calculator?";
        public static readonly string UserExitMsg = "Enter something:";

    }
}
