using System.Diagnostics.CodeAnalysis;
namespace DataDictionary.Resource.Enumerations;

/// <summary>
/// Enumeration support class for Database Routine type.
/// </summary>
public class TemplateDirectoryEnumeration : IEnumeration<TemplateDirectoryType, TemplateDirectoryEnumeration>
{
    /// <inheritdoc />
    public required TemplateDirectoryType Value { get; init; }

    /// <inheritdoc />
    public required String Name { get; init; }

    /// <inheritdoc />
    public required String DisplayName { get; init; }

    public required DirectoryInfo? Directory { get; init; }

    /// <summary>
    /// Internal Constructor for Database Routine Enumeration
    /// </summary>
    /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
    TemplateDirectoryEnumeration() : base() { }

    /// <summary>
    /// Internal list that contains all the values.
    /// </summary>
    static readonly List<TemplateDirectoryEnumeration> values = new List<TemplateDirectoryEnumeration>()
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

    /// <inheritdoc cref="IEnumeration{TEnum, TSelf}.Cast(TEnum)" />
    public static TemplateDirectoryEnumeration Cast(TemplateDirectoryType source)
    { return IEnumeration<TemplateDirectoryType, TemplateDirectoryEnumeration>.Cast(source); }

    /// <inheritdoc />
    public static IReadOnlyDictionary<TemplateDirectoryType, TemplateDirectoryEnumeration> AsDictionary { get { return values.ToDictionary(d => d.Value); } }

    /// <inheritdoc />
    public static TemplateDirectoryEnumeration Parse(String s, IFormatProvider? provider)
    { return IEnumeration<TemplateDirectoryType, TemplateDirectoryEnumeration>.Parse(s); }

    /// <inheritdoc />
    public static Boolean TryParse([NotNullWhen(true)] String? s, IFormatProvider? provider, [MaybeNullWhen(false)] out TemplateDirectoryEnumeration result)
    { return IEnumeration<TemplateDirectoryType, TemplateDirectoryEnumeration>.TryParse(s, out result); }

    /// <inheritdoc />
    public Boolean Equals(TemplateDirectoryEnumeration? other)
    { return other is TemplateDirectoryEnumeration && Value.Equals(other.Value); }

    /// <inheritdoc/>
    public override Boolean Equals(object? other)
    { return other is TemplateDirectoryEnumeration value && Equals(other); }

    /// <inheritdoc/>
    public static Boolean operator ==(TemplateDirectoryEnumeration left, TemplateDirectoryEnumeration right)
    { return left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator !=(TemplateDirectoryEnumeration left, TemplateDirectoryEnumeration right)
    { return !left.Equals(right); }

    /// <inheritdoc/>
    public override int GetHashCode()
    { return HashCode.Combine(Value); }

    public override String ToString()
    { return Name; }
}
