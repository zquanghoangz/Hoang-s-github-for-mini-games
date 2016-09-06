using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace TestPuzzle8
{
    public static class Serializer
    {
        public static string ToXmlString<T>(T objectToSerialize)
        {
            var stream = new MemoryStream();

            TextWriter writer = new StreamWriter(stream, new UTF8Encoding());

            var xmlSerializer = new XmlSerializer(typeof(T));
            xmlSerializer.Serialize(writer, objectToSerialize);

            return Encoding.UTF8.GetString(stream.ToArray(), 0, Convert.ToInt32(stream.Length));
        }

        public static T FromXmlString<T>(String source)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            var sr = new StringReader(source);
            return (T)xmlSerializer.Deserialize(sr);
        }
    }
}
