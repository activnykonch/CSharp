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
<<<<<<< HEAD
        private readonly List<T> collection;
=======
<<<<<<< HEAD
        private readonly List<T> collection;
=======
        private readonly IEnumerable<T> collection;
>>>>>>> 11a14985404befefc2083c4d4468b2eb9064e369
>>>>>>> ed88f2039df0f11b9548e3465822d9aad58615c8
        private string XMLPath;
        private string XSDPath;

        public XMLGenerator(IEnumerable<T> collection)
        {
<<<<<<< HEAD
            this.collection = (List<T>)collection;
=======
<<<<<<< HEAD
            this.collection = (List<T>)collection;
=======
            this.collection = collection;
>>>>>>> 11a14985404befefc2083c4d4468b2eb9064e369
>>>>>>> ed88f2039df0f11b9548e3465822d9aad58615c8
        }

        public string GenerateXML()
        {
            if (collection == null)
                return string.Empty;
            XMLPath = Path.Combine(Environment.CurrentDirectory, Path.Combine(typeof(T).Name + ".xml"));
            using(FileStream file = File.Create(XMLPath))
            {
<<<<<<< HEAD
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
=======
<<<<<<< HEAD
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
=======
                XmlSerializer serializer = new XmlSerializer(typeof(ICollection<T>));
>>>>>>> 11a14985404befefc2083c4d4468b2eb9064e369
>>>>>>> ed88f2039df0f11b9548e3465822d9aad58615c8
                serializer.Serialize(file, collection);
            }
            return XMLPath;
        }

        public string GenerateXSD()
        {
            if (collection == null)
                return string.Empty;
            XSDPath = Path.Combine(Environment.CurrentDirectory, Path.Combine(typeof(T).Name + ".xsd"));
<<<<<<< HEAD
            var xmlserializer = new XmlSerializer(typeof(List<T>));
=======
<<<<<<< HEAD
            var xmlserializer = new XmlSerializer(typeof(List<T>));
=======
            var xmlserializer = new XmlSerializer(typeof(IEnumerable<T>));
>>>>>>> 11a14985404befefc2083c4d4468b2eb9064e369
>>>>>>> ed88f2039df0f11b9548e3465822d9aad58615c8
            string XMLString;
            using (var stringWriter = new StringWriter())
            {
                using (var writer = XmlWriter.Create(stringWriter, new XmlWriterSettings() { Indent = true }))
                {
<<<<<<< HEAD
                    xmlserializer.Serialize(writer, collection);
=======
<<<<<<< HEAD
                        xmlserializer.Serialize(writer, collection);
=======
                    xmlserializer.Serialize(writer, collection);
>>>>>>> 11a14985404befefc2083c4d4468b2eb9064e369
>>>>>>> ed88f2039df0f11b9548e3465822d9aad58615c8
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
