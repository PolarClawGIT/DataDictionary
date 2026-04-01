namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>  
    /// Static class containing constants related to the Process Aliases database operations.  
    /// </summary>  
    static class ProcessAlias
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(ProcessAlias));

        /// <summary>  
        /// The stored procedure used to retrieve process aliases.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The stored procedure used to set process aliases.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The user-defined table type for process aliases.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}