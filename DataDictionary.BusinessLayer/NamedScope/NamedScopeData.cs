﻿using DataDictionary.DataLayer.ApplicationData.Scope;
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
    public interface INamedScopeData
    {
        /// <summary>
        /// Gets the element with the specified key.
        /// </summary>
        /// <param name="key">The key of the element to get.</param>
        /// <returns>The element with the specified key.</returns>
        /// <exception cref="System.ArgumentNullException">key is null</exception> 
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">The property is retrieved and key is not found.</exception> 
        INamedScopeValue this[NamedScopeIndex key] { get; }

        /// <inheritdoc cref="ICollection.Count"/>
        Int32 Count { get; }

        /// <inheritdoc cref="IDictionary.Keys"/>
        IReadOnlyList<NamedScopeIndex> Keys { get; }

        /// <summary>
        /// Returns the list of Root Keys.
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<NamedScopeIndex> RootKeys();

        /// <summary>
        /// Returns the list of Child Keys for the specified key or an empty list.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IReadOnlyList<INamedScopeIndex> ChildrenKeys(INamedScopeIndex key);

        /// <summary>
        /// Returns the list of Parent Keys for the specified key or an empty list.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IReadOnlyList<INamedScopeIndex> ParentKeys(INamedScopeIndex key);

        /// <summary>
        /// Returns the list of Keys for the specified Path (NameSpace).
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <remarks>This is scan of all item.</remarks>
        IReadOnlyList<INamedScopeIndex> PathKeys(INamedScopePath key);

        /// <summary>
        /// Returns the list of Values for the given list of keys or an empty list.
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        IReadOnlyList<INamedScopeValue> Values(IEnumerable<INamedScopeIndex> keys);

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
        void Add(NamedScopeIndex parent, INamedScopeValue value);

        /// <inheritdoc cref="IList.Clear"/>
        void Clear();

        /// <inheritdoc cref="IDictionary{TKey, TValue}.ContainsKey(TKey)"/>
        Boolean ContainsKey(NamedScopeIndex key);

        /// <inheritdoc cref="IDictionary{TKey, TValue}.Remove(TKey)"/>
        Boolean Remove(NamedScopeIndex key);
    }

    /// <summary>
    /// Maps a Model Item into a NamedScope item to support hierarchical navigation and lookups for NamedScopePaths.
    /// </summary>
    class NamedScopeData : INamedScopeData
    {
        // Primary Data
        SortedDictionary<NamedScopeIndex, INamedScopeValue> data = new SortedDictionary<NamedScopeIndex, INamedScopeValue>();

        // Alternate Keys (not sure if Sorted Dictionary or normal Dictionary is better here). Because of the wrapper, it can be changed easy.
        SortedDictionary<NamedScopeIndex, List<NamedScopeIndex>> children = new SortedDictionary<NamedScopeIndex, List<NamedScopeIndex>>();
        SortedDictionary<NamedScopeIndex, List<NamedScopeIndex>> parents = new SortedDictionary<NamedScopeIndex, List<NamedScopeIndex>>();

        // Root Nodes
        List<NamedScopeIndex> roots = new List<NamedScopeIndex>();

        /// <inheritdoc/>
        public virtual INamedScopeValue this[NamedScopeIndex key]
        { get { return data[key]; } }

        /// <inheritdoc/>
        public virtual IReadOnlyList<NamedScopeIndex> Keys { get { return data.Keys.ToList().AsReadOnly(); } }

        /// <inheritdoc/>
        public virtual Int32 Count { get { return data.Count; } }

        /// <inheritdoc/>
        public virtual IReadOnlyList<NamedScopeIndex> RootKeys()
        { return roots.AsReadOnly(); }

        /// <inheritdoc/>
        public virtual IReadOnlyList<INamedScopeIndex> ChildrenKeys(INamedScopeIndex key)
        {
            NamedScopeIndex target = new NamedScopeIndex(key);
            if (children.ContainsKey(target))
            { return children[target].AsReadOnly(); }
            else { return new List<NamedScopeIndex>().AsReadOnly(); }
        }

        /// <inheritdoc/>
        public virtual IReadOnlyList<INamedScopeIndex> ParentKeys(INamedScopeIndex key)
        {
            NamedScopeIndex target = new NamedScopeIndex(key);
            if (parents.ContainsKey(target))
            { return parents[target].AsReadOnly(); }
            else { return new List<NamedScopeIndex>().AsReadOnly(); }
        }

        /// <summary>
        /// Gets the list of Orphaned NameScopeKeys.
        /// The Key exists in the Parent, Child or Root list but not in the main Data collection.
        /// </summary>
        /// <returns></returns>
        /// <remarks>This is a deep scan and is primary intended as a debugging tool.</remarks>
        public virtual IReadOnlyList<INamedScopeIndex> OrphanedKeys()
        {
            List<NamedScopeIndex> result = children.SelectMany(s => s.Value).
                Union(children.Select(s => s.Key)).
                Union(parents.SelectMany(s => s.Value)).
                Union(parents.Select(s => s.Key)).
                Union(roots).
                Except(data.Select(s => s.Key)).
                ToList();

            return result;
        }

        /// <inheritdoc/>
        public virtual IReadOnlyList<INamedScopeIndex> PathKeys(INamedScopePath key)
        {
            NamedScopePath pathKey = new NamedScopePath(key);
            return data.Where(w => pathKey.Equals(w.Value.GetPath())).Select(s => s.Key).ToList();
        }

        /// <inheritdoc/>
        public virtual IReadOnlyList<INamedScopeValue> Values(IEnumerable<INamedScopeIndex> keys)
        {
            List<INamedScopeValue> result = new List<INamedScopeValue>();

            foreach (INamedScopeIndex item in keys)
            {
                NamedScopeIndex target = new NamedScopeIndex(item);
                if (data.ContainsKey(target)) { result.Add(data[target]); }
            }

            return result;
        }

        /// <inheritdoc/>
        public virtual void Add(INamedScopeValue value)
        {
            NamedScopeIndex key = value.GetKey();

            if (data.ContainsKey(key))
            {
                Exception exception = new ArgumentException("An element with the same key already exists.");
                exception.Data.Add(nameof(value.GetKey), key.SystemId);
                throw exception;
            }
            else
            {
                roots.Add(key);
                data.Add(key, value);
            }
        }

        /// <inheritdoc/>
        public virtual void Add(NamedScopeIndex parent, INamedScopeValue value)
        {
            //TODO: Need a trap for infinite loop.
            //      It is when the parent (or child) directly or indirectly points to itself.
            //      Not sure how to detect that.

            NamedScopeIndex key = value.GetKey();
            //NamedScopePath path = value.GetPath();

            if (!children.ContainsKey(parent))
            { children.Add(parent, new List<NamedScopeIndex>()); }

            if (!parents.ContainsKey(key))
            { parents.Add(key, new List<NamedScopeIndex>()); }

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
        public virtual Boolean ContainsKey(NamedScopeIndex key)
        { return data.ContainsKey(key); }

        /// <inheritdoc/>
        public virtual Boolean Remove(NamedScopeIndex key)
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
