using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ApplicationData.Property
{

    /// <summary>
    /// Interface for the MS SQL Extended Property of the Property Item
    /// </summary>
    [Obsolete("Replace Domain Property", true)]
    public interface IPropertyKeyExtended : IKey
    {
        /// <summary>
        /// The name of the MS SQL Extended Property to be populated.
        /// </summary>
        String? ExtendedProperty { get; }
    }

    /// <summary>
    /// Implementation for the MS SQL Extended Property of the Property Item
    /// </summary>
    [Obsolete("Replace Domain Property", true)]
    public class PropertyKeyExtended: IPropertyKeyExtended, IKeyComparable<IPropertyKeyExtended>, IKeyEquality<IDbExtendedPropertyKey>
    {
        /// <inheritdoc/>
        public String ExtendedProperty { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the Extended Property Key.
        /// </summary>
        /// <param name="source"></param>
        public PropertyKeyExtended(IPropertyKeyExtended source) : base()
        {
            if (source.ExtendedProperty is string) { ExtendedProperty = source.ExtendedProperty; }
            else { ExtendedProperty = string.Empty; }
        }

        /// <summary>
        /// Constructor for the Extended Property Key.
        /// </summary>
        /// <param name="source"></param>
        public PropertyKeyExtended(IDbExtendedPropertyKey source)
        {
            if (source.PropertyName is string) { ExtendedProperty = source.PropertyName; }
            else { ExtendedProperty = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(IPropertyKeyExtended? other)
        {
            return
                other is IPropertyKeyExtended &&
                !string.IsNullOrEmpty(ExtendedProperty) &&
                !string.IsNullOrEmpty(other.ExtendedProperty) &&
                ExtendedProperty.Equals(other.ExtendedProperty, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public virtual bool Equals(IDbExtendedPropertyKey? other)
        {
            return
                other is IPropertyKeyExtended &&
                !string.IsNullOrEmpty(ExtendedProperty) &&
                !string.IsNullOrEmpty(other.PropertyName) &&
                ExtendedProperty.Equals(other.PropertyName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IPropertyKeyExtended value && Equals(new PropertyKeyExtended(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(IPropertyKeyExtended? other)
        {
            if (other is PropertyKeyExtended value)
            { return string.Compare(ExtendedProperty, value.ExtendedProperty, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IPropertyKeyExtended value) { return CompareTo(new PropertyKeyExtended(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(PropertyKeyExtended left, PropertyKeyExtended right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(PropertyKeyExtended left, PropertyKeyExtended right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(PropertyKeyExtended left, PropertyKeyExtended right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(PropertyKeyExtended left, PropertyKeyExtended right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(PropertyKeyExtended left, PropertyKeyExtended right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(PropertyKeyExtended left, PropertyKeyExtended right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return ExtendedProperty.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (ExtendedProperty is string) { return ExtendedProperty; }
            else { return string.Empty; }
        }
    }
}
