using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.Resource;

namespace DataDictionary.DataLayer.DatabaseData.Constraint
{
    /// <summary>
    /// Interface for the Database Column Reference Key
    /// </summary>
    public interface IDbConstraintColumnKeyName : IDbConstraintKeyObject
    {
        /// <summary>
        /// Reference Column Name
        /// </summary>
        String? ReferenceColumnName { get; }
    }

    /// <summary>
    /// Implementation of the Database Column Reference Key
    /// </summary>
    public class DbConstraintColumnKeyName : DbConstraintKeyObject, IDbConstraintColumnKeyName, IKeyComparable<IDbConstraintColumnKeyName>
    {
        /// <inheritdoc/>
        public string ReferenceColumnName { get; init; } = String.Empty;

        /// <summary>
        /// Constructor for the Database Column Reference Key
        /// </summary>
        /// <param name="source"></param>
        public DbConstraintColumnKeyName(IDbConstraintColumnKeyName source) : base(source)
        {
            if (source.ReferenceColumnName is string) { ReferenceColumnName = source.ReferenceColumnName; }
            else { ReferenceColumnName = string.Empty; }
        }

        /// <summary>
        /// Constructor for the Database Column (Table Column) Reference Key
        /// </summary>
        /// <param name="source"></param>
        public DbConstraintColumnKeyName(IDbTableColumnKeyName source) : base(source)
        {
            if (source.ColumnName is string) { ReferenceColumnName = source.ColumnName; }
            else { ReferenceColumnName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDbConstraintColumnKeyName? other)
        {
            return
                other is IDbConstraintKeyObject &&
                new DbConstraintKeyObject(this).Equals(other) &&
                !string.IsNullOrEmpty(ReferenceColumnName) &&
                !string.IsNullOrEmpty(other.ReferenceColumnName) &&
                ReferenceColumnName.Equals(other.ReferenceColumnName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDbConstraintColumnKeyName value && Equals(new DbConstraintColumnKeyName(value)); }

        /// <inheritdoc/>
        public int CompareTo(IDbConstraintColumnKeyName? other)
        {
            if (other is null) { return 1; }
            else if (new DbConstraintKeyObject(this).CompareTo(other) is int value && value != 0) { return value; }
            else { return string.Compare(ReferenceColumnName, other.ReferenceColumnName, true); }
        }

        /// <inheritdoc/>
        public override int CompareTo(object? obj)
        { if (obj is IDbConstraintColumnKeyName value) { return CompareTo(new DbConstraintColumnKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DbConstraintColumnKeyName left, DbConstraintColumnKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbConstraintColumnKeyName left, DbConstraintColumnKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DbConstraintColumnKeyName left, DbConstraintColumnKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DbConstraintColumnKeyName left, DbConstraintColumnKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DbConstraintColumnKeyName left, DbConstraintColumnKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DbConstraintColumnKeyName left, DbConstraintColumnKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), ReferenceColumnName.GetHashCode(KeyExtension.CompareString)); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (ReferenceColumnName is string)
            { return string.Format("{0}.{1}", base.ToString(), ReferenceColumnName); }
            else { return string.Empty; }
        }
    }
}
