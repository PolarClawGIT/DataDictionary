namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>  
    /// Static class containing constants related to the Scripting Path database operations.  
    /// </summary>  
    [Obsolete("replace", true)]
    static class ScriptingPath
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(ScriptingPath));

        /// <summary>  
        /// The stored procedure used to retrieve the scripting path.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The stored procedure used to set the scripting path.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The user-defined table type for scripting path operations.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}