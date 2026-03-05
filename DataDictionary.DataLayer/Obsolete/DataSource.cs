namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>
    /// Interface for the Scripting Data Source
    /// </summary>
    [Obsolete]
    public interface IDataSource: IDataSourceKeyName
    {
        /// <summary>
        /// Description of the Scripting Data Source
        /// </summary>
        String? DataSourceDescription { get; set; }
    }

    /// <summary>  
    /// Static class containing constants related to the Scripting Data Source database operations.  
    /// </summary>  
    [Obsolete]
    static class DataSource
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(DataSource));

        /// <summary>  
        /// The parameter name for the Scripting Data Source ID.  
        /// </summary>  
        public readonly static String DataSourceId = "@DataSourceId";

        /// <summary>  
        /// The stored procedure used to retrieve scripting Data Source.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetDataSource]");

        /// <summary>  
        /// The stored procedure used to set scripting Data Source.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetDataSource]");

        /// <summary>  
        /// The user-defined table type for scripting Data Source.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttDataSource]");
    }
}
