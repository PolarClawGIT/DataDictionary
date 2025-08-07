using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.AppScripting
{
    [Obsolete("replace", true)]
    static class ScriptingHelper
    {
        public static XElement? GetXElement(
            this AppModel.IAttribute data,
            IEnumerable<PropertyValue> properties,
            ScriptingWork scripting, IAttributeIndex index)
        {
            XElement? result = null;
            AttributeIndex key = new AttributeIndex(index);
            if (data.Values.FirstOrDefault(w => key.Equals(w)) is AttributeValue attribute)
            {
                foreach (ScriptingNodeValue node in scripting.Nodes.Where(w => w.PropertyScope == attribute.Scope))
                {
                    XObject? value = null;

                    switch (node.PropertyName)
                    {
                        case nameof(attribute.AttributeTitle): value = node.BuildXObject(attribute.AttributeTitle); break;
                        case nameof(attribute.AttributeDescription): value = node.BuildXObject(attribute.AttributeDescription); break;
                        case nameof(attribute.IsCompositeType): value = node.BuildXObject(attribute.IsCompositeType); break;
                        case nameof(attribute.IsDerived): value = node.BuildXObject(attribute.IsDerived); break; ;
                        case nameof(attribute.IsIntegral): value = node.BuildXObject(attribute.IsIntegral); break; ;
                        case nameof(attribute.IsKey): value = node.BuildXObject(attribute.IsKey); break; ;
                        case nameof(attribute.IsMultiValue): value = node.BuildXObject(attribute.IsMultiValue); break; ;
                        case nameof(attribute.IsNonKey): value = node.BuildXObject(attribute.IsNonKey); break; ;
                        case nameof(attribute.IsNullable): value = node.BuildXObject(attribute.IsNullable); break; ;
                        case nameof(attribute.IsSimpleType): value = node.BuildXObject(attribute.IsSimpleType); break; ;
                        case nameof(attribute.IsSingleValue): value = node.BuildXObject(attribute.IsSingleValue); break; ;
                        case nameof(attribute.IsValued): value = node.BuildXObject(attribute.IsValued); break; ;
                        default:
                            break;
                    }

                    if (value is XObject)
                    {
                        if (result is null) { result = new XElement(ScopeEnumeration.Cast(attribute.Scope).Name); }
                        result.Add(value);

                        IReadOnlyList<XAttribute> attributes = properties.GetXAttributes(scripting, node, data.Properties);

                        if (value is XElement element) { element.Add(attributes.ToArray()); }
                        else if (value.Parent is XElement) { value.Parent.Add(attributes.ToArray()); }
                    }
                }

                foreach (AttributeAliasValue alias in data.Aliases.Where(w => key.Equals(w)))
                {
                    XElement? aliasNode = alias.GetXElement(scripting, (node) => properties.GetXAttributes(scripting, node, data.Properties));
                    if (aliasNode is not null && result is null)
                    {
                        result = new XElement(ScopeEnumeration.Cast(attribute.Scope).Name);
                        result.Add(aliasNode);
                    }
                    else if (aliasNode is not null && result is XElement)
                    { result.Add(aliasNode); }
                }
            }

            return result;
        }

        public static IReadOnlyList<XAttribute> GetXAttributes(
            this IEnumerable<PropertyValue> data,
            ScriptingWork scripting, ScriptingNodeValue node,
            IEnumerable<IProperty> properties)
        {
            List<XAttribute> result = new List<XAttribute>();

            ScriptingNodeIndex nodeKey = new ScriptingNodeIndex(node);
            foreach (ScriptingAttributeValue templateAttrib in scripting.Attributes.Where(w => nodeKey.Equals(w)))
            {
                XAttribute? attrib = null;
                PropertyIndex propertyKey = new PropertyIndex(templateAttrib);
                PropertyValue? propertyValue = data.FirstOrDefault(w => propertyKey.Equals(w));
                IProperty? property = properties.FirstOrDefault(w => propertyKey.Equals(w));

                String newTitle = String.Empty;
                String newValue = String.Empty;

                if (!String.IsNullOrWhiteSpace(templateAttrib.AttributeName))
                { newTitle = templateAttrib.AttributeName; }
                else if (propertyValue is PropertyValue && !String.IsNullOrWhiteSpace(propertyValue.PropertyTitle))
                { { newTitle = propertyValue.PropertyTitle; } }

                if (property is IProperty && !String.IsNullOrWhiteSpace(property.PropertyValue))
                { newValue = property.PropertyValue; }
                else if (!String.IsNullOrWhiteSpace(templateAttrib.AttributeValue))
                { newValue = templateAttrib.AttributeValue; }

                attrib = templateAttrib.BuildXAttribute(newTitle, newValue);

                if (attrib is XAttribute)
                { result.Add(attrib); }
            }

            return result;
        }
    }
}
