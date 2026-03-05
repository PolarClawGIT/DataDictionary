namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>  
    /// Static class containing constants related to the Scripting Template Element database operations.  
    /// </summary>  
    [Obsolete]
    static class TemplateElement
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(TemplateElement));

        /// <summary>  
        /// The stored procedure used to retrieve scripting Template Element.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetTemplateElement]");

        /// <summary>  
        /// The stored procedure used to set scripting Template Element.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetTemplateElement]");

        /// <summary>  
        /// The user-defined table type for scripting Template Element.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttTemplateElement]");
    }
}
