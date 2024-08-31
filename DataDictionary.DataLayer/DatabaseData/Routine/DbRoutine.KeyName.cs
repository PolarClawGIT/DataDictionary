using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.Resource;

namespace DataDictionary.DataLayer.DatabaseData.Routine;

/// <summary>
/// Interface for the Database Routine Key
/// </summary>
public interface IDbRoutineKeyName : IKey, IDbSchemaKeyName
{
    /// <summary>
    /// Name of the Database Routine (Procedure or Function)
    /// </summary>
    String? RoutineName { get; }
}

/// <summary>
/// Implementation of the Database Routine Key
/// </summary>
public class DbRoutineKeyName : DbSchemaKeyName, IDbRoutineKeyName,
    IKeyComparable<IDbRoutineKeyName>, IKeyComparable<DbRoutineKeyName>
{
    /// <inheritdoc/>
    public String RoutineName { get; set; } = string.Empty;

    /// <summary>
    /// Constructor for a blank Database Routine Key
    /// </summary>
    protected internal DbRoutineKeyName() : base() { }

    /// <summary>
    /// Constructor for the Database Routine Key
    /// </summary>
    /// <param name="source"></param>
    public DbRoutineKeyName(IDbRoutineKeyName source) : base(source)
    {
        if (source.RoutineName is string) { RoutineName = source.RoutineName; }
        else { RoutineName = string.Empty; }
    }

    #region IEquatable, IComparable
    /// <inheritdoc/>
    public Boolean Equals(DbRoutineKeyName? other)
    {
        return
            other is IDbSchemaKeyName &&
            new DbSchemaKeyName(this).Equals(other) &&
            !string.IsNullOrEmpty(RoutineName) &&
            !string.IsNullOrEmpty(other.RoutineName) &&
            RoutineName.Equals(other.RoutineName, KeyExtension.CompareString);
    }

    /// <inheritdoc/>
    public Boolean Equals(IDbRoutineKeyName? other)
    { return other is IDbRoutineKeyName value && Equals(new DbRoutineKeyName(value)); }

    /// <inheritdoc/>
    public override Boolean Equals(object? obj)
    { return obj is IDbRoutineKeyName value && Equals(new DbRoutineKeyName(value)); }

    /// <inheritdoc/>
    public Int32 CompareTo(DbRoutineKeyName? other)
    {
        if (other is null) { return 1; }
        else if (new DbSchemaKeyName(this).CompareTo(other) is int value && value != 0) { return value; }
        else { return string.Compare(RoutineName, other.RoutineName, true); }
    }

    /// <inheritdoc/>
    public Int32 CompareTo(IDbRoutineKeyName? other)
    { if (other is IDbRoutineKeyName value) { return CompareTo(new DbRoutineKeyName(value)); } else { return 1; } }

    /// <inheritdoc/>
    public override Int32 CompareTo(object? obj)
    { if (obj is IDbRoutineKeyName value) { return CompareTo(new DbRoutineKeyName(value)); } else { return 1; } }

    /// <inheritdoc/>
    public static Boolean operator ==(DbRoutineKeyName left, DbRoutineKeyName right)
    { return left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator !=(DbRoutineKeyName left, DbRoutineKeyName right)
    { return !left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator <(DbRoutineKeyName left, DbRoutineKeyName right)
    { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

    /// <inheritdoc/>
    public static Boolean operator <=(DbRoutineKeyName left, DbRoutineKeyName right)
    { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

    /// <inheritdoc/>
    public static Boolean operator >(DbRoutineKeyName left, DbRoutineKeyName right)
    { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

    /// <inheritdoc/>
    public static Boolean operator >=(DbRoutineKeyName left, DbRoutineKeyName right)
    { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

    /// <inheritdoc/>
    public override Int32 GetHashCode()
    { return HashCode.Combine(base.GetHashCode(), RoutineName.GetHashCode(KeyExtension.CompareString)); }
    #endregion

    /// <inheritdoc/>
    public override String ToString()
    { return DbObjectName.Format(DatabaseName, SchemaName, RoutineName); }
}
