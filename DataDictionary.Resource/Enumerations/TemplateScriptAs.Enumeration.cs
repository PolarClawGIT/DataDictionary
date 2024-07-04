using System.Diagnostics.CodeAnalysis;
namespace DataDictionary.Resource.Enumerations;

/// <summary>
/// Enumeration support class for Template Script As type.
/// </summary>
public class TemplateScriptAsEnumeration : IEnumeration<TemplateScriptAsType, TemplateScriptAsEnumeration>
{
    /// <inheritdoc />
    public required TemplateScriptAsType Value { get; init; }

    /// <inheritdoc />
    public required String Name { get; init; }

    /// <inheritdoc />
    public required String DisplayName { get; init; }

    /// <summary>
    /// Returns the extension used by the Script type.
    /// </summary>
    /// <returns></returns>
    public required String Extension { get;init; }

    /// <summary>
    /// Internal Constructor for Database Routine Enumeration
    /// </summary>
    /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
    TemplateScriptAsEnumeration() : base() { }

    /// <summary>
    /// Internal list that contains all the values.
    /// </summary>
    static readonly List<TemplateScriptAsEnumeration> values = new List<TemplateScriptAsEnumeration>()
    {
        new TemplateScriptAsEnumeration() { Value = TemplateScriptAsType.none,   Name = String.Empty, DisplayName = "not defined", Extension = String.Empty },
        new TemplateScriptAsEnumeration() { Value = TemplateScriptAsType.CSharp, Name = "C#",     DisplayName = "C#",     Extension = "cs"},
        new TemplateScriptAsEnumeration() { Value = TemplateScriptAsType.VBNet,  Name = "VB.Net", DisplayName = "VB.Net", Extension = "vb"},
        new TemplateScriptAsEnumeration() { Value =  TemplateScriptAsType.MsSql, Name = "Ms SQL", DisplayName = "Ms SQL", Extension = "sql"},
        new TemplateScriptAsEnumeration() { Value =  TemplateScriptAsType.Text,  Name = "Text",   DisplayName = "Text",   Extension = "txt"},
        new TemplateScriptAsEnumeration() { Value =  TemplateScriptAsType.XML,   Name = "XML",    DisplayName = "XML",    Extension = "xml"},
    };

    /// <inheritdoc />
    public static IReadOnlyDictionary<TemplateScriptAsType, TemplateScriptAsEnumeration> AsDictionary { get { return values.ToDictionary(d => d.Value); } }

    /// <inheritdoc cref="IEnumeration{TEnum, TSelf}.Cast(TEnum)" />
    public static TemplateScriptAsEnumeration Cast(TemplateScriptAsType source)
    { return IEnumeration<TemplateScriptAsType, TemplateScriptAsEnumeration>.Cast(source); }

    /// <inheritdoc />
    public static TemplateScriptAsEnumeration Parse(String s, IFormatProvider? provider)
    { return IEnumeration<TemplateScriptAsType, TemplateScriptAsEnumeration>.Parse(s); }

    /// <inheritdoc />
    public static Boolean TryParse([NotNullWhen(true)] String? s, IFormatProvider? provider, [MaybeNullWhen(false)] out TemplateScriptAsEnumeration result)
    { return IEnumeration<TemplateScriptAsType, TemplateScriptAsEnumeration>.TryParse(s, out result); }

    /// <inheritdoc />
    public Boolean Equals(TemplateScriptAsEnumeration? other)
    { return other is TemplateScriptAsEnumeration && Value.Equals(other.Value); }

    /// <inheritdoc/>
    public override Boolean Equals(object? other)
    { return other is TemplateScriptAsEnumeration value && Equals(other); }

    /// <inheritdoc/>
    public static Boolean operator ==(TemplateScriptAsEnumeration left, TemplateScriptAsEnumeration right)
    { return left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator !=(TemplateScriptAsEnumeration left, TemplateScriptAsEnumeration right)
    { return !left.Equals(right); }

    /// <inheritdoc/>
    public override int GetHashCode()
    { return HashCode.Combine(Value); }

    public override String ToString()
    { return Name; }
}
