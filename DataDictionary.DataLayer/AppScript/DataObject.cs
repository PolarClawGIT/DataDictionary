namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>  
    /// Static class containing constants related to the Scripting Data Ojbect database operations.  
    /// </summary>  
    static class DataObject
    {
        /// <summary>
        /// Identifier for the Scripting Data Source.
        /// </summary>
        public const String DataSourceId = "@DataSourceId";

        /// <summary>  
        /// The stored procedure used to retrieve scripting Data Object.  
        /// </summary>  
        public const String GetProcedure = "[AppScript].[procGetDataObject]";

        /// <summary>  
        /// The stored procedure used to set scripting Data Object.  
        /// </summary>  
        public const String SetProcedure = "[AppScript].[procSetDataObject]";

        /// <summary>  
        /// The user-defined table type for scripting Data Object.  
        /// </summary>  
        public const String TableType = "[AppScript].[udttDataObject]";
    }
}
