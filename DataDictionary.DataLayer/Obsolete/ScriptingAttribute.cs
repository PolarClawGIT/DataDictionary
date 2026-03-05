namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>  
    /// Static class containing constants related to the Scripting Attribute database operations.  
    /// </summary>  
    [Obsolete("replace",true)]
    static class ScriptingAttribute
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(ScriptingAttribute));

        /// <summary>  
        /// The stored procedure used to retrieve scripting attributes.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetScriptingAttribute]");

        /// <summary>  
        /// The stored procedure used to set scripting attributes.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetScriptingAttribute]");

        /// <summary>  
        /// The user-defined table type for scripting attributes.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttScriptingAttribute]");
    }
}