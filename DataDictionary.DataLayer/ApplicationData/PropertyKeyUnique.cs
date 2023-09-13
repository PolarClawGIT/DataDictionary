﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ApplicationData
{
    /// <summary>
    /// Interface for the Unique Key for the Property.
    /// </summary>
    public interface IPropertyKeyUnique
    {
        /// <summary>
        /// Title for the Property.
        /// </summary>
        String? PropertyTitle { get; }
    }

    /// <summary>
    /// Implementation of the Unique Key for the Property.
    /// </summary>
    public class PropertyKeyUnique : IPropertyKeyUnique, IEquatable<PropertyKeyUnique>, IComparable<PropertyKeyUnique>, IComparable
    {
        /// <inheritdoc/>
        public String PropertyTitle { get; init; } = String.Empty;

        /// <summary>
        /// Constrictor for the Unique Key of the Property.
        /// </summary>
        /// <param name="source"></param>
        public PropertyKeyUnique(IPropertyKeyUnique source) : base()
        {
            if (source.PropertyTitle is String) { PropertyTitle = source.PropertyTitle; }
            else { PropertyTitle = String.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(PropertyKeyUnique? other)
        {
            return (
                other is PropertyKeyUnique &&
                !String.IsNullOrEmpty(PropertyTitle) &&
                !String.IsNullOrEmpty(other.PropertyTitle) &&
                PropertyTitle.Equals(other.PropertyTitle, ModelFactory.CompareString));
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IPropertyKeyUnique value && this.Equals(new PropertyKeyUnique(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(PropertyKeyUnique? other)
        {
            if (other is PropertyKeyUnique value)
            { return String.Compare(PropertyTitle, value.PropertyTitle, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IPropertyKeyUnique value) { return this.CompareTo(new PropertyKeyUnique(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(PropertyKeyUnique left, PropertyKeyUnique right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(PropertyKeyUnique left, PropertyKeyUnique right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(PropertyKeyUnique left, PropertyKeyUnique right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(PropertyKeyUnique left, PropertyKeyUnique right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(PropertyKeyUnique left, PropertyKeyUnique right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(PropertyKeyUnique left, PropertyKeyUnique right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(PropertyTitle); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        {
            if (PropertyTitle is String) { return PropertyTitle; }
            else { return String.Empty; }
        }
    }
}
