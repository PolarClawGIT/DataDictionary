namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>  
    /// Static class containing constants related to the Scripting Template Data database operations.  
    /// </summary>  
    static class TemplateData
    {
        /// <summary>  
        /// The stored procedure used to retrieve scripting Template Data.  
        /// </summary>  
        public const String GetProcedure = "[AppScript].[procGetTemplateData]";

        /// <summary>  
        /// The stored procedure used to set scripting Template Data.  
        /// </summary>  
        public const String SetProcedure = "[AppScript].[procSetTemplateData]";

        /// <summary>  
        /// The user-defined table type for scripting Template Data.  
        /// </summary>  
        public const String TableType = "[AppScript].[udttTemplateData]";
    }
}
