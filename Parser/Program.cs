using System;
using System.Runtime.InteropServices;
using LexerLib;
namespace Parser

{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Intersharp lex = new Intersharp();
            string type = "";
            type = Intersharp.Lexer(Console.ReadLine());
            type = type.Split('{','}')[1];
            type = type.Replace("datatype","");
            Console.WriteLine(type);
            Console.ReadKey();
        }
    }
}