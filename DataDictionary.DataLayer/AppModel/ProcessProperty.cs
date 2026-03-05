namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>  
    /// Static class containing constants related to the Process Properties database operations.
    /// </summary>  
    static class ProcessProperty
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(ProcessProperty));

        /// <summary>  
        /// The name of the stored procedure used to retrieve process properties.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetProcessProperty]");

        /// <summary>  
        /// The name of the stored procedure used to set process properties.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetProcessProperty]");

        /// <summary>  
        /// The name of the user-defined table type for process properties.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttProcessProperty]");
    }
}