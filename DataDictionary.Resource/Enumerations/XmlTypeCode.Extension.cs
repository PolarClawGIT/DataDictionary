using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Xml.Schema;

namespace DataDictionary.Resource.Enumerations
{
    public static class XmlTypeCodeExtension
    {
        /// <summary>
        /// Try to parse the String into a XmlTypeCode enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryParse(this String? value, out XmlTypeCode result)
        {
            if (XmlTypeCodeEnumeration.TryParse(value, null, out XmlTypeCodeEnumeration? enumeration))
            { result = enumeration.Value; return true; }
            else { result = XmlTypeCode.None; return false; }
        }

        /// <summary>
        /// Gets the Name of the XmlTypeCode Enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String GetName(this XmlTypeCode value)
        {
            if (XmlTypeCodeEnumeration.TryGetValue(value, out XmlTypeCodeEnumeration? result))
            { return result.Name; }
            else { return String.Empty; }
        }

        public static Boolean TryGetValue(this XmlTypeCode value, [NotNullWhen(true)] out XmlTypeCodeEnumeration? result)
        {
            if (XmlTypeCodeEnumeration.TryGetValue(value, out result))
            { return true; }
            else { result = null; return false; }
        }

        /// <summary>
        /// Try to Convert the PropertyInfo into an XmlTypeCode.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        /// <remarks>This handled the general case, not specialized classes.</remarks>
        public static Boolean TryConvert(this PropertyInfo type, [NotNullWhen(true)] out XmlTypeCode? result)
        {
            result = null;

            if (XmlTypeCodeEnumeration.TryConvert(type.PropertyType, out XmlTypeCode value))
            { result = value; return true; }
            else { result = null; return false; }
        }
    }
}
