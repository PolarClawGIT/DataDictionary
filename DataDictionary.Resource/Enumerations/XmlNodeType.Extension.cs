using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace DataDictionary.Resource.Enumerations
{
    
    public static class XmlNodeTypeExtension
    {
        /// <summary>
        /// Try to parse the String into a XmlNodeType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryParse(this String? value, out XmlNodeType result)
        {
            if (XmlNodeTypeEnumeration.TryParse(value, null, out XmlNodeTypeEnumeration? enumeration))
            { result = enumeration.Value; return true; }
            else { result = XmlNodeType.None; return false; }
        }

        /// <summary>
        /// Gets the Name of the XmlNodeType Enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String GetName(this XmlNodeType value)
        {
            if (XmlNodeTypeEnumeration.TryGetValue(value, out XmlNodeTypeEnumeration? result))
            { return result.Name; }
            else { return String.Empty; }
        }

        public static Boolean TryGetValue(this XmlNodeType value, [NotNullWhen(true)] out XmlNodeTypeEnumeration? result)
        {
            if (XmlNodeTypeEnumeration.TryGetValue(value, out result))
            { return true; }
            else { result = null; return false; }
        }
    }
}
