using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace ComingoftheHybrid
{
    static class Program
    {
        private static readonly Dictionary<string, string> Strings = new Dictionary<string, string>();
        private static readonly Dictionary<string, int> Ints = new Dictionary<string, int>();
        private static readonly Dictionary<string, decimal> Decimals = new Dictionary<string, decimal>();
        private static string _allException, _newException;
        private static bool _allowPassiveExceptionHandling = true; //whether to display exception or just store it

        [STAThread]
        //TODO: Make program enter CLI after parsing .IS file
        public static void Main(string[] args)
        {
            //Handling file interpreting and main interfacing
            Console.WriteLine("Welcome to InterScript");
            Console.Write("Open file? ");
            string open = Console.ReadLine();
            if (open != null && (open.Equals("y") || open.Equals("yes") || open.Equals("true")))
            {
                //Console.Write("Path to .IS file: ");
                string path;
                using (OpenFileDialog fd = new OpenFileDialog())
                {
                    fd.ShowDialog();
                    path = fd.FileName;
                }

                using (var file = new System.IO.StreamReader(path))
                {
                    int counter = 0;
                    string line = file.ReadLine();
                    if (line != null)
                    {
                        Parse(line);
                        counter++;
                    }
                }
            }

            //Standard interpreter CLI
            else
            {
                while (true)
                {
                    Console.Write(">");
                    var hold = Console.ReadLine();
                    Parse(hold);
                }
            }
        }

        private static void Parse(string command)
        {
            //Lexer: finds key parts of the program, to parse later.
            string keyword = "", dataType = "", args = "", keywordType = "", varName = "", varData = "";
            var spaceSplit = command.Split(' ');
            var equalSplit = command.Split('=');
            if (spaceSplit.Length <= 2)
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

            //Parsing
            if (keywordType.Equals("command"))
                switch (keyword)
                {
                    case "run":
                        try
                        {
                            Process.Start(args);
                        }
                        catch (Exception)
                        {
                            ExceptionHandler("InvalidProgram");
                        }

                        break;
                    case "retrieve":
                        try
                        {
                            Console.WriteLine(Strings[args]);
                        }
                        catch (Exception)
                        {
                            // ignored
                        }

                        try
                        {
                            Console.WriteLine(Ints[args]);
                        }
                        catch (Exception)
                        {
                            // ignored
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
                    case "exit": return;
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
                        SetLcv(varName, varData);
                        break;
                    default:
                        ExceptionHandler("InvalidKeyword");
                        break;
                }
        }

        private static void ExceptionHandler(string exception)
        {
            _newException = exception;
            _allException += Environment.NewLine + exception;
            if (_allowPassiveExceptionHandling == false) Console.WriteLine(_newException);
        }

        private static void SetLcv(string var, string val)
        {
            switch (var)
            {
                case "passiveexceptions":
                    _allowPassiveExceptionHandling = val.Equals(true);
                    break;
                default:
                    ExceptionHandler("LCVDoesNotExist");
                    break;
            }
        }

        private static void Show(string arg)
        {
            switch (arg)
            {
                case "newexceptions":
                    Console.WriteLine(_newException);
                    break;
                case "allexceptions":
                    Console.WriteLine(_allException);
                    break;
            }
        }
    }
}