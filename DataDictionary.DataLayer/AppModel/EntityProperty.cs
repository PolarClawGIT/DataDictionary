namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>  
    /// Static class containing constants related to the Entity Property database operations.  
    /// </summary>  
    static class EntityProperty
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(EntityProperty));

        /// <summary>  
        /// The stored procedure used to retrieve entity property data.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetEntityProperty]");

        /// <summary>  
        /// The stored procedure used to set entity property data.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetEntityProperty]");

        /// <summary>  
        /// The user-defined table type for entity property data.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttEntityProperty]");
    }
}