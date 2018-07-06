using System.Collections.Generic;
using System.IO;

namespace TxtWriterReader
{
    public static class TxtWriter
    {
        public static void Write(List<string> list, string FileName)
        {
            if (File.Exists(FileName))
            {
                using (var sw = new StreamWriter(FileName, false))
                {
                    foreach (var t in list)
                    {
                        sw.WriteLine(t);
                    }
                }
            }
        }
    }
}
