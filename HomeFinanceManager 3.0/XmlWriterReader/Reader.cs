using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace XmlWriterReader
{
    /// <summary>
    /// Class for working with files.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class XmlReader<T>
    {
        private static List<T> _list = new List<T>();

        public static bool condition = true;
   
        /// <summary>
        /// Read all T-type items from file.
        /// </summary>
        /// <param name="fileName">Specifies the file name.</param>
        /// <returns>List of T-type items</returns>
        public static List<T> Read(string fileName)
        {
            var formatter = new XmlSerializer(typeof(List<T>));
            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                    _list = (List<T>)formatter.Deserialize(fs);
            }
            return _list;
        }
    }
}
