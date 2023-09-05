using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData
{
    /// <summary>
    /// Common Properties of a Database Domain (data Type).
    /// Used by Domain, Table Column, and Routine Parameter
    /// </summary>
    public interface IDbDomain
    {
        String? DataType { get; }
        Nullable<Int32> CharacterMaximumLength { get; }
        Nullable<Int32> CharacterOctetLength { get; }
        Nullable<Byte> NumericPrecision { get; }
        Nullable<Int16> NumericPrecisionRadix { get; }
        Nullable<Int32> NumericScale { get; }
        Nullable<Int16> DateTimePrecision { get; }
        // Character Set
        String? CharacterSetCatalog { get; }
        String? CharacterSetSchema { get; }
        String? CharacterSetName { get; }
        // Sort Order
        String? CollationCatalog { get; }
        String? CollationSchema { get; }
        String? CollationName { get; }
    }

    /// <summary>
    /// Common Properties of a Database Column and Parameters.
    /// Used by Table Column and Routine Parameter.
    /// Inherits from IDbDomain.
    /// </summary>
    public interface IDbColumn : IDbDomain
    {
        Nullable<Int32> OrdinalPosition { get; }
    }
}
