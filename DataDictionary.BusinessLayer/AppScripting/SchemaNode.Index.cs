using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface ISchemaNodeIndex : ISchemaNodeKey
    { }

    /// <inheritdoc/>
    public class SchemaNodeIndex : SchemaNodeKey, ISchemaNodeIndex,
        IKeyEquality<ISchemaNodeIndex>, IKeyEquality<SchemaNodeIndex>
    {
        /// <inheritdoc cref="SchemaNodeKey(ISchemaNodeKey)"/>
        public SchemaNodeIndex(ISchemaNodeIndex source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(ISchemaNodeIndex? other)
        { return other is ISchemaNodeKey key && Equals(new SchemaNodeKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(SchemaNodeIndex? other)
        { return other is ISchemaNodeKey key && Equals(new SchemaNodeKey(key)); }

        /// <summary>
        /// Convert SchemaDefinitionIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(SchemaNodeIndex source)
        { return new DataIndex() { SystemId = source.NodeId ?? Guid.Empty }; }
    }
}
