namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>  
    /// Static class containing constants related to the Scripting Template database operations.  
    /// </summary>  
    static class ScriptingTemplate
    {
        /// <summary>  
        /// The stored procedure used to retrieve scripting templates.  
        /// </summary>  
        public const String GetProcedure = "[AppScript].[procGetScriptingTemplate]";

        /// <summary>  
        /// The stored procedure used to set scripting templates.  
        /// </summary>  
        public const String SetProcedure = "[AppScript].[procSetScriptingTemplate]";

        /// <summary>  
        /// The table type used for scripting templates.  
        /// </summary>  
        public const String TableType = "[AppScript].[udttScriptingTemplate]";

        /// <summary>  
        /// The parameter name for the template ID.  
        /// </summary>  
        public const String TemplateId = "@TemplateId";
    }
}