using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{
    partial class AttributeAliasValue : IXElementFactory
    {
        /// <inheritdoc/>
        public static IEnumerable<XElementBuilder> CreateXElements()
        {
            IEnumerable<XElementBuilder> result = XElementBuilder.Create(typeof(AttributeValue));

            result.GetValue(nameof(AttributeId)).NodeValueAs = TemplateNodeValueAsType.none;
            result.GetValue(nameof(Scope)).NodeValueAs = TemplateNodeValueAsType.none;
            result.GetValue(nameof(Temporal)).NodeValueAs = TemplateNodeValueAsType.none;
            result.GetValue(nameof(AliasPath)).NodeValueAs = TemplateNodeValueAsType.none;

            return result;
        }
    }
}
