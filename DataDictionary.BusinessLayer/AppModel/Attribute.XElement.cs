using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{
    partial class AttributeValue: IXElementFactory
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
}
