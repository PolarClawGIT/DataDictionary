namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Static class containing constants related to the Attribute Definition database operations.
    /// </summary>
    static class AttributeDefinition
    {
        /// <summary>
        /// The stored procedure used to retrieve attribute definitions.
        /// </summary>
        public const String GetProcedure = "[AppModel].[procGetAttributeDefinition]";

        /// <summary>
        /// The stored procedure used to set attribute definitions.
        /// </summary>
        public const String SetProcedure = "[AppModel].[procSetAttributeDefinition]";

        /// <summary>
        /// The user-defined table type for attribute definitions.
        /// </summary>
        public const String TableType = "[AppModel].[udttAttributeDefinition]";
    }
}