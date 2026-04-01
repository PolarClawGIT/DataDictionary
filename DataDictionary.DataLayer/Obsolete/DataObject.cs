namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>  
    /// Static class containing constants related to the Scripting Data Ojbect database operations.  
    /// </summary>  
    static class DataObject
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(DataObject));

        /// <summary>  
        /// The stored procedure used to retrieve scripting Data Object.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The stored procedure used to set scripting Data Object.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The user-defined table type for scripting Data Object.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}
