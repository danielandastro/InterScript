using System.Linq;

namespace LexerLib
{
    public class Intersharp
    {
        public static string Lexer(string command)
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
                "string", "int", "string[]", "int[]"
            };

            var classes = new[]
            {
                ""
                //ToDo: Populate this with class instances
            };

            var split = command.Split(' ');
            var lexerReturn = "";
            var varDetails = command.Split('=');
            foreach (string s in split)
                
            {
                
                if (dataTypes.Contains(s) && command.Contains("=")) //check for assignment
                {
                    lexerReturn += $"{{datatype {s.Trim()}}} ";
                    lexerReturn += $"{{varId {varDetails[0].Split(' ')[1]}}} ";
                    lexerReturn += $"{{vardata {varDetails[1]}}}";
                }

                else if(dataTypes.Contains(s))
                {
                    lexerReturn += $"{{datatype {s.Trim()}}}";
                    lexerReturn += $"{{varId {varDetails[0].Split(' ')[1]}}}";
                }

                else if (commands.Contains(s))
                { 
                    lexerReturn += $"{{command {s.Trim()}}} ";
                    //lexerReturn += $"{{commandArgumentDatatype {}}}"
                    lexerReturn += $"{{commandArgument {split[1]}}}";
                    
                }

            }

            return lexerReturn;
        }
    }
}