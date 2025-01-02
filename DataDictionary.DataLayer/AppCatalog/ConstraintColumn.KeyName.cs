using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for the Database Constraint Column Key
    /// </summary>
    public interface IConstraintColumnKeyName : IKey, IConstraintKeyName
    {
        /// <summary>
        /// Name of the Database Column
        /// </summary>
        String? ColumnName { get; }
    }

    /// <summary>
    /// Implementation of the Database Constraint Column Key
    /// </summary>
    public class ConstraintColumnKeyName : ConstraintKeyName, IConstraintColumnKeyName,
        IKeyComparable<IConstraintColumnKeyName>, IKeyComparable<ConstraintColumnKeyName>
    {
        /// <inheritdoc/>
        public String ColumnName { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for a blank Database Constraint Column Key
        /// </summary>
        protected internal ConstraintColumnKeyName() : base() { }

        /// <summary>
        /// Constructor for the Database Constraint Column Key
        /// </summary>
        /// <param name="source"></param>
        public ConstraintColumnKeyName(IConstraintColumnKeyName source) : base(source)
        { if (source.ColumnName is string) { ColumnName = source.ColumnName; } }

        #region IEquaConstraint, IComparable
        /// <inheritdoc/>
        public Boolean Equals(ConstraintColumnKeyName? other)
        {
            return
                other is ISchemaKeyName &&
                new ConstraintKeyName(this).Equals(other) &&
                !String.IsNullOrEmpty(ColumnName) &&
                !String.IsNullOrEmpty(other.ColumnName) &&
                ColumnName.Equals(other.ColumnName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public Boolean Equals(IConstraintColumnKeyName? other)
        { return other is IConstraintColumnKeyName value && Equals(new ConstraintColumnKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IConstraintColumnKeyName value && Equals(new ConstraintColumnKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(ConstraintColumnKeyName? other)
        {
            if (other is null) { return 1; }
            else if (new ConstraintKeyName(this).CompareTo(other) is int value && value != 0) { return value; }
            else { return string.Compare(ColumnName, other.ColumnName, true); }
        }

        /// <inheritdoc/>
        public Int32 CompareTo(IConstraintColumnKeyName? other)
        { if (other is IConstraintColumnKeyName value) { return CompareTo(new ConstraintColumnKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public override Int32 CompareTo(object? obj)
        { if (obj is IConstraintColumnKeyName value) { return CompareTo(new ConstraintColumnKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(ConstraintColumnKeyName left, ConstraintColumnKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(ConstraintColumnKeyName left, ConstraintColumnKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(ConstraintColumnKeyName left, ConstraintColumnKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(ConstraintColumnKeyName left, ConstraintColumnKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(ConstraintColumnKeyName left, ConstraintColumnKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(ConstraintColumnKeyName left, ConstraintColumnKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), ColumnName.GetHashCode(KeyExtension.CompareString)); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        { return DbObjectName.Format(DatabaseName, SchemaName, ConstraintName, ColumnName); }
    }
}
