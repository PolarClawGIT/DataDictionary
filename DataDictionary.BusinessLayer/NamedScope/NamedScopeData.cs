// Ignore Spelling: indices

using DataDictionary.Resource.Enumerations;
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
        /// Returns the list of Keys for the Scopes.
        /// </summary>
        /// <param name="scopes"></param>
        /// <returns></returns>
        IReadOnlyList<NamedScopeIndex> ScopeKeys(params ScopeType[] scopes);

        /// <summary>
        /// Returns the list of Keys for the Scopes.
        /// </summary>
        /// <param name="scopes"></param>
        /// <returns></returns>
        IReadOnlyList<NamedScopeIndex> ScopeKeys(IEnumerable<ScopeType> scopes);

        /// <inheritdoc cref="IDictionary{TKey, TValue}.ContainsKey(TKey)"/>
        Boolean ContainsKey(NamedScopeIndex key);
    }

    /// <summary>
    /// Maps a Model Item into a NamedScope item to support hierarchical navigation and lookups for NamedScopePaths.
    /// </summary>
    class NamedScopeData : INamedScopeData
    {
        // Primary Data
        SortedDictionary<NamedScopeIndex, NamedScopeValue> data = new SortedDictionary<NamedScopeIndex, NamedScopeValue>();

        // Alternate Keys (not sure if Sorted Dictionary or normal Dictionary is better here). Because of the wrapper, it can be changed.
        // Provides performance improvement for alt-keys.
        SortedDictionary<NamedScopeIndex, List<NamedScopeIndex>> children = new SortedDictionary<NamedScopeIndex, List<NamedScopeIndex>>();
        SortedDictionary<NamedScopeIndex, List<NamedScopeIndex>> parents = new SortedDictionary<NamedScopeIndex, List<NamedScopeIndex>>();

        SortedDictionary<DataLayerIndex, List<NamedScopeIndex>> crossWalk = new SortedDictionary<DataLayerIndex, List<NamedScopeIndex>>();

        // Root Nodes
        List<NamedScopeIndex> roots = new List<NamedScopeIndex>();

        /// <inheritdoc/>
        public virtual INamedScopeValue GetValue(NamedScopeIndex index)
        { return data[index]; }

        /// <inheritdoc/>
        public virtual INamedScopeSourceValue GetData(NamedScopeIndex index)
        {
            if (!data.ContainsKey(index))
            {
                Exception ex = new IndexOutOfRangeException();
                ex.Data.Add(nameof(index), index);
                throw ex;
            }
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
            List<NamedScopeIndex> results = new List<NamedScopeIndex>();

            if (children.ContainsKey(index))
            { results.AddRange(children[index]); }

            return results;
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
            return data.Where(w => pathKey.Equals(w.Value.Path)).Select(s => s.Key).ToList();
        }

        /// <inheritdoc/>
        public virtual IReadOnlyList<NamedScopeIndex> ScopeKeys(params ScopeType[] scopes)
        {
            List<NamedScopeIndex> result = new List<NamedScopeIndex>();

            foreach (KeyValuePair<NamedScopeIndex, NamedScopeValue> item in data)
            {
                if (scopes.Length == 0
                    || scopes.Contains(ScopeType.Null)
                    || scopes.Contains(item.Value.Scope))
                { result.Add(item.Key); }
            }

            return result;
        }

        /// <inheritdoc/>
        public virtual IReadOnlyList<NamedScopeIndex> ScopeKeys(IEnumerable<ScopeType> scopes)
        {
            List<NamedScopeIndex> result = new List<NamedScopeIndex>();

            foreach (KeyValuePair<NamedScopeIndex, NamedScopeValue> item in data)
            {
                if (scopes.Count() == 0 || scopes.Contains(ScopeType.Null) || scopes.Contains(item.Value.Scope))
                { result.Add(item.Key); }
            }

            return result;
        }

        /// <summary>
        /// Adds items to the NamedScope structure.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="newValue"></param>
        /// <remarks>This method is passed to INamedScopeSource.LoadNamedScope.</remarks>
        public virtual void Add(INamedScopeSourceValue? parent, NamedScopeValue newValue)
        {
            // TODO: Work out nesting mechanism for NameSpaces.
            // Needs to create NameSpace nodes between Parent and Child.
            // If the NameSpace node already exists, attach to that node.
            // The existing node may be something other then a NameSpace node.

            List<NamedScopePath> nameSpaces = newValue.Source.
                GetPath().
                Group().
                Where(w => !newValue.Path.Equals(w)).
                Where(w => 1==2). // TODO: 
                OrderBy(o => o.MemberFullPath.Length).
                ToList();

            foreach (NamedScopePath item in nameSpaces)
            {
                INamedScopeSourceValue? newItem = null;

                //Think LINQ solution is slower because it will not use the SortedIndex TryGetValue
                var x = crossWalk.
                    Where(w => parent is INamedScopeSourceValue
                        && w.Key.Equals(parent.Index)).
                    SelectMany(s => s.Value).
                    Join(children,
                        parent => parent,
                        child => child.Key,
                        (parent, child) => child.Value).
                    SelectMany(s => s).
                    Join(data,
                        child => child,
                        value => value.Key,
                        (child, value) => value.Value).
                    Where(w => item.Equals(w.Path)).
                    ToList();

                // TODO: Not exactly correct.
                // An extra namespace is being added when not needed.

                if (parent is INamedScopeSourceValue
                    && crossWalk.TryGetValue(parent.Index, out List<NamedScopeIndex>? parentIndexs))
                {
                    foreach (var parentIndex in parentIndexs)
                    {
                        if (children.TryGetValue(parentIndex, out List<NamedScopeIndex>? childIndexs))
                        {
                            foreach (var childIndex in childIndexs)
                            {
                                if (data.TryGetValue(childIndex, out NamedScopeValue? childValue)
                                    && item.Equals(childValue.Path))
                                { newItem = childValue.Source; }
                            }
                        }
                    }
                }

                if (newItem is null)
                {
                    newItem = new NameSpaceSource(item);
                    DoAdd(parent, new NamedScopeValue(newItem));
                }

                parent = newItem;
            }

            DoAdd(parent, newValue);

            void DoAdd(INamedScopeSourceValue? parent, NamedScopeValue newValue)
            {
                if (!data.ContainsKey(newValue.Index))
                { data.Add(newValue.Index, newValue); }

                if (!crossWalk.ContainsKey(newValue.Source.Index))
                { crossWalk.Add(newValue.Source.Index, new List<NamedScopeIndex>()); }

                if (!crossWalk[newValue.Source.Index].Contains(newValue.Index))
                { crossWalk[newValue.Source.Index].Add(newValue.Index); }

                if (parent is null && !roots.Contains(newValue.Index))
                { roots.Add(newValue.Index); }

                if (!parents.ContainsKey(newValue.Index))
                { parents.Add(newValue.Index, new List<NamedScopeIndex>()); }

                if (!children.ContainsKey(newValue.Index))
                { children.Add(newValue.Index, new List<NamedScopeIndex>()); }

                foreach (NamedScopeIndex parentItem in crossWalk.
                    Where(w => parent is INamedScopeSourceValue
                        && w.Key.Equals(parent.Index)).
                    SelectMany(s => s.Value))
                {

                    // Existing logic (no nesting for NameSpaces)
                    if (children.ContainsKey(parentItem) && !children[parentItem].Contains(newValue.Index))
                    { children[parentItem].Add(newValue.Index); }

                    if (!parents[newValue.Index].Contains(parentItem))
                    { parents[newValue.Index].Add(parentItem); }
                }
            }
        }

        /// <inheritdoc/>
        internal virtual void Clear()
        {
            data.Clear();
            children.Clear();
            parents.Clear();
            crossWalk.Clear();
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
