namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>  
    /// Static class containing constants related to the Process Arguments database operations.  
    /// </summary>  
    static class ProcessArgument
    {
        /// <summary>  
        /// The stored procedure used to retrieve process arguments.  
        /// </summary>  
        public const String GetProcedure = "[AppModel].[procGetProcessArgument]";

        /// <summary>  
        /// The stored procedure used to set process arguments.  
        /// </summary>  
        public const String SetProcedure = "[AppModel].[procSetProcessArgument]";

        /// <summary>  
        /// The user-defined table type for process arguments.  
        /// </summary>  
        public const String TableType = "[AppModel].[udttProcessArgument]";
    }
}