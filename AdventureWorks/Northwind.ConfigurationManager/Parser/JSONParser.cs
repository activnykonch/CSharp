using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.Json;
using Northwind.ConfigurationManager.Interfaces;

namespace Northwind.ConfigurationManager.Parser
{
    class JsonParser<T> : IConfigurationParser<T> where T : class
    {
        private readonly string jsonPath;

        public JsonParser(string jsonPath)
        {
            this.jsonPath = jsonPath;
        }

        public T Parse()
        {
            using (var fileStream = new FileStream(jsonPath, FileMode.OpenOrCreate))
            {
                using (var document = JsonDocument.Parse(fileStream))
                {
                    var element = document.RootElement;

                    if (typeof(T).GetProperties().First().Name
                        != element.EnumerateObject().First().Name)
                    {
                        element = element.GetProperty(typeof(T).Name);
                    }
                    try
                    {
                        return JsonSerializer.Deserialize<T>(element.GetRawText()); ;
                    }
                    catch (Exception ex)
                    {
                        using (var streamWriter = new StreamWriter(
                            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt"),
                            true, Encoding.Default))
                        {
                            streamWriter.WriteLine("Json file deserialization error: " + ex.Message);
                        }

                        return null;
                    }

                }
            }
        }
    }
}
