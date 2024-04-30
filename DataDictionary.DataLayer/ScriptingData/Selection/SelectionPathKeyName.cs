using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ScriptingData.Selection
{
    /// <summary>
    /// Interface for the Scripting Selection Path Key
    /// </summary>
    public interface ISelectionPathKeyName : IKey
    {
        /// <summary>
        /// Name of the Instance.
        /// </summary>
        String? InstanceName { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Selection Path Key
    /// </summary>
    public class SelectionPathKeyName : ISelectionPathKeyName, IKeyComparable<ISelectionPathKeyName>
    {
        /// <inheritdoc/>
        public String InstanceName { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the Scripting Selection Path Key
        /// </summary>
        protected SelectionPathKeyName() : base() { }

        /// <summary>
        /// Constructor for the Scripting Selection Path Key
        /// </summary>
        /// <param name="source"></param>
        public SelectionPathKeyName(ISelectionPathKeyName source) : this()
        {
            if (source.InstanceName is string) { InstanceName = source.InstanceName; }
            else { InstanceName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(ISelectionPathKeyName? other)
        {
            return
                other is ISelectionPathKeyName &&
                !string.IsNullOrEmpty(InstanceName) &&
                !string.IsNullOrEmpty(other.InstanceName) &&
                InstanceName.Equals(other.InstanceName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is ISelectionPathKeyName value && Equals(new SelectionPathKeyName(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(ISelectionPathKeyName? other)
        {
            if (other is null) { return 1; }
            else { return string.Compare(InstanceName, other.InstanceName, true); }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is ISelectionPathKeyName value) { return CompareTo(new SelectionPathKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(SelectionPathKeyName left, SelectionPathKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(SelectionPathKeyName left, SelectionPathKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(SelectionPathKeyName left, SelectionPathKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(SelectionPathKeyName left, SelectionPathKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(SelectionPathKeyName left, SelectionPathKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(SelectionPathKeyName left, SelectionPathKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return InstanceName.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (InstanceName is string) { return InstanceName; }
            else { return string.Empty; }
        }
    }
}
