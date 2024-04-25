using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.DataLayer.ApplicationData.Property;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.Application
{
    /// <inheritdoc/>
    public interface IPropertyValue : IPropertyItem 
    { }

    /// <inheritdoc/>
    public class PropertyValue : PropertyItem
    {
        /// <inheritdoc/>
        public PropertyValue() : base() { }
    }
}
