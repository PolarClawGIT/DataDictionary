namespace DataDictionary.DataLayer.AppLibrary
{
    /// <summary>  
    /// Static class containing constants related to the LibraryMember database operations.
    /// </summary>  
    static class LibraryMember
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(LibraryMember));

        /// <summary>  
        /// The stored procedure for retrieving library member information.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetLibraryMember]");

        /// <summary>  
        /// The stored procedure for setting library member information.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetLibraryMember]");

        /// <summary>  
        /// The user-defined table type for library members.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttLibraryMember]");
    }
}