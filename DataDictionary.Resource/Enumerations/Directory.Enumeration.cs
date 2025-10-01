using System.Diagnostics.CodeAnalysis;
namespace DataDictionary.Resource.Enumerations;

/// <summary>
/// Interface for a Directory Type Enumeration.
/// </summary>
public interface IDirectoryEnumeration : IEnumeration<DirectoryType>
{
    /// <summary>
    /// Directory Information for the Directory Enum
    /// </summary>
    DirectoryInfo? Directory { get; }
}

/// <summary>
/// Enumeration support class for Directory Enum.
/// </summary>
class DirectoryEnumeration : Enumeration<DirectoryType, DirectoryEnumeration>,
    IDirectoryEnumeration
{
    /// <inheritdoc/>
    public DirectoryInfo? Directory { get; init; } = null;

    /// <summary>
    /// Internal Constructor for Directory Enumeration
    /// </summary>
    /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
    DirectoryEnumeration(DirectoryType value, String name) : base(value, name) { }

    /// <summary>
    /// Internal Constructor for Directory Enumeration
    /// </summary>
    /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
    DirectoryEnumeration(DirectoryType value, String name, DirectoryInfo directory) : this(value, name)
    { Directory = directory; }

    static DirectoryEnumeration()
    {
        List<DirectoryEnumeration> data = new List<DirectoryEnumeration>()
        {
            new DirectoryEnumeration(DirectoryType.Null,        String.Empty) { DisplayName = "not defined" },
            new DirectoryEnumeration(DirectoryType.MySources,   "My Sources",
                new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "source","repos"))),
            new DirectoryEnumeration(DirectoryType.MyDocuments, "My Documents",
                new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments))),
            new DirectoryEnumeration(DirectoryType.MyDownloads, "My Downloads",
                new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads"))),
        };
        BuildDictionary(data);
    }
}
