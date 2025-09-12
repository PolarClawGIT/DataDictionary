using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Extensions on DbRoutine Enum. 
    /// </summary>
    public static class DbRoutineExtension
    {
        /// <summary>
        /// Gets the Details for the DbRoutineType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumeration<DbRoutineType> GetEnumeration(this DbRoutineType value)
        { return DbRoutineEnumeration.GetValue(value); }

        /// <summary>
        /// Try to parse the String into a DbRoutineType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryParse(this String? value, out DbRoutineType result)
        {
            if (DbRoutineEnumeration.TryParse(value, null, out DbRoutineEnumeration? enumeration))
            { result = enumeration.Value; return true; }
            else { result = DbRoutineType.Null; return false; }
        }
    }
}
