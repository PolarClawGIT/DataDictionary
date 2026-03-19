namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>  
    /// Static class containing constants related to the Attribute Alias database operations.  
    /// </summary>  
    static class AttributeAlias
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(AttributeAlias));

        /// <summary>  
        /// The stored procedure used to retrieve attribute aliases.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The stored procedure used to set attribute aliases.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The table type used for attribute alias operations.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}