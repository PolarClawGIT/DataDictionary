using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Xml.Schema;

namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Extensions on XmlTypeCode Enum. 
    /// </summary>
    public static class XmlTypeCodeExtension
    {
        /// <summary>
        /// Try to parse the String into a XmlTypeCode enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryParse(this String? value, [NotNullWhen(true)] out XmlTypeCode? result)
        {
            if (XmlTypeCodeEnumeration.TryParse(value, null, out XmlTypeCodeEnumeration? enumeration))
            { result = enumeration.Value; return true; }
            else { result = null; return false; }
        }

        /// <summary>
        /// Gets the Name of the XmlTypeCode Enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String GetName(this XmlTypeCode value)
        {
            if (XmlTypeCodeEnumeration.TryGetValue(value, out XmlTypeCodeEnumeration? type))
            { return type.Name; }
            else { return String.Empty; }
        }

        /// <inheritdoc cref="XmlTypeCodeEnumeration.TryConvert(PropertyInfo, out XmlTypeCode?)"/>
        public static Boolean TryConvert(this PropertyInfo type, [NotNullWhen(true)] out XmlTypeCode? result)
        {
            if (XmlTypeCodeEnumeration.TryConvert(type, out result))
            { return true; }
            else { result = null; return false; }
        }
    }
}
