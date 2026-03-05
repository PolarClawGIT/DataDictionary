namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>  
    /// Static class containing constants related to the RoleMembership database operations.  
    /// </summary>  
    static class RoleMembership
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(RoleMembership));

        /// <summary>  
        /// The stored procedure used to retrieve role membership information.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetRoleMembership]");

        /// <summary>  
        /// The stored procedure used to set role membership information.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetRoleMembership]");

        /// <summary>  
        /// The table type used for role membership operations.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttRoleMembership]");
    }
}
