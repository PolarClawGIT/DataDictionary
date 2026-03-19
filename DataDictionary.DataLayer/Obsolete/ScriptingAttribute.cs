namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>  
    /// Static class containing constants related to the Scripting Attribute database operations.  
    /// </summary>  
    [Obsolete("replace",true)]
    static class ScriptingAttribute
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(ScriptingAttribute));

        /// <summary>  
        /// The stored procedure used to retrieve scripting attributes.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The stored procedure used to set scripting attributes.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The user-defined table type for scripting attributes.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}