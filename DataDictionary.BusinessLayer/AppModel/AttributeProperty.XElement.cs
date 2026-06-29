using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{
    partial class AttributePropertyValue 
    {
        [Obsolete("switch to XmlBuilder", true)]
        public static IEnumerable<XElementBuilder> CreateXElements(TryGetValue<IPropertyIndex, IPropertyValue> propertyGet)
        {
            List<XElementBuilder> result = new List<XElementBuilder>();
            result.AddRange(XElementBuilder.Create(typeof(AttributePropertyValue)));
            result.AddRange(AppModel.PropertyValue.CreateXElements(propertyGet));

            result.GetValue(nameof(AttributeId)).NodeValueAs = NodeRenderAsType.none;
            result.GetValue(nameof(Scope)).NodeValueAs = NodeRenderAsType.none;
            result.GetValue(nameof(Temporal)).NodeValueAs = NodeRenderAsType.none;
            result.GetValue(nameof(PropertyId)).NodeValueAs = NodeRenderAsType.none;

            return result;
        }
    }
}
