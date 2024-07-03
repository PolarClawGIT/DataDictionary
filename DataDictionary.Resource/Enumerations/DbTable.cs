// Place holder for file nesting purposes.
namespace DataDictionary.Resource.Enumerations;

/// <summary>
/// Interface for DbRoutineType
/// </summary>
public interface IDbTableType
{
    /// <summary>
    /// Type of Table (Table, Temporal Table, Historic Table, View)
    /// </summary>
    DbTableType TableType { get; }
}

public static class DbTableExtension
{
    /// <summary>
    /// Gets the Data from DomainPropertyEnumeration
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static DbTableEnumeration Data(this DbTableType source)
    { return DbTableEnumeration.AsDictionary[source]; }
}