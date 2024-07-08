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
    public required Char Code { get; init; }

    /// <summary>
    /// Internal Constructor for Database Routine Enumeration
    /// </summary>
    /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
    LibraryMemberEnumeration() : base() { }

    static LibraryMemberEnumeration()
    {
        List<LibraryMemberEnumeration> data = new List<LibraryMemberEnumeration>()
        {
            new LibraryMemberEnumeration() { Value = LibraryMemberType.Null,       Code = ' ', Name = String.Empty, DisplayName = "not defined" },
            new LibraryMemberEnumeration() { Value = LibraryMemberType.NameSpace,  Code = 'N', Name = "NameSpace", DisplayName = "NameSpace"},
            new LibraryMemberEnumeration() { Value = LibraryMemberType.Type,       Code = 'T', Name = "Type", DisplayName = "Type"},
            new LibraryMemberEnumeration() { Value = LibraryMemberType.Field,      Code = 'F', Name = "Field", DisplayName = "Field"},
            new LibraryMemberEnumeration() { Value = LibraryMemberType.Property,   Code = 'P', Name = "Property", DisplayName = "Property"},
            new LibraryMemberEnumeration() { Value = LibraryMemberType.Method,     Code = 'M', Name = "Method", DisplayName = "Method"},
            new LibraryMemberEnumeration() { Value = LibraryMemberType.Event,      Code = 'E', Name = "Event", DisplayName = "Event"},
            new LibraryMemberEnumeration() { Value = LibraryMemberType.Parameter,  Code = '@', Name = "Parameter", DisplayName = "Parameter"},
        };

        BuildDictionary(data);
    }
}
