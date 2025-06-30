namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>  
    /// Static class containing constants related to the Securable Permission database operations.  
    /// </summary>  
    static class SecurablePermission
    {
        /// <summary>  
        /// The stored procedure used to retrieve securable permissions.  
        /// </summary>  
        public const String GetProcedure = "[AppSecurity].[procGetSecurablePermission]";

        /// <summary>  
        /// The stored procedure used to set securable permissions.  
        /// </summary>  
        public const String SetProcedure = "[AppSecurity].[procSetSecurablePermission]";

        /// <summary>  
        /// The table type used for securable permissions.  
        /// </summary>  
        public const String TableType = "[AppSecurity].[udttSecurablePermission]";
    }
}