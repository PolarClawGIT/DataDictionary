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
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(DataSource));

        /// <summary>  
        /// The parameter name for the Scripting Data Source ID.  
        /// </summary>  
        public readonly static String Identifier = dataObject.Identifier;

        /// <summary>  
        /// The stored procedure used to retrieve scripting Data Source.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The stored procedure used to set scripting Data Source.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The user-defined table type for scripting Data Source.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}
