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

    /// <summary>  
    /// Static class containing constants related to the Entity Subject Area database operations.  
    /// </summary>  
    static class EntitySubjectArea
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(EntitySubjectArea));

        /// <summary>  
        /// The stored procedure used to retrieve the Entity Subject Area.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetEntitySubjectArea]");

        /// <summary>  
        /// The stored procedure used to set the Entity Subject Area.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetEntitySubjectArea]");

        /// <summary>  
        /// The table type used for Entity Subject Area operations.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttEntitySubjectArea]");
    }
}