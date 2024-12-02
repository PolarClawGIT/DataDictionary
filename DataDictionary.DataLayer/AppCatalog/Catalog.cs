namespace DataDictionary.DataLayer.AppCatalog;

/// <summary>
/// Base Catalog Interface (data elements only)
/// </summary>
public interface ICatalog : ICatalogKeyName
{
    /// <summary>
    /// The SQL Server that the database was extracted from.
    /// </summary>
    String? ServerName { get; }
}