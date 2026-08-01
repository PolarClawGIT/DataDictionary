using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Extensions on DbLevelElement Enum. 
    /// </summary>
    public static class DbLevelElementExtension
    {
        /// <summary>
        /// Gets the Details for the DbLevelElementType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumeration<DbLevelElementType> GetEnumeration(this DbLevelElementType value)
        { return DbLevelElementEnumeration.GetValue(value); }

        /// <summary>
        /// Try to parse the String into a DbLevelElementType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryParse(this String? value, out DbLevelElementType result)
        {
            if (DbLevelElementEnumeration.TryParse(value, null, out DbLevelElementEnumeration? enumeration))
            { result = enumeration.Value; return true; }
            else { result = DbLevelElementType.Null; return false; }
        }

        public static DbLevelElementType GetDbLevel(String? value)
        { return DbLevelElementEnumeration.Parse(value ?? String.Empty, null).Value; }

        public static String GetName(this DbLevelElementType value)
        { return DbLevelElementEnumeration.GetValue(value).Name; }
    }
}
