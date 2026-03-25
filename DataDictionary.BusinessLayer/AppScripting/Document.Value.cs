using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface IDocumentValue : IDocumentItem, IDocumentIndex, ITemplateIndex,
        IScopeType, ITemporal
    { }

    /// <inheritdoc/>
    public class DocumentValue : DocumentItem, IDocumentValue, IPathValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingDocument; } }

        /// <inheritdoc/>
        public DocumentValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new DocumentIndex(this),
                GetPath = () => new PathIndex(Scope),
                GetScope = () => Scope,
                GetTitle = () => FileName ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(FileName),
                IsTitleChanged = (e) => e.PropertyName is nameof(FileName)
            };
        }
    }
}
