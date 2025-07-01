namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>  
    /// Static class containing constants related to the Scripting Node database operations.  
    /// </summary>  
    static class ScriptingNode
    {
        /// <summary>  
        /// The stored procedure used to retrieve scripting node data.  
        /// </summary>  
        public const String GetProcedure = "[AppScript].[procGetScriptingNode]";

        /// <summary>  
        /// The stored procedure used to set scripting node data.  
        /// </summary>  
        public const String SetProcedure = "[AppScript].[procSetScriptingNode]";

        /// <summary>  
        /// The user-defined table type for scripting node data.  
        /// </summary>  
        public const String TableType = "[AppScript].[udttScriptingNode]";
    }
}