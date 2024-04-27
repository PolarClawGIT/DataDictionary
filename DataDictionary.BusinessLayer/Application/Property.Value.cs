using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.DataLayer.ApplicationData.Property;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.Application
{
    /// <inheritdoc/>
    public interface IPropertyValue : IPropertyItem, IPropertyIndex
    { }

    /// <inheritdoc/>
    public class PropertyValue : PropertyItem
    { }
}
