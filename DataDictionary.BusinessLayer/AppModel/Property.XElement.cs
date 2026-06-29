using DataDictionary.BusinessLayer.AppScripting;

namespace DataDictionary.BusinessLayer.AppModel
{
    partial class PropertyValue 
    {
        [Obsolete("switch to XmlBuilder", true)]
        public static IEnumerable<XElementBuilder> CreateXElements(TryGetValue<IPropertyIndex, IPropertyValue> propertyGet)
        { return XElementBuilder.Create(typeof(PropertyValue), propertyGet, nameof(PropertyTitle)); }
    }
}
