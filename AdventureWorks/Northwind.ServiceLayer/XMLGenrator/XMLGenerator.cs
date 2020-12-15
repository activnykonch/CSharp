using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Linq;
using Northwind.Models;
using Northwind.ServiceLayer.DataTransfer;
using Northwind.ServiceLayer.XMLGenrator;
using Northwind.ServiceLayer.Interfaces;
using System.IO;
using System.Xml.Schema;

namespace Northwind.ServiceLayer.XMLGenrator
{
    public class XMLGenerator<T> : IGenerator where T : class
    {
        private readonly IEnumerable<T> collection;
        private string XMLPath;
        private string XSDPath;

        public XMLGenerator(IEnumerable<T> collection)
        {
            this.collection = collection;
        }

        public string GenerateXML()
        {
            if (collection == null)
                return string.Empty;
            XMLPath = Path.Combine(Environment.CurrentDirectory, Path.Combine(typeof(T).Name + ".xml"));
            using(FileStream file = File.Create(XMLPath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ICollection<T>));
                serializer.Serialize(file, collection);
            }
            return XMLPath;
        }

        public string GenerateXSD()
        {
            if (collection == null)
                return string.Empty;
            XSDPath = Path.Combine(Environment.CurrentDirectory, Path.Combine(typeof(T).Name + ".xsd"));
            var xmlserializer = new XmlSerializer(typeof(IEnumerable<T>));
            string XMLString;
            using (var stringWriter = new StringWriter())
            {
                using (var writer = XmlWriter.Create(stringWriter, new XmlWriterSettings() { Indent = true }))
                {
                    xmlserializer.Serialize(writer, collection);
                    XMLString = stringWriter.ToString();
                }
            }

            XmlReader reader = XmlReader.Create(new StringReader(XMLString));
            XmlSchemaSet schemaSet = new XmlSchemaSet();
            XmlSchemaInference schema = new XmlSchemaInference();
            schemaSet = schema.InferSchema(reader);

            foreach (XmlSchema s in schemaSet.Schemas())
            {
                using (FileStream file = File.Create(XSDPath))
                {
                    s.Write(file);
                }
            }
            return XSDPath;
        }
    }
}
