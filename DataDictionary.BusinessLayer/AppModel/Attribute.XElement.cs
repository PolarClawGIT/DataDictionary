using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{

    partial class AttributeValue 
    {
        [Obsolete("switch to XmlBuilder", true)]
        public static IEnumerable<XElementBuilder> CreateXElements()
        {
            List<XElementBuilder> result = new List<XElementBuilder>();
            result.AddRange(XElementBuilder.Create(typeof(AttributeValue)));

            result.GetValue(nameof(AttributeId)).NodeValueAs = NodeRenderAsType.none;
            result.GetValue(nameof(Scope)).NodeValueAs = NodeRenderAsType.none;
            result.GetValue(nameof(Temporal)).NodeValueAs = NodeRenderAsType.none;

            return result;
        }
    }

    partial class Attribute
    {
        [Obsolete("switch to XmlBuilder", true)]
        public static IXElementBuilderList CreateXElements(
            TryGetValue<IPropertyIndex, IPropertyValue> propertyGet,
            TryGetValue<IDefinitionIndex, IDefinitionValue> definitionGet)
        {
            XElementBuilderList builders = new XElementBuilderList();
            builders.Add(ScopeType.ModelAttribute, AttributeValue.CreateXElements());
            builders.Add(ScopeType.ModelAttributeProperty, AttributePropertyValue.CreateXElements(propertyGet));
            builders.Add(ScopeType.ModelAttributeDefinition, AttributeDefinitionValue.CreateXElements(definitionGet));

            return builders;
        }
    }
}
