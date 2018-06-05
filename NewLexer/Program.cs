/*
 * This DOES NOT WORK
 * DO NOT TRY IT
 * TODO MAKE IT WORK
 * CURRENTLY DOES NOT WORK
 */
using System;
using System.Linq;

namespace NewLexer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
Lexer(Console.ReadLine());
        }

        private static void Lexer(string command)
        {
            var finaldata = "";
            var operators = new[]
            {
                "+", "-", "/", "*", "**"
            };

            var commands = new[]
            {
                "print", "retrieve", "run", "clear", "exit", "script"
            };

            var dataTypes = new[]
            {
                "string", "int", "string[]", "int[]", "set"
            };

            var classes = new[]
            {
                ""
                //ToDo: Populate this with classes
            };
            var langs = new[]
            {
                    "python"
          };
            var split = command.Split(' ');
            var varDetails = command.Split('=');
            var lexerReturn = "";
            foreach (string s in split)
            {
                if (dataTypes.Contains(s) && command.Contains("=")) //check for assignment
                {
                    lexerReturn += $"{{datatype {s.Trim()}}} ";
                    lexerReturn += $"{{varId {varDetails[0].Split(' ')[1]}}} ";
                    lexerReturn += $"{{vardata {varDetails[1]}}}";
                }
                else if (langs.Contains(s) && command.Contains(s)) //check for assignment
                {
                    lexerReturn += $"{{command {s.Trim()}}} ";
                    lexerReturn += $"{{language {1}}}";
                    /*try
                    {
                        lexerReturn += $"{{commandArgument {split[2]}}}";
                    }
                    catch
                    {
                        lexerReturn += $"{{commandArgument {null}}}";
                    }*/
                }

                else if (dataTypes.Contains(s))
                {
                    lexerReturn += $"{{datatype {s.Trim()}}}";
                    lexerReturn += $"{{varId {varDetails[0].Split(' ')[1]}}}";
                }

                else if (commands.Contains(s))
                {
                    lexerReturn += $"{{command {s.Trim()}}} ";
                    //lexerReturn += $"{{commandArgumentDatatype {1}}}";
                    try
                    {
                        lexerReturn += $"{{commandArgument {split[2]}}}";
                    }
                    catch
                    {
                        lexerReturn += $"{{commandArgument {null}}}";
                    }
                }
                
                
                
                Parser(lexerReturn);

            }
        }

        private static void Parser(string command)
            {
                Console.WriteLine(command);
            }

            private static void Feeder(string file)
            {
            }

            private static void Cli()
            {
            }
        }
    }