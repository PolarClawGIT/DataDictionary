namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Attribute Name within the scope of a Subject Area
    /// </summary>
    public interface IAttributeSubjectAreaName
    {
        /// <summary>
        /// Attribute Name within the Subject Area.
        /// </summary>
        String? AttributeName { get; set; }
    }

    static class AttributeSubjectArea
    {
        public const String GetProcedure = "[AppModel].[procGetAttributeSubjectArea]";
        public const String SetProcedure = "[AppModel].[procSetAttributeSubjectArea]";
        public const String TableType = "[AppModel].[udttAttributeSubjectArea]";
    }
}