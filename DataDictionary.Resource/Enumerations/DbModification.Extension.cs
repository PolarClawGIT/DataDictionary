using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Extensions on DbModification Enum. 
    /// </summary>
    public static class DbModificationExtension
    {
        /// <summary>
        /// Gets the Details for the DbModificationType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumeration<DbModificationType> GetEnumeration(this DbModificationType value)
        { return DbModificationEnumeration.GetValue(value); }

        /// <summary>
        /// Try to parse the String into a DbModificationType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryParse(this String? value, out DbModificationType result)
        {
            if (DbModificationEnumeration.TryParse(value, null, out DbModificationEnumeration? enumeration))
            { result = enumeration.Value; return true; }
            else { result = DbModificationType.Null; return false; }
        }
    }
}
