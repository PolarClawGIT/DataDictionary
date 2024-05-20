using DataDictionary.BusinessLayer.Database;
using DataDictionary.DataLayer.DomainData.Attribute;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IAttributeIndex : IDomainAttributeKey
    { }

    /// <inheritdoc/>
    public class AttributeIndex : DomainAttributeKey, IAttributeIndex
    {
        /// <inheritdoc cref="DomainAttributeKey(IDomainAttributeKey)"/>
        public AttributeIndex(IAttributeIndex source) : base(source) { }

        /// <summary>
        /// Convert AttributeIndex to a DataLayerIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataLayerIndex(AttributeIndex source)
        { return new DataLayerIndex() { BusinessLayerId = source.AttributeId ?? Guid.Empty }; }
    }

    /// <inheritdoc/>
    public interface IAttributeIndexName : IDomainAttributeKeyName
    { }

    /// <inheritdoc/>
    public class AttributeIndexName : DomainAttributeKeyName
    {
        /// <inheritdoc cref="DomainAttributeKeyName(IDomainAttributeKeyName)"/>
        public AttributeIndexName(IAttributeIndexName source) : base(source) { }

        /// <inheritdoc cref="DomainAttributeKeyName(DataLayer.DatabaseData.Table.IDbTableColumnKeyName)"/>
        internal AttributeIndexName(ITableColumnIndexName source): base(source) { }

        /// <inheritdoc cref="DomainAttributeKeyName(DataLayer.DatabaseData.Routine.IDbRoutineParameterKeyName)"/>
        internal AttributeIndexName(IRoutineParameterIndexName source) : base(source) { }
    }
}
