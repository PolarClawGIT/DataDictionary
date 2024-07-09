using System.Diagnostics.CodeAnalysis;
namespace DataDictionary.Resource.Enumerations;

/// <summary>
/// Enumeration support class for Database Table type.
/// </summary>
public class DbTableEnumeration : Enumeration<DbTableType, DbTableEnumeration>
{
    /// <summary>
    /// Internal Constructor for Database Table Enumeration
    /// </summary>
    /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
    DbTableEnumeration(DbTableType value, String name) : base(value, name) { }

    /// <summary>
    /// Static constructor, loads data.
    /// </summary>
    static DbTableEnumeration()
    {
        List<DbTableEnumeration> data = new List<DbTableEnumeration>()
        {
            new DbTableEnumeration(DbTableType.Null, String.Empty) { DisplayName = "not defined" },
            new DbTableEnumeration(DbTableType.Table,         "Table"),
            new DbTableEnumeration(DbTableType.TemporalTable, "Temporal Table"),
            new DbTableEnumeration(DbTableType.HistoryTable,  "History Table"),
            new DbTableEnumeration(DbTableType.View,          "View"),
        };

        BuildDictionary(data);
    }
}
