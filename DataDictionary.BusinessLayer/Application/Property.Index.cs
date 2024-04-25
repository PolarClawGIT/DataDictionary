using DataDictionary.DataLayer.ApplicationData.Property;

namespace DataDictionary.BusinessLayer.Application
{
    /// <inheritdoc/>
    public interface IPropertyIndex: IPropertyKey
    { }

    /// <inheritdoc/>
    public class PropertyIndex : PropertyKey, IPropertyIndex
    {
        /// <inheritdoc cref="PropertyKey(IPropertyKey)"/>
        public PropertyIndex(IPropertyIndex source) : base(source) { }
    }
}
