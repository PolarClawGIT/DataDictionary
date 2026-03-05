namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>  
    /// Static class containing constants related to the Role database operations.  
    /// </summary>  
    static class Role
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(Role));

        /// <summary>  
        /// The stored procedure used to retrieve role information.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetRole]");

        /// <summary>  
        /// The parameter name for the Role ID in database operations.  
        /// </summary>  
        public readonly static String RoleId = "@RoleId";

        /// <summary>  
        /// The stored procedure used to set role information.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetRole]");

        /// <summary>  
        /// The user-defined table type for role-related operations.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttRole]");
    }
}
