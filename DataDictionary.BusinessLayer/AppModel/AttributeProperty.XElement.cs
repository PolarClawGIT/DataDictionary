using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppModel
{
    partial class AttributePropertyValue
    {
        public static IEnumerable<XElementNode> GetXElement(IPropertyGetValue propertyGet)
        {
            List<XElementNode> result = new List<XElementNode>();
            result.Add(new XElementNode(propertyGet));
            result.AddRange(XElementNode.Create(typeof(AttributeValue)));

            result.Set(TemplateNodeValueAsType.none,
                nameof(AttributeId),
                nameof(Scope),
                nameof(Temporal),
                nameof(PropertyId));

            return result;
        }
    }
}
