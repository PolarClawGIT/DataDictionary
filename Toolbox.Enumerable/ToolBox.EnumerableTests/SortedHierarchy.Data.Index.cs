using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolBox.EnumerableTests
{
    interface ISortedHierarchyIndex
    {
        String NameKey { get; }
    }

    class SortedHierarchyIndex : ISortedHierarchyIndex, IComparable<SortedHierarchyIndex>, IEquatable<SortedHierarchyIndex>
    {
        public String NameKey { get; init; } = String.Empty;

        public SortedHierarchyIndex(ISortedHierarchyIndex source)
        { NameKey = source.NameKey; }

        #region IComparable, IEquatable
        /// <inheritdoc/>
        public Boolean Equals(SortedHierarchyIndex? other)
        { return other is SortedHierarchyIndex && String.Equals(NameKey, other.NameKey, StringComparison.OrdinalIgnoreCase); }

        /// <inheritdoc/>
        public override Boolean Equals(Object? obj)
        { return Equals(obj as SortedHierarchyIndex); }

        /// <inheritdoc/>
        public Int32 CompareTo(SortedHierarchyIndex? other)
        {
            if (other is null) { return 1; }
            else { return String.Compare(NameKey, other.NameKey, StringComparison.OrdinalIgnoreCase); }
        }

        /// <inheritdoc/>
        public static Boolean operator ==(SortedHierarchyIndex left, SortedHierarchyIndex right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(SortedHierarchyIndex left, SortedHierarchyIndex right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(SortedHierarchyIndex left, SortedHierarchyIndex right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(SortedHierarchyIndex left, SortedHierarchyIndex right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(SortedHierarchyIndex left, SortedHierarchyIndex right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(SortedHierarchyIndex left, SortedHierarchyIndex right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(NameKey); }
        #endregion

        public override String ToString()
        { return NameKey; }
    }
}
