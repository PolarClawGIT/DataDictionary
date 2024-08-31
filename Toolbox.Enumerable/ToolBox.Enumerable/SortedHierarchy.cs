using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolBox.Enumerable
{
    public abstract class HierarchyKey : IComparable<HierarchyKey>, IEquatable<HierarchyKey>
    {
        public Guid Key { get; } = Guid.NewGuid();

        #region IComparable, IEquatable
        /// <inheritdoc/>
        public Boolean Equals(HierarchyKey? other)
        { return other is HierarchyKey && EqualityComparer<Guid?>.Default.Equals(Key, other.Key); }

        /// <inheritdoc/>
        public override Boolean Equals(Object? obj)
        { return Equals(obj as HierarchyKey); }

        /// <inheritdoc/>
        public Int32 CompareTo(HierarchyKey? other)
        {
            if (other is null) { return 1; }
            else { return Key.CompareTo(other.Key); }
        }

        /// <inheritdoc/>
        public static Boolean operator ==(HierarchyKey left, HierarchyKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(HierarchyKey left, HierarchyKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(HierarchyKey left, HierarchyKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(HierarchyKey left, HierarchyKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(HierarchyKey left, HierarchyKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(HierarchyKey left, HierarchyKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(Key); }
        #endregion
    }

    public class SortedHierarchy<TKey, TValue>
        where TKey : class, IComparable<TKey>, IEquatable<TKey>
        where TValue : class
    {
        Func<TValue, TKey> valueToKey { get; init; }

        SortedList<Guid, TValue> dataValues = new SortedList<Guid, TValue>();
        SortedList<Guid, List<Guid>> parentKeys = new SortedList<Guid, List<Guid>>();
        SortedList<TKey, List<Guid>> crossRefrence = new SortedList<TKey, List<Guid>>();

        public SortedHierarchy(Func<TValue, TKey> toKey)
        { valueToKey = toKey; }


        /// <inheritdoc cref="SortedList{TKey, TValue}.Add(TKey, TValue)"/>
        public void Add(TValue value, TKey? parent = null)
        {
            Guid newKey = Guid.NewGuid();
            if (crossRefrence.TryGetValue(key, out _))
            { crossRefrence[key].Add(newKey); }
            else { crossRefrence.Add(key, new List<Guid>() { newKey }); }

            if (parent is not null && crossRefrence.TryGetValue(parent, out List<Guid>? parents))
            { parentKeys.Add(newKey, parents); }
            else { parentKeys.Add(newKey, new List<Guid>()); }

            dataValues.Add(newKey, value);
        }

        public IReadOnlyList<TValue> Parents(TKey key)
        {
            if (crossRefrence.TryGetValue(key, out List<HierarchyKey>? values))
            {
                return values.
                    SelectMany(s => Parents(s)).
                    Select(s => this[s]).
                    ToList();
            }
            else { return new List<TValue>(); }
        }

        public IReadOnlyList<HierarchyKey> Parents(HierarchyKey key)
        {
            if (parentKeys.TryGetValue(key, out List<HierarchyKey>? values))
            { return values; }
            else { return new List<HierarchyKey>(); }
        }


        private new Boolean Remove(HierarchyKey key)
        { throw new NotSupportedException(); }

        public Boolean Remove(TKey key)
        { throw new NotImplementedException(); }

        public new void RemoveAt(Int32 index)
        {
            HierarchyKey target = base.GetKeyAtIndex(index);
            throw new NotImplementedException();
        }

    }
}
