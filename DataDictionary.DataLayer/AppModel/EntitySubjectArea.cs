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
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(EntitySubjectArea));

        /// <summary>  
        /// The stored procedure used to retrieve the Entity Subject Area.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The stored procedure used to set the Entity Subject Area.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The table type used for Entity Subject Area operations.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}