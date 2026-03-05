namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>  
    /// Static class containing constants related to the Process Arguments database operations.  
    /// </summary>  
    static class ProcessArgument
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(ProcessArgument));

        /// <summary>  
        /// The stored procedure used to retrieve process arguments.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetProcessArgument]");

        /// <summary>  
        /// The stored procedure used to set process arguments.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetProcessArgument]");

        /// <summary>  
        /// The user-defined table type for process arguments.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttProcessArgument]");
    }
}