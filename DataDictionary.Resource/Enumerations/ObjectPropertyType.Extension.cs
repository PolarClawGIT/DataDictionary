using System;
using System.Collections.Generic;
using System.Text;

namespace DataDictionary.Resource.Enumerations
{
    public static class ObjectPropertyTypeExtension
    {
        /// <summary>
        /// Try to parse the String into a ObjectPropertyType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryParse(this String? value, out ObjectPropertyType result)
        {
            if (ObjectPropertyTypeEnumeration.TryParse(value, null, out ObjectPropertyTypeEnumeration? enumeration))
            { result = enumeration.Value; return true; }
            else { result = ObjectPropertyType.Null; return false; }
        }

        /// <summary>
        /// Gets the Name of the ObjectPropertyType Enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String GetName(this ObjectPropertyType value)
        {
            if (ObjectPropertyTypeEnumeration.TryGetValue(value, out ObjectPropertyTypeEnumeration? result))
            { return result.Name; }
            else { return String.Empty; }
        }
    }
}
