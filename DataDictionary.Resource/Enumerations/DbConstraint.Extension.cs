using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Extensions on DbConstraint Enum. 
    /// </summary>
    public static class DbConstraintExtension
    {
        /// <summary>
        /// Gets the Details for the DbConstraintType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumeration<DbConstraintType> GetEnumeration(this DbConstraintType value)
        { return DbConstraintEnumeration.Cast(value); }

        /// <summary>
        /// Try to parse the String into a DbConstraintType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryParse(this String? value, out DbConstraintType result)
        {
            if (DbConstraintEnumeration.TryParse(value, null, out DbConstraintEnumeration? enumeration))
            { result = enumeration.Value; return true; }
            else { result = DbConstraintType.Null; return false; }
        }
    }
}
