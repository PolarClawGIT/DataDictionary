using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface IDocumentObjectValue : IDocumentObjectItem,
        IDocumentIndex, ITemplateObjectIndex,
        IScopeType
    { }

    /// <inheritdoc/>
    public class DocumentObjectValue : DocumentObjectItem, IDocumentObjectValue
    {
        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingObject; } }

        /// <inheritdoc/>
        public DocumentObjectValue() : base()
        { }

        /// <inheritdoc cref="DocumentObjectItem.DocumentObjectItem(IDocumentItem)"/>
        public DocumentObjectValue(IDocumentValue document) : base(document)
        { }
    }
}
