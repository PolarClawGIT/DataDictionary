using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.Resource;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData.Attribute
{
    /// <summary>
    /// Interface for the unique Name of a Attribute.
    /// </summary>
    public interface IDomainAttributeKeyName : IKey
    {
        /// <summary>
        /// Title of the Domain Attribute (aka Name of the Attribute)
        /// </summary>
        String? AttributeTitle { get; }
    }

    /// <summary>
    /// Implementation for the unique Name of a Attribute.
    /// </summary>
    public class DomainAttributeKeyName : IDomainAttributeKeyName, IKeyComparable<IDomainAttributeKeyName>
    {
        /// <inheritdoc/>
        public String AttributeTitle { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the Attribute Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public DomainAttributeKeyName(IDomainAttributeKeyName source) : base()
        {
            if (source.AttributeTitle is string) { AttributeTitle = source.AttributeTitle; }
            else { AttributeTitle = string.Empty; }
        }

        /// <summary>
        /// Constructor for the Attribute Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public DomainAttributeKeyName(IDbTableColumnKeyName source) : base()
        {
            if (source.ColumnName is string) { AttributeTitle = source.ColumnName; }
            else { AttributeTitle = string.Empty; }
        }

        /// <summary>
        /// Constructor for the Attribute Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public DomainAttributeKeyName(IDbRoutineParameterKeyName source) : base()
        {
            if (source.ParameterName is string) { AttributeTitle = source.ParameterName.Replace("@",""); }
            else { AttributeTitle = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(IDomainAttributeKeyName? other)
        {
            return
                other is IDomainAttributeKeyName &&
                !string.IsNullOrEmpty(AttributeTitle) &&
                !string.IsNullOrEmpty(other.AttributeTitle) &&
                AttributeTitle.Equals(other.AttributeTitle, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDomainAttributeKeyName value && Equals(new DomainAttributeKeyName(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(IDomainAttributeKeyName? other)
        {
            if (other is DomainAttributeKeyName value)
            { return string.Compare(AttributeTitle, value.AttributeTitle, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IDomainAttributeKeyName value) { return CompareTo(new DomainAttributeKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DomainAttributeKeyName left, DomainAttributeKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DomainAttributeKeyName left, DomainAttributeKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DomainAttributeKeyName left, DomainAttributeKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DomainAttributeKeyName left, DomainAttributeKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DomainAttributeKeyName left, DomainAttributeKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DomainAttributeKeyName left, DomainAttributeKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return AttributeTitle.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (AttributeTitle is string) { return AttributeTitle; }
            else { return string.Empty; }
        }

    }
}
