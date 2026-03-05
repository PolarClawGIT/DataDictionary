namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>  
    /// Static class containing constants related to the Securable Owner database operations.  
    /// </summary>  
    static class SecurableOwner
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(SecurableOwner));

        /// <summary>  
        /// The stored procedure used to retrieve securable owner information.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetSecurableOwner]");

        /// <summary>  
        /// The stored procedure used to set securable owner information.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetSecurableOwner]");

        /// <summary>  
        /// The user-defined table type for securable owner operations.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttSecurableOwner]");
    }
}
