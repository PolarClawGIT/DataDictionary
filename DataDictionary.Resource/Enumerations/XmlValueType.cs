namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// The XML Types supported by the application.
    /// This is a variation on System.Xml.Schema.XmlTypeCode.
    /// </summary>
    [Obsolete("Switch to XmlTypeCode and ObjectPropertyType")]
    public enum XmlValueType
    {
        //TODO: List is incomplete and Mapping from .Net to XmlTypeCode is incomplete.

        // This is not working as desired. Want to be able set the XmlTypeCode based on System.Reflection.PropertyInfo.
        // Then use the XmlTypeCode to determine what icon to show next to the field.
        // Some types, such as GUID, are missing. There is no 1 to 1 mapping of the two.

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

        /// <summary>
        /// An XML Fragment.
        /// </summary>
        Xml,

        /// <summary>
        /// String encoded as Rich Text
        /// </summary>
        RichText,

        /// <summary>
        /// Some type of list. Such as a String as a delimited list of values or List{T}.
        /// </summary>
        List
    }
}
