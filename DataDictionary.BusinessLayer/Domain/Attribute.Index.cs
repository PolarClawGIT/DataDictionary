using DataDictionary.BusinessLayer.Database;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IAttributeIndex : IDomainAttributeKey
    { }

    /// <inheritdoc/>
    public class AttributeIndex : DomainAttributeKey, IAttributeIndex,
        IKeyEquality<IAttributeIndex>, IKeyEquality<AttributeIndex>
    {
        /// <inheritdoc cref="DomainAttributeKey(IDomainAttributeKey)"/>
        public AttributeIndex(IAttributeIndex source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(AttributeIndex? other)
        { return other is IDomainAttributeKey key && Equals(new DomainAttributeKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(IAttributeIndex? other)
        { return other is IDomainAttributeKey key && Equals(new DomainAttributeKey(key)); }

        /// <summary>
        /// Convert AttributeIndex to a DataLayerIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataLayerIndex(AttributeIndex source)
        { return new DataLayerIndex() { DataLayerId = source.AttributeId ?? Guid.Empty }; }
    }

    /// <inheritdoc/>
    public interface IAttributeIndexName : IDomainAttributeKeyName
    { }

    /// <inheritdoc/>
    public class AttributeIndexName : DomainAttributeKeyName, IAttributeIndexName,
        IKeyEquality<IAttributeIndexName>, IKeyEquality<AttributeIndexName>
    {
        /// <inheritdoc cref="DomainAttributeKeyName(IDomainAttributeKeyName)"/>
        public AttributeIndexName(IAttributeIndexName source) : base(source) { }

        /// <inheritdoc cref="DomainAttributeKeyName(DataLayer.DatabaseData.Table.IDbTableColumnKeyName)"/>
        internal AttributeIndexName(ITableColumnIndexName source): base(source) { }

        /// <inheritdoc cref="DomainAttributeKeyName(DataLayer.DatabaseData.Routine.IDbRoutineParameterKeyName)"/>
        internal AttributeIndexName(IRoutineParameterIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IAttributeIndexName? other)
        { return other is IDomainAttributeKeyName value && Equals(new DomainAttributeKeyName(value)); }

        /// <inheritdoc/>
        public Boolean Equals(AttributeIndexName? other)
        { return other is IDomainAttributeKeyName value && Equals(new DomainAttributeKeyName(value)); }
    }
}
