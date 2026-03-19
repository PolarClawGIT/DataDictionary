namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>  
    /// Static class containing constants related to the Scripting Node database operations.  
    /// </summary>  
    [Obsolete("replace", true)]
    static class ScriptingNode
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(ScriptingNode));

        /// <summary>  
        /// The stored procedure used to retrieve scripting node data.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The stored procedure used to set scripting node data.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The user-defined table type for scripting node data.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}