using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Common.Tools
{
    public class XMLSerializeHelper
    {
        private static XMLSerializeHelper root = new XMLSerializeHelper();

        public static XMLSerializeHelper GetInstance()
        {
            if (root == null)
            {
                object o = new object();
                lock (o)
                {
                    if (root == null)
                    {
                        root = new XMLSerializeHelper();
                    }
                }
            }
            return root;

        }

        public string SerializeObject(Object obj)
        {
            var xml = "";

            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            StringBuilder sb = new StringBuilder();

            using (TextWriter writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, obj);
            }

            if (sb.Length > 0)
                xml = sb.ToString();
            return xml;
        }

        public string SerializeObjectNoneDeclare(Object obj)
        {
            var xml = "";

            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            XmlWriterSettings setting = new XmlWriterSettings();
            setting.Indent = false;
            //setting.IndentChars=" ";
            setting.NewLineChars = "\r\n";
            setting.Encoding = Encoding.UTF8;
            setting.OmitXmlDeclaration = true;

            StringBuilder sb = new StringBuilder();

            using (XmlWriter writer = XmlWriter.Create(sb,setting))
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add(string.Empty, string.Empty);//remove xmlns:xsi and xmlns:xsd
                serializer.Serialize(writer, obj);
            }

            if (sb.Length > 0)
                xml = sb.ToString();

            return xml;
        }
        public object DeserializeObject(string xml,Type type)
        {
            XmlSerializer _serializer = new XmlSerializer(type);
            object obj;
            using (StringReader reader = new StringReader(xml))
            {
                using(XmlTextReader xmlReader = new XmlTextReader(reader))
                {
                    obj = _serializer.Deserialize(xmlReader);
                }
            }
            return obj;
        }
    }
}
