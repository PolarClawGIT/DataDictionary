namespace DataDictionary.DataLayer.AppLibrary
{
    /// <summary>  
    /// Static class containing constants related to the Library Source database operations.
    /// </summary>  
    static class LibrarySource
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(LibrarySource)) { Identifier = "@LibraryId" };

        /// <summary>  
        /// The name of the stored procedure used to retrieve library source data.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The parameter name for the library ID used in stored procedures.  
        /// </summary>  
        public readonly static String Identifier = dataObject.Identifier;

        /// <summary>  
        /// The name of the stored procedure used to set library source data.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The name of the user-defined table type for library source data.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}