using DataDictionary.DataLayer.ApplicationData.Scope;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.NamedScope
{
    /// <summary>
    /// Interface for NamedScopeData
    /// </summary>
    /// </summary>
    public interface INamedScopeData
    {
        /// <summary>
        /// Gets the element with the specified key.
        /// </summary>
        /// <param name="key">The key of the element to get.</param>
        /// <returns>The element with the specified key.</returns>
        /// <exception cref="System.ArgumentNullException">key is null</exception> 
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">The property is retrieved and key is not found.</exception> 
        INamedScopeValue this[NamedScopeKey key] { get; }

        /// <inheritdoc cref="ICollection.Count"/>
        Int32 Count { get; }

        /// <inheritdoc cref="IDictionary.Keys"/>
        IReadOnlyList<NamedScopeKey> Keys { get; }

        /// <summary>
        /// Returns the list of Root Keys.
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<NamedScopeKey> RootKeys();

        /// <summary>
        /// Returns the list of Child Keys for the specified key or an empty list.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IReadOnlyList<INamedScopeKey> ChildrenKeys(INamedScopeKey key);

        /// <summary>
        /// Returns the list of Parent Keys for the specified key or an empty list.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IReadOnlyList<INamedScopeKey> ParentKeys(INamedScopeKey key);

        /// <summary>
        /// Returns the list of Keys for the specified Path (NameSpace).
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <remarks>This is scan of all item.</remarks>
        IReadOnlyList<INamedScopeKey> PathKeys(INamedScopePath key);

        /// <summary>
        /// Returns the list of Values for the given list of keys or an empty list.
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        IReadOnlyList<INamedScopeValue> Values(IEnumerable<INamedScopeKey> keys);

        /// <summary>
        /// Adds an item to the Collection as a Root node.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="System.ArgumentException">
        /// An element with the same key already exists.
        /// </exception>
        void Add(INamedScopeValue value);

        /// <summary>
        /// Adds an item to the Collection as a Child of the specified Parent.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="value"></param>
        /// <exception cref="NotImplementedException"></exception>
        void Add(NamedScopeKey parent, INamedScopeValue value);

        /// <inheritdoc cref="IList.Clear"/>
        void Clear();

        /// <inheritdoc cref="IDictionary{TKey, TValue}.ContainsKey(TKey)"/>
        Boolean ContainsKey(NamedScopeKey key);

        /// <inheritdoc cref="IDictionary{TKey, TValue}.Remove(TKey)"/>
        Boolean Remove(NamedScopeKey key);
    }

    /// <summary>
    /// Maps a Model Item into a NamedScope item to support hierarchical navigation and lookups for NamedScopePaths.
    /// </summary>
    class NamedScopeData : INamedScopeData
    {
        // Primary Data
        SortedDictionary<NamedScopeKey, INamedScopeValue> data = new SortedDictionary<NamedScopeKey, INamedScopeValue>();

        // Alternate Keys (not sure if Sorted Dictionary or normal Dictionary is better here). Because of the wrapper, it can be changed easy.
        SortedDictionary<NamedScopeKey, List<NamedScopeKey>> children = new SortedDictionary<NamedScopeKey, List<NamedScopeKey>>();
        SortedDictionary<NamedScopeKey, List<NamedScopeKey>> parents = new SortedDictionary<NamedScopeKey, List<NamedScopeKey>>();

        // Root Nodes
        List<NamedScopeKey> roots = new List<NamedScopeKey>();

        /// <inheritdoc/>
        public virtual INamedScopeValue this[NamedScopeKey key]
        { get { return data[key]; } }

        /// <inheritdoc/>
        public virtual IReadOnlyList<NamedScopeKey> Keys { get { return data.Keys.ToList().AsReadOnly(); } }

        /// <inheritdoc/>
        public virtual Int32 Count { get { return data.Count; } }

        /// <inheritdoc/>
        public virtual IReadOnlyList<NamedScopeKey> RootKeys()
        { return roots.AsReadOnly(); }

        /// <inheritdoc/>
        public virtual IReadOnlyList<INamedScopeKey> ChildrenKeys(INamedScopeKey key)
        {
            NamedScopeKey target = new NamedScopeKey(key);
            if (children.ContainsKey(target))
            { return children[target].AsReadOnly(); }
            else { return new List<NamedScopeKey>().AsReadOnly(); }
        }

        /// <inheritdoc/>
        public virtual IReadOnlyList<INamedScopeKey> ParentKeys(INamedScopeKey key)
        {
            NamedScopeKey target = new NamedScopeKey(key);
            if (parents.ContainsKey(target))
            { return parents[target].AsReadOnly(); }
            else { return new List<NamedScopeKey>().AsReadOnly(); }
        }

        /// <summary>
        /// Gets the list of Orphaned NameScopeKeys.
        /// The Key exists in the Parent, Child or Root list but not in the main Data collection.
        /// </summary>
        /// <returns></returns>
        /// <remarks>This is a deep scan and is primary intended as a debugging tool.</remarks>
        public virtual IReadOnlyList<INamedScopeKey> OrphanedKeys()
        {
            List<NamedScopeKey> result = children.SelectMany(s => s.Value).
                Union(children.Select(s => s.Key)).
                Union(parents.SelectMany(s => s.Value)).
                Union(parents.Select(s => s.Key)).
                Union(roots).
                Except(data.Select(s => s.Key)).
                ToList();

            return result;
        }

        /// <inheritdoc/>
        public virtual IReadOnlyList<INamedScopeKey> PathKeys(INamedScopePath key)
        {
            NamedScopePath pathKey = new NamedScopePath(key);
            return data.Where(w => pathKey.Equals(w.Value.GetPath())).Select(s => s.Key).ToList();
        }

        /// <inheritdoc/>
        public virtual IReadOnlyList<INamedScopeValue> Values(IEnumerable<INamedScopeKey> keys)
        {
            List<INamedScopeValue> result = new List<INamedScopeValue>();

            foreach (INamedScopeKey item in keys)
            {
                NamedScopeKey target = new NamedScopeKey(item);
                if (data.ContainsKey(target)) { result.Add(data[target]); }
            }

            return result;
        }

        /// <inheritdoc/>
        public virtual void Add(INamedScopeValue value)
        {
            NamedScopeKey key = value.GetSystemId();

            if (data.ContainsKey(key))
            {
                Exception exception = new ArgumentException("An element with the same key already exists.");
                exception.Data.Add(nameof(value.GetSystemId), key.SystemId);
                throw exception;
            }
            else
            {
                roots.Add(key);
                data.Add(key, value);
            }
        }

        /// <inheritdoc/>
        public virtual void Add(NamedScopeKey parent, INamedScopeValue value)
        {
            //TODO: Need a trap for infinite loop.
            //      It is when the parent (or child) directly or indirectly points to itself.
            //      Not sure how to detect that.

            NamedScopeKey key = value.GetSystemId();
            //NamedScopePath path = value.GetPath();

            if (!children.ContainsKey(parent))
            { children.Add(parent, new List<NamedScopeKey>()); }

            if (!parents.ContainsKey(key))
            { parents.Add(key, new List<NamedScopeKey>()); }

            if (children.ContainsKey(parent) && !children[parent].Contains(key))
            { children[parent].Add(key); }

            if (parents.ContainsKey(key) && !parents[key].Contains(parent))
            { parents[key].Add(parent); }

            if (!data.ContainsKey(key))
            { data.Add(key, value); }
        }

        public virtual void AddRange(IEnumerable<NamedScopePair> source)
        {
            foreach (NamedScopePair item in source)
            {
                if (item.ParentKey is null) { Add(item.Value); }
                else { Add(item.ParentKey, item.Value); }
            }
        }

        /// <inheritdoc/>
        public virtual void Clear()
        {
            data.Clear();
            children.Clear();
            parents.Clear();
            roots.Clear();
        }

        /// <inheritdoc/>
        public virtual Boolean ContainsKey(NamedScopeKey key)
        { return data.ContainsKey(key); }

        /// <inheritdoc/>
        public virtual Boolean Remove(NamedScopeKey key)
        {
            if (roots.Contains(key))
            { roots.Remove(key); }

            if (children.ContainsKey(key))
            { children.Remove(key); }

            if (parents.ContainsKey(key))
            { parents.Remove(key); }

            return data.Remove(key);
        }
    }
}
