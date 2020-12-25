using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Northwind.Models;

namespace Northwind.ConfigurationManager.Provider
{
    public class SettingsManager
    {
        private readonly string path = null;

        public SettingsManager(string path)
        {
            if (File.Exists(path))
            {
                this.path = (Path.GetExtension(path) == ".xml"
                    || Path.GetExtension(path) == ".json") ? path : null;
            }
            else if (Directory.Exists(path))
            {
                var fileEntries = from file in Directory.GetFiles(path) where 
                                  Path.GetExtension(file) == ".xml" ||
                                  Path.GetExtension(file) == ".json" select file;

                this.path = fileEntries.Count() != 0 ? fileEntries.First() : null;
            }
        }

        public T GetSettings<T>() where T : class
        {
            if (path is null)
            {
                throw new Error("Cannot find configuration file", nameof(Northwind.ConfigurationManager.Provider.SettingsManager), DateTime.Now);
            }

            var provider = new Provider<T>(path);
            return provider.configurationParser.Parse();
        }

        public async Task<T> GetSettingsAsync<T>() where T : class
        {
            return await Task.Run(() => GetSettings<T>());
        }
    }
}
