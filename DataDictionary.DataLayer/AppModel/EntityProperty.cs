namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>  
    /// Static class containing constants related to the Entity Property database operations.  
    /// </summary>  
    static class EntityProperty
    {
        /// <summary>  
        /// The stored procedure used to retrieve entity property data.  
        /// </summary>  
        public const String GetProcedure = "[AppModel].[procGetEntityProperty]";

        /// <summary>  
        /// The stored procedure used to set entity property data.  
        /// </summary>  
        public const String SetProcedure = "[AppModel].[procSetEntityProperty]";

        /// <summary>  
        /// The user-defined table type for entity property data.  
        /// </summary>  
        public const String TableType = "[AppModel].[udttEntityProperty]";
    }
}