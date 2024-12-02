using DataDictionary.DataLayer.DatabaseData;
using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppCatalog;

/// <summary>
/// Interface for the unique Name of a Catalog.
/// </summary>
public interface ICatalogKeyName : IKey
{
    /// <summary>
    /// Name of the Database Name
    /// </summary>
    String? DatabaseName { get; }
}

/// <summary>
/// Implementation for the unique Name of a Catalog.
/// </summary>
public class CatalogKeyName : ICatalogKeyName,
    IKeyComparable<ICatalogKeyName>, IKeyComparable<CatalogKeyName>
{
    /// <inheritdoc/>
    public String DatabaseName { get; init; } = string.Empty;

    /// <summary>
    /// Constructor for a blank Catalog Key
    /// </summary>
    protected internal CatalogKeyName() : base() { }

    /// <summary>
    /// Constructor for the Catalog Unique Key.
    /// </summary>
    /// <param name="source"></param>
    public CatalogKeyName(ICatalogKeyName source) : base()
    {
        if (source.DatabaseName is string) { DatabaseName = source.DatabaseName; }
        else { DatabaseName = string.Empty; }
    }

    #region IEquatable, IComparable
    /// <inheritdoc/>
    public Boolean Equals(CatalogKeyName? other)
    {
        return
            other is CatalogKeyName &&
            !string.IsNullOrEmpty(DatabaseName) &&
            !string.IsNullOrEmpty(other.DatabaseName) &&
            DatabaseName.Equals(other.DatabaseName, KeyExtension.CompareString);
    }

    /// <inheritdoc/>
    public virtual Boolean Equals(ICatalogKeyName? other)
    { return other is ICatalogKeyName value && Equals(new CatalogKeyName(value)); }

    /// <inheritdoc/>
    public override Boolean Equals(object? obj)
    { return obj is ICatalogKeyName value && Equals(new CatalogKeyName(value)); }

    /// <inheritdoc/>
    public Int32 CompareTo(CatalogKeyName? other)
    {
        if (other is CatalogKeyName value)
        { return string.Compare(DatabaseName, value.DatabaseName, true); }
        else { return 1; }
    }

    /// <inheritdoc/>
    public virtual Int32 CompareTo(ICatalogKeyName? other)
    { if (other is ICatalogKeyName value) { return CompareTo(new CatalogKeyName(value)); } else { return 1; } }

    /// <inheritdoc/>
    public virtual Int32 CompareTo(object? obj)
    { if (obj is ICatalogKeyName value) { return CompareTo(new CatalogKeyName(value)); } else { return 1; } }

    /// <inheritdoc/>
    public static Boolean operator ==(CatalogKeyName left, CatalogKeyName right)
    { return left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator !=(CatalogKeyName left, CatalogKeyName right)
    { return !left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator <(CatalogKeyName left, CatalogKeyName right)
    { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

    /// <inheritdoc/>
    public static Boolean operator <=(CatalogKeyName left, CatalogKeyName right)
    { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

    /// <inheritdoc/>
    public static Boolean operator >(CatalogKeyName left, CatalogKeyName right)
    { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

    /// <inheritdoc/>
    public static Boolean operator >=(CatalogKeyName left, CatalogKeyName right)
    { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

    /// <inheritdoc/>
    public override Int32 GetHashCode()
    { return DatabaseName.GetHashCode(KeyExtension.CompareString); }
    #endregion

    /// <inheritdoc/>
    public override String ToString()
    { return DbObjectName.Format(DatabaseName); }
}
