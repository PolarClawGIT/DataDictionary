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
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(Process));

        /// <summary>  
        /// The stored procedure used to retrieve process information.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetProcess]");

        /// <summary>  
        /// The parameter name for the Process ID.  
        /// </summary>  
        public readonly static String ProcessId = "@ProcessId";

        /// <summary>  
        /// The stored procedure used to set process information.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetProcess]");

        /// <summary>  
        /// The user-defined table type for process operations.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttProcess]");
    }
}