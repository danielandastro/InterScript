using System;
using System.Collections.Generic;

namespace ComingoftheHybrid
{
    internal class Program
    {
        public static Dictionary<string, string> Strings = new Dictionary<string, string>();
        public static Dictionary<string, int> Ints = new Dictionary<string, int>();
        public static Dictionary<string, decimal> Decimals = new Dictionary<string, decimal>();
        public static string allException, newException;
        public static bool allowPassiveExceptionHandling = true;//whether to display exception or just store it
        public static void Main(string[] args)
        {
            /* Parse("string x = y");
 Parse("retrieve x");
             string test =Console.ReadLine();
             Parse(test);*/
            string hold = "";
            
            hold = Console.ReadLine();
            Parse(hold);
        
    }

        public static void Parse(string command)
        {
            var strings = new Dictionary<string, string>();
            var ints = new Dictionary<string, int>();
            var decimals = new Dictionary<string, decimal>();
            string keyword = "", dataType="", args="", keywordType="", varName="", varData="";
            var spaceSplit = command.Split(' ');
            var equalSplit = command.Split('=');
            // since the parser and lexer are joined, i made it easier by using separate variables for everything
            if (spaceSplit.Length == 2)
            {
                keywordType = "command";
                keyword = spaceSplit[0];
                args = spaceSplit[1];
            }
            else
            {
                keywordType = "declaration";
                varName = spaceSplit[1];
                dataType = spaceSplit[0];
                varData = equalSplit[1];
            }
//parsing begins here
            if (keywordType.Equals("command"))
            {
                switch (keyword)
                {
                    case "run":
                        System.Diagnostics.Process.Start(args);
                        break;
                    case "retrieve":
                        try{Console.WriteLine(Strings [args]);}
                        catch(Exception){}
                        try{Console.WriteLine(Ints [args]);}
                        catch(Exception){}

                        try
                        {
                            Console.WriteLine(Decimals[args]);
                        }
                        catch (Exception)
                        {
                            ExceptionHandler("varNotInitialised");
                        }
                        break;
                    
                }

                
            }
            else
            {
                switch (dataType)
                {
                    case "string":
                        Strings[varName] = varData;
                        break;
                    case "int":
                        Ints[varName] = int.Parse(varData);
                        break;
                    case "decimal":
                        Decimals[varName] = decimal.Parse(varData);
                        break;
                }
                
                
            }
        }

        public static void ExceptionHandler(string exception)
        {
            newException = exception;
            allException += Environment.NewLine + exception;
            if (allowPassiveExceptionHandling == true) {Console.WriteLine(newException);}

        }

        public static void SetLCV(string var, string val)
        {
            
            
        }
    }
}