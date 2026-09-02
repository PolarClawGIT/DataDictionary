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

            ObjectProperty = property.Name;
            ObjectType = GetObjectType(property);
            RenderTypeCode = property.TryConvert(out XmlTypeCode? xmlValue) ? xmlValue.Value : XmlTypeCode.None;

            BuilderSource = property;

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
        /// Creates a list of XmlBuilder for objects.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        public static IEnumerable<XmlBuilder> Create(ScopeType scope, Type source)
        {
            List<XmlBuilder> result = new List<XmlBuilder>();
            XmlBuilder root = new XmlBuilder(scope);
            result.Add(root);

            foreach (PropertyInfo item in source.GetProperties())
            { result.Add(new XmlBuilder(root.ObjectScope, item)); }

            return result;
        }
    }
}
