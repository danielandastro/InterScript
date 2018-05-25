using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Lexer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Lexer("string[] x = new class()"));
            Console.WriteLine(Lexer("string x = dog"));
            Console.WriteLine(Lexer("int[] get = new class()"));
            Console.WriteLine(Lexer("retrieve string x"));
            Console.ReadKey();
        }

        static string Lexer(string command)
        {
            var operators = new[]
            {
                "+","-","/","*","**"
            };

            var commands = new[]
            {
                "print","retrieve","run","clear","exit"
            };

            var dataTypes = new[]
            {
                "string", "int", "string[]", "int[]"
            };

            var classes = new[]
            {
                ""
                //ToDo: Populate this with class instances
            };

            var split = command.Split(' ');
            var lexerReturn = "";
            var varDetails = command.Split('=');
            foreach (string s in split)
                
            {
                /*if (dataTypes.Contains(s))
                {
                    lexerReturn += $"{{datatype {s.Trim()}}} ";
                    lexerReturn += "{vardata " + vardata[1] + "}";
                }
                
                
                else if (commands.Contains(s))
                    lexerReturn += $"{{command {s.Trim()}}} ";
                else if (operators.Contains(s))
                    lexerReturn += $"{{operator {s.Trim()}}} ";
                else if (classes.Contains(s))
                    lexerReturn += $"{{class {s.Trim()}}} ";

                if ((dataTypes.Contains(s)) && (commands.Contains(s) == false))
                {
                    lexerReturn += "{varid " + split[1] + "}";
                }*/
                if (dataTypes.Contains(s) && command.Contains("=")) //check for assignment
                {
                    lexerReturn += $"{{datatype {s.Trim()}}} ";
                    lexerReturn += $"{{varId {varDetails[0].Split(' ')[1]}}} ";
                    lexerReturn += "{vardata" + varDetails[1] + "}";
                }
                else if (commands.Contains(s))
                { 
                    lexerReturn += $"{{command {s.Trim()}}} ";
                    //lexerReturn += $"{{commandArgumentDatatype {}}}"
                    lexerReturn += "{commandArgument " + split[2] + "}";
                    
                }

            }

            return lexerReturn;
        }
    }
}
