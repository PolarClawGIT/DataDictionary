namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>  
    /// Static class containing constants related to the Authorization Securable database operations.  
    /// </summary>  
    static class AuthorizationSecurable
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(AuthorizationSecurable));

        /// <summary>  
        /// The stored procedure name for retrieving authorization securable data.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetAuthorizationSecurable]");
    }
}
