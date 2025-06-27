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

    static class ProcessSubjectArea
    {
        public const String GetProcedure = "[AppModel].[procGetProcessSubjectArea]";
        public const String SetProcedure = "[AppModel].[procSetProcessSubjectArea]";
        public const String TableType = "[AppModel].[udttProcessSubjectArea]";
    }
}
