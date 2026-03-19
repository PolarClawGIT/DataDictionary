namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>  
    /// Static class containing constants related to the Scripting Template Attribute database operations.  
    /// </summary>  
    [Obsolete]
    static class TemplateAttribute
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(TemplateAttribute));

        /// <summary>  
        /// The stored procedure used to retrieve scripting Template Attribute.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The stored procedure used to set scripting Template Attribute.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The user-defined table type for scripting Template Attribute.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}
