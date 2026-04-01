namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>  
    /// Static class containing constants related to the Entity Definition database operations.  
    /// </summary>  
    static class EntityDefinition
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(EntityDefinition));

        /// <summary>  
        /// The stored procedure used to retrieve entity definitions.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The stored procedure used to set entity definitions.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The table type used for entity definitions.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}