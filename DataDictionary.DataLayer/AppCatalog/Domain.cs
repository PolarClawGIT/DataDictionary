namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Base Catalog Domain interface (data elements only)
    /// </summary>
    public interface IDomain : IDomainKeyName, IDataType
    {
        /// <summary>
        /// The Default value for the Domain
        /// </summary>
        String? DomainDefault { get; }
    }

    /// <summary>  
    /// Static class containing constants related to the Domain database operations.
    /// </summary>  
    static class Domain
    {
        /// <summary>  
        /// Identifier for the Domain  
        /// </summary>  
        public const String DomainId = "@DomainId";

        /// <summary>  
        /// Stored procedure to retrieve Domain information  
        /// </summary>  
        public const String GetProcedure = "[AppCatalog].[procGetDomain]";

        /// <summary>  
        /// Stored procedure to set Domain information  
        /// </summary>  
        public const String SetProcedure = "[AppCatalog].[procSetDomain]";

        /// <summary>  
        /// Table type for Domain  
        /// </summary>  
        public const String TableType = "[AppCatalog].[udttDomain]";
    }
}