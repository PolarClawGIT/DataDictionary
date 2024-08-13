using DataDictionary.Resource;

namespace DataDictionary.DataLayer.DatabaseData.Routine;

/// <summary>
/// Interface for the Database Routine Parameter Key
/// </summary>
public interface IDbRoutineParameterKeyName : IKey, IDbRoutineKeyName
{
    /// <summary>
    /// Name of the Database Parameter
    /// </summary>
    String? ParameterName { get; }
}

/// <summary>
/// Implementation for Database Routine Parameter Key
/// </summary>
public class DbRoutineParameterKeyName : DbRoutineKeyName, IDbRoutineParameterKeyName,
    IKeyComparable<IDbRoutineParameterKeyName>, IKeyComparable<DbRoutineParameterKeyName>
{
    /// <inheritdoc/>
    public String ParameterName { get; set; } = string.Empty;

    /// <summary>
    /// Constructor for a blank Database Routine Parameter Key
    /// </summary>
    protected internal DbRoutineParameterKeyName() : base() { }

    /// <summary>
    /// Constructor for Database Routine Parameter Key
    /// </summary>
    /// <param name="source"></param>
    public DbRoutineParameterKeyName(IDbRoutineParameterKeyName source) : base(source)
    {
        if (source.ParameterName is string) { ParameterName = source.ParameterName; }
        else { ParameterName = string.Empty; }
    }

    #region IEquatable, IComparable
    /// <inheritdoc/>
    public Boolean Equals(DbRoutineParameterKeyName? other)
    {
        return
            other is IDbRoutineKeyName &&
            new DbRoutineKeyName(this).Equals(other) &&
            !string.IsNullOrEmpty(ParameterName) &&
            !string.IsNullOrEmpty(other.ParameterName) &&
            ParameterName.Equals(other.ParameterName, KeyExtension.CompareString);
    }

    /// <inheritdoc/>
    public Boolean Equals(IDbRoutineParameterKeyName? other)
    { return other is IDbRoutineParameterKeyName value && Equals(new DbRoutineParameterKeyName(value)); }

    /// <inheritdoc/>
    public override Boolean Equals(object? obj)
    { return obj is IDbRoutineParameterKeyName value && Equals(new DbRoutineParameterKeyName(value)); }

    /// <inheritdoc/>
    public Int32 CompareTo(DbRoutineParameterKeyName? other)
    {
        if (other is null) { return 1; }
        else if (new DbRoutineKeyName(this).CompareTo(other) is int value && value != 0) { return value; }
        else { return string.Compare(ParameterName, other.ParameterName, true); }
    }

    /// <inheritdoc/>
    public Int32 CompareTo(IDbRoutineParameterKeyName? other)
    { if (other is IDbRoutineParameterKeyName value) { return CompareTo(new DbRoutineParameterKeyName(value)); } else { return 1; } }

    /// <inheritdoc/>
    public override Int32 CompareTo(object? obj)
    { if (obj is IDbRoutineParameterKeyName value) { return CompareTo(new DbRoutineParameterKeyName(value)); } else { return 1; } }

    /// <inheritdoc/>
    public static Boolean operator ==(DbRoutineParameterKeyName left, DbRoutineParameterKeyName right)
    { return left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator !=(DbRoutineParameterKeyName left, DbRoutineParameterKeyName right)
    { return !left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator <(DbRoutineParameterKeyName left, DbRoutineParameterKeyName right)
    { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

    /// <inheritdoc/>
    public static Boolean operator <=(DbRoutineParameterKeyName left, DbRoutineParameterKeyName right)
    { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

    /// <inheritdoc/>
    public static Boolean operator >(DbRoutineParameterKeyName left, DbRoutineParameterKeyName right)
    { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

    /// <inheritdoc/>
    public static Boolean operator >=(DbRoutineParameterKeyName left, DbRoutineParameterKeyName right)
    { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

    /// <inheritdoc/>
    public override Int32 GetHashCode()
    { return HashCode.Combine(base.GetHashCode(), ParameterName.GetHashCode(KeyExtension.CompareString)); }
    #endregion

    /// <inheritdoc/>
    public override String ToString()
    { return DbObjectName.Format(DatabaseName, SchemaName, RoutineName, ParameterName); }
}
