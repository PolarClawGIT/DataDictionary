using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IDefinitionValue : IDefinitionItem, IDefinitionIndex, IDefinitionIndexName,
        IScopeType, ITemporal
    { }

    /// <inheritdoc/>
    public partial class DefinitionValue : DefinitionItem, IDefinitionValue, IPathValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ModelDefinition; } }

        /// <inheritdoc/>
        public DefinitionValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new DefinitionIndex(this),
                GetPath = () => new PathIndex(DefinitionTitle),
                GetScope = () => Scope,
                GetTitle = () => DefinitionTitle ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(DefinitionTitle),
                IsTitleChanged = (e) => e.PropertyName is nameof(DefinitionTitle)
            };
        }
    }
}
