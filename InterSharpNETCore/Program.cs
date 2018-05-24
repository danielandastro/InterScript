using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace InterSharp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Dictionary<string, string> strings = new Dictionary<string, string>();
            Dictionary<string, decimal> numbers = new Dictionary<string, decimal>();
            strings["run"] = "run";
            Console.WriteLine(strings["run"]);
            while (true)
            {
                Console.Write(">");
                var currentCommand = Console.ReadLine();
                if (currentCommand == null)
                    continue;

                var command = currentCommand.Split(' ');
                var vardata = currentCommand.Split('=');
                var printData = currentCommand.Split('(');
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
                        try { Process.Start(command[1]); }
                        catch (Exception) { }
                        break;
                    case "retrieve":
                        if (command[1].Equals("number"))
                        {
                            try { Console.WriteLine(numbers[command[2]]); }
                            catch (Exception) { Console.WriteLine("Error: Value not initialised"); }
                        }
                        else if (command[1].Equals("string"))
                        {
                            try { Console.WriteLine(strings[command[2]]); }
                            catch (Exception) { Console.WriteLine("Error: Value not initialised"); }
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