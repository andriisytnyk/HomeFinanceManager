using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtWriterReader
{
    public static class TxtWriter
    {
        public static void Write(List<string> list, string FileName)
        {
            if (File.Exists(FileName))
            {
                using (StreamWriter sw = new StreamWriter(FileName, false))
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        sw.WriteLine(list[i]);
                    }
                }
            }
        }
    }
}
