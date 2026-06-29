using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource.Enumerations;
using System.Reflection;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.AppScripting
{
    partial class XmlBuilder
    {
        /// <summary>
        /// Specialized constructor needed to handle PropertyInfo.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="property"></param>
        private XmlBuilder(ScopeType scope, PropertyInfo property) : this(scope)
        {
            ObjectPath = new PathIndex(property.Name).Merge(ObjectPath);
            NodeRenderAs = NodeRenderAsType.ElementText;
            GetValue = (value) => GetValueDelegate((dynamic)value, property) ?? String.Empty;
        }

        /// <summary>
        /// Specialized GetValue function that useing refelection.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private String? GetValueDelegate(Object value, PropertyInfo property)
        {
            if (property.GetValue(value) is Object objectValue)
            { return GetValueDelegate((dynamic)objectValue); }
            else { return null; }
        }

        /// <summary>
        /// Sub-Type of the XmlBuilder intended to handle the Properties of an Class.
        /// Uses reflection to get the PropertyInfo.
        /// </summary>
        public class ValueType : XmlBuilder
            //where TValue : class, IScopeType
        {
            /// <summary>
            /// List of Child Properties of the Class and the builders to go with them.
            /// </summary>
            public Dictionary<PropertyInfo, XmlBuilder> Properties = new Dictionary<PropertyInfo, XmlBuilder>();

            /// <summary>
            /// Specialized constructor for handling generic classes.
            /// </summary>
            /// <param name="source"></param>
            /// <param name="scope"></param>
            /// <param name="properties"></param>
            public ValueType(Type source, ScopeType scope, params IEnumerable<String> properties) : base(scope)
            {
                GetValue = (value) => GetValueDelegate((dynamic)value);
                
                foreach (PropertyInfo item in source.GetProperties().
                    Where(w => properties.Count() == 0 || properties.Any(a => String.Equals(a, w.Name))))
                {
                    XmlBuilder child = new XmlBuilder(ObjectScope, item);
                    Properties.Add(item, child);
                }
            }

            /// <inheritdoc/>
            public override XObject? Build<TMethod>(TMethod value)
            {
                XObject? result = base.Build(value);

                if (result is XElement element)
                {
                    foreach (var item in Properties.Values)
                    { element.Add(item.Build(value)); }

                    return result;
                }
                else if(result is null) { return result; }
                else
                {
                    Exception ex = new InvalidOperationException("XmlBuilder.Build returned something other then an XElement");
                    ex.Data.Add(nameof(base.Build), result.GetType().Name);
                    throw ex;
                }
            }
        }
    }
}
