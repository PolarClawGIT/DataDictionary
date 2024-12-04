using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for the Database Column Reference Key
    /// </summary>
    public interface IConstraintColumnKeyReferenced : IConstraintKeyReferenced
    {
        /// <summary>
        /// Reference Column Name
        /// </summary>
        String? ReferencedColumnName { get; }
    }

    /// <summary>
    /// Implementation of the Database Column Reference Key
    /// </summary>
    public class ConstraintColumnKeyReferenced : ConstraintKeyReferenced, IConstraintColumnKeyReferenced,
        IKeyComparable<IConstraintColumnKeyReferenced>, IKeyEquality<ITableColumnKeyName>
    {
        /// <inheritdoc/>
        public String ReferencedColumnName { get; init; } = String.Empty;

        /// <summary>
        /// Constructor for the Database Column Reference Key
        /// </summary>
        /// <param name="source"></param>
        public ConstraintColumnKeyReferenced(IConstraintColumnKeyReferenced source) : base(source)
        {
            if (source.ReferencedColumnName is string) { ReferencedColumnName = source.ReferencedColumnName; }
            else { ReferencedColumnName = string.Empty; }
        }

        /// <summary>
        /// Converts Constraint Column Referenced Key into a Table Column Key.
        /// </summary>
        /// <returns></returns>
        public virtual TableColumnKeyName AsColumnName()
        {
            return new TableColumnKeyName()
            {
                DatabaseName = DatabaseName,
                SchemaName = ReferencedSchemaName,
                TableName = ReferencedTableName,
                ColumnName = ReferencedColumnName
            };
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(IConstraintColumnKeyReferenced? other)
        {
            return
                other is IConstraintKeyReferenced &&
                new ConstraintKeyReferenced(this).Equals(other) &&
                !string.IsNullOrEmpty(ReferencedColumnName) &&
                !string.IsNullOrEmpty(other.ReferencedColumnName) &&
                ReferencedColumnName.Equals(other.ReferencedColumnName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public Boolean Equals(ITableColumnKeyName? other)
        {
            return
                other is ITableColumnKeyName &&
                new ConstraintKeyReferenced(this).Equals(other) &&
                !string.IsNullOrEmpty(ReferencedColumnName) &&
                !string.IsNullOrEmpty(other.ColumnName) &&
                ReferencedColumnName.Equals(other.ColumnName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IConstraintColumnKeyReferenced value && Equals(new ConstraintColumnKeyReferenced(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(IConstraintColumnKeyReferenced? other)
        {
            if (other is null) { return 1; }
            else if (new ConstraintKeyReferenced(this).CompareTo(other) is int value && value != 0) { return value; }
            else { return string.Compare(ReferencedColumnName, other.ReferencedColumnName, true); }
        }

        /// <inheritdoc/>
        public override Int32 CompareTo(object? obj)
        { if (obj is IConstraintColumnKeyReferenced value) { return CompareTo(new ConstraintColumnKeyReferenced(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(ConstraintColumnKeyReferenced left, ConstraintColumnKeyReferenced right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(ConstraintColumnKeyReferenced left, ConstraintColumnKeyReferenced right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(ConstraintColumnKeyReferenced left, ConstraintColumnKeyReferenced right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(ConstraintColumnKeyReferenced left, ConstraintColumnKeyReferenced right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(ConstraintColumnKeyReferenced left, ConstraintColumnKeyReferenced right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(ConstraintColumnKeyReferenced left, ConstraintColumnKeyReferenced right)
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
