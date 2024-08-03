using System.Diagnostics.CodeAnalysis;
namespace DataDictionary.Resource.Enumerations;

/// <summary>
/// Enumeration support class for Template Script As type.
/// </summary>
public class TemplateScriptAsEnumeration : Enumeration<TemplateScriptAsType, TemplateScriptAsEnumeration>
{
    /// <summary>
    /// Returns the extension used by the Script type.
    /// </summary>
    /// <returns></returns>
    public String Extension { get; init; } = String.Empty;

    /// <summary>
    /// Internal Constructor for Database Routine Enumeration
    /// </summary>
    /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
    TemplateScriptAsEnumeration(TemplateScriptAsType value, String name) : base(value, name) { }

    /// <summary>
    /// Internal Constructor for Database Routine Enumeration
    /// </summary>
    /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
    TemplateScriptAsEnumeration(TemplateScriptAsType value, String name, String extension) : this(value, name)
    { Extension = extension; }

    static TemplateScriptAsEnumeration()
    {
        List<TemplateScriptAsEnumeration> data = new List<TemplateScriptAsEnumeration>()
        {
            new TemplateScriptAsEnumeration(TemplateScriptAsType.none,   String.Empty) { DisplayName = "not defined"},
            new TemplateScriptAsEnumeration(TemplateScriptAsType.CSharp, "C#",     "cs"),
            new TemplateScriptAsEnumeration(TemplateScriptAsType.VBNet,  "VB.Net", "vb"),
            new TemplateScriptAsEnumeration(TemplateScriptAsType.MsSql,  "Ms SQL", "sql"),
            new TemplateScriptAsEnumeration(TemplateScriptAsType.Text,   "Text",   "txt"),
            new TemplateScriptAsEnumeration(TemplateScriptAsType.XML,    "XML",    "xml"),
        };

        BuildDictionary(data);
    }
}
