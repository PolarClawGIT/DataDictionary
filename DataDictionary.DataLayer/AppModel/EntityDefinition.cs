namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>  
    /// Static class containing constants related to the Entity Definition database operations.  
    /// </summary>  
    static class EntityDefinition
    {
        /// <summary>  
        /// The stored procedure used to retrieve entity definitions.  
        /// </summary>  
        public const String GetProcedure = "[AppModel].[procGetEntityDefinition]";

        /// <summary>  
        /// The stored procedure used to set entity definitions.  
        /// </summary>  
        public const String SetProcedure = "[AppModel].[procSetEntityDefinition]";

        /// <summary>  
        /// The table type used for entity definitions.  
        /// </summary>  
        public const String TableType = "[AppModel].[udttEntityDefinition]";
    }
}