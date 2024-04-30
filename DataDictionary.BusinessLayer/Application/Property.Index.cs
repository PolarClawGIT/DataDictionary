using DataDictionary.DataLayer.ApplicationData.Property;

namespace DataDictionary.BusinessLayer.Application
{
    /// <inheritdoc/>
    public interface IPropertyIndex : IPropertyKey
    { }

    /// <inheritdoc/>
    public class PropertyIndex : PropertyKey, IPropertyIndex
    {
        /// <inheritdoc cref="PropertyKey.PropertyKey(IPropertyKey)"/>
        public PropertyIndex(IPropertyIndex source) : base(source) { }
    }

    /// <inheritdoc/>
    public interface IPropertyIndexName : IPropertyKeyName
    { }

    /// <inheritdoc/>
    public class PropertyIndexName : PropertyKeyName, IPropertyIndexName
    {
        /// <inheritdoc cref="PropertyKeyName(IPropertyKeyName)"/>
        public PropertyIndexName(IPropertyIndexName source) : base(source) { }
    }
}
