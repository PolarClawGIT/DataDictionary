namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>  
    /// Static class containing constants related to the Principal database operations.  
    /// </summary>  
    static class Principal
    {
        /// <summary>  
        /// The stored procedure used to retrieve Principal information.  
        /// </summary>  
        public const String GetProcedure = "[AppSecurity].[procGetPrincipal]";

        /// <summary>  
        /// Parameter indicating whether the Principal is current.  
        /// </summary>  
        public const String IsCurrent = "@IsCurrent";

        /// <summary>  
        /// Parameter representing the Principal's unique identifier.  
        /// </summary>  
        public const String PrincipalId = "@PrincipalId";

        /// <summary>  
        /// Parameter representing the Principal's login name.  
        /// </summary>  
        public const String PrincipalLogin = "@PrincipalLogin";

        /// <summary>  
        /// The stored procedure used to set Principal information.  
        /// </summary>  
        public const String SetProcedure = "[AppSecurity].[procSetPrincipal]";

        /// <summary>  
        /// The table type used for Principal operations.  
        /// </summary>  
        public const String TableType = "[AppSecurity].[udttPrincipal]";
    }
}
