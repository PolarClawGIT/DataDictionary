namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Base Model SubjectArea Interface (data elements only)
    /// </summary>
    public interface ISubjectArea
    {
        /// <summary>
        /// Description of the Subject Area
        /// </summary>
        String? SubjectAreaDescription { get; }

        /// <summary>
        /// NameSpace used for the Subject Area
        /// </summary>
        String? SubjectName { get; }
    }

    static class SubjectArea
    {
        public const String GetProcedure = "[AppModel].[procGetSubjectArea]";
        public const String SetProcedure = "[AppModel].[procSetSubjectArea]";
        public const String SubjectAreaId = "@SubjectAreaId";
        public const String TableType = "[AppModel].[udttSubjectArea]";
    }
}