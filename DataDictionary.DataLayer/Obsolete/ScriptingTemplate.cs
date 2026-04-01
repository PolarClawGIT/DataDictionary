namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>  
    /// Static class containing constants related to the Scripting Template database operations.  
    /// </summary>  
    [Obsolete("replace", true)]
    static class ScriptingTemplate
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(ScriptingTemplate));

        /// <summary>  
        /// The stored procedure used to retrieve scripting templates.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The stored procedure used to set scripting templates.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The table type used for scripting templates.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}