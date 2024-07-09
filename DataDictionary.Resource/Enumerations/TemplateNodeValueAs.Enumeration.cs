using System.Diagnostics.CodeAnalysis;
namespace DataDictionary.Resource.Enumerations;

/// <summary>
/// Enumeration support class for Template Node Value As type.
/// </summary>
public class TemplateNodeValueAsEnumeration : Enumeration<TemplateNodeValueAsType, TemplateNodeValueAsEnumeration>
{
    /// <summary>
    /// Internal Constructor for Database Routine Enumeration
    /// </summary>
    /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
    TemplateNodeValueAsEnumeration(TemplateNodeValueAsType value, String name) : base(value, name) { }

    static TemplateNodeValueAsEnumeration()
    {
        List<TemplateNodeValueAsEnumeration> data = new List<TemplateNodeValueAsEnumeration>()
        {
            new TemplateNodeValueAsEnumeration(TemplateNodeValueAsType.none,         String.Empty){ DisplayName = "not defined" },
            new TemplateNodeValueAsEnumeration(TemplateNodeValueAsType.ElementText,  "Element.Text"),
            new TemplateNodeValueAsEnumeration(TemplateNodeValueAsType.ElementCData, "Element.CData"),
            new TemplateNodeValueAsEnumeration(TemplateNodeValueAsType.ElementXML,   "Element.XML"),
            new TemplateNodeValueAsEnumeration(TemplateNodeValueAsType.Attribute,    "Attribute"),
        };

        BuildDictionary(data);
    }
}
