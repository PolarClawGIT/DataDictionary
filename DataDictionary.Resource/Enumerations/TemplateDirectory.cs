// Place holder for file nesting purposes.
namespace DataDictionary.Resource.Enumerations;

/// <summary>
/// Interface for TemplateDirectoryType
/// </summary>
public interface ITemplateDirectory
{
    /// <summary>
    /// Root Directory to place documents in (must be a supported Special Folder).
    /// </summary>
    TemplateDirectoryType RootDirectory { get; }
}