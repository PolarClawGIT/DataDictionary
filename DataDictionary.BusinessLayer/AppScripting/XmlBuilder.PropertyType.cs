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
        /// Specialized sub-type of the XML Builder for handling of the Model Property.
        /// </summary>
        public class PropertyType: XmlBuilder
        {
            //TODO: This is messy. How do I get rid of this? What value does it add?

            /// <summary>
            /// Child XML Builders of the Property
            /// </summary>
            public Dictionary<PropertyIndex, XmlBuilder> Children = new Dictionary<PropertyIndex, XmlBuilder>();

            /// <summary>
            /// Constructor for the XML Builder of Model Properties
            /// </summary>
            /// <param name="scope"></param>
            /// <param name="properties"></param>
            public PropertyType(ScopeType scope, IEnumerable<PropertyValue> properties) : base(scope)
            {
                foreach (PropertyValue item in properties)
                {
                    XmlBuilder child = new XmlBuilder(ObjectScope, item)
                    {
                        ObjectProperty = item.PropertyTitle,
                        RenderNodeType = (item.PropertyType is DomainPropertyType.Xml) ? XmlNodeType.Element : XmlNodeType.Text,
                        RenderTypeCode = XmlTypeCode.String,
                        ObjectType = TryConvert(item, out ObjectValueType? objectValue) ? objectValue.Value : ObjectValueType.Null
                    };

                    Children.Add(new PropertyIndex(item), child);
                }
            }

            /// <inheritdoc cref="XmlBuilder(XmlBuilder)"/>
            public PropertyType(PropertyType source) : base(source)
            {
                foreach (var item in source.Children)
                { Children.Add(item.Key, new XmlBuilder(item.Value)); }
            }

            /// <inheritdoc/>
            public override XObject? Build<TMethod>(TMethod value)
            {
                XObject? result = base.Build(value);

                if (result is XElement element
                    && value is IPropertySubType property)
                {
                    PropertyIndex key = new PropertyIndex(property);
                    if (Children.TryGetValue(key, out XmlBuilder? builder))
                    { result = builder.Build(value); }
                }
                else if (result is null) { return result; }
                else
                {
                    Exception ex = new InvalidOperationException("XmlBuilder.Build returned something other then an XElement");
                    ex.Data.Add(nameof(base.Build), result.GetType().Name);
                    throw ex;
                }

                return result;
            }

            /// <summary>
            /// Try to Convert the PropertyInfo into an ObjectValueType.
            /// </summary>
            /// <param name="type"></param>
            /// <param name="result"></param>
            /// <returns></returns>
            public virtual Boolean TryConvert(PropertyValue type, [NotNullWhen(true)] out ObjectValueType? result)
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
    }
}
