namespace DataDictionary.DataLayer.AppCatalog;

/// <summary>
/// Base Catalog Table interface (data elements only)
/// </summary>
public interface ITable : ITableKeyName, ITableType
{

}

/// <summary>
/// Common Catalog Table Type
/// </summary>
public interface ITableType
{
    /// <summary>
    /// Type of Table (Table, Temporal Table, Historic Table, View)
    /// </summary>
    String? TableType { get; }
}