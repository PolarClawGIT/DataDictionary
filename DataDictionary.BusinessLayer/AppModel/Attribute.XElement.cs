using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{
    partial class AttributeValue : IXElementFactory
    {
        /// <inheritdoc/>
        public static IEnumerable<XElementBuilder> CreateXElements()
        {
            List<XElementBuilder> result = new List<XElementBuilder>();
            result.AddRange(XElementBuilder.Create(typeof(AttributeValue)));

            result.GetValue(nameof(AttributeId)).NodeValueAs = TemplateNodeValueAsType.none;
            result.GetValue(nameof(Scope)).NodeValueAs = TemplateNodeValueAsType.none;
            result.GetValue(nameof(Temporal)).NodeValueAs = TemplateNodeValueAsType.none;

            return result;
        }
    }

    partial class Attribute
    {
        public static IXElementBuilderList CreateXElements(
            TryGetValue<IPropertyIndex, IPropertyValue> PropertyGet,
            TryGetValue<IDefinitionIndex, IDefinitionValue> DefinitionGet)
        {
            XElementBuilderList builders = new XElementBuilderList();
            builders.Add(ScopeType.ModelAttribute, AttributeValue.CreateXElements());
            builders.Add(ScopeType.ModelAttributeProperty, AttributePropertyValue.CreateXElements(PropertyGet));
            builders.Add(ScopeType.ModelAttributeDefinition, AttributeDefinitionValue.CreateXElements(DefinitionGet));

            return builders;
        }
    }
}
