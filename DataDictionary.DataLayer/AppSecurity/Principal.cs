namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>  
    /// Static class containing constants related to the Principal database operations.  
    /// </summary>  
    static class Principal
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(Principal));

        /// <summary>  
        /// The stored procedure used to retrieve Principal information.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetPrincipal]");

        /// <summary>  
        /// Parameter indicating whether the Principal is current.  
        /// </summary>  
        public readonly static String IsCurrent = "@IsCurrent";

        /// <summary>  
        /// Parameter representing the Principal's unique identifier.  
        /// </summary>  
        public readonly static String PrincipalId = "@PrincipalId";

        /// <summary>  
        /// Parameter representing the Principal's login name.  
        /// </summary>  
        public readonly static String PrincipalLogin = "@PrincipalLogin";

        /// <summary>  
        /// The stored procedure used to set Principal information.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetPrincipal]");

        /// <summary>  
        /// The table type used for Principal operations.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttPrincipal]");
    }
}
