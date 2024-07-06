using System.Diagnostics.CodeAnalysis;
namespace DataDictionary.Resource.Enumerations;

/// <summary>
/// Enumeration support class for Database Routine type.
/// </summary>
public class LibraryMemberEnumeration : IEnumeration<LibraryMemberType, LibraryMemberEnumeration>
{
    /// <inheritdoc />
    public required LibraryMemberType Value { get; init; }

    /// <inheritdoc />
    public required String Name { get; init; }

    /// <inheritdoc />
    public required String DisplayName { get; init; }

    /// <summary>
    /// Code use by XML Document for the Type
    /// </summary>
    public required Char Code { get; init; }

    /// <summary>
    /// Internal Constructor for Database Routine Enumeration
    /// </summary>
    /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
    LibraryMemberEnumeration() : base() { }

    /// <summary>
    /// Internal list that contains all the values.
    /// </summary>
    static readonly List<LibraryMemberEnumeration> values = new List<LibraryMemberEnumeration>() {
        new LibraryMemberEnumeration() { Value = LibraryMemberType.Null,       Code = ' ', Name = String.Empty, DisplayName = "not defined" },
        new LibraryMemberEnumeration() { Value = LibraryMemberType.NameSpace,  Code = 'N', Name = "NameSpace", DisplayName = "NameSpace"},
        new LibraryMemberEnumeration() { Value = LibraryMemberType.Type,       Code = 'T', Name = "Type", DisplayName = "Type"},
        new LibraryMemberEnumeration() { Value = LibraryMemberType.Field,      Code = 'F', Name = "Field", DisplayName = "Field"},
        new LibraryMemberEnumeration() { Value = LibraryMemberType.Property,   Code = 'P', Name = "Property", DisplayName = "Property"},
        new LibraryMemberEnumeration() { Value = LibraryMemberType.Method,     Code = 'M', Name = "Method", DisplayName = "Method"},
        new LibraryMemberEnumeration() { Value = LibraryMemberType.Event,      Code = 'E', Name = "Event", DisplayName = "Event"},
        new LibraryMemberEnumeration() { Value = LibraryMemberType.Parameter,  Code = '@', Name = "Parameter", DisplayName = "Parameter"},
    };

    /// <inheritdoc />
    public static IReadOnlyDictionary<LibraryMemberType, LibraryMemberEnumeration> Values { get { return values.ToDictionary(d => d.Value); } }

    /// <inheritdoc cref="IEnumeration{TEnum, TSelf}.Cast(TEnum)" />
    public static LibraryMemberEnumeration Cast(LibraryMemberType source)
    { return IEnumeration<LibraryMemberType, LibraryMemberEnumeration>.Cast(source); }

    /// <inheritdoc />
    public static LibraryMemberEnumeration Parse(String s, IFormatProvider? provider)
    { return IEnumeration<LibraryMemberType, LibraryMemberEnumeration>.Parse(s); }

    /// <inheritdoc />
    public static Boolean TryParse([NotNullWhen(true)] String? s, IFormatProvider? provider, [MaybeNullWhen(false)] out LibraryMemberEnumeration result)
    { return IEnumeration<LibraryMemberType, LibraryMemberEnumeration>.TryParse(s, out result); }

    /// <inheritdoc />
    public Boolean Equals(LibraryMemberEnumeration? other)
    { return other is LibraryMemberEnumeration && Value.Equals(other.Value); }

    /// <inheritdoc/>
    public override Boolean Equals(object? other)
    { return other is LibraryMemberEnumeration value && Equals(other); }

    /// <inheritdoc/>
    public static Boolean operator ==(LibraryMemberEnumeration left, LibraryMemberEnumeration right)
    { return left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator !=(LibraryMemberEnumeration left, LibraryMemberEnumeration right)
    { return !left.Equals(right); }

    /// <inheritdoc/>
    public override int GetHashCode()
    { return HashCode.Combine(Value); }

    public override String ToString()
    { return Name; }
}
