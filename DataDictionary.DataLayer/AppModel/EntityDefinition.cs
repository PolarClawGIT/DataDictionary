namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>  
    /// Static class containing constants related to the Entity Definition database operations.  
    /// </summary>  
    static class EntityDefinition
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(EntityDefinition));

        /// <summary>  
        /// The stored procedure used to retrieve entity definitions.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetEntityDefinition]");

        /// <summary>  
        /// The stored procedure used to set entity definitions.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetEntityDefinition]");

        /// <summary>  
        /// The table type used for entity definitions.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttEntityDefinition]");
    }
}