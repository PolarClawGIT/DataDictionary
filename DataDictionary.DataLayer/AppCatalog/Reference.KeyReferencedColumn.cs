using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppCatalog;

/// <summary>
/// Interface for the Referenced Column Name
/// </summary>
public interface IReferencedKeyColumn : IReferencedKeyObject
{
    /// <summary>
    /// The Database Column Name of the Referenced Object
    /// </summary>
    String? ReferencedColumnName { get; }
}

/// <summary>
/// Implementation for the Referenced Column Name
/// </summary>
public class ReferencedKeyColumn : ReferencedKeyObject, IReferencedKeyColumn,
    IKeyComparable<IReferencedKeyColumn>, IKeyComparable<ReferencedKeyColumn>
{
    /// <inheritdoc/>
    public String ReferencedColumnName { get; init; } = string.Empty;

    /// <inheritdoc/>
    public override Boolean HasValue { get { return base.HasValue && !String.IsNullOrEmpty(ReferencedColumnName); } }

    /// <summary>
    /// Constructor for Referenced Object Name
    /// </summary>
    /// <param name="source"></param>
    public ReferencedKeyColumn(IReferencedKeyColumn source) : base(source)
    {
        if (source.ReferencedColumnName is string)
        { ReferencedColumnName = source.ReferencedColumnName; }
        else { ReferencedColumnName = string.Empty; }
    }

    /// <summary>
    /// Constructor for Referenced Object Name by Table
    /// </summary>
    /// <param name="source"></param>
    public ReferencedKeyColumn(ITableColumnKeyName source) : base(source)
    {
        if (source.ColumnName is string)
        { ReferencedColumnName = source.ColumnName; }
        else { ReferencedColumnName = string.Empty; }
    }

    /// <summary>
    /// Converts Reference Column Key into a Table Column Key.
    /// </summary>
    /// <returns></returns>
    public TableColumnKeyName AsColumn()
    {
        return new TableColumnKeyName()
        {
            DatabaseName = ReferencedDatabaseName,
            SchemaName = ReferencedSchemaName,
            TableName = ReferencedObjectName,
            ColumnName = ReferencedColumnName
        };
    }

    #region IEquatable, IComparable
    /// <inheritdoc/>
    public Boolean Equals(ReferencedKeyColumn? other)
    {
        return
            other is ReferencedKeyObject &&
            base.Equals(other) &&
            !string.IsNullOrEmpty(ReferencedColumnName) &&
            !string.IsNullOrEmpty(other.ReferencedColumnName) &&
            ReferencedColumnName.Equals(other.ReferencedColumnName, KeyExtension.CompareString);
    }

    /// <inheritdoc/>
    public Boolean Equals(IReferencedKeyColumn? other)
    { return other is IReferencedKeyColumn value && Equals(new ReferencedKeyObject(value)); }

    /// <inheritdoc/>
    public override Boolean Equals(object? obj)
    { return obj is IReferencedKeyColumn value && Equals(new ReferencedKeyObject(value)); }

    /// <inheritdoc/>
    public Int32 CompareTo(ReferencedKeyColumn? other)
    {
        if (other is null) { return 1; }
        else if (base.CompareTo(other) is int value && value != 0) { return value; }
        else { return string.Compare(ReferencedColumnName, other.ReferencedColumnName, true); }
    }

    /// <inheritdoc/>
    public Int32 CompareTo(IReferencedKeyColumn? other)
    { if (other is IReferencedKeyColumn value) { return CompareTo(new ReferencedKeyColumn(value)); } else { return 1; } }

    /// <inheritdoc/>
    public override Int32 CompareTo(object? obj)
    { if (obj is IReferencedKeyColumn value) { return CompareTo(new ReferencedKeyColumn(value)); } else { return 1; } }

    /// <inheritdoc/>
    public static Boolean operator ==(ReferencedKeyColumn left, ReferencedKeyColumn right)
    { return left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator !=(ReferencedKeyColumn left, ReferencedKeyColumn right)
    { return !left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator <(ReferencedKeyColumn left, ReferencedKeyColumn right)
    { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

    /// <inheritdoc/>
    public static bool operator <=(ReferencedKeyColumn left, ReferencedKeyColumn right)
    { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

    /// <inheritdoc/>
    public static Boolean operator >(ReferencedKeyColumn left, ReferencedKeyColumn right)
    { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

    /// <inheritdoc/>
    public static Boolean operator >=(ReferencedKeyColumn left, ReferencedKeyColumn right)
    { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

    /// <inheritdoc/>
    public override Int32 GetHashCode()
    {
        return HashCode.Combine(
            base.GetHashCode(),
            ReferencedColumnName.GetHashCode(KeyExtension.CompareString)
        );
    }
    #endregion

    /// <inheritdoc/>
    public override String ToString()
    { return DbObjectName.Format(ReferencedDatabaseName, ReferencedSchemaName, ReferencedObjectName, ReferencedColumnName); }


}
