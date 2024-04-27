using DataDictionary.DataLayer.ApplicationData.Property;

namespace DataDictionary.BusinessLayer.Application
{
    /// <inheritdoc/>
    public interface IPropertyIndex : IPropertyKey
    { }

    /// <inheritdoc/>
    public class PropertyIndex : PropertyKey
    {
        /// <inheritdoc cref="PropertyKey.PropertyKey(IPropertyKey)"/>
        public PropertyIndex(IPropertyIndex source) : base(source) { }
    }
}
