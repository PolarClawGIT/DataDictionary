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
    /// Interface for objects that contain NameScope Data
    /// </summary>
    [Obsolete("Needs to be changed over to new version of NamedScope")]
    public interface INamedScopeData
    {
        /// <summary>
        /// Create WorkItems to build the NamedScope Dictionary.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Build(INamedScopeDictionary target);
    }

    /// <summary>
    /// Maps a Model Item into a NamedScope item to support hierarchical navigation and lookups for NamedScopePaths.
    /// </summary>
    public class NamedScopeData
    {
        // Primary Data
        SortedDictionary<NamedScopeKey, INamedScopeValue> data = new SortedDictionary<NamedScopeKey, INamedScopeValue>();

        // Alternate Keys (not sure if Sorted Dictionary or normal Dictionary is better here). Because of the wrapper, it can be changed easy.
        SortedDictionary<NamedScopeKey, List<NamedScopeKey>> children = new SortedDictionary<NamedScopeKey, List<NamedScopeKey>>();
        SortedDictionary<NamedScopeKey, List<NamedScopeKey>> parents = new SortedDictionary<NamedScopeKey, List<NamedScopeKey>>();
        SortedDictionary<NamedScopePath, List<NamedScopeKey>> paths = new SortedDictionary<NamedScopePath, List<NamedScopeKey>>();

        // Root Nodes
        List<NamedScopeKey> roots = new List<NamedScopeKey>();

        /// <summary>
        /// Gets the element with the specified key.
        /// </summary>
        /// <param name="key">The key of the element to get.</param>
        /// <returns>The element with the specified key.</returns>
        /// <exception cref="System.ArgumentNullException">key is null</exception> 
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">The property is retrieved and key is not found.</exception> 
        public virtual INamedScopeValue this[NamedScopeKey key]
        { get { return data[key]; } }


        /// <inheritdoc cref="IDictionary.Keys"/>
        public virtual ICollection<NamedScopeKey> Keys { get { return data.Keys; } }

        /// <inheritdoc cref="ICollection.Count"/>
        public virtual Int32 Count { get { return data.Count; } }

        /// <summary>
        /// Returns the list of Root Keys.
        /// </summary>
        /// <returns></returns>
        public virtual IReadOnlyList<NamedScopeKey> RootKeys()
        { return roots.AsReadOnly(); }

        /// <summary>
        /// Returns the list of Child Keys for the specified key or an empty list.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual IReadOnlyList<NamedScopeKey> ChildrenKeys(NamedScopeKey key)
        {
            if (children.ContainsKey(key))
            { return children[key].AsReadOnly(); }
            else { return new List<NamedScopeKey>().AsReadOnly(); }
        }

        /// <summary>
        /// Returns the list of Parent Keys for the specified key or an empty list.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual IReadOnlyList<NamedScopeKey> ParentKeys(NamedScopeKey key)
        {
            if (parents.ContainsKey(key))
            { return parents[key].AsReadOnly(); }
            else { return new List<NamedScopeKey>().AsReadOnly(); }
        }

        /// <summary>
        /// Returns the list of keys for the specified path key or an empty list.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual IReadOnlyList<NamedScopeKey> PathKeys(NamedScopePath key)
        {
            if (paths.ContainsKey(key))
            { return paths[key].AsReadOnly(); }
            else { return new List<NamedScopeKey>().AsReadOnly(); }
        }

        /// <summary>
        /// Returns the list of Values for the given list of keys or an empty list.
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public virtual IReadOnlyList<INamedScopeValue> Values(IEnumerable<NamedScopeKey> keys)
        {
            List<INamedScopeValue> result = new List<INamedScopeValue>();

            foreach (NamedScopeKey item in keys)
            { if (data.ContainsKey(item)) { result.Add(data[item]); } }

            return result;
        }

        /// <summary>
        /// Adds an item to the Collection as a Root node.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="System.ArgumentException">
        /// An element with the same key already exists.
        /// </exception>
        public virtual void Add(INamedScopeValue value)
        {
            NamedScopeKey key = value.GetSystemId();
            NamedScopePath path = value.GetPath();

            if (data.ContainsKey(key))
            {
                Exception exception = new ArgumentException("An element with the same key already exists.");
                exception.Data.Add(nameof(value.GetSystemId), key.SystemId);
                throw exception;
            }
            else
            {
                roots.Add(key);

                if (!paths.ContainsKey(path))
                { paths.Add(path, new List<NamedScopeKey>()); }

                if (paths.ContainsKey(path) && !paths[path].Contains(key))
                { paths[path].Add(key); }

                value.OnTitleChanged += OnTitleChanged;
                data.Add(key, value);
            }
        }

        /// <summary>
        /// Adds an item to the Collection as a Child of the specified Parent.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="value"></param>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void Add(NamedScopeKey parent, INamedScopeValue value)
        {
            NamedScopeKey key = value.GetSystemId();
            NamedScopePath path = value.GetPath();

            if (data.ContainsKey(key))
            {
                Exception exception = new ArgumentException("An element with the same key already exists.");
                exception.Data.Add(nameof(value.GetSystemId), key.SystemId);
                throw exception;
            }

            if (!children.ContainsKey(parent))
            { children.Add(parent, new List<NamedScopeKey>()); }

            if (!parents.ContainsKey(key))
            { parents.Add(key, new List<NamedScopeKey>()); }

            if (!paths.ContainsKey(path))
            { paths.Add(path, new List<NamedScopeKey>()); }

            if (children.ContainsKey(parent) && !children[parent].Contains(key))
            { children[parent].Add(key); }

            if (parents.ContainsKey(key) && !parents[key].Contains(parent))
            { parents[key].Add(parent); }

            if (paths.ContainsKey(path) && !paths[path].Contains(key))
            { paths[path].Add(key); }

            value.OnTitleChanged += OnTitleChanged;
            data.Add(key, value);
        }

        /// <inheritdoc cref="IList.Clear"/>
        public virtual void Clear()
        {
            data.Clear();
            children.Clear();
            parents.Clear();
            roots.Clear();
        }

        /// <inheritdoc cref="IDictionary{TKey, TValue}.ContainsKey(TKey)"/>
        public virtual Boolean ContainsKey(NamedScopeKey key)
        { return data.ContainsKey(key); }

        /// <inheritdoc cref="IDictionary{TKey, TValue}.Remove(TKey)"/>
        public virtual Boolean Remove(NamedScopeKey key)
        {
            if (roots.Contains(key))
            { roots.Remove(key); }

            if (children.ContainsKey(key))
            { children.Remove(key); }

            if (parents.ContainsKey(key))
            { parents.Remove(key); }

            foreach (KeyValuePair<NamedScopePath, List<NamedScopeKey>> item in paths.Where(w => w.Value.Contains(key)).ToList())
            { //TODO: This may be costly. Normally there is only one item that matches but all paths must be searched.
                item.Value.Remove(key);

                if (item.Value.Count == 0)
                { paths.Remove(item.Key); }
            }

            if (data.ContainsKey(key))
            { data[key].OnTitleChanged -= OnTitleChanged; }

            return data.Remove(key);
        }

        private void OnTitleChanged(Object? sender, EventArgs e)
        {
            if(sender is INamedScopeValue value)
            {
                NamedScopeKey key = value.GetSystemId();
                NamedScopePath path = value.GetPath();

                foreach (KeyValuePair<NamedScopePath, List<NamedScopeKey>> item in paths.Where(w => w.Value.Contains(key)).ToList())
                { //TODO: This may be costly. Normally there is only one item that matches but all paths must be searched.
                    item.Value.Remove(key);

                    if (item.Value.Count == 0)
                    { paths.Remove(item.Key); }
                }

                if (!paths.ContainsKey(path))
                { paths.Add(path, new List<NamedScopeKey>()); }

                if (paths.ContainsKey(path) && !paths[path].Contains(key))
                { paths[path].Add(key); }
            }
        }
    }
}
