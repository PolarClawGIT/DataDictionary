namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting Data Source
    /// </summary>
    public interface IDataSource
    {
        /// <summary>
        /// Title of the Scripting Data Source
        /// </summary>
        String? DataSourceTitle { get; }

        /// <summary>
        /// Description of the Scripting Data Source
        /// </summary>
        String? DataSourceDescription { get; set; }
    }

    /// <summary>  
    /// Static class containing constants related to the Scripting Data Source database operations.  
    /// </summary>  
    static class DataSource
    {
        /// <summary>  
        /// The parameter name for the Scripting Data Source ID.  
        /// </summary>  
        public const String DataSourceId = "@DataSourceId";

        /// <summary>  
        /// The stored procedure used to retrieve scripting Data Source.  
        /// </summary>  
        public const String GetProcedure = "[AppScript].[procGetDataSource]";

        /// <summary>  
        /// The stored procedure used to set scripting Data Source.  
        /// </summary>  
        public const String SetProcedure = "[AppScript].[procSetDataSource]";

        /// <summary>  
        /// The user-defined table type for scripting Data Source.  
        /// </summary>  
        public const String TableType = "[AppScript].[udttDataSource]";
    }
}
