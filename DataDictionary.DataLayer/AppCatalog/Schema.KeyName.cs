using DataDictionary.DataLayer.DatabaseData;
using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppCatalog;

/// <summary>
/// Interface for the Catalog Schema Key
/// </summary>
public interface ISchemaKeyName : IKey, ICatalogKeyName
{
    /// <summary>
    /// Name of the Catalog Schema
    /// </summary>
    String? SchemaName { get; }
}

/// <summary>
/// Implementation of the Database Schema Key
/// </summary>
public class SchemaKeyName : CatalogKeyName, ISchemaKeyName,
    IKeyComparable<ISchemaKeyName>, IKeyComparable<SchemaKeyName>
{
    /// <inheritdoc/>
    public string SchemaName { get; init; } = string.Empty;

    /// <summary>
    /// Constructor for a blank Catalog Scheme Key
    /// </summary>
    protected internal SchemaKeyName() : base() { }

    /// <summary>
    /// Constructor for the Catalog Scheme Key
    /// </summary>
    /// <param name="source"></param>
    public SchemaKeyName(ISchemaKeyName source) : base(source)
    {
        if (source.SchemaName is string) { SchemaName = source.SchemaName; }
        else { SchemaName = string.Empty; }
    }

    #region IEquatable, IComparable
    /// <inheritdoc/>
    public Boolean Equals(SchemaKeyName? other)
    {
        return
            other is SchemaKeyName &&
            new CatalogKeyName(this).Equals(other) &&
            !string.IsNullOrEmpty(SchemaName) &&
            !string.IsNullOrEmpty(other.SchemaName) &&
            SchemaName.Equals(other.SchemaName, KeyExtension.CompareString);
    }

    /// <inheritdoc/>
    public Boolean Equals(ISchemaKeyName? other)
    { return other is ISchemaKeyName value && Equals(new SchemaKeyName(value)); }

    /// <inheritdoc/>
    public override Boolean Equals(object? obj)
    { return obj is ISchemaKeyName value && Equals(new SchemaKeyName(value)); }

    /// <inheritdoc/>
    public Int32 CompareTo(SchemaKeyName? other)
    {
        if (other is null) { return 1; }
        else if (new CatalogKeyName(this).CompareTo(other) is int value && value != 0) { return value; }
        else { return string.Compare(SchemaName, other.SchemaName, true); }
    }

    /// <inheritdoc/>
    public Int32 CompareTo(ISchemaKeyName? other)
    { if (other is ISchemaKeyName value) { return CompareTo(new SchemaKeyName(value)); } else { return 1; } }

    /// <inheritdoc/>
    public override Int32 CompareTo(object? obj)
    { if (obj is ISchemaKeyName value) { return CompareTo(new SchemaKeyName(value)); } else { return 1; } }

    /// <inheritdoc/>
    public static Boolean operator ==(SchemaKeyName left, SchemaKeyName right)
    { return left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator !=(SchemaKeyName left, SchemaKeyName right)
    { return !left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator <(SchemaKeyName left, SchemaKeyName right)
    { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

    /// <inheritdoc/>
    public static bool operator <=(SchemaKeyName left, SchemaKeyName right)
    { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

    /// <inheritdoc/>
    public static Boolean operator >(SchemaKeyName left, SchemaKeyName right)
    { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

    /// <inheritdoc/>
    public static Boolean operator >=(SchemaKeyName left, SchemaKeyName right)
    { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

    /// <inheritdoc/>
    public override Int32 GetHashCode()
    { return HashCode.Combine(base.GetHashCode(), SchemaName.GetHashCode(KeyExtension.CompareString)); }
    #endregion

    /// <inheritdoc/>
    public override String ToString()
    { return DbObjectName.Format(DatabaseName, SchemaName); }
}
