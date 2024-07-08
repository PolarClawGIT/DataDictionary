using System.Diagnostics.CodeAnalysis;
namespace DataDictionary.Resource.Enumerations;

/// <summary>
/// Enumeration support class for Database Routine type.
/// </summary>
public class TemplateDirectoryEnumeration : Enumeration<TemplateDirectoryType, TemplateDirectoryEnumeration>
{

    public required DirectoryInfo? Directory { get; init; }

    /// <summary>
    /// Internal Constructor for Database Routine Enumeration
    /// </summary>
    /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
    TemplateDirectoryEnumeration() : base() { }

    /// <summary>
    /// Internal list that contains all the values.
    /// </summary>
    static readonly new List<TemplateDirectoryEnumeration> Data = new List<TemplateDirectoryEnumeration>()
    {
        new TemplateDirectoryEnumeration()
        {
            Value = TemplateDirectoryType.Null, Name = String.Empty, DisplayName = "not defined",
            Directory = null
        },
        new TemplateDirectoryEnumeration()
        {
            Value = TemplateDirectoryType.MySources, Name = "MySources", DisplayName = "My Sources",
            Directory = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "source","repos"))
        },
        new TemplateDirectoryEnumeration()
        {
            Value = TemplateDirectoryType.MyDocuments, Name = "MyDocuments", DisplayName = "My Documents",
            Directory =new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments))
        },
        new TemplateDirectoryEnumeration()
        {
            Value = TemplateDirectoryType.MyDownloads, Name = "MyDownloads", DisplayName = "My Downloads",
            Directory = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads"))
        },
    };

}
