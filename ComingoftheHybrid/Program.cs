using System;

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
            string keyword, dataType, args, keywordType, varName, varData;
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
        }
    }
}