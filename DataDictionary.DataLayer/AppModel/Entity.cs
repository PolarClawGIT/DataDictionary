namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>  
    /// Interface for the Model Entity  
    /// </summary>  
    public interface IEntity : IEntityKeyName, IEntitySubjectAreaName
    {
        /// <summary>  
        /// Description of the Domain Entity  
        /// </summary>  
        String? EntityDescription { get; set; }
    }

    /// <summary>  
    /// Static class containing constants related to the Entity database operations.  
    /// </summary>  
    static class Entity
    {
        /// <summary>  
        /// Represents the parameter name for the Entity ID in database operations.  
        /// </summary>  
        public const String EntityId = "@EntityId";

        /// <summary>  
        /// Represents the stored procedure name for retrieving an Entity.  
        /// </summary>  
        public const String GetProcedure = "[AppModel].[procGetEntity]";

        /// <summary>  
        /// Represents the stored procedure name for setting an Entity.  
        /// </summary>  
        public const String SetProcedure = "[AppModel].[procSetEntity]";

        /// <summary>  
        /// Represents the table type name for Entity operations.  
        /// </summary>  
        public const String TableType = "[AppModel].[udttEntity]";
    }
}
