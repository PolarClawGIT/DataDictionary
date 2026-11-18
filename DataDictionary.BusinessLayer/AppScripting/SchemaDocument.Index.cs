using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface ISchemaDocumentIndex : ISchemaDocumentKeyName
    { }

    /// <inheritdoc/>
    public class SchemaDocumentIndex : SchemaDocumentKeyName, ISchemaDocumentIndex,
        IKeyEquality<ISchemaDocumentIndex>, IKeyEquality<SchemaDocumentIndex>
    {
        /// <inheritdoc cref="SchemaDocumentKeyName"/>
        public SchemaDocumentIndex() : base() { }

        /// <inheritdoc cref="SchemaDocumentKeyName"/>
        public SchemaDocumentIndex(ISchemaDocumentIndex source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(ISchemaDocumentIndex? other)
        { return other is IDocumentKey key && Equals(new DocumentKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(SchemaDocumentIndex? other)
        { return other is IDocumentKey key && Equals(new DocumentKey(key)); }
    }
}
