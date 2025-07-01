namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>  
    /// Static class containing constants related to the Securable Owner database operations.  
    /// </summary>  
    static class SecurableOwner
    {
        /// <summary>  
        /// The stored procedure used to retrieve securable owner information.  
        /// </summary>  
        public const String GetProcedure = "[AppSecurity].[procGetSecurableOwner]";

        /// <summary>  
        /// The stored procedure used to set securable owner information.  
        /// </summary>  
        public const String SetProcedure = "[AppSecurity].[procSetSecurableOwner]";

        /// <summary>  
        /// The user-defined table type for securable owner operations.  
        /// </summary>  
        public const String TableType = "[AppSecurity].[udttSecurableOwner]";
    }
}
