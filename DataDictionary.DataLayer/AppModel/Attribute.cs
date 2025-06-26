namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for the Model Attribute
    /// </summary>
    public interface IAttribute : IAttributeKeyName, IAttributeSubjectAreaName
    {
        /// <summary>
        /// Description of the Model Attribute
        /// </summary>
        String? AttributeDescription { get; set; }

        /// <summary>
        /// Generic Data Type of the Attribute
        /// </summary>
        String? DataType { get; set; }

        /// <summary>
        /// Data Length of the Attribute. Generally for Character data. (Null generally = max allowed)
        /// </summary>
        /// <remarks>Unicode characters can take of more then one byte. This is the length in characters, not bytes.</remarks>
        Int16? DataLength { get; set; }

        /// <summary>
        /// Data Precision of the Attribute. Total number of digits. Generally for numerics and Date/Time.
        /// </summary>
        Byte? DataPrecision { get; set; }

        /// <summary>
        /// Data Scale of the Attribute. Number of digits right of the decimal place. Generally for floating point numerics.
        /// </summary>
        Byte? DataScale { get; set; }

        /// <summary>
        /// Is Attribute Single Valued (has only one value, not multi-valued)
        /// </summary>
        Boolean IsSingleValue { get; set; }

        /// <summary>
        /// Is Attribute Multi Valued (has multiple values, not single)
        /// </summary>
        Boolean IsMultiValue { get; set; }

        /// <summary>
        /// Is Attribute a Simple Type (cannot be decomposed, not composite)
        /// </summary>
        Boolean IsSimpleType { get; set; }

        /// <summary>
        /// Is Attribute a Composite Type (composed of multiple Simple Types, not Simple)
        /// </summary>
        Boolean IsCompositeType { get; set; }

        /// <summary>
        /// Is Attribute a Derived Value (Computed value, not Integral)
        /// </summary>
        Boolean IsDerived { get; set; }

        /// <summary>
        /// Is Attribute an Integral value (distinct or basic value, not Derived)
        /// </summary>
        Boolean IsIntegral { get; set; }

        /// <summary>
        /// Is the Attribute a Null-able value (allows Null value)
        /// </summary>
        Boolean IsNullable { get; set; }

        /// <summary>
        /// Is the Attribute a valued (not null) attribute.
        /// </summary>
        Boolean IsValued { get; set; }

        /// <summary>
        /// Is the Attribute is used as a Key (part of a PK, FK, or AK)
        /// </summary>
        Boolean IsKey { get; set; }

        /// <summary>
        /// Is the Attribute a Non-Key item (not part of a PK, FK or AK)
        /// </summary>
        Boolean IsNonKey { get; set; }
    }

    static class Attribute
    {
        public const String AttributeId = "@AttributeId";
        public const String GetProcedure = "[AppModel].[procGetAttribute]";
        public const String SetProcedure = "[AppModel].[procSetAttribute]";
        public const String TableType = "[AppModel].[udttAttribute]";
    }
}