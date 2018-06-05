using System.Diagnostics;
using System.IO;
using System.Text;
namespace ISAExternalHandler
{
    public class Start
    {
        public static void Runner(string lang, string data, bool file)
        {
            switch (lang)
                
            {
                 case "python":
                     if(file) Process.Start("python", data);
                     else
                     {
                         var path = @"cacherun.py";
                         if (File.Exists(path)) File.Delete(path);

                         using (var writer = File.Create(path))
                         {
                             var info = new UTF8Encoding(true).GetBytes(data);
                             writer.Write(info, 0, info.Length);
                         }

                         Process.Start("python", "./cacherun.py");
                     }
                     break;
            }
        }
    }
}