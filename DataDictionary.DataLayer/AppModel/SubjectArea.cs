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

    /// <summary>
    /// Static class containing constants related to the Subject Area database operations.
    /// </summary>
    static class SubjectArea
    {
        /// <summary>
        /// Stored procedure for retrieving Subject Area data.
        /// </summary>
        public const String GetProcedure = "[AppModel].[procGetSubjectArea]";

        /// <summary>
        /// Stored procedure for setting Subject Area data.
        /// </summary>
        public const String SetProcedure = "[AppModel].[procSetSubjectArea]";

        /// <summary>
        /// Parameter name for Subject Area ID.
        /// </summary>
        public const String SubjectAreaId = "@SubjectAreaId";

        /// <summary>
        /// Table type for Subject Area data.
        /// </summary>
        public const String TableType = "[AppModel].[udttSubjectArea]";
    }
}