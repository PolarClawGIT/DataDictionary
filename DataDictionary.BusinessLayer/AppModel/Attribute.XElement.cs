using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppModel
{
    partial class AttributeValue
    {
        public static IEnumerable<XElementNode> GetXElement()
        {
            List<XElementNode> result = XElementNode.Create(typeof(AttributeValue)).ToList();

            result.Set(TemplateNodeValueAsType.none,
                nameof(AttributeId),
                nameof(Scope),
                nameof(Temporal));

            return result;
        }
    }
}
