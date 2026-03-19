namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for the Model Process
    /// </summary>
    public interface IProcess : IProcessKeyName, IProcessSubjectAreaName
    {
        /// <summary>
        /// Description of the Domain Process
        /// </summary>
        String? ProcessDescription { get; set; }
    }

    /// <summary>  
    /// Static class containing constants related to the Process database operations.  
    /// </summary>  
    static class Process
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(Process));

        /// <summary>  
        /// The stored procedure used to retrieve process information.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The parameter name for the Process ID.  
        /// </summary>  
        public readonly static String Identifier = dataObject.Identifier;

        /// <summary>  
        /// The stored procedure used to set process information.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The user-defined table type for process operations.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}