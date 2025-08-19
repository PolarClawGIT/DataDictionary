using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{
    partial class AttributePropertyValue : IXElementFactory<IPropertyIndex, IPropertyValue>
    {
        /// <inheritdoc/>
        public static IEnumerable<XElementBuilder> CreateXElements(TryGetValue<IPropertyIndex, IPropertyValue> propertyGet)
        {
            List<XElementBuilder> result = new List<XElementBuilder>();
            result.AddRange(XElementBuilder.Create(typeof(AttributePropertyValue)));
            result.AddRange(AppModel.PropertyValue.CreateXElements(propertyGet));

            result.GetValue(nameof(IAttributePropertyValue.AttributeId)).NodeValueAs = TemplateNodeValueAsType.none;
            result.GetValue(nameof(IAttributePropertyValue.Scope)).NodeValueAs = TemplateNodeValueAsType.none;
            result.GetValue(nameof(IAttributePropertyValue.Temporal)).NodeValueAs = TemplateNodeValueAsType.none;
            result.GetValue(nameof(IAttributePropertyValue.PropertyId)).NodeValueAs = TemplateNodeValueAsType.none;

            return result;
        }
    }
}
