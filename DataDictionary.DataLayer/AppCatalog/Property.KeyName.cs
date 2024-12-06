using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for the Database Extended Property Name Key
    /// </summary>
    public interface IPropertyKeyName : IPropertyKeyObject
    {
        /// <summary>
        /// Name of the Extended Property.
        /// </summary>
        String? PropertyName { get; }
    }

    /// <summary>
    /// Implementation for the Database Extended Property Name Key
    /// </summary>
    public class PropertyKeyName : PropertyKeyObject, IPropertyKeyName, IKeyComparable<IPropertyKeyName>
    {
        /// <inheritdoc/>
        public String PropertyName { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for Extended Property Name Key
        /// </summary>
        /// <param name="source"></param>
        public PropertyKeyName(IPropertyKeyName source) : base(source)
        {
            if (!String.IsNullOrWhiteSpace(source.PropertyName)) { PropertyName = source.PropertyName; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IPropertyKeyName? other)
        {
            return
                other is IPropertyKeyName &&
                new PropertyKeyObject(this).Equals(other) &&
                (String.IsNullOrWhiteSpace(PropertyName) &&
                  String.IsNullOrWhiteSpace(other.PropertyName) ||
                  PropertyName.Equals(other.PropertyName, KeyExtension.CompareString));
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IPropertyKeyName value && Equals(new PropertyKeyName(value)); }

        /// <inheritdoc/>
        public int CompareTo(IPropertyKeyName? other)
        {
            if (other is null) { return 1; }
            else if (new PropertyKeyObject(this).CompareTo(other) is int value && value != 0) { return value; }
            { return string.Compare(Level2Name, other.PropertyName, true); }
        }

        /// <inheritdoc/>
        public override int CompareTo(object? obj)
        { if (obj is IPropertyKeyName value) { return CompareTo(new PropertyKeyName(value)); } else { return 1; } }

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
        public override int GetHashCode()
        {
            return HashCode.Combine(
            base.GetHashCode(),
            PropertyName.GetHashCode(KeyExtension.CompareString));
        }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            String result = base.ToString();
            if (!String.IsNullOrWhiteSpace(PropertyName)) { result = String.Format("{0}: {1}", result, PropertyName); }

            return result;
        }

    }
}
