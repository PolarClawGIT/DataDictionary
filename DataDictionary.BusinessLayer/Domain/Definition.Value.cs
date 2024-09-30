using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.DomainData.Definition;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IDefinitionValue : IDomainDefinitionItem, IDefinitionIndex, IDefinitionIndexName
    { }

    /// <inheritdoc/>
    public class DefinitionValue : DomainDefinitionItem, IDefinitionValue, IPathValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public DefinitionValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new DefinitionIndex(this),
                GetPath = () => new PathIndex(DefinitionTitle),
                GetScope = () => Scope,
                GetTitle = () => DefinitionTitle ?? ScopeEnumeration.Cast(Scope).Name,
                IsPathChanged = (e) => e.PropertyName is nameof(DefinitionTitle),
                IsTitleChanged = (e) => e.PropertyName is nameof(DefinitionTitle)
            };
        }
    }
}
