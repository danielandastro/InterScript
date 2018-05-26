using System;
using LexerLib;

namespace Parser
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //Intersharp lex = new Intersharp();
            string read = Console.ReadLine();
            read = Intersharp.Lexer(read);
            //string type = "";
            /*type = Intersharp.Lexer(read).Split('{','}')[1].Replace("datatype ","");
            Console.WriteLine(type);
            string id = Intersharp.Lexer(read).Split('{','}')[3].Replace("varId ","");
            Console.WriteLine(id);
            string data = Intersharp.Lexer(read).Split('{','}')[5].Replace("vardata ","");
            Console.WriteLine(data);
            Console.ReadKey();*/
            var i = 0;
            var reRecompile = new string[5];
            foreach (string s in read.Split('{', '}'))
            {
                if (s == " ") //check if it is just a newline character
                    continue;
                
            }

            Console.ReadKey();
        }
    }
}