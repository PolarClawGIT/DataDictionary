// Place holder for file nesting purposes.
namespace DataDictionary.Resource.Enumerations;

/// <summary>
/// Interface for DbTableType
/// </summary>
public interface IDbTableType
{
    /// <summary>
    /// Type of Table (Table, Temporal Table, Historic Table, View)
    /// </summary>
    DbTableType TableType { get; }
}
