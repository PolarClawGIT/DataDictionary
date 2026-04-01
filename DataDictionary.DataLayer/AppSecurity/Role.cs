namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>  
    /// Static class containing constants related to the Role database operations.  
    /// </summary>  
    static class Role
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(Role));

        /// <summary>  
        /// The stored procedure used to retrieve role information.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The parameter name for the Role ID in database operations.  
        /// </summary>  
        public readonly static String Identifier = dataObject.Identifier;

        /// <summary>  
        /// The stored procedure used to set role information.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The user-defined table type for role-related operations.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}
