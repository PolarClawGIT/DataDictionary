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
    public required String Extension { get;init; }

    /// <summary>
    /// Internal Constructor for Database Routine Enumeration
    /// </summary>
    /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
    TemplateScriptAsEnumeration() : base() { }

    static TemplateScriptAsEnumeration ()
    {
        List<TemplateScriptAsEnumeration> data = new List<TemplateScriptAsEnumeration>()
        {
            new TemplateScriptAsEnumeration() { Value = TemplateScriptAsType.none,   Name = String.Empty, DisplayName = "not defined", Extension = String.Empty },
            new TemplateScriptAsEnumeration() { Value = TemplateScriptAsType.CSharp, Name = "C#",     DisplayName = "C#",     Extension = "cs"},
            new TemplateScriptAsEnumeration() { Value = TemplateScriptAsType.VBNet,  Name = "VB.Net", DisplayName = "VB.Net", Extension = "vb"},
            new TemplateScriptAsEnumeration() { Value =  TemplateScriptAsType.MsSql, Name = "Ms SQL", DisplayName = "Ms SQL", Extension = "sql"},
            new TemplateScriptAsEnumeration() { Value =  TemplateScriptAsType.Text,  Name = "Text",   DisplayName = "Text",   Extension = "txt"},
            new TemplateScriptAsEnumeration() { Value =  TemplateScriptAsType.XML,   Name = "XML",    DisplayName = "XML",    Extension = "xml"},
        };

        BuildDictionary(data);
    }
}
