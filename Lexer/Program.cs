﻿using System;
using System.Linq;

namespace Lexer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Lexer("string[] x = new nig"));

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
                "string", "int", "new"
            };

            var classes = new[]
            {
                "string","int","string[]","int[]"
            };

            var split = command.Split(' ');
            var lexerReturn = "";
            foreach (string s in split)
            {
                if (dataTypes.Contains(s))
                    lexerReturn += $"{{datatype {s.Trim()}}} ";
                else if (commands.Contains(s))
                    lexerReturn += $"{{command {s.Trim()}}} ";
                else if (operators.Contains(s))
                    lexerReturn += $"{{operator {s.Trim()}}} ";
                else if (classes.Contains(s))
                    lexerReturn += $"{{class {s.Trim()}}} ";
            }

            return lexerReturn;
        }
    }
}
