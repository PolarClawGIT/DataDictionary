using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface IDocumentIndexName : IDocumentKeyName
    { }

    /// <inheritdoc/>
    public class DocumentIndexName : DocumentKeyName, IDocumentIndexName,
        IKeyEquality<IDocumentIndexName>, IKeyEquality<DocumentIndexName>
    {
        /// <inheritdoc cref="DocumentKeyName(IDocumentKeyName)"/>
        public DocumentIndexName(IDocumentIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IDocumentIndexName? other)
        { return other is IDocumentKeyName value && Equals(new DocumentKeyName(value)); }

        /// <inheritdoc/>
        public Boolean Equals(DocumentIndexName? other)
        { return other is IDocumentKeyName value && Equals(new DocumentKeyName(value)); }

        /// <summary>
        /// Convert DocumentIndexName to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(DocumentIndexName source)
        { return new DataIndexName() { Title = source.DocumentTitle ?? String.Empty }; }
    }
}
