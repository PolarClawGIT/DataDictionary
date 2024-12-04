using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppCatalog;

/// <summary>
/// Interface for the Database Domain (Type) Key
/// </summary>
public interface IDomainKeyName : IKey, ISchemaKeyName
{
    /// <summary>
    /// Name of the Database Domain (Type)
    /// </summary>
    String? DomainName { get; }
}

/// <summary>
/// Implementation of the Database DomainKey
/// </summary>
public class DomainKeyName : SchemaKeyName, IDomainKeyName,
    IKeyComparable<IDomainKeyName>, IKeyComparable<DomainKeyName>
{
    /// <inheritdoc/>
    public String DomainName { get; init; } = string.Empty;

    /// <summary>
    /// Constructor for a blank Database Domain Key
    /// </summary>
    protected internal DomainKeyName() : base() { }

    /// <summary>
    /// Constructor for the Database Domain Key
    /// </summary>
    /// <param name="source"></param>
    public DomainKeyName(IDomainKeyName source) : base(source)
    {
        if (source.DomainName is string) { DomainName = source.DomainName; }
        else { DomainName = string.Empty; }
    }

    #region IEquatable, IComparable
    /// <inheritdoc/>
    public Boolean Equals(DomainKeyName? other)
    {
        return
            other is SchemaKeyName &&
            new SchemaKeyName(this).Equals(other) &&
            !string.IsNullOrEmpty(DomainName) &&
            !string.IsNullOrEmpty(other.DomainName) &&
            DomainName.Equals(other.DomainName, KeyExtension.CompareString);
    }

    /// <inheritdoc/>
    public Boolean Equals(IDomainKeyName? other)
    { return other is IDomainKeyName value && Equals(new DomainKeyName(value)); }

    /// <inheritdoc/>
    public override Boolean Equals(object? obj)
    { return obj is IDomainKeyName value && Equals(new DomainKeyName(value)); }

    /// <inheritdoc/>
    public Int32 CompareTo(DomainKeyName? other)
    {
        if (other is null) { return 1; }
        else if (new SchemaKeyName(this).CompareTo(other) is int value && value != 0) { return value; }
        else { return string.Compare(DomainName, other.DomainName, true); }
    }

    /// <inheritdoc/>
    public Int32 CompareTo(IDomainKeyName? other)
    { if (other is IDomainKeyName value) { return CompareTo(new DomainKeyName(value)); } else { return 1; } }

    /// <inheritdoc/>
    public override Int32 CompareTo(object? obj)
    { if (obj is IDomainKeyName value) { return CompareTo(new DomainKeyName(value)); } else { return 1; } }

    /// <inheritdoc/>
    public static Boolean operator ==(DomainKeyName left, DomainKeyName right)
    { return left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator !=(DomainKeyName left, DomainKeyName right)
    { return !left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator <(DomainKeyName left, DomainKeyName right)
    { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

    /// <inheritdoc/>
    public static Boolean operator <=(DomainKeyName left, DomainKeyName right)
    { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

    /// <inheritdoc/>
    public static Boolean operator >(DomainKeyName left, DomainKeyName right)
    { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

    /// <inheritdoc/>
    public static Boolean operator >=(DomainKeyName left, DomainKeyName right)
    { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }


    /// <inheritdoc/>
    public override Int32 GetHashCode()
    { return HashCode.Combine(base.GetHashCode(), DomainName.GetHashCode(KeyExtension.CompareString)); }
    #endregion

    /// <inheritdoc/>
    public override String ToString()
    { return DbObjectName.Format(DatabaseName, SchemaName, DomainName); }

}
