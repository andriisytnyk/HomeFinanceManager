using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace XmlWriterReader
{
    public static class XmlWriter<T>
    {
        public static void Write(List<T> list, string fileName)
        {
            var formatter = new XmlSerializer(typeof(List<T>));
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Create))
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
