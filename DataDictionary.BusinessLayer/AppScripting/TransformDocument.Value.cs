using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface ITransformDocumentValue : ITransformDocumentItem, IDocumentIndex, ITransformComposite,
        IScopeType, ITemporal
    { }

    /// <inheritdoc/>
    public class TransformDocumentValue : TransformDocumentItem, ITransformDocumentValue, IPathValue, INamedScopeSourceValue
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
        public TransformDocumentValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new DocumentIndex(this),
                GetPath = () => new PathIndex(PathIndex.Parse(FileName).ToArray()),
                GetScope = () => Scope,
                GetTitle = () => this.FileName ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(FileName),
                IsTitleChanged = (e) => e.PropertyName is nameof(FileName)
            };
        }

        /// <inheritdoc cref="TransformDocumentItem.TransformDocumentItem(ITemplateKey, ITransformKey)"/>
        public TransformDocumentValue(ITransformComposite transform) : base(transform, transform)
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new DocumentIndex(this),
                GetPath = () => new PathIndex(PathIndex.Parse(FileName).ToArray()),
                GetScope = () => Scope,
                GetTitle = () => this.FileName ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(FileName),
                IsTitleChanged = (e) => e.PropertyName is nameof(FileName)
            };
        }
    }
}
