using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.Resource;

namespace DataDictionary.DataLayer.DatabaseData.Reference
{
    /// <summary>
    /// Interface for the Database Reference Key
    /// </summary>
    public interface IDbReferenceKeyName : IKey, IDbSchemaKeyName
    {
        /// <summary>
        /// Name of the Database Reference Object (Table, View, Procedure, Function, ...)
        /// </summary>
        String? ObjectName { get; }
    }

    /// <summary>
    /// Implementation of the Database Reference Key
    /// </summary>
    public class DbReferenceKeyName : DbSchemaKeyName, IDbReferenceKeyName,
        IKeyComparable<IDbReferenceKeyName>, IKeyComparable<DbReferenceKeyName>
    {
        /// <inheritdoc/>
        public String ObjectName { get; set; } = string.Empty;

        /// <summary>
        /// Constructor for a blank Database Reference Key
        /// </summary>
        protected internal DbReferenceKeyName() : base() { }

        /// <summary>
        /// Constructor for the Database Reference Key
        /// </summary>
        /// <param name="source"></param>
        public DbReferenceKeyName(IDbReferenceKeyName source) : base(source)
        {
            if (source.ObjectName is string) { ObjectName = source.ObjectName; }
            else { ObjectName = string.Empty; }
        }

        /// <summary>
        /// Constructor for the Database Reference Key
        /// </summary>
        /// <param name="source"></param>
        public DbReferenceKeyName(IDbTableKeyName source) : base(source)
        {
            if (source.TableName is string) { ObjectName = source.TableName; }
            else { ObjectName = string.Empty; }
        }

        /// <summary>
        /// Constructor for the Database Reference Key
        /// </summary>
        /// <param name="source"></param>
        public DbReferenceKeyName(IDbRoutineKeyName source) : base(source)
        {
            if (source.RoutineName is string) { ObjectName = source.RoutineName; }
            else { ObjectName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(DbReferenceKeyName? other)
        {
            return
                other is IDbSchemaKeyName &&
                new DbSchemaKeyName(this).Equals(other) &&
                !string.IsNullOrEmpty(ObjectName) &&
                !string.IsNullOrEmpty(other.ObjectName) &&
                ObjectName.Equals(other.ObjectName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public Boolean Equals(IDbReferenceKeyName? other)
        { return other is IDbReferenceKeyName value && Equals(new DbReferenceKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IDbReferenceKeyName value && Equals(new DbReferenceKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(DbReferenceKeyName? other)
        {
            if (other is null) { return 1; }
            else if (new DbSchemaKeyName(this).CompareTo(other) is int value && value != 0) { return value; }
            else { return string.Compare(ObjectName, other.ObjectName, true); }
        }

        /// <inheritdoc/>
        public Int32 CompareTo(IDbReferenceKeyName? other)
        { if (other is IDbReferenceKeyName value) { return CompareTo(new DbReferenceKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public override Int32 CompareTo(object? obj)
        { if (obj is IDbReferenceKeyName value) { return CompareTo(new DbReferenceKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(DbReferenceKeyName left, DbReferenceKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(DbReferenceKeyName left, DbReferenceKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(DbReferenceKeyName left, DbReferenceKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(DbReferenceKeyName left, DbReferenceKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(DbReferenceKeyName left, DbReferenceKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(DbReferenceKeyName left, DbReferenceKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), ObjectName.GetHashCode(KeyExtension.CompareString)); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        { return DbObjectName.Format(DatabaseName, SchemaName, ObjectName); }
    }
}
