using System.IO;
using EveOPreview.Configuration.Interface;
using EveOPreview.Core.Configuration.Interface;
using Newtonsoft.Json;

namespace EveOPreview.Configuration.Implementation
{
    internal sealed class ConfigurationStorage : IStorage
    {
        private const string CONFIGURATION_FILE_NAME = "EVE-O Preview.json";
        public string ConfigurationFileName => CONFIGURATION_FILE_NAME;

        private readonly IThumbnailConfiguration _thumbnailConfiguration;

        public ConfigurationStorage(IThumbnailConfiguration thumbnailConfiguration)
        {
            _thumbnailConfiguration = thumbnailConfiguration;
        }
        
        public void Load()
        {
            if (!File.Exists(ConfigurationFileName))
            {
                return;
            }

            string rawData = File.ReadAllText(ConfigurationFileName);
            JsonConvert.PopulateObject(rawData, _thumbnailConfiguration);

            // Validate data after loading it
            _thumbnailConfiguration.ApplyRestrictions();
        }

        public void Save()
        {
            string rawData = JsonConvert.SerializeObject(_thumbnailConfiguration, Formatting.Indented);
            try
            {
                File.WriteAllText(ConfigurationFileName, rawData);
            }
            catch (IOException)
            {
                // Ignore error if for some reason the updated config cannot be written down
            }
        }

        private string GetConfigFileName()
        {
            return string.IsNullOrEmpty(ConfigurationFileName) ? CONFIGURATION_FILE_NAME : ConfigurationFileName;
        }
    }
}