namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>  
    /// Static class containing constants related to the RoleMembership database operations.  
    /// </summary>  
    static class RoleMembership
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(RoleMembership));

        /// <summary>  
        /// The stored procedure used to retrieve role membership information.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The stored procedure used to set role membership information.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The table type used for role membership operations.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}
