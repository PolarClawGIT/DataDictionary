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
        // TODO:Combined children/parents as one list as the data is redundant.
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

        internal virtual void Add(INamedScopeSourceValue? parent, NamedScopeValue newValue)
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
                // TODO: Work out nesting mechanism for NameSpaces.
                // Needs to create NameSpace nodes between Parent and Child.
                // If the NameSpace node already exists, attach to that node.
                // The existing node may be something other then a NameSpace node.
                NamedScopePath parentPath;
                if (newValue.Path.Group().
                    Where(w => !newValue.Source.Path.Group().
                        Any(a => w.MemberFullPath.EndsWith(a.MemberFullPath))).
                    OrderBy(o => o.MemberFullPath.Length).
                    LastOrDefault() is NamedScopePath path)
                { parentPath = path; }
                else { parentPath = newValue.Source.Path; }

                var nodes = newValue.Source.
                    GetPath().
                    Group().
                    OrderBy(o => o.MemberFullPath.Length).
                    //Select(s => new NameSpaceSource(s)).
                    ToList();


                // Existing logic (no nesting for NameSpaces)
                if (children.ContainsKey(parentItem) && !children[parentItem].Contains(newValue.Index))
                { children[parentItem].Add(newValue.Index); }

                if (!parents[newValue.Index].Contains(parentItem))
                { parents[newValue.Index].Add(parentItem); }
            }
        }

        internal virtual void Add(NamedScopeValue value)
        {
            if (data.ContainsKey(value.Index))
            {
                Exception ex = new ArgumentException("An element with the same key already exists.");
                ex.Data.Add(nameof(value.Title), value.Title);
                ex.Data.Add(nameof(value.Scope), ScopeEnumeration.Cast(value.Scope).Name);
                ex.Data.Add(nameof(value.Path), value.Path.MemberFullPath);
                throw ex;
            }
            else
            {
                if (!crossWalk.ContainsKey(value.Source.Index))
                { crossWalk.Add(value.Source.Index, new List<NamedScopeIndex>()); }

                if (!crossWalk[value.Source.Index].Contains(value.Index))
                { crossWalk[value.Source.Index].Add(value.Index); }

                if (!data.ContainsKey(value.Index))
                { data.Add(value.Index, value); }

                if (!roots.Contains(value.Index))
                { roots.Add(value.Index); }
            }
        }

        internal virtual void Add(DataLayerIndex parent, NamedScopeValue value)
        {
            //TODO: Need a trap for infinite loop.
            //      It is when the parent (or child) directly or indirectly points to itself.
            //      Not sure how to detect that.

            if (!crossWalk.ContainsKey(value.Source.Index))
            { crossWalk.Add(value.Source.Index, new List<NamedScopeIndex>()); }

            if (!crossWalk[value.Source.Index].Contains(value.Index))
            { crossWalk[value.Source.Index].Add(value.Index); }

            if (!data.ContainsKey(value.Index))
            { data.Add(value.Index, value); }



            if (crossWalk.ContainsKey(parent) && crossWalk[parent].Count > 0)
            {
                foreach (NamedScopeIndex index in crossWalk[parent])
                {
                    var parentNode = data[index];


                    if (!children.ContainsKey(index))
                    { children.Add(index, new List<NamedScopeIndex>()); }

                    if (!parents.ContainsKey(value.Index))
                    { parents.Add(value.Index, new List<NamedScopeIndex>()); }

                    if (children.ContainsKey(index) && !children[index].Contains(value.Index))
                    { children[index].Add(value.Index); }

                    if (parents.ContainsKey(value.Index) && !parents[value.Index].Contains(index))
                    { parents[value.Index].Add(index); }
                }
            }
            else
            {
                Exception ex = new InvalidOperationException("Could not find parent for value passed");
                ex.Data.Add(nameof(value.Title), value.Title);
                ex.Data.Add(nameof(value.Scope), ScopeEnumeration.Cast(value.Scope).Name);
                ex.Data.Add(nameof(value.Path), value.Path.MemberFullPath);
                throw ex;
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
