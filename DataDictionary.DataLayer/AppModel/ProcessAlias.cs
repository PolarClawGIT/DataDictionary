namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>  
    /// Static class containing constants related to the Process Aliases database operations.  
    /// </summary>  
    static class ProcessAlias
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(ProcessAlias));

        /// <summary>  
        /// The stored procedure used to retrieve process aliases.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetProcessAlias]");

        /// <summary>  
        /// The stored procedure used to set process aliases.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetProcessAlias]");

        /// <summary>  
        /// The user-defined table type for process aliases.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttProcessAlias]");
    }
}