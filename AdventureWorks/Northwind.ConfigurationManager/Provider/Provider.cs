using System.IO;
using Northwind.ConfigurationManager.Interfaces;
using Northwind.ConfigurationManager.Parser;

namespace Northwind.ConfigurationManager.Provider
{
    public class Provider<T> where T : class
    {
        public readonly IConfigurationParser<T> configurationParser;

        public Provider(string path)
        {
            switch (Path.GetExtension(path))
            {
                case ".xml":
                    configurationParser = new XmlParser<T>(path);
                    break;

                case ".json":
                    configurationParser = new JsonParser<T>(path);
                    break;
            }
        }
    }
}