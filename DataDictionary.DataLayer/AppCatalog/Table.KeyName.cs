using DataDictionary.DataLayer.DatabaseData;
using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppCatalog;

/// <summary>
/// Interface for the Database Table Key
/// </summary>
public interface ITableKeyName : IKey, ISchemaKeyName
{
    /// <summary>
    /// Name of the Database Table (or View)
    /// </summary>
    String? TableName { get; }
}

/// <summary>
/// Implementation for the Database Table Key
/// </summary>
public class TableKeyName : SchemaKeyName, ITableKeyName,
    IKeyComparable<ITableKeyName>, IKeyComparable<TableKeyName>
{
    /// <inheritdoc/>
    public String TableName { get; init; } = string.Empty;

    /// <summary>
    /// Constructor for a blank Database Table Key
    /// </summary>
    protected internal TableKeyName() : base() { }

    /// <summary>
    /// Constructor for the Database Table Key
    /// </summary>
    /// <param name="source"></param>
    public TableKeyName(ITableKeyName source) : base(source)
    { if (source.TableName is string) { TableName = source.TableName; } }

    #region IEquatable, IComparable
    /// <inheritdoc/>
    public Boolean Equals(TableKeyName? other)
    {
        return
            other is ISchemaKeyName &&
            new SchemaKeyName(this).Equals(other) &&
            !string.IsNullOrEmpty(TableName) &&
            !string.IsNullOrEmpty(other.TableName) &&
            TableName.Equals(other.TableName, KeyExtension.CompareString);
    }

    /// <inheritdoc/>
    public Boolean Equals(ITableKeyName? other)
    { return other is ITableKeyName value && Equals(new TableKeyName(value)); }

    /// <inheritdoc/>
    public override Boolean Equals(object? obj)
    { return obj is ITableKeyName value && Equals(new TableKeyName(value)); }

    /// <inheritdoc/>
    public Int32 CompareTo(TableKeyName? other)
    {
        if (other is null) { return 1; }
        else if (new SchemaKeyName(this).CompareTo(other) is int value && value != 0) { return value; }
        else { return string.Compare(TableName, other.TableName, true); }
    }

    /// <inheritdoc/>
    public Int32 CompareTo(ITableKeyName? other)
    { if (other is ITableKeyName value) { return CompareTo(new TableKeyName(value)); } else { return 1; } }

    /// <inheritdoc/>
    public override Int32 CompareTo(object? obj)
    { if (obj is ITableKeyName value) { return CompareTo(new TableKeyName(value)); } else { return 1; } }

    /// <inheritdoc/>
    public static Boolean operator ==(TableKeyName left, TableKeyName right)
    { return left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator !=(TableKeyName left, TableKeyName right)
    { return !left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator <(TableKeyName left, TableKeyName right)
    { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

    /// <inheritdoc/>
    public static Boolean operator <=(TableKeyName left, TableKeyName right)
    { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

    /// <inheritdoc/>
    public static Boolean operator >(TableKeyName left, TableKeyName right)
    { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

    /// <inheritdoc/>
    public static Boolean operator >=(TableKeyName left, TableKeyName right)
    { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

    /// <inheritdoc/>
    public override Int32 GetHashCode()
    { return HashCode.Combine(base.GetHashCode(), TableName.GetHashCode(KeyExtension.CompareString)); }
    #endregion

    /// <inheritdoc/>
    public override String ToString()
    { return DbObjectName.Format(DatabaseName, SchemaName, TableName); }
}
