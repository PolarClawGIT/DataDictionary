using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{
    partial class ProcessValue : IXElementFactory
    {
        /// <inheritdoc/>
        public static IEnumerable<XElementBuilder> CreateXElements()
        {
            List<XElementBuilder> result = new List<XElementBuilder>();
            result.AddRange(XElementBuilder.Create(typeof(ProcessValue)));

            result.GetValue(nameof(ProcessId)).NodeValueAs = TemplateNodeValueAsType.none;
            result.GetValue(nameof(Scope)).NodeValueAs = TemplateNodeValueAsType.none;
            result.GetValue(nameof(Temporal)).NodeValueAs = TemplateNodeValueAsType.none;

            return result;
        }
    }

    partial class Process
    {
        public static IXElementBuilderList CreateXElements(
            TryGetValue<IPropertyIndex, IPropertyValue> PropertyGet,
            TryGetValue<IDefinitionIndex, IDefinitionValue> DefinitionGet)
        {
            XElementBuilderList builders = new XElementBuilderList();
            builders.Add(ScopeType.ModelProcess, ProcessValue.CreateXElements());
            builders.Add(ScopeType.ModelProcessProperty, ProcessPropertyValue.CreateXElements(PropertyGet));
            builders.Add(ScopeType.ModelProcessDefinition, ProcessDefinitionValue.CreateXElements(DefinitionGet));

            return builders;
        }
    }
}
