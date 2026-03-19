namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>  
    /// Static class containing constants related to the Securable Permission database operations.  
    /// </summary>  
    static class SecurablePermission
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(SecurablePermission));

        /// <summary>  
        /// The stored procedure used to retrieve securable permissions.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The stored procedure used to set securable permissions.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The table type used for securable permissions.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}