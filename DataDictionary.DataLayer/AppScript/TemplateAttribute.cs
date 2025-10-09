namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>  
    /// Static class containing constants related to the Scripting Template Attribute database operations.  
    /// </summary>  
    [Obsolete]
    static class TemplateAttribute
    {
        /// <summary>  
        /// The stored procedure used to retrieve scripting Template Attribute.  
        /// </summary>  
        public const String GetProcedure = "[AppScript].[procGetTemplateAttribute]";

        /// <summary>  
        /// The stored procedure used to set scripting Template Attribute.  
        /// </summary>  
        public const String SetProcedure = "[AppScript].[procSetTemplateAttribute]";

        /// <summary>  
        /// The user-defined table type for scripting Template Attribute.  
        /// </summary>  
        public const String TableType = "[AppScript].[udttTemplateAttribute]";
    }
}
