namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>  
    /// Static class containing constants related to the Scripting Template database operations.  
    /// </summary>  
    [Obsolete("replace", true)]
    static class ScriptingTemplate
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(ScriptingTemplate));

        /// <summary>  
        /// The stored procedure used to retrieve scripting templates.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetScriptingTemplate]");

        /// <summary>  
        /// The stored procedure used to set scripting templates.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetScriptingTemplate]");

        /// <summary>  
        /// The table type used for scripting templates.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttScriptingTemplate]");

        /// <summary>  
        /// The parameter name for the template ID.  
        /// </summary>  
        public readonly static String TemplateId = "@TemplateId";
    }
}