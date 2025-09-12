using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Extensions on DbLevelObject Enum. 
    /// </summary>
    public static class DbLevelObjectExtension
    {
        /// <summary>
        /// Gets the Details for the DbLevelObjectType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumeration<DbLevelObjectType> GetEnumeration(this DbLevelObjectType value)
        { return DbLevelObjectEnumeration.Cast(value); }

        /// <summary>
        /// Try to parse the String into a DbLevelObjectType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryParse(this String? value, out DbLevelObjectType result)
        {
            if (DbLevelObjectEnumeration.TryParse(value, null, out DbLevelObjectEnumeration? enumeration))
            { result = enumeration.Value; return true; }
            else { result = DbLevelObjectType.Null; return false; }
        }
    }
}
