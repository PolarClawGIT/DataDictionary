namespace DataDictionary.Resource.Enumerations;

/// <summary>
/// Interface for Scripting NodeValueAs
/// </summary>
public interface INodeValueAsType
{
    /// <summary>
    /// How the Value of the Node is to be rendered.
    /// </summary>
    TemplateNodeValueAsType NodeValueAs { get; }
}
