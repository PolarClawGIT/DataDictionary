// Ignore Spelling: Securable

namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>  
    /// Static class containing constants related to the Securable database operations.  
    /// </summary>  
    static class Securable
    {
        /// <summary>  
        /// The stored procedure name for retrieving securable information.  
        /// </summary>  
        public const String GetProcedure = "[AppSecurity].[procGetSecurable]";

        /// <summary>  
        /// The parameter name for the securable ID.  
        /// </summary>  
        public const String SecurableId = "@SecurableId";
    }
}
