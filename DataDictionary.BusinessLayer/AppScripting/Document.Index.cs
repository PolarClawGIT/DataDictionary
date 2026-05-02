using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface IDocumentIndex : IDocumentKey
    { }

    /// <inheritdoc/>
    public class DocumentIndex : DocumentKey, IDocumentIndex,
        IKeyEquality<IDocumentIndex>, IKeyEquality<DocumentIndex>
    {
        /// <inheritdoc cref="DocumentKey()"/>
        public DocumentIndex() : base() { }

        /// <inheritdoc cref="DocumentKey(IDocumentKey)"/>
        public DocumentIndex(IDocumentIndex source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IDocumentIndex? other)
        { return other is IDocumentKey key && Equals(new DocumentKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(DocumentIndex? other)
        { return other is IDocumentKey key && Equals(new DocumentKey(key)); }

        /// <summary>
        /// Convert DocumentIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(DocumentIndex source)
        { return new DataIndex() { SystemId = source.DocumentId ?? Guid.Empty }; }
    }
}
