using System.Diagnostics.CodeAnalysis;
namespace DataDictionary.Resource.Enumerations;

/// <summary>
/// Enumeration support class for Database Routine type.
/// </summary>
public class TemplateDirectoryEnumeration : Enumeration<TemplateDirectoryType, TemplateDirectoryEnumeration>
{

    public DirectoryInfo? Directory { get; init; } = null;

    /// <summary>
    /// Internal Constructor for Database Routine Enumeration
    /// </summary>
    /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
    TemplateDirectoryEnumeration(TemplateDirectoryType value, String name) : base(value, name) { }

    /// <summary>
    /// Internal Constructor for Database Routine Enumeration
    /// </summary>
    /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
    TemplateDirectoryEnumeration(TemplateDirectoryType value, String name, DirectoryInfo directory) : this(value, name)
    { Directory = directory; }

    static TemplateDirectoryEnumeration()
    {
        List<TemplateDirectoryEnumeration> data = new List<TemplateDirectoryEnumeration>()
        {
            new TemplateDirectoryEnumeration(TemplateDirectoryType.Null,        String.Empty) { DisplayName = "not defined" },
            new TemplateDirectoryEnumeration(TemplateDirectoryType.MySources,   "My Sources",
                new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "source","repos"))),
            new TemplateDirectoryEnumeration(TemplateDirectoryType.MyDocuments, "My Documents",
                new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments))),
            new TemplateDirectoryEnumeration(TemplateDirectoryType.MyDownloads, "My Downloads",
                new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads"))),
        };
        BuildDictionary(data);
    }
}
