namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>  
    /// Static class containing constants related to the Entity Alias database operations.  
    /// </summary>  
    static class EntityAlias
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(EntityAlias));

        /// <summary>  
        /// The stored procedure used to retrieve entity alias information.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetEntityAlias]");

        /// <summary>  
        /// The stored procedure used to set entity alias information.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetEntityAlias]");

        /// <summary>  
        /// The table type used for entity alias operations.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttEntityAlias]");
    }
}