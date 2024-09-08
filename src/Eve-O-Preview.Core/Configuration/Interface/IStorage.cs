namespace EveOPreview.Core.Configuration.Interface;

public interface IStorage
{
    /// <summary>
    /// Configuration file name
    /// </summary>
    string ConfigurationFileName { get; }
    
    /// <summary>
    /// Load configuration from file
    /// </summary>
    void Load();
    
    /// <summary>
    /// Save configuration file
    /// </summary>
    void Save();
}