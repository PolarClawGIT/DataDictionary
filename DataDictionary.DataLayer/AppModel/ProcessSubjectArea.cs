namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Process Name within the scope of a Subject Area
    /// </summary>
    public interface IProcessSubjectAreaName
    {
        /// <summary>
        /// Process Name within the Subject Area.
        /// </summary>
        String? ProcessName { get; set; }
    }

    /// <summary>  
    /// Static class containing constants related to the Process Subject Area database operations.
    /// </summary>  
    static class ProcessSubjectArea
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(ProcessSubjectArea));

        /// <summary>  
        /// The name of the stored procedure used to retrieve Process Subject Area data.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetProcessSubjectArea]");

        /// <summary>  
        /// The name of the stored procedure used to set Process Subject Area data.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetProcessSubjectArea]");

        /// <summary>  
        /// The name of the user-defined table type for Process Subject Area.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttProcessSubjectArea]");
    }
}
