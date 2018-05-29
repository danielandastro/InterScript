using System;
using System.Collections.Generic;

namespace ComingoftheHybrid
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Parse("run x");
        }

        public static void Parse(string command)
        {
            var strings = new Dictionary<string, string>();
            var ints = new Dictionary<string, int>();
            var decimals = new Dictionary<string, decimal>();
            string keyword = "", dataType="", args="", keywordType="", varName="", varData="";
            var spaceSplit = command.Split(' ');
            var equalSplit = command.Split('=');
            if (spaceSplit.Length == 2)
            {
                keywordType = "command";
                keyword = spaceSplit[0];
                args = spaceSplit[1];
            }
            else
            {
                keywordType = "declaration";
                varName = spaceSplit[1];
                dataType = spaceSplit[0];
                varData = equalSplit[1];
            }

            if (keywordType.Equals("command"))
            {
                switch (keyword)
                {
                    case "run":
                        System.Diagnostics.Process.Start(args);
                        break;
                    
                    
                }

                
            }
            else
            {
                switch (dataType)
                {
                    case "string":
                        strings[varName] = varData;
                        break;
                    case "int":
                        ints[varName] = int.Parse(varData);
                        break;
                    case "decimal":
                        decimals[varName] = decimal.Parse(varData);
                        break;
                }
                
                
            }
        }
    }
}