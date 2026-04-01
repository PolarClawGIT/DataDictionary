namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>  
    /// Static class containing constants related to the Process Definitions database operations.
    /// </summary>  
    static class ProcessDefinition
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(ProcessDefinition));

        /// <summary>  
        /// The stored procedure for retrieving process definitions.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The stored procedure for setting process definitions.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The user-defined table type for process definitions.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }

}