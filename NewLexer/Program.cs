/*
 * This DOES NOT WORK
 * DO NOT TRY IT
 * TODO MAKE IT WORK
 * CURRENTLY DOES NOT WORK
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Text;
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
        private static bool _ispath = true;

        [STAThread]
        public static void Main(string[] args)
        {
            Lexer(Console.ReadLine());
        }

        private static void Lexer(string command)
        {
            var _Assignment = false;
            var spaceSplit = command.Split(' ');
            var equalSplit = command.Split('=');
            var keyword = spaceSplit[0];
            var args = spaceSplit[1];
            var assignment = "";
            try
            {
                assignment = equalSplit[1];
            }
            catch (Exception)
            {
            }
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
                default:
                    ExceptionHandler("InvalidKeyword");
                    break;
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

        private static void ScriptRunner(string lang, string script)
        {
            Start.Runner(lang, script, _ispath);
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
                    string line = file.ReadLine();
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