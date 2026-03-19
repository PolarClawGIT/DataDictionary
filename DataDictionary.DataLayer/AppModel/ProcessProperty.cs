namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>  
    /// Static class containing constants related to the Process Properties database operations.
    /// </summary>  
    static class ProcessProperty
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(ProcessProperty));

        /// <summary>  
        /// The name of the stored procedure used to retrieve process properties.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The name of the stored procedure used to set process properties.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The name of the user-defined table type for process properties.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}