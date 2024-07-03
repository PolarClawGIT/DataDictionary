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

public static class TemplateDirectoryExtension
{
    /// <summary>
    /// Gets the Data from TemplateDirectoryEnumeration
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static TemplateDirectoryEnumeration Data(this TemplateDirectoryType source)
    { return TemplateDirectoryEnumeration.AsDictionary[source]; }
}