namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>  
    /// Static class containing constants related to the Entity Alias database operations.  
    /// </summary>  
    static class EntityAlias
    {
        /// <summary>  
        /// The stored procedure used to retrieve entity alias information.  
        /// </summary>  
        public const String GetProcedure = "[AppModel].[procGetEntityAlias]";

        /// <summary>  
        /// The stored procedure used to set entity alias information.  
        /// </summary>  
        public const String SetProcedure = "[AppModel].[procSetEntityAlias]";

        /// <summary>  
        /// The table type used for entity alias operations.  
        /// </summary>  
        public const String TableType = "[AppModel].[udttEntityAlias]";
    }
}