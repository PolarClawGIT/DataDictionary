using DataDictionary.DataLayer.DomainData.Property;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IPropertyIndex : IDomainPropertyKey
    { }

    /// <inheritdoc/>
    public class PropertyIndex : DomainPropertyKey, IPropertyIndex,
        IKeyEquality<IPropertyIndex>, IKeyEquality<PropertyIndex>
    {
        /// <inheritdoc cref="DomainPropertyKey(IDomainPropertyKey)"/>
        public PropertyIndex(IPropertyIndex source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IPropertyIndex? other)
        { return other is IDomainPropertyKey key && Equals(new DomainPropertyKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(PropertyIndex? other)
        { return other is IDomainPropertyKey key && Equals(new DomainPropertyKey(key)); }

        /// <summary>
        /// Convert PropertyIndex to a DataLayerIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataLayerIndex(PropertyIndex source)
        { return new DataLayerIndex() { DataLayerId = source.PropertyId ?? Guid.Empty }; }
    }

    /// <inheritdoc/>
    public interface IPropertyIndexName : IDomainPropertyKeyName
    { }

    /// <inheritdoc/>
    public class PropertyIndexName : DomainPropertyKeyName, IPropertyIndexName,
        IKeyEquality<IPropertyIndexName>, IKeyEquality<PropertyIndexName>
    {
        /// <inheritdoc cref="DomainPropertyKeyName(IDomainPropertyKeyName)"/>
        public PropertyIndexName(IPropertyIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IPropertyIndexName? other)
        { return other is IDomainPropertyKeyName key && Equals(new DomainPropertyKeyName(key)); }

        /// <inheritdoc/>
        public Boolean Equals(PropertyIndexName? other)
        { return other is IDomainPropertyKeyName key && Equals(new DomainPropertyKeyName(key)); }
    }

}
