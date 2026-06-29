using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{
    public partial class EntityDefinitionValue
    {
        [Obsolete("switch to XmlBuilder", true)]
        public static IEnumerable<XElementBuilder> CreateXElements(TryGetValue<IDefinitionIndex, IDefinitionValue> definitionGet)
        {
            List<XElementBuilder> result = new List<XElementBuilder>();
            result.AddRange(XElementBuilder.Create(typeof(EntityDefinitionValue)));
            result.AddRange(AppModel.DefinitionValue.CreateXElements(definitionGet));

            result.GetValue(nameof(EntityId)).NodeValueAs = NodeRenderAsType.none;
            result.GetValue(nameof(Scope)).NodeValueAs = NodeRenderAsType.none;
            result.GetValue(nameof(Temporal)).NodeValueAs = NodeRenderAsType.none;
            result.GetValue(nameof(DefinitionId)).NodeValueAs = NodeRenderAsType.none;
            result.GetValue(nameof(DefinitionText)).NodeValueAs = NodeRenderAsType.ElementCData;

            return result;
        }
    }
}
