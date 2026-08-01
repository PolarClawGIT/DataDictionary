using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource.Enumerations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

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
            BuilderPath = new XmlBuilderIndex(scope, property.Name);
            RenderNodeType = XmlNodeType.Text;
            GetValue = (value) => GetValueDelegate((dynamic)value, property) ?? String.Empty;
        }

        /// <summary>
        /// Specialized GetValue function that using reflection.
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
        {
            //TODO: This is messy. How do I get rid of this? What value does it add?

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
                //GetValue = (value) => GetValueDelegate((dynamic)value);

                foreach (PropertyInfo item in source.GetProperties().
                    Where(w => properties.Count() == 0 || properties.Any(a => String.Equals(a, w.Name))))
                {
                    XmlBuilder child = new XmlBuilder(ObjectScope, item)
                    {
                        ObjectProperty = item.Name,
                        RenderTypeCode= item.TryConvert(out XmlTypeCode? xmlValue) ? xmlValue.Value : XmlTypeCode.None,
                        ObjectType = GetObjectType(item)
                    };

                    Properties.Add(item, child);
                }

                ObjectValueType GetObjectType(PropertyInfo info)
                {
                    if (info.PropertyType == typeof(PathIndex))
                    { return ObjectValueType.NameSpace; }
                    else if (info.TryConvert(out ObjectValueType value))
                    { return value; }
                    else { return ObjectValueType.Null; }
                }
            }

            /// <summary>
            /// Creates a Clone of the ValueType XmlBuilder
            /// </summary>
            /// <param name="source"></param>
            public ValueType(ValueType source) : base(source)
            {
                foreach (var item in source.Properties)
                { Properties.Add(item.Key, new XmlBuilder(item.Value)); }
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
                else if (result is null) { return result; }
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
