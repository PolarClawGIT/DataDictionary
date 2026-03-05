namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>  
    /// Static class containing constants related to the Scripting Node database operations.  
    /// </summary>  
    [Obsolete("replace", true)]
    static class ScriptingNode
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(ScriptingNode));

        /// <summary>  
        /// The stored procedure used to retrieve scripting node data.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetScriptingNode]");

        /// <summary>  
        /// The stored procedure used to set scripting node data.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetScriptingNode]");

        /// <summary>  
        /// The user-defined table type for scripting node data.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttScriptingNode]");
    }
}