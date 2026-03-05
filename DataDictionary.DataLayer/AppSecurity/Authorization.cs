namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>  
    /// Static class containing constants related to the Authorization database operations.  
    /// </summary>  
    static class Authorization
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(Authorization));

        /// <summary>  
        /// The stored procedure name for retrieving authorization data.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetAuthorization]");
    }
}