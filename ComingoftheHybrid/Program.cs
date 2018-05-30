using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
namespace ComingoftheHybrid
{
    internal class Program
    {
        public static Dictionary<string, string> Strings = new Dictionary<string, string>();
        public static Dictionary<string, int> Ints = new Dictionary<string, int>();
        public static Dictionary<string, decimal> Decimals = new Dictionary<string, decimal>();
        public static string allException, newException;
        public static bool allowPassiveExceptionHandling = true; //whether to display exception or just store it

        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to InterScript");
            Console.Write("Open file? ");
            string open = Console.ReadLine();
            if (open.Equals("y") || open.Equals("yes") || open.Equals("true"))
            {
                //Console.Write("Path to .IS file: ");
                int counter = 0;
                string line, path="";
                OpenFileDialog fd = new OpenFileDialog();
                fd.ShowDialog();
                path = fd.FileName;
                var file=new System.IO.StreamReader(path);  
                while((line = file.ReadLine()) != null)
                {  
                    Parse(line);  
                    counter++;  
                }     
            }
            else{var hold = "";
                while(true){
                    Console.Write(">");
                    hold = Console.ReadLine();
                    Parse(hold);
                }}
        }

        public static void Parse(string command)
        {
            string keyword = "", dataType = "", args = "", keywordType = "", varName = "", varData = "";
            var spaceSplit = command.Split(' ');
            var equalSplit = command.Split('=');
            // since the parser and lexer are joined, i made it easier by using separate variables for everything
            if (spaceSplit.Length <=2)
            {
                keywordType = "command";
                keyword = spaceSplit[0];
                try
                {
                    args = spaceSplit[1];
                }
                catch (Exception)
                {
                    ExceptionHandler("NoArgs");
                }
            }
            else
            {
                keywordType = "declaration";
                varName = spaceSplit[1];
                dataType = spaceSplit[0];

                try
                {
                    varData = equalSplit[1];
                }
                catch (Exception)
                {
                    ExceptionHandler("InvalidExpression");
                }
            }

//parsing begins here
            if (keywordType.Equals("command"))
                switch (keyword)
                {
                    case "run":
                        Process.Start(args);
                        break;
                    case "retrieve":
                        try
                        {
                            Console.WriteLine(Strings[args]);
                        }
                        catch (Exception)
                        {
                        }

                        try
                        {
                            Console.WriteLine(Ints[args]);
                        }
                        catch (Exception)
                        {
                        }

                        try
                        {
                            Console.WriteLine(Decimals[args]);
                        }
                        catch (Exception)
                        {
                            ExceptionHandler("varNotInitialised");
                        }

                        break;
                    case "show":
                        Show(args);
                        break;
                    default:
                        ExceptionHandler("InvalidKeyword");
                        break;
                }
            else
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
                    case "set":
                        SetLCV(varName, varData);
                        break;
                    default:
                        ExceptionHandler("InvalidKeyword");
                        break;
                    
                }
        }

        public static void ExceptionHandler(string exception)
        {
            newException = exception;
            allException += Environment.NewLine + exception;
            if (allowPassiveExceptionHandling == false) Console.WriteLine(newException);
        }

        public static void SetLCV(string var, string val)
        {
            switch (var)
            {
                case "passiveexceptions":
                    if (val.Equals(true))
                        allowPassiveExceptionHandling = true;
                    else
                        allowPassiveExceptionHandling = false;
                    break;
                default:
                    ExceptionHandler("LCVDoesNotExist");
                    break;
            }
        }

        public static void Show(string arg)
        {
            switch (arg)
            {
                case "newexceptions":
                    Console.WriteLine(newException);
                    break;
                case "allexceptions":
                    Console.WriteLine(allException);
                    break;
            }
            
        }
    }
}