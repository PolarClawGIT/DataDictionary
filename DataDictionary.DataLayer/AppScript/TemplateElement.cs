namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>  
    /// Static class containing constants related to the Scripting Template Element database operations.  
    /// </summary>  
    [Obsolete]
    static class TemplateElement
    {
        /// <summary>  
        /// The stored procedure used to retrieve scripting Template Element.  
        /// </summary>  
        public const String GetProcedure = "[AppScript].[procGetTemplateElement]";

        /// <summary>  
        /// The stored procedure used to set scripting Template Element.  
        /// </summary>  
        public const String SetProcedure = "[AppScript].[procSetTemplateElement]";

        /// <summary>  
        /// The user-defined table type for scripting Template Element.  
        /// </summary>  
        public const String TableType = "[AppScript].[udttTemplateElement]";
    }
}
