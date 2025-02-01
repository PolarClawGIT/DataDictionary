namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Common Properties of a Database Domain (data Type).
    /// Used by Domain, Table Column, and Routine Parameter
    /// </summary>
    public interface IDataType
    {
        /// <summary>
        /// SQL Data Type
        /// </summary>
        String? DataType { get; }

        /// <summary>
        /// If Character Field, Maximum Length allowed
        /// </summary>
        Int16? CharacterMaximumLength { get; }

        /// <summary>
        /// If Character Field, Maximum Octet Length allowed
        /// </summary>
        Int16? CharacterOctetLength { get; }

        /// <summary>
        /// If Numeric, the Precision
        /// </summary>
        Byte? NumericPrecision { get; }

        /// <summary>
        /// If Numeric, the Radix Precision
        /// </summary>
        Byte? NumericPrecisionRadix { get; }

        /// <summary>
        /// If Numeric, the Scale
        /// </summary>
        Byte? NumericScale { get; }

        /// <summary>
        /// If DateTime, the Precision
        /// </summary>
        Byte? DateTimePrecision { get; }

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

    /// <summary>
    /// Common Properties of a Database Domain (data Type) that have an OrdinalPosition (Columns, Parameters)
    /// </summary>
    public interface IOrdinalPosition
    {
        /// <summary>
        /// The Position/Order of the Column
        /// </summary>
        Nullable<Int32> OrdinalPosition { get; }
    }
}
