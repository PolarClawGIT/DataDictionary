using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppModel
{
    partial class AttributeAliasValue : IXElementFactory
    {
        /// <inheritdoc/>
        public static IEnumerable<XElementBuilder> CreateXElementBuilders()
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
