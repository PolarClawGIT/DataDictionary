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
    DbTableEnumeration() : base() { }

    /// <summary>
    /// Internal list that contains all the values.
    /// </summary>
    static readonly new List<DbTableEnumeration> Data = new List<DbTableEnumeration>()
    {
        new DbTableEnumeration() { Value = DbTableType.Null, Name = String.Empty, DisplayName = "not defined" },
        new DbTableEnumeration() { Value = DbTableType.Table, Name = "Table", DisplayName = "Table" },
        new DbTableEnumeration() { Value = DbTableType.TemporalTable, Name = "Temporal Table", DisplayName = "Temporal Table" },
        new DbTableEnumeration() { Value = DbTableType.HistoryTable, Name = "History Table", DisplayName = "History Table" },
        new DbTableEnumeration() { Value = DbTableType.View, Name = "View", DisplayName = "View" },
    };

}
