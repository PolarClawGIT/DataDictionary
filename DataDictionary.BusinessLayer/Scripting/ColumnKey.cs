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
    public interface IColumnKey : IScopeKey
    {
        /// <summary>
        /// Name of the Column/Property to map too.
        /// </summary>
        String? ColumnName { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Schema Column key.
    /// </summary>
    public class ColumnKey : IColumnKey, IKeyComparable<IColumnKey>
    {
        /// <inheritdoc/>
        public String ColumnName { get; init; } = string.Empty;

        /// <inheritdoc/>
        public ScopeType Scope { get; init; } = ScopeType.Null;

        /// <summary>
        /// Constructor for a blank Catalog Key
        /// </summary>
        protected internal ColumnKey() : base() { }


        /// <summary>
        /// Constructor for the Catalog Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public ColumnKey(IColumnKey source) : this()
        {
            Scope = source.Scope;

            if (source.ColumnName is string) { ColumnName = source.ColumnName; }
            else { ColumnName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(IColumnKey? other)
        {
            return
                other is IColumnKey &&
                Scope.Equals(other.Scope) &&
                !string.IsNullOrEmpty(ColumnName) &&
                !string.IsNullOrEmpty(other.ColumnName) &&
                ColumnName.Equals(other.ColumnName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IColumnKey value && Equals(new ColumnKey(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(IColumnKey? other)
        {
            if (other is null) { return 1; }
            else if (Scope.CompareTo(other.Scope) is int value && value != 0) { return value; }
            else { return String.Compare(ColumnName, other.ColumnName, true); }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IColumnKey value) { return CompareTo(new ColumnKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(ColumnKey left, ColumnKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(ColumnKey left, ColumnKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(ColumnKey left, ColumnKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(ColumnKey left, ColumnKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(ColumnKey left, ColumnKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(ColumnKey left, ColumnKey right)
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
