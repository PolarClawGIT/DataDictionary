using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.Resource;

namespace DataDictionary.DataLayer.DatabaseData.Reference;


/// <summary>
/// Interface for the Referenced Object Name
/// </summary>
public interface IDbReferencedKeyObject : IKey
{
    /// <summary>
    /// The Database Name of the Referenced Object
    /// </summary>
    String? ReferencedDatabaseName { get; }

    /// <summary>
    /// The Database Schema Name of the Referenced Object
    /// </summary>
    String? ReferencedSchemaName { get; }

    /// <summary>
    /// The Database Object Name of the Referenced Object
    /// </summary>
    String? ReferencedObjectName { get; }
}

/// <summary>
/// Implementation for the Referenced Object Name
/// </summary>
public class DbReferencedKeyObject : IDbReferencedKeyObject,
    IKeyComparable<IDbReferencedKeyObject>, IKeyComparable<DbReferencedKeyObject>
{
    /// <inheritdoc/>
    public String ReferencedDatabaseName { get; init; } = string.Empty;

    /// <inheritdoc/>
    public String ReferencedSchemaName { get; init; } = string.Empty;

    /// <inheritdoc/>
    public String ReferencedObjectName { get; init; } = string.Empty;

    /// <summary>
    /// Constructor for Referenced Object Name
    /// </summary>
    /// <param name="source"></param>
    public DbReferencedKeyObject(IDbReferencedKeyObject source) : base()
    {
        if (source.ReferencedDatabaseName is string)
        { ReferencedDatabaseName = source.ReferencedDatabaseName; }
        else { ReferencedDatabaseName = string.Empty; }

        if (source.ReferencedSchemaName is string)
        { ReferencedSchemaName = source.ReferencedSchemaName; }
        else { ReferencedSchemaName = string.Empty; }

        if (source.ReferencedObjectName is string)
        { ReferencedObjectName = source.ReferencedObjectName; }
        else { ReferencedObjectName = string.Empty; }
    }

    /// <summary>
    /// Constructor for Referenced Object Name by Table
    /// </summary>
    /// <param name="source"></param>
    public DbReferencedKeyObject(IDbTableKeyName source) : base()
    {
        if (source.DatabaseName is string)
        { ReferencedDatabaseName = source.DatabaseName; }
        else { ReferencedDatabaseName = string.Empty; }

        if (source.SchemaName is string)
        { ReferencedSchemaName = source.SchemaName; }
        else { ReferencedSchemaName = string.Empty; }

        if (source.TableName is string)
        { ReferencedObjectName = source.TableName; }
        else { ReferencedObjectName = string.Empty; }
    }

    /// <summary>
    /// Constructor for Referenced Object Name by Routine
    /// </summary>
    /// <param name="source"></param>
    public DbReferencedKeyObject(IDbRoutineKeyName source) : base()
    {
        if (source.DatabaseName is string)
        { ReferencedDatabaseName = source.DatabaseName; }
        else { ReferencedDatabaseName = string.Empty; }

        if (source.SchemaName is string)
        { ReferencedSchemaName = source.SchemaName; }
        else { ReferencedSchemaName = string.Empty; }

        if (source.RoutineName is string)
        { ReferencedObjectName = source.RoutineName; }
        else { ReferencedObjectName = string.Empty; }
    }

    #region IEquatable, IComparable
    /// <inheritdoc/>
    public Boolean Equals(DbReferencedKeyObject? other)
    {
        return
            other is DbReferencedKeyObject &&
            !string.IsNullOrEmpty(ReferencedDatabaseName) &&
            !string.IsNullOrEmpty(other.ReferencedDatabaseName) &&
            !string.IsNullOrEmpty(ReferencedSchemaName) &&
            !string.IsNullOrEmpty(other.ReferencedSchemaName) &&
            !string.IsNullOrEmpty(ReferencedObjectName) &&
            !string.IsNullOrEmpty(other.ReferencedObjectName) &&
            ReferencedDatabaseName.Equals(other.ReferencedDatabaseName, KeyExtension.CompareString) &&
            ReferencedSchemaName.Equals(other.ReferencedSchemaName, KeyExtension.CompareString) &&
            ReferencedObjectName.Equals(other.ReferencedObjectName, KeyExtension.CompareString);
    }

    /// <inheritdoc/>
    public Boolean Equals(IDbReferencedKeyObject? other)
    { return other is IDbReferencedKeyObject value && Equals(new DbReferencedKeyObject(value)); }

    /// <inheritdoc/>
    public override Boolean Equals(object? obj)
    { return obj is IDbReferencedKeyObject value && Equals(new DbReferencedKeyObject(value)); }

    /// <inheritdoc/>
    public Int32 CompareTo(DbReferencedKeyObject? other)
    {
        if (other is null) { return 1; }
        else if (String.Compare(ReferencedDatabaseName, other.ReferencedDatabaseName, true) is int dbValue && dbValue != 0) { return dbValue; }
        else if (String.Compare(ReferencedSchemaName, other.ReferencedSchemaName, true) is int schemaValue && dbValue != 0) { return schemaValue; }
        else { return String.Compare(ReferencedObjectName, other.ReferencedObjectName, true); }
    }

    /// <inheritdoc/>
    public Int32 CompareTo(IDbReferencedKeyObject? other)
    { if (other is IDbReferencedKeyObject value) { return CompareTo(new DbReferencedKeyObject(value)); } else { return 1; } }

    /// <inheritdoc/>
    public virtual Int32 CompareTo(object? obj)
    { if (obj is IDbReferencedKeyObject value) { return CompareTo(new DbReferencedKeyObject(value)); } else { return 1; } }

    /// <inheritdoc/>
    public static Boolean operator ==(DbReferencedKeyObject left, DbReferencedKeyObject right)
    { return left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator !=(DbReferencedKeyObject left, DbReferencedKeyObject right)
    { return !left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator <(DbReferencedKeyObject left, DbReferencedKeyObject right)
    { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

    /// <inheritdoc/>
    public static bool operator <=(DbReferencedKeyObject left, DbReferencedKeyObject right)
    { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

    /// <inheritdoc/>
    public static Boolean operator >(DbReferencedKeyObject left, DbReferencedKeyObject right)
    { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

    /// <inheritdoc/>
    public static Boolean operator >=(DbReferencedKeyObject left, DbReferencedKeyObject right)
    { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

    /// <inheritdoc/>
    public override Int32 GetHashCode()
    {
        return HashCode.Combine(
            ReferencedDatabaseName.GetHashCode(KeyExtension.CompareString),
            ReferencedSchemaName.GetHashCode(KeyExtension.CompareString),
            ReferencedObjectName.GetHashCode(KeyExtension.CompareString)
        );
    }
    #endregion

    /// <inheritdoc/>
    public override String ToString()
    { return DbObjectName.Format(ReferencedDatabaseName, ReferencedSchemaName, ReferencedObjectName); }
}
