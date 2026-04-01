namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>  
    /// Static class containing constants related to the Authorization database operations.  
    /// </summary>  
    static class Authorization
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(Authorization));

        /// <summary>  
        /// The stored procedure name for retrieving authorization data.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;
    }
}