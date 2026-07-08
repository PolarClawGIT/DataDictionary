namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// The XML Types supported by the application.
    /// This is a variation on System.Xml.Schema.XmlTypeCode.
    /// </summary>
    public enum XmlValueType
    {
        /// <inheritdoc cref="System.Xml.Schema.XmlTypeCode.None" />
        /// <remarks>Also represents Null or does not apply</remarks>
        None,

        /// <inheritdoc cref="System.Xml.Schema.XmlTypeCode.String" />
        String,

        /// <inheritdoc cref="System.Xml.Schema.XmlTypeCode.Integer" />
        Integer,

        /// <inheritdoc cref="System.Xml.Schema.XmlTypeCode.Long" />
        Long,

        /// <inheritdoc cref="System.Xml.Schema.XmlTypeCode.Boolean" />
        Boolean,

        /// <inheritdoc cref="System.Xml.Schema.XmlTypeCode.Decimal" />
        Decimal,

        /// <inheritdoc cref="System.Xml.Schema.XmlTypeCode.Float" />
        Float,

        /// <inheritdoc cref="System.Xml.Schema.XmlTypeCode.Double" />
        Double,

        /// <inheritdoc cref="System.Xml.Schema.XmlTypeCode.DateTime" />
        DateTime,

        // ** items not in System.Xml.Schema.XmlTypeCode **

        /// <summary>
        /// Guid Type. No System.Xml.Schema.XmlTypeCode equivalent.
        /// </summary>
        Guid,

        /// <summary>
        /// Class Type. Translates approximately to a System.Xml.Schema.XmlTypeCode.Element
        /// </summary>
        Class,

        /// <summary>
        /// Enum type without a specific representation.
        /// </summary>
        Enum,
    }
}
