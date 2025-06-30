namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>  
    /// Static class containing constants related to the Attribute Property database operations.  
    /// </summary>  
    static class AttributeProperty
    {
        /// <summary>  
        /// The stored procedure used to retrieve attribute properties.  
        /// </summary>  
        public const String GetProcedure = "[AppModel].[procGetAttributeProperty]";

        /// <summary>  
        /// The stored procedure used to set attribute properties.  
        /// </summary>  
        public const String SetProcedure = "[AppModel].[procSetAttributeProperty]";

        /// <summary>  
        /// The table type used for attribute properties.  
        /// </summary>  
        public const String TableType = "[AppModel].[udttAttributeProperty]";
    }
}