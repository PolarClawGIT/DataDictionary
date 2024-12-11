using DataDictionary.DataLayer.AppModel;

namespace DataDictionary.DataLayer.DomainData.Definition;

/// <summary>
/// Interface for the Domain Definition
/// </summary>
public interface IDomainDefinition : IDefinitionKey
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