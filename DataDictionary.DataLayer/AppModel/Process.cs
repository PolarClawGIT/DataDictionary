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
        /// <summary>  
        /// The stored procedure used to retrieve process information.  
        /// </summary>  
        public const String GetProcedure = "[AppModel].[procGetProcess]";

        /// <summary>  
        /// The parameter name for the Process ID.  
        /// </summary>  
        public const String ProcessId = "@ProcessId";

        /// <summary>  
        /// The stored procedure used to set process information.  
        /// </summary>  
        public const String SetProcedure = "[AppModel].[procSetProcess]";

        /// <summary>  
        /// The user-defined table type for process operations.  
        /// </summary>  
        public const String TableType = "[AppModel].[udttProcess]";
    }
}