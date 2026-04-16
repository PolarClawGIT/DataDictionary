using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for the unique Name of a Entity.
    /// </summary>
    public interface IEntityKeyName : IKey
    {
        /// <summary>
        /// Title of the Domain Entity (aka Name of the Entity)
        /// </summary>
        String? EntityTitle { get; }
    }

    /// <summary>
    /// Implementation for the unique Name of a Entity.
    /// </summary>
    public class EntityKeyName : IEntityKeyName,
        IKeyComparable<IEntityKeyName>, IKeyComparable<EntityKeyName>
    {
        /// <inheritdoc/>
        public String EntityTitle { get; init; } = string.Empty;

        /// <inheritdoc/>
        public virtual Boolean HasValue { get { return !String.IsNullOrEmpty(EntityTitle); } }

        /// <summary>
        /// Constructor for the Entity Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public EntityKeyName(IEntityKeyName source) : base()
        {
            if (source.EntityTitle is string) { EntityTitle = source.EntityTitle; }
            else { EntityTitle = string.Empty; }
        }

        /// <summary>
        /// Constructor for the Entity Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public EntityKeyName(ITableKeyName source) : base()
        {
            if (source.TableName is string) { EntityTitle = source.TableName; }
            else { EntityTitle = string.Empty; }
        }

        /// <summary>
        /// Constructor for the Entity Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public EntityKeyName(IRoutineKeyName source) : base()
        {
            if (source.RoutineName is string) { EntityTitle = source.RoutineName; }
            else { EntityTitle = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(EntityKeyName? other)
        {
            return
                other is EntityKeyName &&
                !string.IsNullOrEmpty(EntityTitle) &&
                !string.IsNullOrEmpty(other.EntityTitle) &&
                EntityTitle.Equals(other.EntityTitle, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(IEntityKeyName? other)
        { return other is IEntityKeyName value && Equals(new EntityKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IEntityKeyName value && Equals(new EntityKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(EntityKeyName? other)
        {
            if (other is EntityKeyName value)
            { return string.Compare(EntityTitle, value.EntityTitle, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(IEntityKeyName? other)
        { if (other is IEntityKeyName value) { return CompareTo(new EntityKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        { if (obj is IEntityKeyName value) { return CompareTo(new EntityKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(EntityKeyName left, EntityKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(EntityKeyName left, EntityKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(EntityKeyName left, EntityKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(EntityKeyName left, EntityKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(EntityKeyName left, EntityKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(EntityKeyName left, EntityKeyName right)
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
