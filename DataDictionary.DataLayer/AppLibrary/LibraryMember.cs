namespace DataDictionary.DataLayer.AppLibrary
{
    /// <summary>  
    /// Static class containing constants related to the LibraryMember database operations.
    /// </summary>  
    static class LibraryMember
    {
        /// <summary>  
        /// The stored procedure for retrieving library member information.  
        /// </summary>  
        public const string GetProcedure = "[AppLibrary].[procGetLibraryMember]";

        /// <summary>  
        /// The stored procedure for setting library member information.  
        /// </summary>  
        public const string SetProcedure = "[AppLibrary].[procSetLibraryMember]";

        /// <summary>  
        /// The user-defined table type for library members.  
        /// </summary>  
        public const string TableType = "[AppLibrary].[udttLibraryMember]";
    }
}