namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>  
    /// Static class containing constants related to the Process Aliases database operations.  
    /// </summary>  
    static class ProcessAlias
    {
        /// <summary>  
        /// The stored procedure used to retrieve process aliases.  
        /// </summary>  
        public const String GetProcedure = "[AppModel].[procGetProcessAlias]";

        /// <summary>  
        /// The stored procedure used to set process aliases.  
        /// </summary>  
        public const String SetProcedure = "[AppModel].[procSetProcessAlias]";

        /// <summary>  
        /// The user-defined table type for process aliases.  
        /// </summary>  
        public const String TableType = "[AppModel].[udttProcessAlias]";
    }
}