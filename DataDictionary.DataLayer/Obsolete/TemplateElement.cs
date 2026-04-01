namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>  
    /// Static class containing constants related to the Scripting Template Element database operations.  
    /// </summary>  
    [Obsolete]
    static class TemplateElement
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(TemplateElement));

        /// <summary>  
        /// The stored procedure used to retrieve scripting Template Element.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The stored procedure used to set scripting Template Element.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The user-defined table type for scripting Template Element.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}
