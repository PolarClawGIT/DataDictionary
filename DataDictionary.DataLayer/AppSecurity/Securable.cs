// Ignore Spelling: Securable

namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>  
    /// Static class containing constants related to the Securable database operations.  
    /// </summary>  
    static class Securable
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(Securable));

        /// <summary>  
        /// The stored procedure name for retrieving securable information.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetSecurable]");

        /// <summary>  
        /// The parameter name for the securable ID.  
        /// </summary>  
        public readonly static String SecurableId = "@SecurableId";
    }
}
