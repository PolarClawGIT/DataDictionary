using DataDictionary.DataLayer;
using DataDictionary.DataLayer.ApplicationData.Scope;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <summary>
    /// Interface for the Scripting Schema Column key.
    /// </summary>
    public interface IColumnIndex : IScopeKey
    {
        /// <summary>
        /// Name of the Column/Property to map too.
        /// </summary>
        String? ColumnName { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Schema Column key.
    /// </summary>
    public class ColumnIndex : IColumnIndex, IKeyComparable<IColumnIndex>
    {
        /// <inheritdoc/>
        public String ColumnName { get; init; } = string.Empty;

        /// <inheritdoc/>
        public ScopeType Scope { get; init; } = ScopeType.Null;

        /// <summary>
        /// Constructor for a blank Catalog Key
        /// </summary>
        protected internal ColumnIndex() : base() { }


        /// <summary>
        /// Constructor for the Catalog Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public ColumnIndex(IColumnIndex source) : this()
        {
            Scope = source.Scope;

            if (source.ColumnName is string) { ColumnName = source.ColumnName; }
            else { ColumnName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(IColumnIndex? other)
        {
            return
                other is IColumnIndex &&
                Scope.Equals(other.Scope) &&
                !string.IsNullOrEmpty(ColumnName) &&
                !string.IsNullOrEmpty(other.ColumnName) &&
                ColumnName.Equals(other.ColumnName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IColumnIndex value && Equals(new ColumnIndex(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(IColumnIndex? other)
        {
            if (other is null) { return 1; }
            else if (Scope.CompareTo(other.Scope) is int value && value != 0) { return value; }
            else { return String.Compare(ColumnName, other.ColumnName, true); }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IColumnIndex value) { return CompareTo(new ColumnIndex(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(ColumnIndex left, ColumnIndex right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(ColumnIndex left, ColumnIndex right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(ColumnIndex left, ColumnIndex right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(ColumnIndex left, ColumnIndex right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(ColumnIndex left, ColumnIndex right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(ColumnIndex left, ColumnIndex right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), ColumnName.GetHashCode(KeyExtension.CompareString)); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return String.Format("{0}: {1}", Scope.ToName(), ColumnName); }
    }
}
