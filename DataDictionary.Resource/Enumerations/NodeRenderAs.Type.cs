namespace DataDictionary.Resource.Enumerations;

/// <summary>
/// List of supported rendering methods for Node Values
/// </summary>
public enum NodeRenderAsType // NodeRenderAsTypeEnumeration
{
    /// <summary>
    /// Not Defined or do not render
    /// </summary>
    none,

    /// <summary>
    /// Render as Element without a Value
    /// </summary>
    Element,

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
    /// Render value as an Attribute, value as Text.
    /// </summary>
    AttributeText,
}
