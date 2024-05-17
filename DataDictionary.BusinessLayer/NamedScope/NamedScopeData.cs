// Ignore Spelling: indices

using DataDictionary.DataLayer.ApplicationData.Scope;
using System.Collections;

namespace DataDictionary.BusinessLayer.NamedScope
{
    /// <summary>
    /// Interface for NamedScopeData
    /// </summary>
    public interface INamedScopeData
    {
        /// <summary>
        /// Gets the NamedScopeValue for the specified Index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        INamedScopeValue GetValue(NamedScopeIndex index);

        /// <summary>
        /// Gets the Data for the specified index;
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        INamedScopeSourceValue GetData(NamedScopeIndex index);

        /// <inheritdoc cref="ICollection.Count"/>
        Int32 Count { get; }

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
        IReadOnlyList<NamedScopeIndex> ChildrenKeys(NamedScopeIndex key);

        /// <summary>
        /// Returns the list of Parent Keys for the specified key or an empty list.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IReadOnlyList<NamedScopeIndex> ParentKeys(NamedScopeIndex key);

        /// <summary>
        /// Returns the list of Keys for the specified Path (NameSpace).
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <remarks>This is scan of all item.</remarks>
        IReadOnlyList<NamedScopeIndex> PathKeys(INamedScopePath key);

        /// <summary>
        /// Returns the list of Values for the given list of keys or an empty list.
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        IReadOnlyList<INamedScopeSourceValue> Values(IEnumerable<NamedScopeIndex> keys);

        /// <inheritdoc cref="IDictionary{TKey, TValue}.ContainsKey(TKey)"/>
        Boolean ContainsKey(NamedScopeIndex key);
    }

    /// <summary>
    /// Maps a Model Item into a NamedScope item to support hierarchical navigation and lookups for NamedScopePaths.
    /// </summary>
    class NamedScopeData : INamedScopeData
    {
        // Primary Data
        SortedDictionary<NamedScopeIndex, NamedScopeValueCore> data = new SortedDictionary<NamedScopeIndex, NamedScopeValueCore>();

        // Alternate Keys (not sure if Sorted Dictionary or normal Dictionary is better here). Because of the wrapper, it can be changed easy.
        SortedDictionary<NamedScopeIndex, List<NamedScopeIndex>> children = new SortedDictionary<NamedScopeIndex, List<NamedScopeIndex>>();
        SortedDictionary<NamedScopeIndex, List<NamedScopeIndex>> parents = new SortedDictionary<NamedScopeIndex, List<NamedScopeIndex>>();

        // Root Nodes
        List<NamedScopeIndex> roots = new List<NamedScopeIndex>();

        /// <inheritdoc/>
        public virtual INamedScopeValue GetValue(NamedScopeIndex index)
        { return data[index]; }

        /// <inheritdoc/>
        public virtual INamedScopeSourceValue GetData(NamedScopeIndex index)
        {
            return data[index].Source;
        }

        /// <inheritdoc/>
        public virtual Int32 Count { get { return data.Count; } }

        /// <inheritdoc/>
        public virtual IReadOnlyList<NamedScopeIndex> RootKeys()
        { return roots.AsReadOnly(); }

        /// <inheritdoc/>
        public virtual IReadOnlyList<NamedScopeIndex> ChildrenKeys(NamedScopeIndex index)
        {
            if (children.ContainsKey(index))
            { return children[index].AsReadOnly(); }
            else { return new List<NamedScopeIndex>().AsReadOnly(); }
        }

        /// <inheritdoc/>
        public virtual IReadOnlyList<NamedScopeIndex> ParentKeys(NamedScopeIndex index)
        {
            if (parents.ContainsKey(index))
            { return parents[index].AsReadOnly(); }
            else { return new List<NamedScopeIndex>().AsReadOnly(); }
        }

        /// <summary>
        /// Gets the list of Orphaned NameScopeKeys.
        /// The Key exists in the Parent, Child or Root list but not in the main Data collection.
        /// </summary>
        /// <returns></returns>
        /// <remarks>This is a deep scan and is primary intended as a debugging tool.</remarks>
        public virtual IReadOnlyList<NamedScopeIndex> OrphanedKeys()
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
        public virtual IReadOnlyList<NamedScopeIndex> PathKeys(INamedScopePath key)
        {
            NamedScopePath pathKey = new NamedScopePath(key);
            return data.Where(w => pathKey.Equals(w.Value.NamedPath)).Select(s => s.Key).ToList();
        }

        /// <inheritdoc/>
        public virtual IReadOnlyList<INamedScopeSourceValue> Values(IEnumerable<NamedScopeIndex> indices)
        {
            List<INamedScopeSourceValue> result = new List<INamedScopeSourceValue>();

            foreach (NamedScopeIndex item in indices)
            { if (data.ContainsKey(item)) { result.Add(data[item].Source); } }

            return result;
        }


        internal virtual void Add(NamedScopeValueCore value)
        {
            NamedScopeIndex key = value.Index;

            if (data.ContainsKey(key))
            {
                Exception exception = new ArgumentException("An element with the same key already exists.");
                exception.Data.Add(nameof(value.Index), key.NamedScopeId);
                throw exception;
            }
            else
            {
                roots.Add(key);
                data.Add(key, value);
            }
        }

        internal virtual void Add(DataLayerIndex parent, NamedScopeValueCore value)
        {
            //TODO: Need a trap for infinite loop.
            //      It is when the parent (or child) directly or indirectly points to itself.
            //      Not sure how to detect that.


            List<NamedScopeValueCore> parentValues = data.Values.Where(w => parent.Equals(w.Source.Index)).ToList();

            if(parentValues.Count == 0)
            {
                Exception ex = new InvalidOperationException("Add parent first");
                ex.Data.Add(nameof(value.Title), value.Title);
                ex.Data.Add(nameof(value.Scope), value.Scope.ToName());
                ex.Data.Add(nameof(value.NamedPath), value.NamedPath.MemberFullPath);
                throw ex;
            }

            foreach (NamedScopeValueCore parentValue in parentValues)
            {
                if (!children.ContainsKey(parentValue.Index))
                { children.Add(parentValue.Index, new List<NamedScopeIndex>()); }

                if (!parents.ContainsKey(value.Index))
                { parents.Add(value.Index, new List<NamedScopeIndex>()); }

                if (children.ContainsKey(parentValue.Index) && !children[parentValue.Index].Contains(value.Index))
                { children[parentValue.Index].Add(value.Index); }

                if (parents.ContainsKey(value.Index) && !parents[value.Index].Contains(parentValue.Index))
                { parents[value.Index].Add(parentValue.Index); }

                if (!data.ContainsKey(value.Index))
                { data.Add(value.Index, value); }
            }
        }

        internal virtual void AddRange(IEnumerable<NamedScopePair> source)
        {
            foreach (NamedScopePair item in source)
            {
                if (item.ParentKey is null) { Add(item.Value); }
                else { Add(item.ParentKey, item.Value); }
            }
        }

        /// <inheritdoc/>
        internal virtual void Clear()
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
        internal virtual Boolean Remove(NamedScopeIndex key)
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
