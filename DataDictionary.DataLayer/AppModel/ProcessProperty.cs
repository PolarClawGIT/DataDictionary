namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>  
    /// Static class containing constants related to the Process Properties database operations.
    /// </summary>  
    static class ProcessProperty
    {
        /// <summary>  
        /// The name of the stored procedure used to retrieve process properties.  
        /// </summary>  
        public const string GetProcedure = "[AppModel].[procGetProcessProperty]";

        /// <summary>  
        /// The name of the stored procedure used to set process properties.  
        /// </summary>  
        public const string SetProcedure = "[AppModel].[procSetProcessProperty]";

        /// <summary>  
        /// The name of the user-defined table type for process properties.  
        /// </summary>  
        public const string TableType = "[AppModel].[udttProcessProperty]";
    }
}