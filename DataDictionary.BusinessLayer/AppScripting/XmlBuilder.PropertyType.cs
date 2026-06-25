using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.AppScripting
{
    partial class XmlBuilder
    {
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
