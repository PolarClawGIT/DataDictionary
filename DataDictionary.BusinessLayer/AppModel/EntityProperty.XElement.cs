using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppModel
{
    partial class EntityPropertyValue : IXElementFactory<IPropertyIndex, IPropertyValue>
    {
        /// <inheritdoc/>
        public static IEnumerable<XElementBuilder> CreateXElements(TryGetValue<IPropertyIndex, IPropertyValue> propertyGet)
        {
            List<XElementBuilder> result = new List<XElementBuilder>();
            result.AddRange(XElementBuilder.Create(typeof(EntityPropertyValue)));
            result.AddRange(AppModel.PropertyValue.CreateXElements(propertyGet));

            result.GetValue(nameof(EntityId)).NodeValueAs = TemplateNodeValueAsType.none;
            result.GetValue(nameof(Scope)).NodeValueAs = TemplateNodeValueAsType.none;
            result.GetValue(nameof(Temporal)).NodeValueAs = TemplateNodeValueAsType.none;
            result.GetValue(nameof(PropertyId)).NodeValueAs = TemplateNodeValueAsType.none;

            return result;
        }
    }
}
