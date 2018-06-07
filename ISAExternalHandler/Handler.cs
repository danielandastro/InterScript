using System.Diagnostics;
using System.IO;
using System.Text;
namespace ISAExternalHandler
{
    public class Start
    {
        public static void Runner(string lang, string data)
        {
            switch (lang)
                
            {
                 case "python":
                     Process.Start("python", data);
                    
                     break;
            }
        }
    }
}