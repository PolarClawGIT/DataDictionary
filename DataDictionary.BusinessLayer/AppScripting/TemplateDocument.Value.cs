using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppScripting
{

    /// <inheritdoc/>
    public interface ITemplateDocumentValue : ITemplateDocumentItem, IDocumentIndex, 
        IScopeType, ITemporal
    { }

    /// <inheritdoc/>
    public class TemplateDocumentValue : TemplateDocumentItem, ITemplateDocumentValue, IPathValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathItem IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingDocument; } }


        /// <inheritdoc/>
        public TemplateDocumentValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new DocumentIndex(this),
                GetPath = () => new PathItem(PathItem.Parse(ScriptedFileName).ToArray()),
                GetScope = () => Scope,
                GetTitle = () => this.ScriptedFileName ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(ScriptedFileName),
                IsTitleChanged = (e) => e.PropertyName is nameof(ScriptedFileName)
            };

        }

        /// <inheritdoc cref="TemplateDocumentItem.TemplateDocumentItem(ITemplateKey)"/>
        public TemplateDocumentValue(ITemplateIndex template) : base(template)
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new DocumentIndex(this),
                GetPath = () => new PathItem(PathItem.Parse(ScriptedFileName).ToArray()),
                GetScope = () => Scope,
                GetTitle = () => this.ScriptedFileName ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(ScriptedFileName),
                IsTitleChanged = (e) => e.PropertyName is nameof(ScriptedFileName)
            };
        }
    }
}
