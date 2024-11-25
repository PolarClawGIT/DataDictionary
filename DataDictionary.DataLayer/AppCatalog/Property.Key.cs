using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for the Database Extended Property Name
    /// </summary>
    public interface IPropertyName
    {
        /// <summary>
        /// Name of the Extended Property.
        /// </summary>
        String? PropertyName { get; }
    }

    /// <summary>
    /// Interface for the Database Extended Property Key
    /// </summary>
    public interface IPropertyKey : IPropertyKeyName, IPropertyName
    { }

    /// <summary>
    /// Implementation for the Database Extended Property Key
    /// </summary>
    public class PropertyKey : PropertyKeyName, IPropertyKey,
        IKeyComparable<IPropertyKey>, IKeyComparable<PropertyKey>
    {
        /// <inheritdoc/>
        public String PropertyName { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the Database Extended Property Key
        /// </summary>
        /// <param name="source"></param>
        public PropertyKey(IPropertyKey source) : base(source)
        {
            if (source.PropertyName is String) { Level0Name = source.PropertyName; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(PropertyKey? other)
        {
            return
                other is IPropertyKey &&
                new PropertyKeyName(this).Equals(other) &&
                PropertyName.Equals(other.PropertyName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public Boolean Equals(IPropertyKey? other)
        { return other is IPropertyKey value && Equals(new PropertyKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IPropertyKey value && Equals(new PropertyKey(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(PropertyKey? other)
        {
            if (other is null) { return 1; }
            else if (new PropertyKeyName(this).CompareTo(other) is Int32 value && value != 0) { return value; }
            else { return string.Compare(PropertyName, other.PropertyName, true); }
        }

        /// <inheritdoc/>
        public Int32 CompareTo(IPropertyKey? other)
        { if (other is IPropertyKey value) { return CompareTo(new PropertyKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public override Int32 CompareTo(object? obj)
        { if (obj is IPropertyKey value) { return CompareTo(new PropertyKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(PropertyKey left, PropertyKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(PropertyKey left, PropertyKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(PropertyKey left, PropertyKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(PropertyKey left, PropertyKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(PropertyKey left, PropertyKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(PropertyKey left, PropertyKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), PropertyName.GetHashCode(KeyExtension.CompareString)); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        {
            String result = base.ToString();
            if (!String.IsNullOrWhiteSpace(PropertyName)) { result = String.Format("{0}.{1}", result, PropertyName); }

            return result;
        }

    }
}
