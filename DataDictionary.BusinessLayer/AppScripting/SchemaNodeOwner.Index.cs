using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface ISchemaNodeOwnerIndex : ISchemaNodeOwnerKey
    { }

    /// <inheritdoc/>
    public class SchemaNodeOwnerIndex : SchemaNodeOwnerKey, ISchemaNodeOwnerIndex,
        IKeyEquality<ISchemaNodeOwnerIndex>, IKeyEquality<SchemaNodeOwnerIndex>
    {
        /// <inheritdoc cref="SchemaNodeOwnerKey(ISchemaNodeOwnerKey)"/>
        public SchemaNodeOwnerIndex(ISchemaNodeOwnerIndex source) : base(source) { }

        /// <inheritdoc cref="SchemaNodeOwnerKey(ISchemaNodeKey)"/>
        public SchemaNodeOwnerIndex(ISchemaNodeIndex source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(ISchemaNodeOwnerIndex? other)
        { return other is ISchemaNodeOwnerKey key && Equals(new SchemaNodeOwnerKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(SchemaNodeOwnerIndex? other)
        { return other is ISchemaNodeOwnerKey key && Equals(new SchemaNodeOwnerKey(key)); }
    }
}
