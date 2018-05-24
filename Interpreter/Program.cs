using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace Interpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentCommand = "";
            Dictionary<string, string> strings = new Dictionary<string, string>();
            Dictionary<string, decimal> numbers = new Dictionary<string, decimal>();
            strings["run"] = "run";
            Console.WriteLine(strings["run"]);
            var command = new string[10];
            int counter = 0;
            Console.Write("Enter file name or path: ");
            var filepath = Console.ReadLine();
            StreamReader file =
                new StreamReader(@filepath);
            string line;
            while ((line = file.ReadLine()) != null)
            {
                currentCommand = line;
                command = currentCommand.Split(' ');
                string[] vardata = new string[2];
                vardata = currentCommand.Split('=');
                string[] printData = new string[2];
                printData = currentCommand.Split('(');
                switch (command[0])
                {
                    case "string":
                        strings[command[1]] = vardata[1];
                        break;
                    case "number":
                        numbers[command[1]] = decimal.Parse(vardata[1]);
                        break;
                    case "print":
                        Console.WriteLine(printData[1]);
                        break;
                    case "run":
                        try
                        {
                            Process.Start(command[1]);
                        }
                        catch (Exception)
                        {
                        }

                        break;
                    case "retrieve":
                        if (command[1].Equals("number"))
                        {
                            try
                            {
                                Console.WriteLine(numbers[command[2]]);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Error: Value not initialised");
                            }
                        }
                        else if (command[1].Equals("string"))
                        {
                            try
                            {
                                Console.WriteLine(strings[command[2]]);
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
                        counter++;
                }

                
            }
        }
    }
}