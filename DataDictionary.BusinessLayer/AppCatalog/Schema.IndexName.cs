using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <inheritdoc/>
    public interface ISchemaIndexName : ISchemaKeyName, ICatalogIndexName
    { }

    /// <inheritdoc/>
    public class SchemaIndexName : SchemaKeyName, ISchemaIndexName,
        IKeyEquality<ISchemaIndexName>, IKeyEquality<SchemaIndexName>
    {
        /// <inheritdoc cref="SchemaKeyName(ISchemaKeyName)"/>
        public SchemaIndexName(ISchemaIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(ISchemaIndexName? other)
        { return other is ISchemaKeyName value && Equals(new SchemaKeyName(value)); }

        /// <inheritdoc/>
        public Boolean Equals(SchemaIndexName? other)
        { return other is ISchemaKeyName value && Equals(new SchemaKeyName(value)); }

        /// <summary>
        /// Convert SchemaIndexName to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(SchemaIndexName source)
        { return new DataIndexName() { Title = source.SchemaName ?? String.Empty }; }
    }
}
