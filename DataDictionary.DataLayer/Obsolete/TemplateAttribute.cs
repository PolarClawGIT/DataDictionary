namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>  
    /// Static class containing constants related to the Scripting Template Attribute database operations.  
    /// </summary>  
    [Obsolete]
    static class TemplateAttribute
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(TemplateAttribute));

        /// <summary>  
        /// The stored procedure used to retrieve scripting Template Attribute.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetTemplateAttribute]");

        /// <summary>  
        /// The stored procedure used to set scripting Template Attribute.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetTemplateAttribute]");

        /// <summary>  
        /// The user-defined table type for scripting Template Attribute.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttTemplateAttribute]");
    }
}
