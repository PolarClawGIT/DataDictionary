using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IPropertyIndex : IPropertyKey
    { }

    /// <inheritdoc/>
    public class PropertyIndex : PropertyKey, IPropertyIndex,
        IKeyEquality<IPropertyIndex>, IKeyEquality<PropertyIndex>
    {
        /// <inheritdoc cref="PropertyKey(IPropertyKey)"/>
        public PropertyIndex(IPropertyIndex source) : base(source) { }

        /// <inheritdoc cref="PropertyKey(IPropertyKey)"/>
        public PropertyIndex(IPropertySubType source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IPropertyIndex? other)
        { return other is IPropertyKey key && Equals(new PropertyKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(PropertyIndex? other)
        { return other is IPropertyKey key && Equals(new PropertyKey(key)); }

        /// <summary>
        /// Convert PropertyIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(PropertyIndex source)
        { return new DataIndex() { SystemId = source.PropertyId ?? Guid.Empty }; }
    }
}
