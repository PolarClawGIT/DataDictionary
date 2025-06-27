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

    static class Domain
    {
        public const String DomainId = "@DomainId";
        public const String GetProcedure = "[AppCatalog].[procGetDomain]";
        public const String SetProcedure = "[AppCatalog].[procSetDomain]";
        public const String TableType = "[AppCatalog].[udttDomain]";
    }
}