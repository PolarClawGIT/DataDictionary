using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData.ExtendedProperty
{
    /// <summary>
    /// Interface for the Database Extended Property Name
    /// </summary>
    public interface IDbExtendedPropertyName
    {
        /// <summary>
        /// Name of the Extended Property.
        /// </summary>
        String? PropertyName { get; }
    }

    /// <summary>
    /// Interface for the Database Extended Property Key
    /// </summary>
    public interface IDbExtendedPropertyKey : IDbExtendedPropertyKeyName, IDbExtendedPropertyName
    { }

    /// <summary>
    /// Implementation for the Database Extended Property Key
    /// </summary>
    public class DbExtendedPropertyKey : DbExtendedPropertyKeyName, IDbExtendedPropertyKey, IKeyComparable<IDbExtendedPropertyKey>
    {
        /// <inheritdoc/>
        public String PropertyName { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the Database Extended Property Key
        /// </summary>
        /// <param name="source"></param>
        public DbExtendedPropertyKey(IDbExtendedPropertyKey source) : base(source)
        {
            if (source.PropertyName is String) { Level0Name = source.PropertyName; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDbExtendedPropertyKey? other)
        {
            return
                other is IDbExtendedPropertyKey &&
                new DbExtendedPropertyKeyName(this).Equals(other) &&
                PropertyName.Equals(other.PropertyName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDbExtendedPropertyKey value && Equals(new DbExtendedPropertyKey(value)); }


        /// <inheritdoc/>
        public int CompareTo(IDbExtendedPropertyKey? other)
        {
            if (other is null) { return 1; }
            else if (new DbExtendedPropertyKeyName(this).CompareTo(other) is int value && value != 0) { return value; }
            else { return string.Compare(PropertyName, other.PropertyName, true); }
        }

        /// <inheritdoc/>
        public override int CompareTo(object? obj)
        { if (obj is IDbExtendedPropertyKey value) { return CompareTo(new DbExtendedPropertyKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DbExtendedPropertyKey left, DbExtendedPropertyKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbExtendedPropertyKey left, DbExtendedPropertyKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DbExtendedPropertyKey left, DbExtendedPropertyKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DbExtendedPropertyKey left, DbExtendedPropertyKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DbExtendedPropertyKey left, DbExtendedPropertyKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DbExtendedPropertyKey left, DbExtendedPropertyKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), PropertyName.GetHashCode(KeyExtension.CompareString)); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            String result = base.ToString();
            if (!String.IsNullOrWhiteSpace(PropertyName)) { result = String.Format("{0}.{1}", result, PropertyName); }

            return result;
        }
    }
}
