using DataDictionary.BusinessLayer.Scripting;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.Application
{
    /// <inheritdoc/>
    public interface IPropertyItem : DataLayer.ApplicationData.Property.IPropertyItem 
    { }

    /// <inheritdoc/>
    public class PropertyItem : DataLayer.ApplicationData.Property.PropertyItem
    {
        /// <inheritdoc/>
        public PropertyItem() : base() { }
    }
}
