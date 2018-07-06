using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XmlWriterReader
{
    public static class XmlWriter<T>
    {
        public static void Write(List<T> list, string FileName)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<T>));
            try
            {
                using (FileStream fs = new FileStream(FileName, FileMode.Create))
                {
                    formatter.Serialize(fs, list);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
