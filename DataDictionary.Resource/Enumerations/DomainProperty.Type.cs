namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// List of supported Table Types.
    /// </summary>
    public enum DomainPropertyType
    {
        /// <summary>
        /// Unknown Type
        /// </summary>
        Null,

        /// <summary>
        /// Contains String Data
        /// </summary>
        String,

        /// <summary>
        /// Contains Integer Data
        /// </summary>
        Integer,

        /// <summary>
        /// Contains a List of Comma Separated values
        /// </summary>
        /// <remarks>Data element contains valid list values.</remarks>
        List,

        /// <summary>
        /// Contains XML
        /// </summary>
        Xml,

        /// <summary>
        /// Contains an MS Extended Property.
        /// </summary>
        /// <remarks>This needs special handling. Data Element contains the Extended Property Name.</remarks>
        MS_ExtendedProperty
    }
}
