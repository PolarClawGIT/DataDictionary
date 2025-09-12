using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Extensions on DbObject Enum. 
    /// </summary>
    public static class DbObjectExtension
    {
        /// <summary>
        /// Gets the Details for the DbObjectType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumeration<DbObjectType> GetEnumeration(this DbObjectType value)
        { return DbObjectEnumeration.Cast(value); }

        /// <summary>
        /// Try to parse the String into a DbObjectType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryParse(this String? value, out DbObjectType result)
        {
            if (DbObjectEnumeration.TryParse(value, null, out DbObjectEnumeration? enumeration))
            { result = enumeration.Value; return true; }
            else { result = DbObjectType.Null; return false; }
        }
    }
}
