namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>  
    /// Static class containing constants related to the Entity Alias database operations.  
    /// </summary>  
    static class EntityAlias
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(EntityAlias));

        /// <summary>  
        /// The stored procedure used to retrieve entity alias information.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The stored procedure used to set entity alias information.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The table type used for entity alias operations.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}