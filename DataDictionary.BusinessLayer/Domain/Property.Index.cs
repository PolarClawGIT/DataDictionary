using DataDictionary.BusinessLayer.ToolSet;
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
        /// Convert PropertyIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(PropertyIndex source)
        { return new DataIndex() { SystemId = source.PropertyId ?? Guid.Empty }; }
    }



}
