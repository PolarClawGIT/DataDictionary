using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Extensions on DbTable Enum. 
    /// </summary>
    public static class DbTableExtension
    {
        /// <summary>
        /// Gets the Details for the DbTableType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumeration<DbTableType> GetEnumeration(this DbTableType value)
        { return DbTableEnumeration.Cast(value); }

        /// <summary>
        /// Try to parse the String into a DbTableType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryParse(this String? value, out DbTableType result)
        {
            if (DbTableEnumeration.TryParse(value, null, out DbTableEnumeration? enumeration))
            { result = enumeration.Value; return true; }
            else { result = DbTableType.Null; return false; }
        }
    }
}
