using DataDictionary.BusinessLayer.Database;
using DataDictionary.DataLayer;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DomainData.Property;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IPropertyIndex : IDomainPropertyKey
    { }

    /// <inheritdoc/>
    public class PropertyIndex : DomainPropertyKey, IPropertyIndex
    {
        /// <inheritdoc cref="DomainPropertyKey(IDomainPropertyKey)"/>
        public PropertyIndex(IPropertyIndex source) : base(source) { }

        /// <summary>
        /// Convert PropertyIndex to a DataLayerIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataLayerIndex(PropertyIndex source)
        { return new DataLayerIndex() { BusinessLayerId = source.PropertyId ?? Guid.Empty }; }
    }

    /// <inheritdoc/>
    public interface IPropertyIndexName : IDomainPropertyKeyName
    { }

    /// <inheritdoc/>
    public class PropertyIndexName : DomainPropertyKeyName
    {
        /// <inheritdoc cref="DomainPropertyKeyName(IDomainPropertyKeyName)"/>
        public PropertyIndexName(IPropertyIndexName source) : base(source) { }
    }

}
