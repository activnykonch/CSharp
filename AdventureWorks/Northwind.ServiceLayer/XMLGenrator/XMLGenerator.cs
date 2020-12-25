using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;
using System.Threading.Tasks;
using Northwind.ServiceLayer.Interfaces;
using System.IO;
using System.Xml.Schema;

namespace Northwind.ServiceLayer.XMLGenrator
{
    public class XMLGenerator<T> : IGenerator where T : class
    {
        private readonly List<T> collection;
        private string XMLPath;
        private string XSDPath;

        public XMLGenerator(IEnumerable<T> collection)
        {
            this.collection = (List<T>)collection;
        }

        public string GenerateXML()
        {
            if (collection == null)
                return string.Empty;
            XMLPath = Path.Combine(Environment.CurrentDirectory, Path.Combine(typeof(T).Name + ".xml"));
            using(FileStream file = File.Create(XMLPath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                serializer.Serialize(file, collection);
            }
            return XMLPath;
        }

        public async Task<string> GenerateXMLAsync()
        {
            return await Task.Run(() => GenerateXML());
        }

        public string GenerateXSD()
        {
            if (collection == null)
                return string.Empty;
            XSDPath = Path.Combine(Environment.CurrentDirectory, Path.Combine(typeof(T).Name + ".xsd"));

            var xmlserializer = new XmlSerializer(typeof(List<T>));
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

        public async Task<string> GenerateXSDAsync()
        {
            return await Task.Run(() => GenerateXSD());
        }
    }
}
