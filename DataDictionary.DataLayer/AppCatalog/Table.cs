namespace DataDictionary.DataLayer.AppCatalog;

/// <summary>
/// Base Catalog Table interface (data elements only)
/// </summary>
public interface ITable : ITableKeyName
{
    /// <summary>
    /// Type of Table (Table, Temporal Table, Historic Table, View)
    /// </summary>
    String? TableType { get; }
}