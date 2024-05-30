using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ApplicationData.Property
{
    /// <summary>
    /// Interface for the Unique Key for the Property.
    /// </summary>
    [Obsolete("Replace Domain Property", true)]
    public interface IPropertyKeyName: IKey
    {
        /// <summary>
        /// Title for the Property.
        /// </summary>
        String? PropertyTitle { get; }
    }

    /// <summary>
    /// Implementation of the Unique Key for the Property.
    /// </summary>
    [Obsolete("Replace Domain Property", true)]
    public class PropertyKeyName : IPropertyKeyName, IKeyComparable<IPropertyKeyName>
    {
        /// <inheritdoc/>
        public String PropertyTitle { get; init; } = String.Empty;

        /// <summary>
        /// Constrictor for the Unique Key of the Property.
        /// </summary>
        /// <param name="source"></param>
        public PropertyKeyName(IPropertyKeyName source) : base()
        {
            if (source.PropertyTitle is String) { PropertyTitle = source.PropertyTitle; }
            else { PropertyTitle = String.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(IPropertyKeyName? other)
        {
            return (
                other is IPropertyKeyName &&
                !String.IsNullOrEmpty(PropertyTitle) &&
                !String.IsNullOrEmpty(other.PropertyTitle) &&
                PropertyTitle.Equals(other.PropertyTitle, KeyExtension.CompareString));
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IPropertyKeyName value && this.Equals(new PropertyKeyName(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(IPropertyKeyName? other)
        {
            if (other is IPropertyKeyName value)
            { return String.Compare(PropertyTitle, value.PropertyTitle, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IPropertyKeyName value) { return this.CompareTo(new PropertyKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(PropertyKeyName left, PropertyKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(PropertyKeyName left, PropertyKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(PropertyKeyName left, PropertyKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(PropertyKeyName left, PropertyKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(PropertyKeyName left, PropertyKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(PropertyKeyName left, PropertyKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return PropertyTitle.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        {
            if (PropertyTitle is String) { return PropertyTitle; }
            else { return String.Empty; }
        }
    }
}
