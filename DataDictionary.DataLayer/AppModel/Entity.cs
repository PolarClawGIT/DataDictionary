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
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(Entity));

        /// <summary>  
        /// Represents the parameter name for the Entity ID in database operations.  
        /// </summary>  
        public readonly static String Identifier = dataObject.Identifier;

        /// <summary>  
        /// Represents the stored procedure name for retrieving an Entity.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// Represents the stored procedure name for setting an Entity.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// Represents the table type name for Entity operations.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}
