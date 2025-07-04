using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.AppScripting
{
    public class XmlBuilderBase
    {
        /// <summary>
        /// Gets or sets a delegate that returns the text used as the XName for the XAttribute/XElement.
        /// </summary>
        public Func<String> GetNodeName { get; set; } = () => String.Empty;

        /// <summary>
        /// Gets or sets a delegate that retrieves the value of a XAttribute/XElement node as a string.
        /// </summary>
        /// <remarks>The delegate can be used to dynamically fetch the value of a node.  Ensure
        /// the function handles cases where the node value might be null.</remarks>
        public Func<String?> GetNodeValue { get; set; } = () => null;

        /// <summary>
        /// Gets or sets the rendering behavior for the node.
        /// </summary>
        public TemplateNodeValueAsType RenderAs { get; set; } = TemplateNodeValueAsType.none;

        public XmlBuilderBase(ScopeType nodeName) : base()
        {
            GetNodeName = () => ScopeEnumeration.Cast(nodeName).Name;
            RenderAs = TemplateNodeValueAsType.Element;
        }

        public XmlBuilderBase(String nodeName, String? nodeValue, TemplateNodeValueAsType renderAs = TemplateNodeValueAsType.ElementText) : base()
        {
            GetNodeName = () => nodeName;
            GetNodeValue = () => nodeValue;
            RenderAs = renderAs;
        }

        public XmlBuilderBase(String nodeName, Object? nodeValue, TemplateNodeValueAsType renderAs = TemplateNodeValueAsType.ElementText) : base()
        {
            GetNodeName = () => nodeName;
            GetNodeValue = GetValue((dynamic?)nodeValue);

            RenderAs = renderAs;
        }

        Func<String?> GetValue(Object value)
        {
            if (value is null) { return () => null; }
            return value.ToString;
        }

        Func<String?> GetValue(ScopeType value)
        { return () => ScopeEnumeration.Cast(value).Name; }

        public override String ToString()
        { return GetNodeName(); }
    }

    /// <summary>
    /// Builder class (see Builder pattern) that creates an XElement out of a object data source.
    /// </summary>
    public class XElementBuilder : XmlBuilderBase
    {
        public List<XElementBuilder> Children { get; } = new List<XElementBuilder>();

        public XElementBuilder(ScopeType nodeName) : base(nodeName)
        { }

        public XElementBuilder(String nodeName, String? nodeValue, TemplateNodeValueAsType renderAs = TemplateNodeValueAsType.ElementText) :
            base(nodeName, nodeValue, renderAs)
        { }

        public XElementBuilder(String nodeName, Object? nodeValue, TemplateNodeValueAsType renderAs = TemplateNodeValueAsType.ElementText) :
            base(nodeName, nodeValue, renderAs)
        { }

        public static IEnumerable<XElementBuilder> Create(Object value)
        {
            List<XElementBuilder> result = new List<XElementBuilder>();

            foreach (PropertyInfo property in value.GetType().GetProperties().ToList())
            {
                if (property.CanRead && property.GetIndexParameters().Length == 0)
                { result.Add(new XElementBuilder(property.Name, property.GetValue(value))); }
            }

            return result;
        }

        public XElement Build()
        {
            XElement result;
            XObject? value = BuildXObject(this);

            if (value is XElement elementValue)
            { result = elementValue; }
            else if (value is XObject objectValue)
            {
                result = new XElement(GetNodeName());
                result.Add(objectValue);
            }
            else { result = new XElement(GetNodeName()); }

            return result;
        }

        XObject? BuildXObject(XElementBuilder setting)
        {
            String name = setting.GetNodeName();
            String? value = setting.GetNodeValue();
            XElement element;

            if (String.IsNullOrWhiteSpace(name))
            { setting.RenderAs = TemplateNodeValueAsType.none; }

            switch (setting.RenderAs)
            {
                case TemplateNodeValueAsType.none:
                    element = new XElement("not.rendered"); break;
                case TemplateNodeValueAsType.Element:
                    element = new XElement(name); break;
                case TemplateNodeValueAsType.ElementText:
                    if (String.IsNullOrWhiteSpace(value)) { return null; }
                    element = new XElement(name, value); break;
                case TemplateNodeValueAsType.ElementCData:
                    if (String.IsNullOrWhiteSpace(value)) { return null; }
                    element = new XElement(name, new XCData(value)); break;
                case TemplateNodeValueAsType.ElementXML:
                    try
                    {
                        if (String.IsNullOrWhiteSpace(value)) { return null; }
                        element = new XElement(setting.GetNodeName(), XElement.Parse(value)); break;
                    }
                    catch (Exception fragementEx)
                    {
                        fragementEx.Data.Add(nameof(setting.GetNodeName), setting.GetNodeName());
                        fragementEx.Data.Add(nameof(setting.GetNodeValue), value);
                        fragementEx.Data.Add(nameof(setting.RenderAs), setting.RenderAs.ToString());
                        throw;
                    }
                case TemplateNodeValueAsType.Attribute:
                    if (String.IsNullOrWhiteSpace(value)) { return null; }
                    return new XAttribute(name, value);
                default:
                    Exception ex = new InvalidOperationException(String.Format("Unknown {0}", nameof(TemplateNodeValueAsType)));
                    ex.Data.Add(nameof(setting.GetNodeName), setting.GetNodeName());
                    ex.Data.Add(nameof(setting.RenderAs), setting.RenderAs.ToString());
                    throw ex;
            }

            // Add the Children, if any
            foreach (XElementBuilder item in setting.Children)
            {
                var values = BuildXObject(item);
                if (values is XElement elementValue && item.RenderAs is TemplateNodeValueAsType.none)
                { element.Add(elementValue.Nodes()); }
                else { element.Add(values); }
            }

            return element;
        }
    }

    public static class XElementExtension
    {
        public static void Add(this IList<XElementBuilder> values, String nodeName, Object? nodeValue = null, TemplateNodeValueAsType renderAs = TemplateNodeValueAsType.ElementText)
        { values.Add(new XElementBuilder(nodeName, nodeValue, renderAs)); }

        public static void Remove(this IList<XElementBuilder> values, String nodeName)
        {
            while (values.FirstOrDefault(w => nodeName.Equals(w.GetNodeName())) is XElementBuilder child)
            { values.Remove(child); }
        }

        public static Boolean TryGet(this IList<XElementBuilder> values, String nodeName, [NotNullWhen(true)] out XmlBuilderBase? result)
        {
            if (values.FirstOrDefault(w => nodeName.Equals(w.GetNodeName)) is XmlBuilderBase value)
            { result = value; return true; }
            else { result = null; return false; }
        }

        public static XmlBuilderBase Get(this IList<XElementBuilder> values, String nodeName)
        {
            if (values.FirstOrDefault(w => nodeName.Equals(w.GetNodeName())) is XmlBuilderBase value)
            { return value; }
            else
            {
                Exception ex = new IndexOutOfRangeException();
                ex.Data.Add(nameof(nodeName), nodeName);
                throw ex;
            }
        }
    }
}
