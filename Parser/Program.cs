using System;
using System.Linq;
using LexerLib;
using System.Collections.Generic;
using System.Diagnostics;

namespace Parser
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Dictionary<string, string> strings = new Dictionary<string, string>();
            Dictionary<string, int> numbers = new Dictionary<string, int>();
            strings["run"] = "run";
            Console.WriteLine(strings["run"]);
            string read;
            while (true)
            {
                read = Console.ReadLine();
                read = Intersharp.Lexer(read);
                var i = 0;
                var commanddata = new string[5];
                foreach (string s in read.Split('{', '}'))
                {
                    if (s == " ") //check if it is just a newline character
                        continue;
                    foreach (var str in s.Split(' ')) //break it down into only 'datatype' or 'string' per line of
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
                                commanddata[i] = str;
                                break;
                        }
                    }

                    i++;
                }

                commanddata = commanddata.Where(s => !string.IsNullOrEmpty(s)).ToArray();
                switch (commanddata[0])
                {
                    case "string":
                        try{strings[commanddata[1]] = commanddata[2];}
                        catch{strings[commanddata[1]] = null;}

                        break;
                    case "int":
                        numbers[commanddata[1]] = int.Parse(commanddata[2]);
                        break;
                    case "print":
                        Console.WriteLine(commanddata[1]);
                        break;
                    case "run":
                        try
                        {
                            Process.Start(commanddata[1]);
                        }
                        catch (Exception)
                        {
                        }

                        break;
                    case "retrieve":
                        if (commanddata[1].Equals("number"))
                        {
                            try
                            {
                                Console.WriteLine(numbers[commanddata[2]]);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Error: Value not initialised");
                            }
                        }
                        else if (commanddata[1].Equals("string"))
                        {
                            try
                            {
                                Console.WriteLine(strings[commanddata[2]]);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Error: Value not initialised");
                            }
                        }

                        break;
                    case "clear":
                        Console.Clear();
                        break;
                    case "exit":
                        Console.Clear();
                        return;


                }

            }
        }
    }
}