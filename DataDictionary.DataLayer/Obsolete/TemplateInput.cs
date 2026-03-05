namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>  
    /// Static class containing constants related to the Scripting Template Input Data Source database operations.  
    /// </summary>  
    [Obsolete]
    static class TemplateInput
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(TemplateInput));

        /// <summary>  
        /// The stored procedure used to retrieve scripting Template Input Data Source .  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetTemplateInput]");

        /// <summary>  
        /// The stored procedure used to set scripting Template Input Data Source .  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetTemplateInput]");

        /// <summary>  
        /// The user-defined table type for scripting Template Input Data Source .  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttTemplateInput]");
    }
}
