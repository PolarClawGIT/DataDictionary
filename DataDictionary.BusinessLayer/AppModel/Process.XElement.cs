using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{
    partial class ProcessValue
    {
        [Obsolete("switch to XmlBuilder", true)]
        public static IEnumerable<XElementBuilder> CreateXElements()
        {
            List<XElementBuilder> result = new List<XElementBuilder>();
            result.AddRange(XElementBuilder.Create(typeof(ProcessValue)));

            result.GetValue(nameof(ProcessId)).NodeValueAs = NodeRenderAsType.none;
            result.GetValue(nameof(Scope)).NodeValueAs = NodeRenderAsType.none;
            result.GetValue(nameof(Temporal)).NodeValueAs = NodeRenderAsType.none;

            return result;
        }
    }

    partial class Process
    {
        [Obsolete("switch to XmlBuilder", true)]
        public static IXElementBuilderList CreateXElements(
            TryGetValue<IPropertyIndex, IPropertyValue> propertyGet,
            TryGetValue<IDefinitionIndex, IDefinitionValue> definitionGet)
        {
            XElementBuilderList builders = new XElementBuilderList();
            builders.Add(ScopeType.ModelProcess, ProcessValue.CreateXElements());
            builders.Add(ScopeType.ModelProcessProperty, ProcessPropertyValue.CreateXElements(propertyGet));
            builders.Add(ScopeType.ModelProcessDefinition, ProcessDefinitionValue.CreateXElements(definitionGet));

            return builders;
        }
    }
}
