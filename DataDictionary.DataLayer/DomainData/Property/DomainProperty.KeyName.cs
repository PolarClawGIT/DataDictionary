using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData.Property
{
    /// <summary>
    /// Interface for the unique Name of a Property.
    /// </summary>
    public interface IDomainPropertyKeyName : IKey
    {
        /// <summary>
        /// Title of the Domain Property (aka Name of the Property)
        /// </summary>
        String? PropertyTitle { get; }
    }

    /// <summary>
    /// Implementation for the unique Name of a Property.
    /// </summary>
    public class DomainPropertyKeyName : IDomainPropertyKeyName,
        IKeyComparable<IDomainPropertyKeyName>, IKeyComparable<DomainPropertyKeyName>
    {
        /// <inheritdoc/>
        public String PropertyTitle { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the Property Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public DomainPropertyKeyName(IDomainPropertyKeyName source) : base()
        {
            if (source.PropertyTitle is string) { PropertyTitle = source.PropertyTitle; }
            else { PropertyTitle = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(DomainPropertyKeyName? other)
        {
            return
                other is DomainPropertyKeyName &&
                !string.IsNullOrEmpty(PropertyTitle) &&
                !string.IsNullOrEmpty(other.PropertyTitle) &&
                PropertyTitle.Equals(other.PropertyTitle, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(IDomainPropertyKeyName? other)
        { return other is IDomainPropertyKeyName value && Equals(new DomainPropertyKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IDomainPropertyKeyName value && Equals(new DomainPropertyKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(DomainPropertyKeyName? other)
        {
            if (other is DomainPropertyKeyName value)
            { return string.Compare(PropertyTitle, value.PropertyTitle, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(IDomainPropertyKeyName? other)
        { if (other is IDomainPropertyKeyName value) { return CompareTo(new DomainPropertyKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        { if (obj is IDomainPropertyKeyName value) { return CompareTo(new DomainPropertyKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(DomainPropertyKeyName left, DomainPropertyKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(DomainPropertyKeyName left, DomainPropertyKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(DomainPropertyKeyName left, DomainPropertyKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(DomainPropertyKeyName left, DomainPropertyKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(DomainPropertyKeyName left, DomainPropertyKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(DomainPropertyKeyName left, DomainPropertyKeyName right)
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
