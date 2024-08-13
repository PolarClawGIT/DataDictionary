using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.Resource;

namespace DataDictionary.DataLayer.DatabaseData.Table;

/// <summary>
/// Interface for the Database Table Column Key
/// </summary>
public interface IDbTableColumnKeyName : IKey, IDbTableKeyName
{
    /// <summary>
    /// Name of the Database Column
    /// </summary>
    String? ColumnName { get; }
}

/// <summary>
/// Implementation of the Database Table Column Key
/// </summary>
public class DbTableColumnKeyName : DbTableKeyName, IDbTableColumnKeyName,
    IKeyComparable<IDbTableColumnKeyName>, IKeyComparable<DbTableColumnKeyName>
{
    /// <inheritdoc/>
    public string ColumnName { get; init; } = string.Empty;

    /// <summary>
    /// Constructor for a blank Database Column Key
    /// </summary>
    protected internal DbTableColumnKeyName() : base() { }

    /// <summary>
    /// Constructor for the Database Column Key
    /// </summary>
    /// <param name="source"></param>
    public DbTableColumnKeyName(IDbTableColumnKeyName source) : base(source)
    { if (source.ColumnName is string) { ColumnName = source.ColumnName; } }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    public DbTableColumnKeyName(DbConstraintColumnKeyName source)
    {
        this.DatabaseName = source.DatabaseName;
        this.SchemaName = source.ReferenceSchemaName;
        this.TableName = source.ReferenceTableName;
        this.ColumnName = source.ReferenceColumnName;
    }

    #region IEquatable, IComparable
    /// <inheritdoc/>
    public Boolean Equals(DbTableColumnKeyName? other)
    {
        return
            other is IDbSchemaKeyName &&
            new DbTableKeyName(this).Equals(other) &&
            !string.IsNullOrEmpty(ColumnName) &&
            !string.IsNullOrEmpty(other.ColumnName) &&
            ColumnName.Equals(other.ColumnName, KeyExtension.CompareString);
    }

    /// <inheritdoc/>
    public Boolean Equals(IDbTableColumnKeyName? other)
    { return other is IDbTableColumnKeyName value && Equals(new DbTableColumnKeyName(value)); }

    /// <inheritdoc/>
    public override Boolean Equals(object? obj)
    { return obj is IDbTableColumnKeyName value && Equals(new DbTableColumnKeyName(value)); }

    /// <inheritdoc/>
    public Int32 CompareTo(DbTableColumnKeyName? other)
    {
        if (other is null) { return 1; }
        else if (new DbTableKeyName(this).CompareTo(other) is int value && value != 0) { return value; }
        else { return string.Compare(ColumnName, other.ColumnName, true); }
    }

    /// <inheritdoc/>
    public Int32 CompareTo(IDbTableColumnKeyName? other)
    { if (other is IDbTableColumnKeyName value) { return CompareTo(new DbTableColumnKeyName(value)); } else { return 1; } }

    /// <inheritdoc/>
    public override Int32 CompareTo(object? obj)
    { if (obj is IDbTableColumnKeyName value) { return CompareTo(new DbTableColumnKeyName(value)); } else { return 1; } }

    /// <inheritdoc/>
    public static Boolean operator ==(DbTableColumnKeyName left, DbTableColumnKeyName right)
    { return left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator !=(DbTableColumnKeyName left, DbTableColumnKeyName right)
    { return !left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator <(DbTableColumnKeyName left, DbTableColumnKeyName right)
    { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

    /// <inheritdoc/>
    public static Boolean operator <=(DbTableColumnKeyName left, DbTableColumnKeyName right)
    { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

    /// <inheritdoc/>
    public static Boolean operator >(DbTableColumnKeyName left, DbTableColumnKeyName right)
    { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

    /// <inheritdoc/>
    public static bool operator >=(DbTableColumnKeyName left, DbTableColumnKeyName right)
    { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

    /// <inheritdoc/>
    public override Int32 GetHashCode()
    { return HashCode.Combine(base.GetHashCode(), ColumnName.GetHashCode(KeyExtension.CompareString)); }
    #endregion

    /// <inheritdoc/>
    public override String ToString()
    { return DbObjectName.Format(DatabaseName, SchemaName, TableName, ColumnName); }
}
