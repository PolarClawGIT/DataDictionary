using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Extensions on DomainProperty Enum. 
    /// </summary>
    public static class DomainPropertyExtension
    {
        /// <summary>
        /// Gets the Details for the DomainPropertyType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DomainPropertyEnumeration GetEnumeration(this DomainPropertyType value)
        { return DomainPropertyEnumeration.Cast(value); }

        /// <summary>
        /// Try to parse the String into a DomainPropertyType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryParse(this String? value, out DomainPropertyType result)
        {
            if (DomainPropertyEnumeration.TryParse(value, null, out DomainPropertyEnumeration? enumeration))
            { result = enumeration.Value; return true; }
            else { result = DomainPropertyType.Null; return false; }
        }
    }
}
