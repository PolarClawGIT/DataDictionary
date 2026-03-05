namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>  
    /// Static class containing constants related to the Entity Attribute database operations.  
    /// </summary>  
    static class EntityAttribute
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(EntityAttribute));

        /// <summary>  
        /// The stored procedure used to retrieve entity attributes.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetEntityAttribute]");

        /// <summary>  
        /// The stored procedure used to set entity attributes.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetEntityAttribute]");

        /// <summary>  
        /// The table type used for entity attributes.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttEntityAttribute]");
    }
}