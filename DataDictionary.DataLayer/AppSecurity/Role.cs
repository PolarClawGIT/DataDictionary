namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>  
    /// Static class containing constants related to the Role database operations.  
    /// </summary>  
    static class Role
    {
        /// <summary>  
        /// The stored procedure used to retrieve role information.  
        /// </summary>  
        public const String GetProcedure = "[AppSecurity].[procGetRole]";

        /// <summary>  
        /// The parameter name for the Role ID in database operations.  
        /// </summary>  
        public const String RoleId = "@RoleId";

        /// <summary>  
        /// The stored procedure used to set role information.  
        /// </summary>  
        public const String SetProcedure = "[AppSecurity].[procSetRole]";

        /// <summary>  
        /// The user-defined table type for role-related operations.  
        /// </summary>  
        public const String TableType = "[AppSecurity].[udttRole]";
    }
}
