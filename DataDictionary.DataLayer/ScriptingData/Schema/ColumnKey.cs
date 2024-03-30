using DataDictionary.DataLayer.ApplicationData.Scope;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ScriptingData.Schema
{
    /// <summary>
    /// Interface for the Scripting Schema Column key.
    /// </summary>
    public interface IColumnKey : IScopeKeyName
    {
        /// <inheritdoc cref="DataColumn.ColumnName"/>
        String? ColumnName { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Schema Column key.
    /// </summary>
    public class ColumnKey : ScopeKeyName, IColumnKey, IKeyComparable<IColumnKey>
    {
        /// <inheritdoc/>
        public String ColumnName { get; init; } = string.Empty;


        /// <summary>
        /// Constructor for a blank Catalog Key
        /// </summary>
        protected internal ColumnKey() : base() { }

        /// <summary>
        /// Constructor for the Catalog Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public ColumnKey(IColumnKey source) : base(source)
        {
            if (source.ColumnName is string) { ColumnName = source.ColumnName; }
            else { ColumnName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(IColumnKey? other)
        {
            return
                other is IColumnKey &&
                new ScopeKeyName(this).Equals(other) &&
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
            else if (new ScopeKeyName(this).CompareTo(other) is int value && value != 0) { return value; }
            else { return string.Compare(ColumnName, other.ColumnName, true); }
        }

        /// <inheritdoc/>
        public override int CompareTo(object? obj)
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
        { return String.Format("{0}: {1}",base.ToString(),ColumnName); }
    }
}
