namespace DataDictionary.DataLayer.AppLibrary
{
    /// <summary>  
    /// Static class containing constants related to the Library Source database operations.
    /// </summary>  
    static class LibrarySource
    {
        /// <summary>  
        /// The name of the stored procedure used to retrieve library source data.  
        /// </summary>  
        public const string GetProcedure = "[AppLibrary].[procGetLibrarySource]";

        /// <summary>  
        /// The parameter name for the library ID used in stored procedures.  
        /// </summary>  
        public const string LibraryId = "@LibraryId";

        /// <summary>  
        /// The name of the stored procedure used to set library source data.  
        /// </summary>  
        public const string SetProcedure = "[AppLibrary].[procSetLibrarySource]";

        /// <summary>  
        /// The name of the user-defined table type for library source data.  
        /// </summary>  
        public const string TableType = "[AppLibrary].[udttLibrarySource]";
    }
}