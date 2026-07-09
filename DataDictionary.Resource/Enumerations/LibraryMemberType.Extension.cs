using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Resource.Enumerations
{
    internal class LibraryMember
    {
    }
    /// <summary>
    /// Extensions on LibraryMember Enum. 
    /// </summary>
    public static class LibraryMemberExtension
    {
        /// <summary>
        /// Gets the Details for the LibraryMemberType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ILibraryMemberEnumeration GetEnumeration(this LibraryMemberType value)
        { return LibraryMemberEnumeration.GetValue(value); }

        /// <summary>
        /// Try to parse the String into a LibraryMemberType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryParse(this String? value, out LibraryMemberType result)
        {
            if (LibraryMemberEnumeration.TryParse(value, null, out LibraryMemberEnumeration? enumeration))
            { result = enumeration.Value; return true; }
            else { result = LibraryMemberType.Null; return false; }
        }

        /// <summary>
        /// Try to parse the Library Character Code into a LibraryMemberType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryParse(this Char value, out LibraryMemberType result)
        {
            if (LibraryMemberEnumeration.TryParse(value, null, out LibraryMemberEnumeration? enumeration))
            { result = enumeration.Value; return true; }
            else { result = LibraryMemberType.Null; return false; }
        }
    }
}
