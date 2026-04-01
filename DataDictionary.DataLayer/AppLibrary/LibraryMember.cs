namespace DataDictionary.DataLayer.AppLibrary
{
    /// <summary>  
    /// Static class containing constants related to the LibraryMember database operations.
    /// </summary>  
    static class LibraryMember
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(LibraryMember));

        /// <summary>  
        /// The stored procedure for retrieving library member information.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The stored procedure for setting library member information.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The user-defined table type for library members.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}