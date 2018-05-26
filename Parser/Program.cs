using System;
using System.Linq;
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
                foreach(var str in s.Split(' ')) //break it down into only 'datatype' or 'string' per line of
                {
                    switch (str)
                    {
                        case "datatype":
                            continue;
                        case "varId":
                            break;
                        case "varData":
                            break;
                        case "":
                            break;
                        default:
                            reRecompile[i] = str;
                            break;
                    }
                }

                i++;
            }
            reRecompile = reRecompile.Where(s => !string.IsNullOrEmpty(s)).ToArray();

            Console.ReadKey();
        }
    }
}