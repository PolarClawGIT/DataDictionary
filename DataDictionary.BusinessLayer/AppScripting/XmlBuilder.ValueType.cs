using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.AppScripting
{
    partial class XmlBuilder
    {
        public class ValueType<TValue> : XmlBuilder
            where TValue : class, IScopeType
        {
            public Dictionary<PropertyInfo, XmlBuilder> Children = new Dictionary<PropertyInfo, XmlBuilder>();

            public ValueType(ScopeType scope, params IEnumerable<String> properties) : base(scope)
            {
                GetValue = (value) => GetValueDelegate((dynamic)value);

                Type value = typeof(TValue);

                foreach (PropertyInfo item in value.GetProperties().
                    Where(w => properties.Count() == 0 || properties.Any(a => String.Equals(a, w.Name))))
                {
                    XmlBuilder child = new XmlBuilder(ObjectScope, item);
                    Children.Add(item, child);
                }
            }

            public override XObject? Build<TMethod>(TMethod value)
            {
                XObject? result = base.Build(value);

                if (result is XElement element)
                {
                    foreach (var item in Children.Values)
                    { element.Add(item.Build(value)); }
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
