using System;
using System.Collections.Generic;
using System.Text;

namespace DataDictionary.Resource.Enumerations
{
    public static class ValueTypeExtension
    {
        /// <summary>
        /// Try to parse the String into a ObjectPropertyType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryParse(this String? value, out ObjectValueType result)
        {
            if (ObjectValueTypeEnumeration.TryParse(value, null, out ObjectValueTypeEnumeration? enumeration))
            { result = enumeration.Value; return true; }
            else { result = ObjectValueType.Null; return false; }
        }

        /// <summary>
        /// Gets the Name of the ObjectPropertyType Enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String GetName(this ObjectValueType value)
        {
            if (ObjectValueTypeEnumeration.TryGetValue(value, out ObjectValueTypeEnumeration? result))
            { return result.Name; }
            else { return String.Empty; }
        }
    }
}
