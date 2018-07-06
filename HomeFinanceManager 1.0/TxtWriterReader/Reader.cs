using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtWriterReader
{
    public static class TxtReader
    {
        public static List<string> Read(string FileName)
        {
            List<string> list = new List<string>();
            if (File.Exists(FileName))
            {
                using (StreamReader sr = new StreamReader(FileName))
                {
                    string s;
                    while ((s = sr.ReadLine()) != null)
                    {
                        list.Add(s);
                    }
                }
            }
            return list;
        }
    }
}
