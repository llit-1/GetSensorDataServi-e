using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetSensorDataService
{
    internal static class Logging
    {
        public static string Path { get; set; }
        internal static void  Log(string message)
        {
            FileInfo log = new FileInfo(Path);
            if (log.Exists)
            {
                var size = log.Length / 1024;
                if (size >= 1024) log.Delete();
            }
            File.AppendAllText(Path, $"\n{DateTime.Now} {message}");
        }
    }
}
