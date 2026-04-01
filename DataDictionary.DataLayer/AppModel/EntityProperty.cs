namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>  
    /// Static class containing constants related to the Entity Property database operations.  
    /// </summary>  
    static class EntityProperty
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(EntityProperty));

        /// <summary>  
        /// The stored procedure used to retrieve entity property data.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The stored procedure used to set entity property data.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The user-defined table type for entity property data.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}