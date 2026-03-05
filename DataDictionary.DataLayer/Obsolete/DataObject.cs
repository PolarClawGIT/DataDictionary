namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>  
    /// Static class containing constants related to the Scripting Data Ojbect database operations.  
    /// </summary>  
    static class DataObject
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(DataObject));

        /// <summary>  
        /// The stored procedure used to retrieve scripting Data Object.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetDataObject]");

        /// <summary>  
        /// The stored procedure used to set scripting Data Object.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetDataObject]");

        /// <summary>  
        /// The user-defined table type for scripting Data Object.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttDataObject]");
    }
}
