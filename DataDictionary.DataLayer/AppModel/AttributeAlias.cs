namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>  
    /// Static class containing constants related to the Attribute Alias database operations.  
    /// </summary>  
    static class AttributeAlias
    {
        /// <summary>  
        /// The stored procedure used to retrieve attribute aliases.  
        /// </summary>  
        public const String GetProcedure = "[AppModel].[procGetAttributeAlias]";

        /// <summary>  
        /// The stored procedure used to set attribute aliases.  
        /// </summary>  
        public const String SetProcedure = "[AppModel].[procSetAttributeAlias]";

        /// <summary>  
        /// The table type used for attribute alias operations.  
        /// </summary>  
        public const String TableType = "[AppModel].[udttAttributeAlias]";
    }
}