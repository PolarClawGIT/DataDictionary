using System.Diagnostics.CodeAnalysis;
namespace DataDictionary.Resource.Enumerations;

/// <summary>
/// Enumeration support class for Database Routine type.
/// </summary>
public class LibraryMemberEnumeration : Enumeration<LibraryMemberType, LibraryMemberEnumeration>
{
    /// <summary>
    /// Code use by XML Document for the Type
    /// </summary>
    public Char Code { get; init; }

    /// <summary>
    /// Internal Constructor for Database Routine Enumeration
    /// </summary>
    /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
    LibraryMemberEnumeration(LibraryMemberType value, Char code, String name) : base(value, name)
    { Code = code; }

    /// <summary>
    /// Static constructor, loads data.
    /// </summary>
    static LibraryMemberEnumeration()
    {
        List<LibraryMemberEnumeration> data = new List<LibraryMemberEnumeration>()
        {
            new LibraryMemberEnumeration(LibraryMemberType.Null,       ' ', String.Empty) { DisplayName = "not defined" },
            new LibraryMemberEnumeration(LibraryMemberType.NameSpace,  'N', "NameSpace"),
            new LibraryMemberEnumeration(LibraryMemberType.Type,       'T', "Type"),
            new LibraryMemberEnumeration(LibraryMemberType.Field,      'F', "Field"),
            new LibraryMemberEnumeration(LibraryMemberType.Property,   'P', "Property"),
            new LibraryMemberEnumeration(LibraryMemberType.Method,     'M', "Method"),
            new LibraryMemberEnumeration(LibraryMemberType.Event,      'E', "Event"),
            new LibraryMemberEnumeration(LibraryMemberType.Parameter,  '@', "Parameter"),
        };

        BuildDictionary(data);
    }

    /// <summary>
    /// Tries to parse the Code into a value.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="format"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static Boolean TryParse([NotNullWhen(true)] Char? source, IFormatProvider? format, [MaybeNullWhen(false)] out LibraryMemberEnumeration? result)
    {
        if (Members is null) { result = null; return false; }

        if (LibraryMemberEnumeration.Members.Values.FirstOrDefault(w => w.Code == source) is LibraryMemberEnumeration item)
        { result = item; return true; }
        else { result = null; return false; }

    }
}
