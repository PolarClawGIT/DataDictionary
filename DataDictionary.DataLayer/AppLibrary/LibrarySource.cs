namespace DataDictionary.DataLayer.AppLibrary
{
    /// <summary>  
    /// Static class containing constants related to the Library Source database operations.
    /// </summary>  
    static class LibrarySource
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(LibrarySource));

        /// <summary>  
        /// The name of the stored procedure used to retrieve library source data.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetLibrarySource]");

        /// <summary>  
        /// The parameter name for the library ID used in stored procedures.  
        /// </summary>  
        public readonly static String LibraryId = "@LibraryId";

        /// <summary>  
        /// The name of the stored procedure used to set library source data.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetLibrarySource]");

        /// <summary>  
        /// The name of the user-defined table type for library source data.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttLibrarySource]");
    }
}