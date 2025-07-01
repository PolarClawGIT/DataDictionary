namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>  
    /// Static class containing constants related to the Scripting Attribute database operations.  
    /// </summary>  
    static class ScriptingAttribute
    {
        /// <summary>  
        /// The stored procedure used to retrieve scripting attributes.  
        /// </summary>  
        public const String GetProcedure = "[AppScript].[procGetScriptingAttribute]";

        /// <summary>  
        /// The stored procedure used to set scripting attributes.  
        /// </summary>  
        public const String SetProcedure = "[AppScript].[procSetScriptingAttribute]";

        /// <summary>  
        /// The user-defined table type for scripting attributes.  
        /// </summary>  
        public const String TableType = "[AppScript].[udttScriptingAttribute]";
    }
}