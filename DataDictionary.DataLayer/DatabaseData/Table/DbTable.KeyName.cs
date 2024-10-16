﻿using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.Resource;

namespace DataDictionary.DataLayer.DatabaseData.Table;

/// <summary>
/// Interface for the Database Table Key
/// </summary>
public interface IDbTableKeyName : IKey, IDbSchemaKeyName
{
    /// <summary>
    /// Name of the Database Table (or View)
    /// </summary>
    String? TableName { get; }
}

/// <summary>
/// Implementation for the Database Table Key
/// </summary>
public class DbTableKeyName : DbSchemaKeyName, IDbTableKeyName,
    IKeyComparable<IDbTableKeyName>, IKeyComparable<DbTableKeyName>
{
    /// <inheritdoc/>
    public String TableName { get; init; } = string.Empty;

    /// <summary>
    /// Constructor for a blank Database Table Key
    /// </summary>
    protected internal DbTableKeyName() : base() { }

    /// <summary>
    /// Constructor for the Database Table Key
    /// </summary>
    /// <param name="source"></param>
    public DbTableKeyName(IDbTableKeyName source) : base(source)
    { if (source.TableName is string) { TableName = source.TableName; } }

    #region IEquatable, IComparable
    /// <inheritdoc/>
    public Boolean Equals(DbTableKeyName? other)
    {
        return
            other is IDbSchemaKeyName &&
            new DbSchemaKeyName(this).Equals(other) &&
            !string.IsNullOrEmpty(TableName) &&
            !string.IsNullOrEmpty(other.TableName) &&
            TableName.Equals(other.TableName, KeyExtension.CompareString);
    }

    /// <inheritdoc/>
    public Boolean Equals(IDbTableKeyName? other)
    { return other is IDbTableKeyName value && Equals(new DbTableKeyName(value)); }

    /// <inheritdoc/>
    public override Boolean Equals(object? obj)
    { return obj is IDbTableKeyName value && Equals(new DbTableKeyName(value)); }

    /// <inheritdoc/>
    public Int32 CompareTo(DbTableKeyName? other)
    {
        if (other is null) { return 1; }
        else if (new DbSchemaKeyName(this).CompareTo(other) is int value && value != 0) { return value; }
        else { return string.Compare(TableName, other.TableName, true); }
    }

    /// <inheritdoc/>
    public Int32 CompareTo(IDbTableKeyName? other)
    { if (other is IDbTableKeyName value) { return CompareTo(new DbTableKeyName(value)); } else { return 1; } }

    /// <inheritdoc/>
    public override Int32 CompareTo(object? obj)
    { if (obj is IDbTableKeyName value) { return CompareTo(new DbTableKeyName(value)); } else { return 1; } }

    /// <inheritdoc/>
    public static Boolean operator ==(DbTableKeyName left, DbTableKeyName right)
    { return left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator !=(DbTableKeyName left, DbTableKeyName right)
    { return !left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator <(DbTableKeyName left, DbTableKeyName right)
    { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

    /// <inheritdoc/>
    public static Boolean operator <=(DbTableKeyName left, DbTableKeyName right)
    { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

    /// <inheritdoc/>
    public static Boolean operator >(DbTableKeyName left, DbTableKeyName right)
    { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

    /// <inheritdoc/>
    public static Boolean operator >=(DbTableKeyName left, DbTableKeyName right)
    { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

    /// <inheritdoc/>
    public override Int32 GetHashCode()
    { return HashCode.Combine(base.GetHashCode(), TableName.GetHashCode(KeyExtension.CompareString)); }
    #endregion

    /// <inheritdoc/>
    public override String ToString()
    { return DbObjectName.Format(DatabaseName, SchemaName, TableName); }
}
