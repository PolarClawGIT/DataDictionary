using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface ISchemaIndexName : IDbSchemaKeyName, ICatalogIndexName
    { }

    /// <inheritdoc/>
    public class SchemaIndexName : DbSchemaKeyName, ISchemaIndexName,
        IKeyEquality<ISchemaIndexName>, IKeyEquality<SchemaIndexName>
    {
        /// <inheritdoc cref="DbSchemaKeyName(IDbSchemaKeyName)"/>
        public SchemaIndexName(ISchemaIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(ISchemaIndexName? other)
        { return other is IDbSchemaKeyName value && Equals(new DbSchemaKeyName(value)); }

        /// <inheritdoc/>
        public Boolean Equals(SchemaIndexName? other)
        { return other is IDbSchemaKeyName value && Equals(new DbSchemaKeyName(value)); }

        /// <summary>
        /// Convert SchemaIndexName to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(SchemaIndexName source)
        { return new DataIndexName() { Title = source.SchemaName ?? String.Empty }; }
    }
}
