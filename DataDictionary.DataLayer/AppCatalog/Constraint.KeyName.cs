using DataDictionary.DataLayer.DatabaseData;
using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppCatalog;

/// <summary>
/// Interface for the Database Constraint Key.
/// </summary>
public interface IConstraintKeyName : IKey, ISchemaKeyName
{
    /// <summary>
    /// Name of the Database Constraint
    /// </summary>
    String? ConstraintName { get; }
}

/// <summary>
/// Implementation for the Database Constraint Key.
/// </summary>
public class ConstraintKeyName : SchemaKeyName, IConstraintKeyName,
    IKeyComparable<IConstraintKeyName>, IKeyComparable<ConstraintKeyName>
{
    /// <inheritdoc/>
    public String ConstraintName { get; init; } = string.Empty;

    /// <summary>
    /// Constructor for a blank Database Constraint Key
    /// </summary>
    protected internal ConstraintKeyName() : base() { }

    /// <summary>
    /// Constructor for the Database Constraint Key.
    /// </summary>
    /// <param name="source"></param>
    public ConstraintKeyName(IConstraintKeyName source) : base(source)
    {
        if (source.ConstraintName is string) { ConstraintName = source.ConstraintName; }
        else { ConstraintName = string.Empty; }
    }

    #region IEquatable, IComparable
    /// <inheritdoc/>
    public Boolean Equals(ConstraintKeyName? other)
    {
        return
            other is SchemaKeyName &&
            new SchemaKeyName(this).Equals(other) &&
            !string.IsNullOrEmpty(ConstraintName) &&
            !string.IsNullOrEmpty(other.ConstraintName) &&
            ConstraintName.Equals(other.ConstraintName, KeyExtension.CompareString);
    }

    /// <inheritdoc/>
    public Boolean Equals(IConstraintKeyName? other)
    { return other is IConstraintKeyName value && Equals(new ConstraintKeyName(value)); }

    /// <inheritdoc/>
    public override Boolean Equals(object? obj)
    { return obj is IConstraintKeyName value && Equals(new ConstraintKeyName(value)); }

    /// <inheritdoc/>
    public Int32 CompareTo(ConstraintKeyName? other)
    {
        if (other is null) { return 1; }
        else if (new SchemaKeyName(this).CompareTo(other) is int value && value != 0) { return value; }
        else { return string.Compare(ConstraintName, other.ConstraintName, true); }
    }

    /// <inheritdoc/>
    public Int32 CompareTo(IConstraintKeyName? other)
    { if (other is IConstraintKeyName value) { return CompareTo(new ConstraintKeyName(value)); } else { return 1; } }

    /// <inheritdoc/>
    public override Int32 CompareTo(object? obj)
    { if (obj is IConstraintKeyName value) { return CompareTo(new ConstraintKeyName(value)); } else { return 1; } }

    /// <inheritdoc/>
    public static Boolean operator ==(ConstraintKeyName left, ConstraintKeyName right)
    { return left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator !=(ConstraintKeyName left, ConstraintKeyName right)
    { return !left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator <(ConstraintKeyName left, ConstraintKeyName right)
    { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

    /// <inheritdoc/>
    public static Boolean operator <=(ConstraintKeyName left, ConstraintKeyName right)
    { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

    /// <inheritdoc/>
    public static Boolean operator >(ConstraintKeyName left, ConstraintKeyName right)
    { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

    /// <inheritdoc/>
    public static Boolean operator >=(ConstraintKeyName left, ConstraintKeyName right)
    { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

    /// <inheritdoc/>
    public override Int32 GetHashCode()
    { return HashCode.Combine(base.GetHashCode(), ConstraintName.GetHashCode(KeyExtension.CompareString)); }
    #endregion

    /// <inheritdoc/>
    public override String ToString()
    { return DbObjectName.Format(DatabaseName, SchemaName, ConstraintName); }
}
