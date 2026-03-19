// Ignore Spelling: Securable

namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>  
    /// Static class containing constants related to the Securable database operations.  
    /// </summary>  
    static class Securable
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(Securable));

        /// <summary>  
        /// The stored procedure name for retrieving securable information.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The parameter name for the securable ID.  
        /// </summary>  
        public readonly static String Identifier = dataObject.Identifier;
    }
}
