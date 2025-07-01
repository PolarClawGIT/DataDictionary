namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>  
    /// Static class containing constants related to the Process Definitions database operations.
    /// </summary>  
    static class ProcessDefinition
    {
        /// <summary>  
        /// The stored procedure for retrieving process definitions.  
        /// </summary>  
        public const string GetProcedure = "[AppModel].[procGetProcessDefinition]";

        /// <summary>  
        /// The stored procedure for setting process definitions.  
        /// </summary>  
        public const string SetProcedure = "[AppModel].[procSetProcessDefinition]";

        /// <summary>  
        /// The user-defined table type for process definitions.  
        /// </summary>  
        public const string TableType = "[AppModel].[udttProcessDefinition]";
    }

}