using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{
    partial class AttributeDefinitionValue: IXElementFactory<IDefinitionIndex, IDefinitionValue>
    {
        /// <inheritdoc/>
        public static IEnumerable<XElementBuilder> CreateXElements(TryGetValue<IDefinitionIndex, IDefinitionValue> definitionGet)
        {
            List<XElementBuilder> result = new List<XElementBuilder>();
            result.AddRange(XElementBuilder.Create(typeof(AttributeDefinitionValue)));
            result.AddRange(AppModel.DefinitionValue.CreateXElements(definitionGet));

            result.GetValue(nameof(AttributeId)).NodeValueAs = TemplateNodeValueAsType.none;
            result.GetValue(nameof(Scope)).NodeValueAs = TemplateNodeValueAsType.none;
            result.GetValue(nameof(Temporal)).NodeValueAs = TemplateNodeValueAsType.none;
            result.GetValue(nameof(DefinitionId)).NodeValueAs = TemplateNodeValueAsType.none;
            result.GetValue(nameof(DefinitionText)).NodeValueAs = TemplateNodeValueAsType.ElementCData;

            return result;
        }
    }
}
