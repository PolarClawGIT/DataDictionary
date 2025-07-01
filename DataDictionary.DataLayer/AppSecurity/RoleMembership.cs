namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>  
    /// Static class containing constants related to the RoleMembership database operations.  
    /// </summary>  
    static class RoleMembership
    {
        /// <summary>  
        /// The stored procedure used to retrieve role membership information.  
        /// </summary>  
        public const String GetProcedure = "[AppSecurity].[procGetRoleMembership]";

        /// <summary>  
        /// The stored procedure used to set role membership information.  
        /// </summary>  
        public const String SetProcedure = "[AppSecurity].[procSetRoleMembership]";

        /// <summary>  
        /// The table type used for role membership operations.  
        /// </summary>  
        public const String TableType = "[AppSecurity].[udttRoleMembership]";
    }
}
