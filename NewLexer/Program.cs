/*
 * This DOES NOT WORK
 * DO NOT TRY IT
 * TODO MAKE IT WORK
 * CURRENTLY DOES NOT WORK
 * Edit: after my whole rant, this should function as well as the old one, with better performance
 * Now it has all the functionality
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using ISAExternalHandler;

namespace NewLexer
{
    internal class Program
    {
        private static readonly Dictionary<string, string> Strings = new Dictionary<string, string>();
        private static readonly Dictionary<string, int> Ints = new Dictionary<string, int>();
        private static readonly Dictionary<string, decimal> Decimals = new Dictionary<string, decimal>();
        private static string _allException, _newException;
        private static bool _allowPassiveExceptionHandling = true; //whether to display exception or just store it
        private static bool _autocacheclean = true;

        [STAThread]
        public static void Main(string[] args)
        {
            PreFlightChecks(); // Runs all the pre loading 
            //Handling file interpreting and main interfacing
            Console.WriteLine("Welcome to InterScript");
            Console.Write("Open file? ");
            var open = Console.ReadLine();
            if (open != null && (open.Equals("y") || open.Equals("yes") || open.Equals("true")))
            {
                //Console.Write("Path to .IS file: ");
                string path;
                using (var fd = new OpenFileDialog())
                {
                    fd.ShowDialog();
                    path = fd.FileName;
                }

                using (var file = new StreamReader(path))
                {
                    var line = file.ReadLine();
                    while (line != null)
                    {
                        line = file.ReadLine();
                        Lexer(line);
                    }

                    if (_autocacheclean) CacheCleaner();
                }
            }

            //Standard interpreter CLI

            while (true)
            {
                Console.Write(">");
                var hold = Console.ReadLine();
                Lexer(hold);
            }
        }

        private static void Lexer(string command)
        {
            var _Assignment = false; //Can be removed, not used currently
            var spaceSplit = command.Split(' ');
            var equalSplit = command.Split('=');
            var keyword = "";
            var args = "";
            var assignment = "";
            //The following Try block contains the entire lexer, it is a very simple lexer that is completely generalised, with no
            //keyword recognition etc.
            try
            {
                args = spaceSplit[1];
                keyword = spaceSplit[0];
                assignment = equalSplit[1];
            }
            catch (Exception)
            {
                /*This is only here to remove the error*/
            } //Yes this supressant catch block is intentional

            if (assignment != null)
            {
                _Assignment = true;
            }

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
                case "exit":
                    if (_autocacheclean) CacheCleaner();
                    return;
                case "read":
                    try
                    {
                        Strings[args] = Console.ReadLine();
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                    try
                    {
                        Ints[args] = Console.Read();
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                    try
                    {
                        Decimals[args] = decimal.Parse(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        ExceptionHandler("varNotInitialised");
                    }

                    break;
                case "clean":
                    CacheCleaner();
                    break;
                case "string":
                    Strings[args] = assignment;
                    break;
                case "int":
                    try
                    {
                        Ints[args] = int.Parse(assignment);
                    }
                    catch (Exception)
                    {
                        ExceptionHandler("InvalidDeclaration");
                    }

                    break;
                case "decimal":
                    try
                    {
                        Decimals[args] = decimal.Parse(assignment);
                    }
                    catch (Exception)
                    {
                        ExceptionHandler("InvalidDeclaration");
                    }

                    break;
                case "set":
                    try
                    {
                        SetLcv(args, assignment);
                    }
                    catch (Exception)
                    {
                        ExceptionHandler("InvalidDeclaration");
                    }

                    break;
                case "script":
                    try
                    {
                        Start.Runner(args, spaceSplit[2]);
                    }
                    catch (Exception)
                    {
                        ExceptionHandler("NoPathProvided");
                    }

                    break;
                default:
                    ExceptionHandler("InvalidKeyword");
                    break;
            }
        }

        private static void Feeder(string file)
        {
        }

        private static void Cli()
        {
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
                    _allowPassiveExceptionHandling = bool.Parse(val);
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

        private static void CsharpCodeRunner(string execute)
        {
            //new Evaluator(new CompilerContext(new CompilerSettings(), new ConsoleReportPrinter())).Run(execute);
            // that will execute the c# script code (when I sort out the MonoCompiler Service)
        }

        private static void CacheCleaner()
        {
            if (File.Exists(@"cacherun.py")) File.Delete(@"cacherun.py");
            //ToDo: Add cleanup for all supported languages
        }

        private static void PreFlightChecks() //Loads global langauge settings into memory
            //ToDo: Add integrity checks for all executables
        {
            if (!File.Exists(@"config.ini"))
            {
                Console.WriteLine("config.ini missing");
            }
            else
            {
                using (var file = new StreamReader(@"config.ini"))
                {
                    var line = file.ReadLine();
                    while (line != null)
                    {
                        line = file.ReadLine();
                        switch (line)
                        {
                            case "autocleancache":
                                if (line.Contains("true"))
                                {
                                    _autocacheclean = true;
                                }

                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
}