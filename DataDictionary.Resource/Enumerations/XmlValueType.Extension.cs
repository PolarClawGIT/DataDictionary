using System.Diagnostics.CodeAnalysis;
using System.Xml.Schema;

namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Extensions on XmlType Enum. 
    /// </summary>
    public static class XmlTypeExtension
    {
        /// <summary>
        /// Gets the Details for the XmlType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IXmlTypeEnumeration GetEnumeration(this XmlValueType value)
        { return XmlTypeEnumeration.GetValue(value); }

        /// <summary>
        /// Try to parse the String into a XmlType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryParse(this String? value, [NotNullWhen(true)] out XmlValueType? result)
        {
            if (XmlTypeEnumeration.TryParse(value, null, out XmlTypeEnumeration? enumeration))
            { result = enumeration.Value; return true; }
            else { result = null; return false; }
        }

        /// <summary>
        /// Try to get the XML Type Code from the .Net Type.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryConvert(this Type type, [NotNullWhen(true)] out XmlValueType? result)
        {
            if (XmlTypeEnumeration.TryConvert(type, out XmlValueType? value))
            { result = value; return true; }
            else { result = null; return false; }
        }

        /// <summary>
        /// Gets the Name of the XmlType Enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String GetName(this XmlValueType value)
        {
            if (XmlTypeEnumeration.TryGetValue(value, out XmlTypeEnumeration? type))
            { return type.Name; }
            else { return String.Empty; }
        }
    }
}
