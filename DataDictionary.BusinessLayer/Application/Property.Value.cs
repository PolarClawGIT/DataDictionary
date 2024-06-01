using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.DataLayer.ApplicationData.Property;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.Application
{
    /// <inheritdoc/>
    [Obsolete("Replace Domain Property", true)]
    public interface IPropertyValue : IPropertyItem, IPropertyIndex
    { }

    /// <inheritdoc/>
    [Obsolete("Replace Domain Property", true)]
    public class PropertyValue : PropertyItem
    { }
}
