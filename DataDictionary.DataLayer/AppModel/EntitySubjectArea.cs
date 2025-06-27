namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Entity Name within the scope of a Subject Area
    /// </summary>
    public interface IEntitySubjectAreaName
    {
        /// <summary>
        /// Entity Name within the Subject Area.
        /// </summary>
        String? EntityName { get; set; }
    }

    static class EntitySubjectArea
    {
        public const String GetProcedure = "[AppModel].[procGetEntitySubjectArea]";
        public const String SetProcedure = "[AppModel].[procSetEntitySubjectArea]";
        public const String TableType = "[AppModel].[udttEntitySubjectArea]";
    }
}