using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppModel
{
    partial class EntityPropertyValue
    {
        [Obsolete("switch to XmlBuilder", true)]
        public static IEnumerable<XElementBuilder> CreateXElements(TryGetValue<IPropertyIndex, IPropertyValue> propertyGet)
        {
            List<XElementBuilder> result = new List<XElementBuilder>();
            result.AddRange(XElementBuilder.Create(typeof(EntityPropertyValue)));
            result.AddRange(AppModel.PropertyValue.CreateXElements(propertyGet));

            result.GetValue(nameof(EntityId)).NodeValueAs = NodeRenderAsType.none;
            result.GetValue(nameof(Scope)).NodeValueAs = NodeRenderAsType.none;
            result.GetValue(nameof(Temporal)).NodeValueAs = NodeRenderAsType.none;
            result.GetValue(nameof(PropertyId)).NodeValueAs = NodeRenderAsType.none;

            return result;
        }
    }
}
