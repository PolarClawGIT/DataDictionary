using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.AppScripting
{
    partial class XmlBuilder
    {

        private XmlBuilder(ScopeType scope, IPropertyValue property) : this(scope)
        {
            ObjectPath = new PathIndex(property.PropertyTitle).Merge(ObjectPath);
            NodeRenderAs = NodeRenderAsType.ElementText;
            GetValue = (value) => GetValueDelegate((dynamic)value, property) ?? String.Empty;
        }

        private String? GetValueDelegate(Object value, IPropertyIndex property)
        {
            PropertyIndex key = new PropertyIndex(property);

            if (value is IPropertySubType propertyValue && key.Equals(propertyValue))
            { return propertyValue.PropertyValue; }
            else { return null; }
        }

        public class PropertyType<TValue> : XmlBuilder
            where TValue : IPropertySubType
        {
            public Dictionary<PropertyIndex, XmlBuilder> Children = new Dictionary<PropertyIndex, XmlBuilder>();

            public PropertyType(ScopeType scope, IEnumerable<PropertyValue> properties) : base(scope)
            {
                foreach (PropertyValue item in properties)
                {
                    XmlBuilder child = new XmlBuilder(ObjectScope, item);
                    Children.Add(new PropertyIndex(item), child);
                }
            }

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
                else
                {
                    throw new NotImplementedException(); // Not sure what to do here.
                }

                return result;
            }
        }
    }
}
