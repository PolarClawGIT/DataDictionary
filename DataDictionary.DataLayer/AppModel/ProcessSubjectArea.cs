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
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(ProcessSubjectArea));

        /// <summary>  
        /// The name of the stored procedure used to retrieve Process Subject Area data.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The name of the stored procedure used to set Process Subject Area data.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The name of the user-defined table type for Process Subject Area.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}
