using System.Diagnostics.CodeAnalysis;
namespace DataDictionary.Resource.Enumerations;

/// <summary>
/// Interface for a Directory Type Enumeration.
/// </summary>
public interface IDirectoryEnumeration : IEnumeration<DirectoryType>
{
    /// <summary>
    /// The SpecialFolder to use as the base for the directory.
    /// </summary>
    Environment.SpecialFolder SpecialFolder { get; }

    /// <summary>
    /// The Relative folder within the SpecialFolder for the directory.
    /// </summary>
    String? RelativeFolder { get; set; }

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
    public Environment.SpecialFolder SpecialFolder { get; init; }

    /// <inheritdoc/>
    public String? RelativeFolder { get; set; }

    /// <inheritdoc/>
    public DirectoryInfo? Directory
    {
        get
        {
            if (this.Value is DirectoryType.Null) { return null; }

            String path = Environment.GetFolderPath(SpecialFolder);
            if (RelativeFolder is not null)
            { path = Path.Combine(path, RelativeFolder); }

            return new DirectoryInfo(path);
        }
    }

    /// <summary>
    /// Internal Constructor for Directory Enumeration
    /// </summary>
    /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
    DirectoryEnumeration(DirectoryType value, String name) : base(value, name) { }

    static DirectoryEnumeration()
    {
        List<DirectoryEnumeration> data = new List<DirectoryEnumeration>()
        {
            new DirectoryEnumeration(DirectoryType.Null,        String.Empty)   { DisplayName = "not defined" },
            new DirectoryEnumeration(DirectoryType.MyDocuments, "My Documents") {SpecialFolder = Environment.SpecialFolder.MyDocuments},
            new DirectoryEnumeration(DirectoryType.MyDownloads, "My Downloads") {SpecialFolder = Environment.SpecialFolder.UserProfile, RelativeFolder = "Downloads" },
            new DirectoryEnumeration(DirectoryType.Projects,    "VS Projects")  {SpecialFolder = Environment.SpecialFolder.UserProfile, RelativeFolder = Path.Combine("source","repos") },
            new DirectoryEnumeration(DirectoryType.Data,        "App. Data")    {SpecialFolder = Environment.SpecialFolder.MyDocuments, RelativeFolder = "DataDictionary" },
        };

        BuildDictionary(data);
    }
}
