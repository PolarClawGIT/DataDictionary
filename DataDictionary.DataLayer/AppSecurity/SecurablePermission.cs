namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>  
    /// Static class containing constants related to the Securable Permission database operations.  
    /// </summary>  
    static class SecurablePermission
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(SecurablePermission));

        /// <summary>  
        /// The stored procedure used to retrieve securable permissions.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetSecurablePermission]");

        /// <summary>  
        /// The stored procedure used to set securable permissions.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetSecurablePermission]");

        /// <summary>  
        /// The table type used for securable permissions.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttSecurablePermission]");
    }
}