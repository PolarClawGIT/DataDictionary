using DataDictionary.BusinessLayer.ToolSet;
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
    public class XElementNode
    {
        public String NodeName { get; init; }

        public TemplateNodeValueAsType RenderAs { get; init; } = TemplateNodeValueAsType.none;

        public Func<Object, String?> GetValue { get; init; }

        public XElementNode(
            String name,
            TemplateNodeValueAsType renderAs = TemplateNodeValueAsType.Element)
            : base()
        {
            NodeName = name;

            if (String.IsNullOrWhiteSpace(name))
            { RenderAs = TemplateNodeValueAsType.none; }
            else { RenderAs = renderAs; }

            GetValue = (value) => GetValueDelegate((dynamic)value);
        }

        public XElementNode(ScopeType scope) : this(ScopeEnumeration.Cast(scope).Name)
        { }

        public XElementNode(PropertyInfo property) : this(property.Name)
        { GetValue = (value) => GetValueDelegate(property, value); }

        public virtual String? GetValueDelegate(Object value)
        {
            if (value is null) { return null; }
            else { return value.ToString(); }
        }

        public virtual String? GetValueDelegate(ScopeType value)
        { return ScopeEnumeration.Cast(value).Name; }

        public virtual String? GetValueDelegate(PropertyInfo property, Object value)
        {
            if (property.GetValue(value) is Object objectValue)
            { return GetValueDelegate((dynamic)objectValue); ; }
            else { return null; }
        }

        public virtual String? GetValueDelegate(PathIndex value)
        { return value.MemberFullPath; }

        protected virtual XObject? BuildBase(Object value)
        {
            String? nodeValue = GetValue(value);

            if (String.IsNullOrEmpty(NodeName) || RenderAs is TemplateNodeValueAsType.none)
            { return null; }

            switch (RenderAs)
            {
                case TemplateNodeValueAsType.none:
                    return null;
                case TemplateNodeValueAsType.Element:
                    return new XElement(NodeName);
                case TemplateNodeValueAsType.ElementText:
                    if (String.IsNullOrWhiteSpace(nodeValue)) { return null; }
                    return new XElement(NodeName, nodeValue);
                case TemplateNodeValueAsType.ElementCData:
                    if (String.IsNullOrWhiteSpace(nodeValue)) { return null; }
                    return new XElement(NodeName, new XCData(nodeValue));
                case TemplateNodeValueAsType.ElementXML:
                    try
                    {
                        if (String.IsNullOrWhiteSpace(nodeValue)) { return null; }
                        return new XElement(NodeName, XElement.Parse(nodeValue));
                    }
                    catch (Exception fragementEx)
                    {
                        fragementEx.Data.Add(nameof(NodeName), NodeName);
                        fragementEx.Data.Add(nameof(nodeValue), nodeValue);
                        fragementEx.Data.Add(nameof(RenderAs), RenderAs.ToString());
                        throw;
                    }
                case TemplateNodeValueAsType.Attribute:
                    if (String.IsNullOrWhiteSpace(nodeValue)) { return null; }
                    return new XAttribute(NodeName, value);
                default:
                    Exception ex = new InvalidOperationException(String.Format("Unknown {0}", nameof(TemplateNodeValueAsType)));
                    ex.Data.Add(nameof(NodeName), NodeName);
                    ex.Data.Add(nameof(RenderAs), RenderAs.ToString());
                    throw ex;
            }
        }

        public override String ToString()
        { return NodeName; }
    }
}
