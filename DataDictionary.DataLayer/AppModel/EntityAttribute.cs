namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>  
    /// Static class containing constants related to the Entity Attribute database operations.  
    /// </summary>  
    static class EntityAttribute
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(EntityAttribute));

        /// <summary>  
        /// The stored procedure used to retrieve entity attributes.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The stored procedure used to set entity attributes.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The table type used for entity attributes.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}