using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.AppModel
{
    partial class AttributeValue
    {
        public XElement? GetXElement(IEnumerable<TemplateNodeValue>? templateValues = null)
        {   //TODO Proof of concept, test if the properties can be read using reflection.
            //Maybe part of the Scripting namespace using a generic Object?
            XElement? result = null;

            var nodes = GetType().GetProperties().
                Join(
                templateValues ?? new List<TemplateNodeValue>(),
                pKey => pKey.Name,
                tKey => tKey.NodeName, (prop, template) => new
                {
                    NodeName = prop.Name,
                    NodeValueAs = template.NodeValueAs,
                    NodeValue = prop.GetValue(this)
                }).Union(
                    GetType().GetProperties().
                    Where(w => templateValues is null).
                    Select(s => new
                    {
                        NodeName = s.Name,
                        NodeValueAs = TemplateNodeValueAsType.ElementText,
                        NodeValue = s.GetValue(this)
                    })).ToList();

            foreach (var node in nodes)
            {
                XObject? value = BuildXObject(node.NodeName, node.NodeValue, node.NodeValueAs);

                if (value is XObject)
                {
                    if (result is null) { result = new XElement(ScopeEnumeration.Cast(Scope).Name); }
                    result.Add(value);
                }
            }

            return result;
        }

        XObject? BuildXObject(String nodeName, Object? nodeValue, TemplateNodeValueAsType nodeValueAs)
        { // TODO: Goes to Scripting namespace. Comon code.
            if (nodeName is null) { return null; }

            if (nodeValue is null) { return null; }
            String? value = nodeValue.ToString();
            if (String.IsNullOrWhiteSpace(value))
            { return null; }

            switch (nodeValueAs)
            {
                case TemplateNodeValueAsType.none:
                    return null;
                case TemplateNodeValueAsType.ElementText:
                    return new XElement(nodeName, value);
                case TemplateNodeValueAsType.ElementCData:
                    return new XElement(nodeName, new XCData(value));
                case TemplateNodeValueAsType.ElementXML:
                    try
                    {
                        if (String.IsNullOrWhiteSpace(value))
                        { return new XElement(nodeName, XElement.Parse(value)); }
                        else { return null; }
                    }
                    catch (Exception fragementEx)
                    {
                        fragementEx.Data.Add(nameof(nodeName), nodeName);
                        fragementEx.Data.Add(nameof(nodeValue), nodeValue.ToString());
                        fragementEx.Data.Add(nameof(nodeValueAs), nodeValueAs.ToString());
                        throw;
                    }
                case TemplateNodeValueAsType.Attribute:
                    return new XAttribute(nodeName, nodeValue);
                default:
                    Exception ex = new InvalidOperationException("Unknown NodeValueAsType");
                    ex.Data.Add(nameof(nodeValueAs), nodeValueAs.ToString());
                    throw ex;
            }
        }

        XObject? BuildXObject(PropertyInfo property, TemplateNodeValueAsType nodeValueAs = TemplateNodeValueAsType.none)
        {
            String? nodeName = property.Name;
            Object? value = property.GetValue(this);
            if (nodeName is null) { return null; }

            if (value is null) { return null; }
            String? nodeValue = value.ToString();
            if (String.IsNullOrWhiteSpace(nodeValue))
            { return null; }

            switch (nodeValueAs)
            {
                case TemplateNodeValueAsType.none:
                    return null;
                case TemplateNodeValueAsType.ElementText:
                    return new XElement(nodeName, value);
                case TemplateNodeValueAsType.ElementCData:
                    return new XElement(nodeName, new XCData(nodeValue));
                case TemplateNodeValueAsType.ElementXML:
                    try
                    {
                        if (String.IsNullOrWhiteSpace(nodeValue))
                        { return new XElement(nodeName, XElement.Parse(nodeValue)); }
                        else { return null; }
                    }
                    catch (Exception fragementEx)
                    {
                        fragementEx.Data.Add(nameof(property), nodeName);
                        fragementEx.Data.Add(nameof(nodeValueAs), nodeValueAs.ToString());
                        throw;
                    }
                case TemplateNodeValueAsType.Attribute:
                    return new XAttribute(nodeName, nodeValue);
                default:
                    Exception ex = new InvalidOperationException("Unknown NodeValueAsType");
                    ex.Data.Add(nameof(nodeValueAs), nodeValueAs.ToString());
                    ex.Data.Add(nameof(nodeValueAs), nodeValueAs.ToString());
                    throw ex;
            }
        }
    }
}
