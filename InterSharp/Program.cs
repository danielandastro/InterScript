using System;
using System.CodeDom.Compiler;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

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
            var command = new string[10]; 
            string currentCommand = "";
            while (true)
            {
                Console.Write(">");
                currentCommand = Console.ReadLine();
                command = currentCommand.Split(' ');
                switch (command[0])
                {
                    case "string":
                        strings[command[1]] = command[2];
                        break;
                    case "number":
                        numbers[command[1]] = decimal.Parse(command[2]);
                        break;
                    case "print":
                        Console.WriteLine(command[1]);
                        break;
                    case "run":
                        try{Process.Start(command[1]);}
                        catch (Exception){/*Console.WriteLine("Error: File not found");*/}
                        break;
                    case "retrieve":
                        if (command[1].Equals("number"))
                        {
                            try {Console.WriteLine(numbers[command[2]]);}
                            catch (Exception){Console.WriteLine("Error: Value not initialised");}
                        }
                        else if (command[1].Equals("string"))
                        {
                            try {Console.WriteLine(strings[command[2]]);}
                            catch (Exception){Console.WriteLine("Error: Value not initialised");}
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