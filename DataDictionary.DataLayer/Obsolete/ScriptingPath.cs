namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>  
    /// Static class containing constants related to the Scripting Path database operations.  
    /// </summary>  
    [Obsolete("replace", true)]
    static class ScriptingPath
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(ScriptingPath));

        /// <summary>  
        /// The stored procedure used to retrieve the scripting path.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetScriptingPath]");

        /// <summary>  
        /// The stored procedure used to set the scripting path.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetScriptingPath]");

        /// <summary>  
        /// The user-defined table type for scripting path operations.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttScriptingPath]");
    }
}