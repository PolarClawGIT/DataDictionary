using DataDictionary.DataLayer.ApplicationData.Property;

namespace DataDictionary.BusinessLayer.Application
{
    /// <inheritdoc/>
    [Obsolete("Replace Domain Property", true)]
    public interface IPropertyIndex : IPropertyKey
    { }

    /// <inheritdoc/>
    [Obsolete("Replace Domain Property", true)]
    public class PropertyIndex : PropertyKey, IPropertyIndex
    {
        /// <inheritdoc cref="PropertyKey.PropertyKey(IPropertyKey)"/>
        public PropertyIndex(IPropertyIndex source) : base(source) { }
    }

    /// <inheritdoc/>
    [Obsolete("Replace Domain Property", true)]
    public interface IPropertyIndexName : IPropertyKeyName
    { }

    /// <inheritdoc/>
    [Obsolete("Replace Domain Property", true)]
    public class PropertyIndexName : PropertyKeyName, IPropertyIndexName
    {
        /// <inheritdoc cref="PropertyKeyName(IPropertyKeyName)"/>
        public PropertyIndexName(IPropertyIndexName source) : base(source) { }
    }
}
