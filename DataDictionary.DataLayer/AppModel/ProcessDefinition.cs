namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>  
    /// Static class containing constants related to the Process Definitions database operations.
    /// </summary>  
    static class ProcessDefinition
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(ProcessDefinition));

        /// <summary>  
        /// The stored procedure for retrieving process definitions.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetProcessDefinition]");

        /// <summary>  
        /// The stored procedure for setting process definitions.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetProcessDefinition]");

        /// <summary>  
        /// The user-defined table type for process definitions.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttProcessDefinition]");
    }

}