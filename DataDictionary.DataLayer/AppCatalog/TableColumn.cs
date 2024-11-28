// Ignore Spelling: Nullable
namespace DataDictionary.DataLayer.AppCatalog;

/// <summary>
/// Base Catalog TableColumn interface (data elements only)
/// </summary>
public interface ITableColumn: ITableColumnKeyName, IDataType, IDomainKeyReference
{
    /// <summary>
    /// Type of Table (Table, Temporal Table, Historic Table, View)
    /// </summary>
    String? TableType { get; }

    /// <summary>
    /// The Position/Order of the Column
    /// </summary>
    Nullable<Int32> OrdinalPosition { get; }

    /// <summary>
    /// Is the Column Nullable
    /// </summary>
    Boolean? IsNullable { get; }

    /// <summary>
    /// Column Default value
    /// </summary>
    String? ColumnDefault { get; }

    /// <summary>
    /// Is the Column an Identity
    /// </summary>
    Boolean? IsIdentity { get; }

    /// <summary>
    /// Is the Column Hidden
    /// </summary>
    Boolean? IsHidden { get; }

    /// <summary>
    /// Is the Column Computed
    /// </summary>
    Boolean? IsComputed { get; }

    /// <summary>
    /// If the Column is Computed, the Computed Definition
    /// </summary>
    String? ComputedDefinition { get; }

    /// <summary>
    /// Type of Always Generated Column. Used by System Version.
    /// </summary>
    String? GeneratedAlwayType { get; }
}