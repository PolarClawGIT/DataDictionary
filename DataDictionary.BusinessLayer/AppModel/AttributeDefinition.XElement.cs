using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppModel
{
    partial class AttributeDefinitionValue
    {
        public static IEnumerable<XElementBuilder> GetXElementNodes(IDefinitionGetValue definitionGet)
        {
            List<XElementBuilder> result = new List<XElementBuilder>();
            result.Add(new XElementBuilder(definitionGet));
            result.AddRange(XElementBuilder.Create(typeof(AttributeDefinitionValue)));

            result.GetValue(nameof(AttributeId)).NodeValueAs = TemplateNodeValueAsType.none;
            result.GetValue(nameof(Scope)).NodeValueAs = TemplateNodeValueAsType.none;
            result.GetValue(nameof(Temporal)).NodeValueAs = TemplateNodeValueAsType.none;
            result.GetValue(nameof(DefinitionId)).NodeValueAs = TemplateNodeValueAsType.none;
            result.GetValue(nameof(DefinitionText)).NodeValueAs = TemplateNodeValueAsType.ElementCData;

            return result;
        }
    }
}
