namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>  
    /// Static class containing constants related to the Scripting Path database operations.  
    /// </summary>  
    [Obsolete("replace", true)]
    static class ScriptingPath
    {
        /// <summary>  
        /// The stored procedure used to retrieve the scripting path.  
        /// </summary>  
        public const String GetProcedure = "[AppScript].[procGetScriptingPath]";

        /// <summary>  
        /// The stored procedure used to set the scripting path.  
        /// </summary>  
        public const String SetProcedure = "[AppScript].[procSetScriptingPath]";

        /// <summary>  
        /// The user-defined table type for scripting path operations.  
        /// </summary>  
        public const String TableType = "[AppScript].[udttScriptingPath]";
    }
}