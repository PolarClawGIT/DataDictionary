using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.Resource;

namespace DataDictionary.DataLayer.DatabaseData.Constraint
{
    /// <summary>
    /// Interface for the Database Column Reference Key
    /// </summary>
    public interface IDbConstraintColumnKeyReferenced : IDbConstraintKeyReferenced
    {
        /// <summary>
        /// Reference Column Name
        /// </summary>
        String? ReferencedColumnName { get; }
    }

    /// <summary>
    /// Implementation of the Database Column Reference Key
    /// </summary>
    public class DbConstraintColumnKeyReferenced : DbConstraintKeyReferenced, IDbConstraintColumnKeyReferenced,
        IKeyComparable<IDbConstraintColumnKeyReferenced>, IKeyEquality<IDbTableColumnKeyName>
    {
        /// <inheritdoc/>
        public String ReferencedColumnName { get; init; } = String.Empty;

        /// <summary>
        /// Constructor for the Database Column Reference Key
        /// </summary>
        /// <param name="source"></param>
        public DbConstraintColumnKeyReferenced(IDbConstraintColumnKeyReferenced source) : base(source)
        {
            if (source.ReferencedColumnName is string) { ReferencedColumnName = source.ReferencedColumnName; }
            else { ReferencedColumnName = string.Empty; }
        }

        /// <summary>
        /// Converts Constraint Column Referenced Key into a Table Column Key.
        /// </summary>
        /// <returns></returns>
        public virtual DbTableColumnKeyName AsColumnName()
        {
            return new DbTableColumnKeyName()
            {
                DatabaseName = this.DatabaseName,
                SchemaName = this.ReferencedSchemaName,
                TableName = this.ReferencedTableName,
                ColumnName = this.ReferencedColumnName
            };
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(IDbConstraintColumnKeyReferenced? other)
        {
            return
                other is IDbConstraintKeyReferenced &&
                new DbConstraintKeyReferenced(this).Equals(other) &&
                !string.IsNullOrEmpty(ReferencedColumnName) &&
                !string.IsNullOrEmpty(other.ReferencedColumnName) &&
                ReferencedColumnName.Equals(other.ReferencedColumnName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public Boolean Equals(IDbTableColumnKeyName? other)
        {
            return
                other is IDbTableColumnKeyName &&
                new DbConstraintKeyReferenced(this).Equals(other) &&
                !string.IsNullOrEmpty(ReferencedColumnName) &&
                !string.IsNullOrEmpty(other.ColumnName) &&
                ReferencedColumnName.Equals(other.ColumnName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IDbConstraintColumnKeyReferenced value && Equals(new DbConstraintColumnKeyReferenced(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(IDbConstraintColumnKeyReferenced? other)
        {
            if (other is null) { return 1; }
            else if (new DbConstraintKeyReferenced(this).CompareTo(other) is int value && value != 0) { return value; }
            else { return string.Compare(ReferencedColumnName, other.ReferencedColumnName, true); }
        }

        /// <inheritdoc/>
        public override Int32 CompareTo(object? obj)
        { if (obj is IDbConstraintColumnKeyReferenced value) { return CompareTo(new DbConstraintColumnKeyReferenced(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(DbConstraintColumnKeyReferenced left, DbConstraintColumnKeyReferenced right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(DbConstraintColumnKeyReferenced left, DbConstraintColumnKeyReferenced right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(DbConstraintColumnKeyReferenced left, DbConstraintColumnKeyReferenced right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(DbConstraintColumnKeyReferenced left, DbConstraintColumnKeyReferenced right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(DbConstraintColumnKeyReferenced left, DbConstraintColumnKeyReferenced right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(DbConstraintColumnKeyReferenced left, DbConstraintColumnKeyReferenced right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), ReferencedColumnName.GetHashCode(KeyExtension.CompareString)); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        { return DbObjectName.Format(DatabaseName, ReferencedSchemaName, ReferencedTableName, ReferencedColumnName); }
    }
}
