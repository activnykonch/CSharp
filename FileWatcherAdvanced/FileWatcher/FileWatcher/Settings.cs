using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using ConfigurationProvider;
using System.ComponentModel;
namespace FileWatcher
{
    public class Settings
    {
        public StorageSettings StorageSettings { get; set; }
        public ArchiveSettings ArchiveSettings { get; set; }
        public CryptingSettings CryptingSettings { get; set; }
    }

    public class StorageSettings
    {
        public string SourceDirectory { get; set; }
        public string ArchiveDirectory { get; set; }
        public string TargetDirectory { get; set; }
    }

    public class ArchiveSettings
    {
        [JsonConverter(typeof(ConfigurationProvider.BooleanConverter))]
        public bool NeedToArchive { get; set; }
    }

    public class CryptingSettings
    {
        public string EncryptionKey { get; set; }
        public string EncryptionIV { get; set; }
    }
}
