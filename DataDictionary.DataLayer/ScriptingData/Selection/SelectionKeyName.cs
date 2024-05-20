using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ScriptingData.Selection
{
    /// <summary>
    /// Interface for the Scripting Selection Name
    /// </summary>
    public interface ISelectionKeyName : IKey
    {
        /// <summary>
        /// Title of the Selection.
        /// </summary>
        String? SelectionTitle { get; }
    }

    /// <summary>
    /// Implementation for Scripting Selection Name
    /// </summary>
    public class SelectionKeyName : ISelectionKeyName, IKeyComparable<ISelectionKeyName>
    {
        /// <inheritdoc/>
        public String SelectionTitle { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for a blank Scripting Selection Name
        /// </summary>
        protected internal SelectionKeyName() : base() { }

        /// <summary>
        /// Constructor for the Scripting Selection Name.
        /// </summary>
        /// <param name="source"></param>
        public SelectionKeyName(ISelectionKeyName source) : base()
        {
            if (source.SelectionTitle is string) { SelectionTitle = source.SelectionTitle; }
            else { SelectionTitle = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(ISelectionKeyName? other)
        {
            return
                other is ISelectionKeyName &&
                !string.IsNullOrEmpty(SelectionTitle) &&
                !string.IsNullOrEmpty(other.SelectionTitle) &&
                SelectionTitle.Equals(other.SelectionTitle, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is ISelectionKeyName value && Equals(new SelectionKeyName(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(ISelectionKeyName? other)
        {
            if (other is SelectionKeyName value)
            { return string.Compare(SelectionTitle, value.SelectionTitle, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is ISelectionKeyName value) { return CompareTo(new SelectionKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(SelectionKeyName left, SelectionKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(SelectionKeyName left, SelectionKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(SelectionKeyName left, SelectionKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(SelectionKeyName left, SelectionKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(SelectionKeyName left, SelectionKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(SelectionKeyName left, SelectionKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return SelectionTitle.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return SelectionTitle; }
    }
}
