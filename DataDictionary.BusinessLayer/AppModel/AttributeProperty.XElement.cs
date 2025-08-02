using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppModel
{
    partial class AttributePropertyValue : IXElementFactory<IPropertyGetValue>
    {
        /// <inheritdoc/>
        public static IEnumerable<XElementBuilder> CreateXElementBuilders(IPropertyGetValue propertyGet)
        {
            List<XElementBuilder> result = new List<XElementBuilder>();
            result.Add(new XElementBuilder(propertyGet));
            result.AddRange(XElementBuilder.Create(typeof(AttributePropertyValue)));

            result.GetValue(nameof(AttributeId)).NodeValueAs = TemplateNodeValueAsType.none;
            result.GetValue(nameof(Scope)).NodeValueAs = TemplateNodeValueAsType.none;
            result.GetValue(nameof(Temporal)).NodeValueAs = TemplateNodeValueAsType.none;
            result.GetValue(nameof(PropertyId)).NodeValueAs = TemplateNodeValueAsType.none;
            
            return result;
        }
    }
}
