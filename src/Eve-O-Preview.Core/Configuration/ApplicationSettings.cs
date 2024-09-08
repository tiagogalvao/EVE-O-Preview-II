using EveOPreview.Core.Configuration.Interface;
using Newtonsoft.Json;

namespace EveOPreview.Core.Configuration;

public class ApplicationSettings : IStorage
{
    private const string CONFIGURATION_FILE_NAME = "EVE-O PreviewV7.json";
    public string ConfigurationFileName => CONFIGURATION_FILE_NAME;
    
    // Application Settings
    public bool CompatibilityMode { get; set; }
    public bool MinimizeToTray { get; set; }
    
    public bool ClientMinimizeInactive { get; set; }
    
    public bool ThumbnailAlwaysOnTop { get; set; }
    public bool ThumbnailHideActiveClient { get; set; }
    public int ThumbnailHideDelay { get; set; }
    public bool ThumbnailHideOnLostFocus { get; set; }
    public bool ThumbnailHideWhenInactive { get; set; }
    public int ThumbnailRefreshPeriod { get; set; }
    
    /// <summary>
    /// Character specific settings
    /// </summary>
    public List<CharacterSettings> CharacterSettings { get; set; } =  new List<CharacterSettings>();
    
    /// <summary>
    /// Default Settings for Thumbnails
    /// </summary>
    public CharacterThumbnailSettings CharacterThumbnailSettings { get; set; } = new CharacterThumbnailSettings();
    
    public void Load()
    {
        if (!File.Exists(ConfigurationFileName))
        {
            return;
        }

        string rawData = File.ReadAllText(ConfigurationFileName);
        JsonConvert.PopulateObject(rawData, this);

        // Validate data after loading it
        //this._thumbnailConfiguration.ApplyRestrictions();
    }
    
    public void Save()
    {
        string rawData = JsonConvert.SerializeObject(this, Formatting.Indented);

        try
        {
            File.WriteAllText(ConfigurationFileName, rawData);
        }
        catch (IOException)
        {
            // Ignore error if for some reason the updated config cannot be written down
        }
    }
}