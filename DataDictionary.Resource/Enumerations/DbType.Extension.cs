using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Extensions on DbType Enum. 
    /// </summary>
    public static class DbTypeExtension
    {
        /// <summary>
        /// Gets the Details for the DbType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IDbTypeEnumeration GetEnumeration(this DbType value)
        { return DbTypeEnumeration.GetValue(value); }

        /// <summary>
        /// Try to parse the String into a DbType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryParse(this String? value, [NotNullWhen(true)] out DbType? result)
        {
            if (DbTypeEnumeration.TryParse(value, null, out DbTypeEnumeration? enumeration))
            { result = enumeration.Value; return true; }
            else { result = null; return false; }
        }
    }
}
