using DataDictionary.DataLayer.ApplicationData.Property;

namespace DataDictionary.BusinessLayer.Application
{
    /// <inheritdoc/>
    public interface IPropertyValue : IPropertyItem, IPropertyIndex
    { }

    /// <inheritdoc/>
    public class PropertyValue : PropertyItem, IPropertyValue
    {
        /// <inheritdoc/>
        public PropertyValue() : base() { }
    }
}
