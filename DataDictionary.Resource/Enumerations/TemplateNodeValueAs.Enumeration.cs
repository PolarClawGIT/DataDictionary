using System.Diagnostics.CodeAnalysis;
namespace DataDictionary.Resource.Enumerations;

/// <summary>
/// Enumeration support class for Template Node Value As type.
/// </summary>
public class TemplateNodeValueAsEnumeration : IEnumeration<TemplateNodeValueAsType, TemplateNodeValueAsEnumeration>
{
    /// <inheritdoc />
    public required TemplateNodeValueAsType Value { get; init; }

    /// <inheritdoc />
    public required String Name { get; init; }

    /// <inheritdoc />
    public required String DisplayName { get; init; }

    /// <summary>
    /// Internal Constructor for Database Routine Enumeration
    /// </summary>
    /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
    TemplateNodeValueAsEnumeration() : base() { }

    /// <summary>
    /// Internal list that contains all the values.
    /// </summary>
    static readonly List<TemplateNodeValueAsEnumeration> values = new List<TemplateNodeValueAsEnumeration>()
    {
        new TemplateNodeValueAsEnumeration() { Value = TemplateNodeValueAsType.none, Name = String.Empty, DisplayName = "not defined" },
        new TemplateNodeValueAsEnumeration() { Value = TemplateNodeValueAsType.ElementText,  Name = "Element.Text",  DisplayName = "Element.Text"},
        new TemplateNodeValueAsEnumeration() { Value = TemplateNodeValueAsType.ElementCData, Name = "Element.CData", DisplayName = "Element.CData"},
        new TemplateNodeValueAsEnumeration() { Value = TemplateNodeValueAsType.ElementXML,   Name = "Element.XML",   DisplayName = "Element.XML"},
        new TemplateNodeValueAsEnumeration() { Value = TemplateNodeValueAsType.Attribute,    Name = "Attribute",     DisplayName = "Attribute"},
    };

    /// <inheritdoc />
    public static IReadOnlyDictionary<TemplateNodeValueAsType, TemplateNodeValueAsEnumeration> AsDictionary { get { return values.ToDictionary(d => d.Value); } }

    /// <inheritdoc cref="IEnumeration{TEnum, TSelf}.Cast(TEnum)" />
    public static TemplateNodeValueAsEnumeration Cast(TemplateNodeValueAsType source)
    { return IEnumeration<TemplateNodeValueAsType, TemplateNodeValueAsEnumeration>.Cast(source); }

    /// <inheritdoc />
    public static TemplateNodeValueAsEnumeration Parse(String s, IFormatProvider? provider)
    { return IEnumeration<TemplateNodeValueAsType, TemplateNodeValueAsEnumeration>.Parse(s); }

    /// <inheritdoc />
    public static Boolean TryParse([NotNullWhen(true)] String? s, IFormatProvider? provider, [MaybeNullWhen(false)] out TemplateNodeValueAsEnumeration result)
    { return IEnumeration<TemplateNodeValueAsType, TemplateNodeValueAsEnumeration>.TryParse(s, out result); }

    /// <inheritdoc />
    public Boolean Equals(TemplateNodeValueAsEnumeration? other)
    { return other is TemplateNodeValueAsEnumeration && Value.Equals(other.Value); }

    /// <inheritdoc/>
    public override Boolean Equals(object? other)
    { return other is TemplateNodeValueAsEnumeration value && Equals(other); }

    /// <inheritdoc/>
    public static Boolean operator ==(TemplateNodeValueAsEnumeration left, TemplateNodeValueAsEnumeration right)
    { return left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator !=(TemplateNodeValueAsEnumeration left, TemplateNodeValueAsEnumeration right)
    { return !left.Equals(right); }

    /// <inheritdoc/>
    public override int GetHashCode()
    { return HashCode.Combine(Value); }

    public override String ToString()
    { return Name; }
}
