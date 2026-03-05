namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>  
    /// Static class containing constants related to the Attribute Property database operations.  
    /// </summary>  
    static class AttributeProperty
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(AttributeProperty));

        /// <summary>  
        /// The stored procedure used to retrieve attribute properties.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetAttributeProperty]");

        /// <summary>  
        /// The stored procedure used to set attribute properties.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetAttributeProperty]");

        /// <summary>  
        /// The table type used for attribute properties.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttAttributeProperty]");
    }
}