using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for the Database Extended Property Object Key
    /// </summary>
    public interface IPropertyKeyObject : IKey, ICatalogKeyName
    {
        /// <summary>
        /// Level 0 (Catalog) Name parameter
        /// </summary>
        String? Level0Name { get; }

        /// <summary>
        /// Level 1 (Object) Name parameter
        /// </summary>
        String? Level1Name { get; }

        /// <summary>
        /// Level 2 (Element) Name parameter
        /// </summary>
        String? Level2Name { get; }
    }

    /// <summary>
    /// Implementation for the Database Extended Property Object Key
    /// </summary>
    public class PropertyKeyObject : CatalogKeyName, IPropertyKeyObject, IKeyComparable<IPropertyKeyObject>
    {
        /// <inheritdoc/>
        public String Level0Name { get; init; } = string.Empty;

        /// <inheritdoc/>
        public String Level1Name { get; init; } = string.Empty;

        /// <inheritdoc/>
        public String Level2Name { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the Database Extended Property Name Key
        /// </summary>
        /// <param name="source"></param>
        public PropertyKeyObject(IPropertyKeyObject source) : base(source)
        {
            if (!String.IsNullOrWhiteSpace(source.Level0Name)) { Level0Name = source.Level0Name; }
            if (!String.IsNullOrWhiteSpace(source.Level1Name)) { Level1Name = source.Level1Name; }
            if (!String.IsNullOrWhiteSpace(source.Level2Name)) { Level2Name = source.Level2Name; }
        }

        /// <summary>
        /// Constructor for the Database Extended Property Name Key
        /// </summary>
        /// <param name="source"></param>
        public PropertyKeyObject(ITableKeyName source) : base(source)
        {
            if (source.SchemaName is String) { Level0Name = source.SchemaName; }
            if (source.TableName is String) { Level1Name = source.TableName; }
        }

        /// <summary>
        /// Constructor for the Database Extended Property Name Key
        /// </summary>
        /// <param name="source"></param>
        public PropertyKeyObject(ITableColumnKeyName source) : base(source)
        {
            if (source.SchemaName is String) { Level0Name = source.SchemaName; }
            if (source.TableName is String) { Level1Name = source.TableName; }
            if (source.ColumnName is String) { Level2Name = source.ColumnName; }
        }

        /// <summary>
        /// Constructor for the Database Extended Property Name Key
        /// </summary>
        /// <param name="source"></param>
        public PropertyKeyObject(IRoutineKeyName source) : base(source)
        {
            if (source.SchemaName is String) { Level0Name = source.SchemaName; }
            if (source.RoutineName is String) { Level1Name = source.RoutineName; }
        }

        /// <summary>
        /// Constructor for the Database Extended Property Name Key
        /// </summary>
        /// <param name="source"></param>
        public PropertyKeyObject(IRoutineParameterKeyName source) : base(source)
        {
            if (source.SchemaName is String) { Level0Name = source.SchemaName; }
            if (source.RoutineName is String) { Level1Name = source.RoutineName; }
            if (source.ParameterName is String) { Level2Name = source.ParameterName; }
        }

        /// <summary>
        /// Constructor for the Database Extended Property Name Key
        /// </summary>
        /// <param name="source"></param>
        public PropertyKeyObject(IConstraintKeyName source) : base(source)
        {
            if (source.SchemaName is String) { Level0Name = source.SchemaName; }
            if (source.ConstraintName is String) { Level1Name = source.ConstraintName; }
        }

        /// <summary>
        /// Constructor for the Database Extended Property Name Key
        /// </summary>
        /// <param name="source"></param>
        public PropertyKeyObject(ISchemaKeyName source) : base(source)
        {
            if (source.SchemaName is String) { Level0Name = source.SchemaName; }
        }

        /// <summary>
        /// Constructor for the Database Extended Property Name Key
        /// </summary>
        /// <param name="source"></param>
        public PropertyKeyObject(IDomainKeyName source) : base(source)
        {
            if (source.SchemaName is String) { Level0Name = source.SchemaName; }
            if (source.DomainName is String) { Level1Name = source.DomainName; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IPropertyKeyObject? other)
        {
            return
                other is IPropertyKeyObject &&
                new CatalogKeyName(this).Equals(other) &&
                (String.IsNullOrWhiteSpace(Level0Name) &&
                  String.IsNullOrWhiteSpace(other.Level0Name) ||
                  Level0Name.Equals(other.Level0Name, KeyExtension.CompareString)) &&
                (String.IsNullOrWhiteSpace(Level1Name) &&
                  String.IsNullOrWhiteSpace(other.Level1Name) ||
                  Level1Name.Equals(other.Level1Name, KeyExtension.CompareString)) &&
                (String.IsNullOrWhiteSpace(Level2Name) &&
                  String.IsNullOrWhiteSpace(other.Level2Name) ||
                  Level2Name.Equals(other.Level2Name, KeyExtension.CompareString));
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IPropertyKeyObject value && Equals(new PropertyKeyObject(value)); }

        /// <inheritdoc/>
        public int CompareTo(IPropertyKeyObject? other)
        {
            if (other is null) { return 1; }
            else if (new CatalogKeyName(this).CompareTo(other) is int value && value != 0) { return value; }
            else if (string.Compare(Level0Name, other.Level0Name, true) is int level0 && level0 != 0) { return level0; }
            else if (string.Compare(Level1Name, other.Level1Name, true) is int level1 && level0 != 0) { return level1; }
            { return string.Compare(Level2Name, other.Level2Name, true); }
        }

        /// <inheritdoc/>
        public override int CompareTo(object? obj)
        { if (obj is IPropertyKeyObject value) { return CompareTo(new PropertyKeyObject(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(PropertyKeyObject left, PropertyKeyObject right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(PropertyKeyObject left, PropertyKeyObject right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(PropertyKeyObject left, PropertyKeyObject right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(PropertyKeyObject left, PropertyKeyObject right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(PropertyKeyObject left, PropertyKeyObject right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(PropertyKeyObject left, PropertyKeyObject right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(
            base.GetHashCode(),
            Level0Name.GetHashCode(KeyExtension.CompareString),
            Level1Name.GetHashCode(KeyExtension.CompareString),
            Level2Name.GetHashCode(KeyExtension.CompareString));
        }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            String result = base.ToString();
            if (!String.IsNullOrWhiteSpace(Level0Name)) { result = String.Format("{0}.{1}", result, Level0Name); }
            if (!String.IsNullOrWhiteSpace(Level1Name)) { result = String.Format("{0}.{1}", result, Level1Name); }
            if (!String.IsNullOrWhiteSpace(Level2Name)) { result = String.Format("{0}.{1}", result, Level2Name); }

            return result;
        }
    }
}
