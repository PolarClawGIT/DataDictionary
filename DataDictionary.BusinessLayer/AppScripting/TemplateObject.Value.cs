using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppScripting
{

    /// <inheritdoc/>
    public interface ITemplateObjectValue : ITemplateObjectItem, ITemplateObjectIndex, ITemplateIndex,
        IScopeType, ITemporal
    { }

    /// <inheritdoc/>
    public class TemplateObjectValue : TemplateObjectItem, ITemplateObjectValue, IPathValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingObject; } }

        /// <inheritdoc/>
        public TemplateObjectValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new TemplateObjectIndex(this),
                GetPath = () => new PathIndex(Scope),
                GetScope = () => Scope,
                GetTitle = () => ObjectName ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(ObjectName),
                IsTitleChanged = (e) => e.PropertyName is nameof(ObjectName)
            };
        }
    }
}
