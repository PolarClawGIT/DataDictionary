namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>  
    /// Static class containing constants related to the Scripting Template Input Data Source database operations.  
    /// </summary>  
    [Obsolete]
    static class TemplateInput
    {
        /// <summary>  
        /// The stored procedure used to retrieve scripting Template Input Data Source .  
        /// </summary>  
        public const String GetProcedure = "[AppScript].[procGetTemplateInput]";

        /// <summary>  
        /// The stored procedure used to set scripting Template Input Data Source .  
        /// </summary>  
        public const String SetProcedure = "[AppScript].[procSetTemplateInput]";

        /// <summary>  
        /// The user-defined table type for scripting Template Input Data Source .  
        /// </summary>  
        public const String TableType = "[AppScript].[udttTemplateInput]";
    }
}
