namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>  
    /// Static class containing constants related to the Process Arguments database operations.  
    /// </summary>  
    static class ProcessArgument
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(ProcessArgument));

        /// <summary>  
        /// The stored procedure used to retrieve process arguments.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The stored procedure used to set process arguments.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The user-defined table type for process arguments.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}