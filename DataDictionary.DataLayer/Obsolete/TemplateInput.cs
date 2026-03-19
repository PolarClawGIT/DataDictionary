namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>  
    /// Static class containing constants related to the Scripting Template Input Data Source database operations.  
    /// </summary>  
    [Obsolete]
    static class TemplateInput
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(TemplateInput));

        /// <summary>  
        /// The stored procedure used to retrieve scripting Template Input Data Source .  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The stored procedure used to set scripting Template Input Data Source .  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The user-defined table type for scripting Template Input Data Source .  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}
