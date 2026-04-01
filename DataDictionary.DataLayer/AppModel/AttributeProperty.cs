namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>  
    /// Static class containing constants related to the Attribute Property database operations.  
    /// </summary>  
    static class AttributeProperty
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(AttributeProperty));

        /// <summary>  
        /// The stored procedure used to retrieve attribute properties.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The stored procedure used to set attribute properties.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The table type used for attribute properties.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}