using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.Resource;

namespace DataDictionary.DataLayer.DatabaseData.Schema;

/// <summary>
/// Interface for the Database Schema Key
/// </summary>
public interface IDbSchemaKeyName : IKey, IDbCatalogKeyName
{
    /// <summary>
    /// Name of the Database Schema
    /// </summary>
    String? SchemaName { get; }
}

/// <summary>
/// Implementation of the Database Schema Key
/// </summary>
public class DbSchemaKeyName : DbCatalogKeyName, IDbSchemaKeyName,
    IKeyComparable<IDbSchemaKeyName>, IKeyComparable<DbSchemaKeyName>
{
    /// <inheritdoc/>
    public string SchemaName { get; init; } = string.Empty;

    /// <summary>
    /// Constructor for a blank Database Scheme Key
    /// </summary>
    protected internal DbSchemaKeyName() : base() { }

    /// <summary>
    /// Constructor for the Database Scheme Key
    /// </summary>
    /// <param name="source"></param>
    public DbSchemaKeyName(IDbSchemaKeyName source) : base(source)
    {
        if (source.SchemaName is string) { SchemaName = source.SchemaName; }
        else { SchemaName = string.Empty; }
    }

    #region IEquatable, IComparable
    /// <inheritdoc/>
    public Boolean Equals(DbSchemaKeyName? other)
    {
        return
            other is DbSchemaKeyName &&
            new DbCatalogKeyName(this).Equals(other) &&
            !string.IsNullOrEmpty(SchemaName) &&
            !string.IsNullOrEmpty(other.SchemaName) &&
            SchemaName.Equals(other.SchemaName, KeyExtension.CompareString);
    }

    /// <inheritdoc/>
    public Boolean Equals(IDbSchemaKeyName? other)
    { return other is IDbSchemaKeyName value && Equals(new DbSchemaKeyName(value)); }

    /// <inheritdoc/>
    public override Boolean Equals(object? obj)
    { return obj is IDbSchemaKeyName value && Equals(new DbSchemaKeyName(value)); }

    /// <inheritdoc/>
    public Int32 CompareTo(DbSchemaKeyName? other)
    {
        if (other is null) { return 1; }
        else if (new DbCatalogKeyName(this).CompareTo(other) is int value && value != 0) { return value; }
        else { return string.Compare(SchemaName, other.SchemaName, true); }
    }

    /// <inheritdoc/>
    public Int32 CompareTo(IDbSchemaKeyName? other)
    { if (other is IDbSchemaKeyName value) { return CompareTo(new DbSchemaKeyName(value)); } else { return 1; } }

    /// <inheritdoc/>
    public override Int32 CompareTo(object? obj)
    { if (obj is IDbSchemaKeyName value) { return CompareTo(new DbSchemaKeyName(value)); } else { return 1; } }

    /// <inheritdoc/>
    public static Boolean operator ==(DbSchemaKeyName left, DbSchemaKeyName right)
    { return left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator !=(DbSchemaKeyName left, DbSchemaKeyName right)
    { return !left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator <(DbSchemaKeyName left, DbSchemaKeyName right)
    { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

    /// <inheritdoc/>
    public static bool operator <=(DbSchemaKeyName left, DbSchemaKeyName right)
    { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

    /// <inheritdoc/>
    public static Boolean operator >(DbSchemaKeyName left, DbSchemaKeyName right)
    { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

    /// <inheritdoc/>
    public static Boolean operator >=(DbSchemaKeyName left, DbSchemaKeyName right)
    { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

    /// <inheritdoc/>
    public override Int32 GetHashCode()
    { return HashCode.Combine(base.GetHashCode(), SchemaName.GetHashCode(KeyExtension.CompareString)); }
    #endregion

    /// <inheritdoc/>
    public override String ToString()
    { return DbObjectName.Format(DatabaseName, SchemaName); }
}
