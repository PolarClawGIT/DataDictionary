namespace DataDictionary.DataLayer.AppCatalog;

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