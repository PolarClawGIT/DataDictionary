using System.Diagnostics.CodeAnalysis;
namespace DataDictionary.Resource.Enumerations;

/// <summary>
/// Enumeration support class for DomainProperty.
/// </summary>
public class DomainPropertyEnumeration : IEnumeration<DomainPropertyType, DomainPropertyEnumeration>
{
    /// <inheritdoc />
    public required DomainPropertyType Value { get; init; }

    /// <inheritdoc />
    public required String Name { get; init; }

    /// <inheritdoc />
    public required String DisplayName { get; init; }

    /// <summary>
    /// Internal Constructor for DomainProperty Enumeration
    /// </summary>
    /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
    DomainPropertyEnumeration() : base() { }

    /// <summary>
    /// Internal list that contains all the values.
    /// </summary>
    static readonly List<DomainPropertyEnumeration> values = new List<DomainPropertyEnumeration>()
    {
        new DomainPropertyEnumeration() { Value = DomainPropertyType.Null, Name = String.Empty, DisplayName = "not defined" },
        new DomainPropertyEnumeration() { Value = DomainPropertyType.String, Name = "String", DisplayName = "String" },
        new DomainPropertyEnumeration() { Value = DomainPropertyType.Integer, Name = "Integer", DisplayName = "Integer" },
        new DomainPropertyEnumeration() { Value = DomainPropertyType.List, Name = "List", DisplayName = "List" },
        new DomainPropertyEnumeration() { Value = DomainPropertyType.Xml, Name = "Xml", DisplayName = "Xml" },
        new DomainPropertyEnumeration() { Value = DomainPropertyType.MS_ExtendedProperty, Name = "MS_ExtendedProperty", DisplayName = "MS Extended Property" },
    };

    /// <inheritdoc />
    public static IReadOnlyDictionary<DomainPropertyType, DomainPropertyEnumeration> Values { get { return values.ToDictionary(d => d.Value); } }

    /// <inheritdoc cref="IEnumeration{TEnum, TSelf}.Cast(TEnum)" />
    public static DomainPropertyEnumeration Cast(DomainPropertyType source)
    { return IEnumeration<DomainPropertyType, DomainPropertyEnumeration>.Cast(source); }

    /// <inheritdoc />
    public static DomainPropertyEnumeration Parse(String s, IFormatProvider? provider)
    { return IEnumeration<DomainPropertyType, DomainPropertyEnumeration>.Parse(s); }

    /// <inheritdoc />
    public static Boolean TryParse([NotNullWhen(true)] String? s, IFormatProvider? provider, [MaybeNullWhen(false)] out DomainPropertyEnumeration result)
    { return IEnumeration<DomainPropertyType, DomainPropertyEnumeration>.TryParse(s, out result); }

    /// <inheritdoc />
    public Boolean Equals(DomainPropertyEnumeration? other)
    { return other is DomainPropertyEnumeration && Value.Equals(other.Value); }

    /// <inheritdoc/>
    public override Boolean Equals(object? other)
    { return other is DomainPropertyEnumeration value && Equals(other); }

    /// <inheritdoc/>
    public static Boolean operator ==(DomainPropertyEnumeration left, DomainPropertyEnumeration right)
    { return left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator !=(DomainPropertyEnumeration left, DomainPropertyEnumeration right)
    { return !left.Equals(right); }

    /// <inheritdoc/>
    public override int GetHashCode()
    { return HashCode.Combine(Value); }

    public override String ToString()
    { return Name; }
}