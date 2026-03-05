namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Static class containing constants related to the Attribute Definition database operations.
    /// </summary>
    static class AttributeDefinition
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(AttributeDefinition));

        /// <summary>
        /// The stored procedure used to retrieve attribute definitions.
        /// </summary>
        public readonly static String GetProcedure = schema.FullName("[procGetAttributeDefinition]");

        /// <summary>
        /// The stored procedure used to set attribute definitions.
        /// </summary>
        public readonly static String SetProcedure = schema.FullName("[procSetAttributeDefinition]");

        /// <summary>
        /// The user-defined table type for attribute definitions.
        /// </summary>
        public readonly static String TableType = schema.FullName("[udttAttributeDefinition]");
    }
}