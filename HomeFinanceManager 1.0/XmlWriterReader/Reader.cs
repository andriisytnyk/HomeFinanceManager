using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XmlWriterReader
{
    /// <summary>
    /// Class for working with files.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class XmlReader<T>
    {
        static List<T> list = new List<T>();

        public static bool condition = true;
   
        /// <summary>
        /// Read all T-type items from file.
        /// </summary>
        /// <param name="FileName">Specifies the file name.</param>
        /// <returns>List of T-type items</returns>
        public static List<T> Read(string FileName)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<T>));
            using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
            {
                    list = (List<T>)formatter.Deserialize(fs);
            }
            return list;
        }
    }
}
