using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface ISchemaNodeIndexName : ISchemaNodeKeyName
    { }

    /// <inheritdoc/>
    public class SchemaNodeIndexName : SchemaNodeKeyName, ISchemaNodeIndexName,
        IKeyEquality<ISchemaNodeIndexName>, IKeyEquality<SchemaNodeIndexName>
    {
        /// <inheritdoc cref="SchemaNodeKey(ISchemaNodeKey)"/>
        public SchemaNodeIndexName(ISchemaNodeIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(ISchemaNodeIndexName? other)
        { return other is ISchemaNodeKey key && Equals(new SchemaNodeKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(SchemaNodeIndexName? other)
        { return other is ISchemaNodeKey key && Equals(new SchemaNodeKey(key)); }
    }
}
