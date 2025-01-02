namespace DataDictionary.DataLayer.AppCatalog;

/// <summary>
/// Base Catalog Interface (data elements only)
/// </summary>
public interface ICatalog : ICatalogKeyName
{
    /// <summary>
    /// Title given to the Catalog. Default is the Database Name.
    /// </summary>
    String? CatalogTitle { get; }

    /// <summary>
    /// Description given to the Catalog.
    /// </summary>
    String? CatalogDescription { get; }

    /// <summary>
    /// The SQL Server that the database was extracted from.
    /// </summary>
    String? ServerName { get; }

    /// <summary>
    /// The Date that the database was extracted.
    /// </summary>
    DateTime? SourceDate { get; }
}