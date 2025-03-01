using DataDictionary.DataLayer.AppModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IDefinitionSubType : IDefinition, IBindingPropertyChanged
    {
        Guid? IDefinitionKey.DefinitionId { get { return DefinitionId; } }
        String? IDefinition.DefinitionSummary { get { return DefinitionSummary; } }
        String? IDefinition.DefinitionText { get { return DefinitionText; } }

        /// <inheritdoc cref="IDefinitionKey.DefinitionId"/>
        new Guid? DefinitionId { get; set; }

        /// <inheritdoc cref="IDefinition.DefinitionSummary"/>
        new String? DefinitionSummary { get; set; }

        /// <inheritdoc cref="IDefinition.DefinitionText"/>
        new String? DefinitionText { get; set; }
    }
}
