using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppCatalog;

/// <summary>
/// Interface for the Database Routine Key
/// </summary>
public interface IRoutineKeyName : IKey, ISchemaKeyName
{
    /// <summary>
    /// Name of the Database Routine (Procedure or Function)
    /// </summary>
    String? RoutineName { get; }
}

/// <summary>
/// Implementation of the Database Routine Key
/// </summary>
public class RoutineKeyName : SchemaKeyName, IRoutineKeyName,
    IKeyComparable<IRoutineKeyName>, IKeyComparable<RoutineKeyName>
{
    /// <inheritdoc/>
    public String RoutineName { get; set; } = string.Empty;

    /// <summary>
    /// Constructor for a blank Database Routine Key
    /// </summary>
    protected internal RoutineKeyName() : base() { }

    /// <summary>
    /// Constructor for the Database Routine Key
    /// </summary>
    /// <param name="source"></param>
    public RoutineKeyName(IRoutineKeyName source) : base(source)
    {
        if (source.RoutineName is string) { RoutineName = source.RoutineName; }
        else { RoutineName = string.Empty; }
    }

    #region IEquatable, IComparable
    /// <inheritdoc/>
    public Boolean Equals(RoutineKeyName? other)
    {
        return
            other is ISchemaKeyName &&
            new SchemaKeyName(this).Equals(other) &&
            !string.IsNullOrEmpty(RoutineName) &&
            !string.IsNullOrEmpty(other.RoutineName) &&
            RoutineName.Equals(other.RoutineName, KeyExtension.CompareString);
    }

    /// <inheritdoc/>
    public Boolean Equals(IRoutineKeyName? other)
    { return other is IRoutineKeyName value && Equals(new RoutineKeyName(value)); }

    /// <inheritdoc/>
    public override Boolean Equals(object? obj)
    { return obj is IRoutineKeyName value && Equals(new RoutineKeyName(value)); }

    /// <inheritdoc/>
    public Int32 CompareTo(RoutineKeyName? other)
    {
        if (other is null) { return 1; }
        else if (new SchemaKeyName(this).CompareTo(other) is int value && value != 0) { return value; }
        else { return string.Compare(RoutineName, other.RoutineName, true); }
    }

    /// <inheritdoc/>
    public Int32 CompareTo(IRoutineKeyName? other)
    { if (other is IRoutineKeyName value) { return CompareTo(new RoutineKeyName(value)); } else { return 1; } }

    /// <inheritdoc/>
    public override Int32 CompareTo(object? obj)
    { if (obj is IRoutineKeyName value) { return CompareTo(new RoutineKeyName(value)); } else { return 1; } }

    /// <inheritdoc/>
    public static Boolean operator ==(RoutineKeyName left, RoutineKeyName right)
    { return left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator !=(RoutineKeyName left, RoutineKeyName right)
    { return !left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator <(RoutineKeyName left, RoutineKeyName right)
    { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

    /// <inheritdoc/>
    public static Boolean operator <=(RoutineKeyName left, RoutineKeyName right)
    { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

    /// <inheritdoc/>
    public static Boolean operator >(RoutineKeyName left, RoutineKeyName right)
    { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

    /// <inheritdoc/>
    public static Boolean operator >=(RoutineKeyName left, RoutineKeyName right)
    { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

    /// <inheritdoc/>
    public override Int32 GetHashCode()
    { return HashCode.Combine(base.GetHashCode(), RoutineName.GetHashCode(KeyExtension.CompareString)); }
    #endregion

    /// <inheritdoc/>
    public override String ToString()
    { return DbObjectName.Format(DatabaseName, SchemaName, RoutineName); }
}
