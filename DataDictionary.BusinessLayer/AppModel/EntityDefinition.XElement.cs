using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{
    public partial class EntityDefinitionValue : IXElementFactory<IDefinitionIndex, IDefinitionValue>
    {
        /// <inheritdoc/>
        public static IEnumerable<XElementBuilder> CreateXElements(TryGetValue<IDefinitionIndex, IDefinitionValue> definitionGet)
        {
            List<XElementBuilder> result = new List<XElementBuilder>();
            result.AddRange(XElementBuilder.Create(typeof(EntityDefinitionValue)));
            result.AddRange(AppModel.DefinitionValue.CreateXElements(definitionGet));

            result.GetValue(nameof(EntityId)).NodeValueAs = TemplateNodeValueAsType.none;
            result.GetValue(nameof(Scope)).NodeValueAs = TemplateNodeValueAsType.none;
            result.GetValue(nameof(Temporal)).NodeValueAs = TemplateNodeValueAsType.none;
            result.GetValue(nameof(DefinitionId)).NodeValueAs = TemplateNodeValueAsType.none;
            result.GetValue(nameof(DefinitionText)).NodeValueAs = TemplateNodeValueAsType.ElementCData;

            return result;
        }
    }
}
