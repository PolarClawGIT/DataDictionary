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
    TemplateNodeValueAsEnumeration() : base() { }

    static TemplateNodeValueAsEnumeration()
    {
        List<TemplateNodeValueAsEnumeration> data = new List<TemplateNodeValueAsEnumeration>()
        {
            new TemplateNodeValueAsEnumeration() { Value = TemplateNodeValueAsType.none, Name = String.Empty, DisplayName = "not defined" },
            new TemplateNodeValueAsEnumeration() { Value = TemplateNodeValueAsType.ElementText,  Name = "Element.Text",  DisplayName = "Element.Text"},
            new TemplateNodeValueAsEnumeration() { Value = TemplateNodeValueAsType.ElementCData, Name = "Element.CData", DisplayName = "Element.CData"},
            new TemplateNodeValueAsEnumeration() { Value = TemplateNodeValueAsType.ElementXML,   Name = "Element.XML",   DisplayName = "Element.XML"},
            new TemplateNodeValueAsEnumeration() { Value = TemplateNodeValueAsType.Attribute,    Name = "Attribute",     DisplayName = "Attribute"},
        };

        BuildDictionary(data);
    }
}
