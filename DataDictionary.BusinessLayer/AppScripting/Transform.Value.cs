using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface ITransformValue : ITransformItem, ITransformIndex, ITemplateIndex,
        IScopeType, ITemporal
    { }

    /// <inheritdoc/>
    public class TransformValue : TransformItem, ITransformValue, IPathValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingTransform; } }

        /// <inheritdoc/>
        public TransformValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new TransformIndex(this),
                GetPath = () => new PathIndex(Scope),
                GetScope = () => Scope,
                GetTitle = () => TransformTitle ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(TransformTitle),
                IsTitleChanged = (e) => e.PropertyName is nameof(TransformTitle)
            };
        }
    }
}
