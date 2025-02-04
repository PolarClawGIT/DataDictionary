namespace DataDictionary.DataLayer.AppModel;

/// <summary>
/// Interface for the Model Definition Sub Type
/// </summary>
public interface IDefinition : IDefinitionKey
{
    /// <summary>
    /// Definition Summary (Plain Text, limited length)
    /// </summary>
    public String? DefinitionSummary { get; }

    /// <summary>
    /// Definition Text (Rich Text)
    /// </summary>
    public String? DefinitionText { get; }

}