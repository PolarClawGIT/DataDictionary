using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.AppScripting
{
    public class XElementBuilder : XElementNode
    {
        public List<XElementBuilder> Children { get; } = new List<XElementBuilder>();

        public XElementBuilder(ScopeType scope) : base(scope) 
        { }

        public XElementBuilder(PropertyInfo property) : base(property)
        { }

        public static IEnumerable<XElementBuilder> Create(Type value)
        {
            List<XElementBuilder> result = new List<XElementBuilder>();

            foreach (PropertyInfo property in value.GetProperties().ToList())
            {
                result.Add(
                new XElementBuilder(property)
                { RenderAs = TemplateNodeValueAsType.ElementText }
                );
            }

            return result;
        }

        public XElement Build(Object value)
        {
            XObject? nodeObject = BuildBase(value);

            if (nodeObject is XElement nodeElement)
            { return nodeElement; }
            else
            {
                XElement result = new XElement(NodeName);
                result.Add(nodeObject);
                return result;
            }
        }

        protected override XObject? BuildBase(Object value)
        {
            XObject? result = base.BuildBase(value);

            if (result is XElement nodeElement)
            {
                foreach (var item in Children)
                { nodeElement.Add(item.BuildBase(value)); }
            }

            return result;
        }
    }
}
