using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData.Domain
{
    /// <summary>
    /// Common Properties of a Database Domain (data Type).
    /// Used by Domain, Table Column, and Routine Parameter
    /// </summary>
    public interface IDbDomain
    {
        /// <summary>
        /// SQL Data Type
        /// </summary>
        String? DataType { get; }

        /// <summary>
        /// If Character Field, Maximum Length allowed
        /// </summary>
        Nullable<Int32> CharacterMaximumLength { get; }

        /// <summary>
        /// If Character Field, Maximum Octet Length allowed
        /// </summary>
        Nullable<Int32> CharacterOctetLength { get; }

        /// <summary>
        /// If Numeric, the Precision
        /// </summary>
        Nullable<Byte> NumericPrecision { get; }

        /// <summary>
        /// If Numeric, the Radix Precision
        /// </summary>
        Nullable<Int16> NumericPrecisionRadix { get; }

        /// <summary>
        /// If Numeric, the Scale
        /// </summary>
        Nullable<Int32> NumericScale { get; }

        /// <summary>
        /// If DateTime, the Precision
        /// </summary>
        Nullable<Int16> DateTimePrecision { get; }

        /// <summary>
        /// If Character, the Catalog Name of the Character Set.
        /// </summary>
        String? CharacterSetCatalog { get; }

        /// <summary>
        /// If Character, the Schema Name of the Character Set
        /// </summary>
        String? CharacterSetSchema { get; }

        /// <summary>
        /// If Character, the Name of the Character Set
        /// </summary>
        String? CharacterSetName { get; }
        
        /// <summary>
        /// If Character, the Catalog Name of the Collation (sort) set.
        /// </summary>
        String? CollationCatalog { get; }

        /// <summary>
        /// If Character, the Schema Name of the Collation (sort) set.
        /// </summary>
        String? CollationSchema { get; }

        /// <summary>
        /// If Character, the Name of the Collation (sort) set.
        /// </summary>
        String? CollationName { get; }
    }
}
