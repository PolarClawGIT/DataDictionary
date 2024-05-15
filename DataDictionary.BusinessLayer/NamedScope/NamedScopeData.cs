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
        INamedScopeValue GetValue(INamedScopeIndex index);

        /// <summary>
        /// Gets the Data for the specified index;
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        INamedScopeSourceValue GetData(INamedScopeIndex index);

        /// <inheritdoc cref="ICollection.Count"/>
        Int32 Count { get; }

        /// <summary>
        /// Returns the list of Root Keys.
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<INamedScopeIndex> RootKeys();

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
        IReadOnlyList<INamedScopeSourceValue> Values(IEnumerable<INamedScopeIndex> keys);

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

        SortedDictionary<DataLayerIndex, List<NamedScopeIndex>> crossWalkIndex = new SortedDictionary<DataLayerIndex, List<NamedScopeIndex>>();

        // Root Nodes
        List<NamedScopeIndex> roots = new List<NamedScopeIndex>();

        /// <inheritdoc/>
        public virtual INamedScopeValue GetValue(INamedScopeIndex index)
        {
            NamedScopeIndex key = new NamedScopeIndex(index);
            return data[key];
        }

        /// <inheritdoc/>
        public virtual INamedScopeSourceValue GetData(INamedScopeIndex index)
        {
            NamedScopeIndex key = new NamedScopeIndex(index);
            return data[key].Source;
        }

        /// <inheritdoc/>
        public virtual Int32 Count { get { return data.Count; } }

        /// <inheritdoc/>
        public virtual IReadOnlyList<INamedScopeIndex> RootKeys()
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
            return data.Where(w => pathKey.Equals(w.Value.NamedPath)).Select(s => s.Key).ToList();
        }

        /// <inheritdoc/>
        public virtual IReadOnlyList<INamedScopeSourceValue> Values(IEnumerable<INamedScopeIndex> keys)
        {
            List<INamedScopeSourceValue> result = new List<INamedScopeSourceValue>();

            foreach (INamedScopeIndex item in keys)
            {
                NamedScopeIndex target = new NamedScopeIndex(item);
                if (data.ContainsKey(target)) { result.Add(data[target].Source); }
            }

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
                if (!crossWalkIndex.ContainsKey(value.Source.Index))
                { crossWalkIndex.Add(value.Source.Index, new List<NamedScopeIndex>()); }
                crossWalkIndex[value.Source.Index].Add(key);

                roots.Add(key);
                data.Add(key, value);
            }
        }

        [Obsolete("Use new method", true)]
        internal virtual void Add(NamedScopeIndex item, NamedScopeValueCore value)
        {
            //TODO: Need a trap for infinite loop.
            //      It is when the parent (or child) directly or indirectly points to itself.
            //      Not sure how to detect that.

            NamedScopeIndex key = value.Index;

            if (!children.ContainsKey(item))
            { children.Add(item, new List<NamedScopeIndex>()); }

            if (!parents.ContainsKey(key))
            { parents.Add(key, new List<NamedScopeIndex>()); }

            if (!crossWalkIndex.ContainsKey(value.Source.Index))
            { crossWalkIndex.Add(value.Source.Index, new List<NamedScopeIndex>()); }

            if (children.ContainsKey(item) && !children[item].Contains(key))
            { children[item].Add(key); }

            if (parents.ContainsKey(key) && !parents[key].Contains(item))
            { parents[key].Add(item); }

            if (crossWalkIndex.ContainsKey(value.Source.Index) && !crossWalkIndex[value.Source.Index].Contains(item))
            { crossWalkIndex[value.Source.Index].Add(key); }

            if (!data.ContainsKey(key))
            { data.Add(key, value); }
        }

        internal virtual void Add(DataLayerIndex parent, NamedScopeValueCore value)
        {
            //TODO: Need a trap for infinite loop.
            //      It is when the parent (or child) directly or indirectly points to itself.
            //      Not sure how to detect that.

            if (!crossWalkIndex.ContainsKey(parent))
            { crossWalkIndex.Add(parent, new List<NamedScopeIndex>() { value.Index}); }

            if (crossWalkIndex.ContainsKey(parent))
            {
                foreach (NamedScopeIndex item in crossWalkIndex[parent])
                {
                    NamedScopeIndex key = value.Index;

                    if (!children.ContainsKey(item))
                    { children.Add(item, new List<NamedScopeIndex>()); }

                    if (!parents.ContainsKey(key))
                    { parents.Add(key, new List<NamedScopeIndex>()); }

                    if (!crossWalkIndex.ContainsKey(value.Source.Index))
                    { crossWalkIndex.Add(value.Source.Index, new List<NamedScopeIndex>()); }

                    if (children.ContainsKey(item) && !children[item].Contains(key))
                    { children[item].Add(key); }

                    if (parents.ContainsKey(key) && !parents[key].Contains(item))
                    { parents[key].Add(item); }

                    if (crossWalkIndex.ContainsKey(value.Source.Index) && !crossWalkIndex[value.Source.Index].Contains(item))
                    { crossWalkIndex[value.Source.Index].Add(key); }

                    if (!data.ContainsKey(key))
                    { data.Add(key, value); }

                }
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
