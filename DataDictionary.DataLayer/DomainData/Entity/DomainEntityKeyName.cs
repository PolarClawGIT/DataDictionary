using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData.Entity
{
    /// <summary>
    /// Interface for the unique Name of a Entity.
    /// </summary>
    public interface IDomainEntityKeyName : IKey
    {
        /// <summary>
        /// Title of the Domain Entity (aka Name of the Entity)
        /// </summary>
        String? EntityTitle { get; }
    }

    /// <summary>
    /// Implementation for the unique Name of a Entity.
    /// </summary>
    public class DomainEntityKeyName : IDomainEntityKeyName, IKeyComparable<IDomainEntityKeyName>
    {
        /// <inheritdoc/>
        public String EntityTitle { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the Entity Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public DomainEntityKeyName(IDomainEntityKeyName source) : base()
        {
            if (source.EntityTitle is string) { EntityTitle = source.EntityTitle; }
            else { EntityTitle = string.Empty; }
        }

        /// <summary>
        /// Constructor for the Entity Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public DomainEntityKeyName(IDbTableKeyName source) : base()
        {
            if (source.TableName is string) { EntityTitle = source.TableName; }
            else { EntityTitle = string.Empty; }
        }

        /// <summary>
        /// Constructor for the Entity Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public DomainEntityKeyName(IDbRoutineKeyName source) : base()
        {
            if (source.RoutineName is string) { EntityTitle = source.RoutineName; }
            else { EntityTitle = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(IDomainEntityKeyName? other)
        {
            return
                other is IDomainEntityKeyName &&
                !string.IsNullOrEmpty(EntityTitle) &&
                !string.IsNullOrEmpty(other.EntityTitle) &&
                EntityTitle.Equals(other.EntityTitle, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDomainEntityKeyName value && Equals(new DomainEntityKeyName(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(IDomainEntityKeyName? other)
        {
            if (other is DomainEntityKeyName value)
            { return string.Compare(EntityTitle, value.EntityTitle, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IDomainEntityKeyName value) { return CompareTo(new DomainEntityKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DomainEntityKeyName left, DomainEntityKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DomainEntityKeyName left, DomainEntityKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DomainEntityKeyName left, DomainEntityKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DomainEntityKeyName left, DomainEntityKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DomainEntityKeyName left, DomainEntityKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DomainEntityKeyName left, DomainEntityKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return EntityTitle.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (EntityTitle is string) { return EntityTitle; }
            else { return string.Empty; }
        }
    }
}
