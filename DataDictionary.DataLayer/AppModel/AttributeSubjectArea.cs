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

    /// <summary>  
    /// Static class containing constants related to the Attribute Subject Area database operations.  
    /// </summary>  
    static class AttributeSubjectArea
    {
        /// <summary>  
        /// The stored procedure used to retrieve Attribute Subject Area data.  
        /// </summary>  
        public const String GetProcedure = "[AppModel].[procGetAttributeSubjectArea]";

        /// <summary>  
        /// The stored procedure used to set Attribute Subject Area data.  
        /// </summary>  
        public const String SetProcedure = "[AppModel].[procSetAttributeSubjectArea]";

        /// <summary>  
        /// The table type used for Attribute Subject Area data operations.  
        /// </summary>  
        public const String TableType = "[AppModel].[udttAttributeSubjectArea]";
    }
}