namespace DataDictionary.Resource.Enumerations;

/// <summary>
/// List of supported rendering methods for Node Values
/// </summary>
public enum TemplateNodeValueAsType // TemplateNodeValueAsEnumeration
{
    /// <summary>
    /// Not Defined or do not render
    /// </summary>
    none,

    /// <summary>
    /// Render value as an Element Text
    /// </summary>
    ElementText,

    /// <summary>
    /// Render value as an Element CData
    /// </summary>
    ElementCData,

    /// <summary>
    /// Render value as an Element with the data as a child XML node.
    /// </summary>
    ElementXML,

    /// <summary>
    /// Render value as an Attribute Text. Attribute Name = Data
    /// </summary>
    Attribute
}
