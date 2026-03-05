namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>  
    /// Static class containing constants related to the Attribute Alias database operations.  
    /// </summary>  
    static class AttributeAlias
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(AttributeAlias));

        /// <summary>  
        /// The stored procedure used to retrieve attribute aliases.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetAttributeAlias]");

        /// <summary>  
        /// The stored procedure used to set attribute aliases.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetAttributeAlias]");

        /// <summary>  
        /// The table type used for attribute alias operations.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttAttributeAlias]");
    }
}