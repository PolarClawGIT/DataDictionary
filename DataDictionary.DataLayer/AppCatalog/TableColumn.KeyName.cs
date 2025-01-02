using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppCatalog;

/// <summary>
/// Interface for the Database Table Column Key
/// </summary>
public interface ITableColumnKeyName : IKey, ITableKeyName
{
    /// <summary>
    /// Name of the Database Column
    /// </summary>
    String? ColumnName { get; }
}

/// <summary>
/// Implementation of the Database Table Column Key
/// </summary>
public class TableColumnKeyName : TableKeyName, ITableColumnKeyName,
    IKeyComparable<ITableColumnKeyName>, IKeyComparable<TableColumnKeyName>
{
    /// <inheritdoc/>
    public String ColumnName { get; init; } = string.Empty;

    /// <summary>
    /// Constructor for a blank Database Column Key
    /// </summary>
    protected internal TableColumnKeyName() : base() { }

    /// <summary>
    /// Constructor for the Database Column Key
    /// </summary>
    /// <param name="source"></param>
    public TableColumnKeyName(ITableColumnKeyName source) : base(source)
    { if (source.ColumnName is string) { ColumnName = source.ColumnName; } }

    #region IEquatable, IComparable
    /// <inheritdoc/>
    public Boolean Equals(TableColumnKeyName? other)
    {
        return
            other is ISchemaKeyName &&
            new TableKeyName(this).Equals(other) &&
            !String.IsNullOrEmpty(ColumnName) &&
            !String.IsNullOrEmpty(other.ColumnName) &&
            ColumnName.Equals(other.ColumnName, KeyExtension.CompareString);
    }

    /// <inheritdoc/>
    public Boolean Equals(ITableColumnKeyName? other)
    { return other is ITableColumnKeyName value && Equals(new TableColumnKeyName(value)); }

    /// <inheritdoc/>
    public override Boolean Equals(object? obj)
    { return obj is ITableColumnKeyName value && Equals(new TableColumnKeyName(value)); }

    /// <inheritdoc/>
    public Int32 CompareTo(TableColumnKeyName? other)
    {
        if (other is null) { return 1; }
        else if (new TableKeyName(this).CompareTo(other) is int value && value != 0) { return value; }
        else { return string.Compare(ColumnName, other.ColumnName, true); }
    }

    /// <inheritdoc/>
    public Int32 CompareTo(ITableColumnKeyName? other)
    { if (other is ITableColumnKeyName value) { return CompareTo(new TableColumnKeyName(value)); } else { return 1; } }

    /// <inheritdoc/>
    public override Int32 CompareTo(object? obj)
    { if (obj is ITableColumnKeyName value) { return CompareTo(new TableColumnKeyName(value)); } else { return 1; } }

    /// <inheritdoc/>
    public static Boolean operator ==(TableColumnKeyName left, TableColumnKeyName right)
    { return left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator !=(TableColumnKeyName left, TableColumnKeyName right)
    { return !left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator <(TableColumnKeyName left, TableColumnKeyName right)
    { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

    /// <inheritdoc/>
    public static Boolean operator <=(TableColumnKeyName left, TableColumnKeyName right)
    { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

    /// <inheritdoc/>
    public static Boolean operator >(TableColumnKeyName left, TableColumnKeyName right)
    { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

    /// <inheritdoc/>
    public static bool operator >=(TableColumnKeyName left, TableColumnKeyName right)
    { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

    /// <inheritdoc/>
    public override Int32 GetHashCode()
    { return HashCode.Combine(base.GetHashCode(), ColumnName.GetHashCode(KeyExtension.CompareString)); }
    #endregion

    /// <inheritdoc/>
    public override String ToString()
    { return DbObjectName.Format(DatabaseName, SchemaName, TableName, ColumnName); }
}
