using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.Resource;
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
    public class DomainEntityKeyName : IDomainEntityKeyName,
        IKeyComparable<IDomainEntityKeyName>, IKeyComparable<DomainEntityKeyName>
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
        public Boolean Equals(DomainEntityKeyName? other)
        {
            return
                other is DomainEntityKeyName &&
                !string.IsNullOrEmpty(EntityTitle) &&
                !string.IsNullOrEmpty(other.EntityTitle) &&
                EntityTitle.Equals(other.EntityTitle, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(IDomainEntityKeyName? other)
        { return other is IDomainEntityKeyName value && Equals(new DomainEntityKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IDomainEntityKeyName value && Equals(new DomainEntityKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(DomainEntityKeyName? other)
        {
            if (other is DomainEntityKeyName value)
            { return string.Compare(EntityTitle, value.EntityTitle, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(IDomainEntityKeyName? other)
        { if (other is IDomainEntityKeyName value) { return CompareTo(new DomainEntityKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        { if (obj is IDomainEntityKeyName value) { return CompareTo(new DomainEntityKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(DomainEntityKeyName left, DomainEntityKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(DomainEntityKeyName left, DomainEntityKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(DomainEntityKeyName left, DomainEntityKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(DomainEntityKeyName left, DomainEntityKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(DomainEntityKeyName left, DomainEntityKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(DomainEntityKeyName left, DomainEntityKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return EntityTitle.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        {
            if (EntityTitle is string) { return EntityTitle; }
            else { return string.Empty; }
        }



    }
}
