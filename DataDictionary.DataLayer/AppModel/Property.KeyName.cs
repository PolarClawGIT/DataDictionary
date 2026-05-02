using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for the unique Name of a Property.
    /// </summary>
    public interface IPropertyKeyName : IKey
    {
        /// <summary>
        /// Title of the Model Property (aka Name of the Property)
        /// </summary>
        String? PropertyTitle { get; }
    }

    /// <summary>
    /// Implementation for the unique Name of a Property.
    /// </summary>
    public class PropertyKeyName : IPropertyKeyName,
        IKeyComparable<IPropertyKeyName>, IKeyComparable<PropertyKeyName>
    {
        /// <inheritdoc/>
        public String PropertyTitle { get; init; } = string.Empty;

        /// <inheritdoc/>
        public virtual Boolean HasValue { get { return !String.IsNullOrEmpty(PropertyTitle); } }

        /// <summary>
        /// Constructor for the Property Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public PropertyKeyName(IPropertyKeyName source) : base()
        {
            if (source.PropertyTitle is string) { PropertyTitle = source.PropertyTitle; }
            else { PropertyTitle = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(PropertyKeyName? other)
        {
            return
                other is PropertyKeyName &&
                !string.IsNullOrEmpty(PropertyTitle) &&
                !string.IsNullOrEmpty(other.PropertyTitle) &&
                PropertyTitle.Equals(other.PropertyTitle, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(IPropertyKeyName? other)
        { return other is IPropertyKeyName value && Equals(new PropertyKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IPropertyKeyName value && Equals(new PropertyKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(PropertyKeyName? other)
        {
            if (other is PropertyKeyName value)
            { return string.Compare(PropertyTitle, value.PropertyTitle, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(IPropertyKeyName? other)
        { if (other is IPropertyKeyName value) { return CompareTo(new PropertyKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        { if (obj is IPropertyKeyName value) { return CompareTo(new PropertyKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(PropertyKeyName left, PropertyKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(PropertyKeyName left, PropertyKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(PropertyKeyName left, PropertyKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(PropertyKeyName left, PropertyKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(PropertyKeyName left, PropertyKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(PropertyKeyName left, PropertyKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return PropertyTitle.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        {
            if (PropertyTitle is string) { return PropertyTitle; }
            else { return string.Empty; }
        }
    }
}
