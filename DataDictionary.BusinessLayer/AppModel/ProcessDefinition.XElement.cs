using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{
    partial class ProcessDefinitionValue : IXElementFactory<IDefinitionIndex, IDefinitionValue>
    {
        /// <inheritdoc/>
        public static IEnumerable<XElementBuilder> CreateXElements(TryGetValue<IDefinitionIndex, IDefinitionValue> definitionGet)
        {
            List<XElementBuilder> result = new List<XElementBuilder>();
            result.AddRange(XElementBuilder.Create(typeof(AttributeDefinitionValue)));
            result.AddRange(AppModel.DefinitionValue.CreateXElements(definitionGet));

            result.GetValue(nameof(ProcessId)).NodeValueAs = NodeRenderAsType.none;
            result.GetValue(nameof(Scope)).NodeValueAs = NodeRenderAsType.none;
            result.GetValue(nameof(Temporal)).NodeValueAs = NodeRenderAsType.none;
            result.GetValue(nameof(DefinitionId)).NodeValueAs = NodeRenderAsType.none;
            result.GetValue(nameof(DefinitionText)).NodeValueAs = NodeRenderAsType.ElementCData;

            return result;
        }
    }
}
