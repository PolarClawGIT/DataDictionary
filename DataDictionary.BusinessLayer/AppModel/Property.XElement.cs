using DataDictionary.BusinessLayer.AppScripting;

namespace DataDictionary.BusinessLayer.AppModel
{
    partial class PropertyValue : IXElementFactory<IPropertyIndex, IPropertyValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XElementBuilder"/> class,
        /// configuring how the node retrieves and renders <see cref="PropertyValue"/>.
        /// </summary>
        /// <param name="propertyGet"></param>
        /// <returns></returns>
        public static IEnumerable<XElementBuilder> CreateXElements(TryGetValue<IPropertyIndex, IPropertyValue> propertyGet)
        { return XElementBuilder.Create(typeof(PropertyValue), propertyGet, nameof(PropertyTitle)); }
    }
}
