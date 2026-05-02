using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppCatalog;


/// <summary>
/// Interface for the Referenced Object Name
/// </summary>
public interface IReferencedKeyObject : IKey
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
public class ReferencedKeyObject : IReferencedKeyObject,
    IKeyComparable<IReferencedKeyObject>, IKeyComparable<ReferencedKeyObject>
{
    /// <inheritdoc/>
    public String ReferencedDatabaseName { get; init; } = string.Empty;

    /// <inheritdoc/>
    public String ReferencedSchemaName { get; init; } = string.Empty;

    /// <inheritdoc/>
    public String ReferencedObjectName { get; init; } = string.Empty;

    /// <inheritdoc/>
    public virtual Boolean HasValue
    {
        get
        {
            return !String.IsNullOrEmpty(ReferencedDatabaseName)
                && !String.IsNullOrEmpty(ReferencedSchemaName)
                && !String.IsNullOrEmpty(ReferencedObjectName);
        }
    }

    /// <summary>
    /// Constructor for Referenced Object Name
    /// </summary>
    /// <param name="source"></param>
    public ReferencedKeyObject(IReferencedKeyObject source) : base()
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
    public ReferencedKeyObject(ITableKeyName source) : base()
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
    public ReferencedKeyObject(IRoutineKeyName source) : base()
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

    /// <summary>
    /// Converts Reference Object Key into a Table Key.
    /// </summary>
    /// <returns></returns>
    public TableKeyName AsTable()
    {
        return new TableKeyName()
        {
            DatabaseName = ReferencedDatabaseName,
            SchemaName = ReferencedSchemaName,
            TableName = ReferencedObjectName
        };
    }

    /// <summary>
    /// Converts Reference Object Key into a Routine Key.
    /// </summary>
    /// <returns></returns>
    public RoutineKeyName AsRoutine()
    {
        return new RoutineKeyName()
        {
            DatabaseName = ReferencedDatabaseName,
            SchemaName = ReferencedSchemaName,
            RoutineName = ReferencedObjectName
        };
    }


    #region IEquatable, IComparable
    /// <inheritdoc/>
    public Boolean Equals(ReferencedKeyObject? other)
    {
        return
            other is ReferencedKeyObject &&
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
    public Boolean Equals(IReferencedKeyObject? other)
    { return other is IReferencedKeyObject value && Equals(new ReferencedKeyObject(value)); }

    /// <inheritdoc/>
    public override Boolean Equals(object? obj)
    { return obj is IReferencedKeyObject value && Equals(new ReferencedKeyObject(value)); }

    /// <inheritdoc/>
    public Int32 CompareTo(ReferencedKeyObject? other)
    {
        if (other is null) { return 1; }
        else if (String.Compare(ReferencedDatabaseName, other.ReferencedDatabaseName, true) is int dbValue && dbValue != 0) { return dbValue; }
        else if (String.Compare(ReferencedSchemaName, other.ReferencedSchemaName, true) is int schemaValue && dbValue != 0) { return schemaValue; }
        else { return String.Compare(ReferencedObjectName, other.ReferencedObjectName, true); }
    }

    /// <inheritdoc/>
    public Int32 CompareTo(IReferencedKeyObject? other)
    { if (other is IReferencedKeyObject value) { return CompareTo(new ReferencedKeyObject(value)); } else { return 1; } }

    /// <inheritdoc/>
    public virtual Int32 CompareTo(object? obj)
    { if (obj is IReferencedKeyObject value) { return CompareTo(new ReferencedKeyObject(value)); } else { return 1; } }

    /// <inheritdoc/>
    public static Boolean operator ==(ReferencedKeyObject left, ReferencedKeyObject right)
    { return left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator !=(ReferencedKeyObject left, ReferencedKeyObject right)
    { return !left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator <(ReferencedKeyObject left, ReferencedKeyObject right)
    { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

    /// <inheritdoc/>
    public static bool operator <=(ReferencedKeyObject left, ReferencedKeyObject right)
    { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

    /// <inheritdoc/>
    public static Boolean operator >(ReferencedKeyObject left, ReferencedKeyObject right)
    { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

    /// <inheritdoc/>
    public static Boolean operator >=(ReferencedKeyObject left, ReferencedKeyObject right)
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
