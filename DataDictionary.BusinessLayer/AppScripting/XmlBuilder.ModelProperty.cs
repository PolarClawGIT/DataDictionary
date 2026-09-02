using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.Resource.Enumerations;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace DataDictionary.BusinessLayer.AppScripting
{
    partial class XmlBuilder
    {
        /// <summary>
        /// Specialized constructor for XML Builder use by Model Property Type.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="property"></param>
        private XmlBuilder(ScopeType scope, IPropertyValue property) : this(scope)
        {
            BuilderPath = new XmlBuilderIndex(scope, property.PropertyTitle);
            RenderNodeType = XmlNodeType.Text;
            GetValue = (value) => GetValueDelegate((dynamic)value, property) ?? String.Empty;

            ObjectProperty = property.PropertyTitle;
            RenderNodeType = (property.PropertyType is DomainPropertyType.Xml) ? XmlNodeType.Element : XmlNodeType.Text;
            RenderTypeCode = XmlTypeCode.String;
            ObjectType = TryConvert(property, out ObjectValueType? objectValue) ? objectValue.Value : ObjectValueType.Null;
            BuilderSource = property;

            Boolean TryConvert(IPropertyValue type, [NotNullWhen(true)] out ObjectValueType? result)
            {
                result = null;

                // I included but the .Net name and the C# name of the Type.
                // This is mostly for clarity and to remind myself that these things are the same.

                switch (type.PropertyType)
                {
                    case DomainPropertyType.Null: result = ObjectValueType.Null; break;
                    case DomainPropertyType.String: result = ObjectValueType.String; break;
                    case DomainPropertyType.Integer: result = ObjectValueType.Numeric; break;
                    case DomainPropertyType.List: result = ObjectValueType.StringList; break;
                    case DomainPropertyType.Xml: result = ObjectValueType.StringXML; break;
                    case DomainPropertyType.MS_ExtendedProperty: result = ObjectValueType.String; break;
                    default:
                        break;
                }

                if (result is null)
                { return false; }
                else { return true; }
            }
        }

        /// <summary>
        /// Specialized Value Delegate for Properties
        /// </summary>
        /// <param name="value"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private String? GetValueDelegate(Object value, IPropertyIndex property)
        {
            PropertyIndex key = new PropertyIndex(property);

            if (value is IPropertySubType propertyValue && key.Equals(propertyValue))
            { return propertyValue.PropertyValue; }
            else { return null; }
        }

        /// <summary>
        /// Creates a list of XmlBuilders for Model Properties.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static IEnumerable<XmlBuilder> Create(ScopeType scope, IEnumerable<PropertyValue> properties)
        {
            List<XmlBuilder> result = new List<XmlBuilder>();
            XmlBuilder root = new XmlBuilder(scope);
            result.Add(root);

            foreach (PropertyValue item in properties)
            {
                XmlBuilder child = new XmlBuilder(root.ObjectScope, item);
                result.Add(child);
            }

            return result;
        }

        
    }
}
