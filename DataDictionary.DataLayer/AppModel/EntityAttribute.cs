namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>  
    /// Static class containing constants related to the Entity Attribute database operations.  
    /// </summary>  
    static class EntityAttribute
    {
        /// <summary>  
        /// The stored procedure used to retrieve entity attributes.  
        /// </summary>  
        public const String GetProcedure = "[AppModel].[procGetEntityAttribute]";

        /// <summary>  
        /// The stored procedure used to set entity attributes.  
        /// </summary>  
        public const String SetProcedure = "[AppModel].[procSetEntityAttribute]";

        /// <summary>  
        /// The table type used for entity attributes.  
        /// </summary>  
        public const String TableType = "[AppModel].[udttEntityAttribute]";
    }
}