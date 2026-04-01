namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>  
    /// Static class containing constants related to the Securable Owner database operations.  
    /// </summary>  
    static class SecurableOwner
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(SecurableOwner));

        /// <summary>  
        /// The stored procedure used to retrieve securable owner information.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The stored procedure used to set securable owner information.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The user-defined table type for securable owner operations.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}
