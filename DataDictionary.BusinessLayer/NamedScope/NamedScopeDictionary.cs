using DataDictionary.BusinessLayer.Database;
using DataDictionary.BusinessLayer.DbWorkItem;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.NamedScope
{
    /// <summary>
    /// Interface for NameScope Items with hierarchy support.
    /// </summary>
    public interface INamedScopeDictionary : IDictionary<NamedScopeKey, NamedScopeItem>,
        IRemoveData
    {
        /// <summary>
        /// Adds an item to the collection.
        /// </summary>
        /// <param name="value"></param>
        void Add(NamedScopeItem value);

        /// <summary>
        /// Build the NamedScope data
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Build();

        /// <summary>
        /// Root key for the hierarchy.
        /// </summary>
        NamedScopeItem RootItem { get; }
    }

    /// <summary>
    /// Collection of NameScope Items with hierarchy support.
    /// </summary>
    /// <remarks>
    /// SortedDictionary was chosen over Dictionary or a SortedList.
    /// The application needs to be able to load the structure then be able to 
    /// lookup any given node in the tree by Key.
    /// The SortedDictionary and SortedList both provided a high performance lookup.
    /// The functionality of both are identical for the purpose of this application.
    /// A Dictionary, by contrast, provided to be a slower because of a lack of a B-Tree lookup.
    /// For effective use of this structure, get the element by Key not any type of ForEach including LINQ.
    /// 
    /// Important: The Key is a GUID. As such, the order is not reflective of how the structure is displayed.
    /// The actual order is not important. Only that the structure uses an internal B-Tree lookup to speed process up.
    /// 
    /// NameScope is also called NameSpace, Alias Names and Context Names.
    /// NameScope was chosen to avoid naming collusions and reduce confusion.
    /// 
    /// TODO: The base class does not allow overriding of a number methods. Consider re-writing as a Wrapper.
    /// </remarks>
    class NamedScopeDictionary : SortedDictionary<NamedScopeKey, NamedScopeItem>, INamedScopeDictionary
    {
        /// <summary>
        /// Connection to the root of the Business Data.
        /// </summary>
        public required BusinessLayerData Source { get; init; }

        /// <inheritdoc/>
        public NamedScopeItem RootItem { get; private set; }

        /// <summary>
        /// Constructor for ModelAliasCollection
        /// </summary>
        public NamedScopeDictionary() : base()
        {
            NamedScopeItem rootItem = new NamedScopeItem();
            RootItem = rootItem;
        }

        /// <summary>
        /// Do not use. Use the overload of Add instead.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <remarks>
        /// This method is expected to catch calls to the base.Add.
        /// </remarks>
        public void Add(INamedScopeKey key, INamedScopeItem value)
        { throw new InvalidOperationException("Do not use. Use Add by ContextNameItem."); }

        /// <inheritdoc/>
        public virtual void Add(NamedScopeItem value)
        {
            //TODO: Problem, Some items need to have multiple Parents. This currently restricts to a single parent.
            //      The item itself is expected to be in the list once, but may appear in multiple Children lists.
            if (this.ContainsKey(value.SystemKey)) { return; }

            if (value.SystemParentKey is NamedScopeKey parentKey)
            {
                if (this.ContainsKey(parentKey))
                { this[parentKey].Children.Add(value.SystemKey); }
                else
                {
                    this.RootItem.Children.Add(value.SystemKey);
                }
            }
            else { this.RootItem.Children.Add(value.SystemKey); }

            base.Add(value.SystemKey, value);
        }

        /// <summary>
        /// Adds a list of items to the collection
        /// </summary>
        /// <param name="values"></param>
        public virtual void AddRange(IEnumerable<NamedScopeItem> values)
        {
            foreach (NamedScopeItem item in values)
            { Add(item); }
        }

        /// <inheritdoc/>
        /// <remarks>
        /// This method catches calls to the base.Remove and directs it to the correct method.
        /// </remarks>
        public new virtual Boolean Remove(NamedScopeKey key)
        { return Remove((INamedScopeKey)key); }

        /// <summary>
        /// Removes an item and the children
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual Boolean Remove(INamedScopeKey key)
        {
            Boolean result = false;

            NamedScopeKey removeKey = new NamedScopeKey(key);
            if (this.ContainsKey(removeKey) && this[removeKey] is NamedScopeItem removeItem)
            { result = RemoveCore(key); }
            return result;
        }

        /// <summary>
        /// Removes an item and the children
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected virtual Boolean RemoveCore(INamedScopeKey key)
        {
            NamedScopeKey removeKey = new NamedScopeKey(key);


            if (this.ContainsKey(removeKey) && this[removeKey] is NamedScopeItem removeItem)
            {
                List<NamedScopeKey> children = removeItem.Children.ToList();


                foreach (NamedScopeKey childKey in children)
                { this.RemoveCore(childKey); }

                while (this.RootItem.Children.FirstOrDefault(w => removeKey.Equals(w)) is NamedScopeKey rootChild)
                { this.RootItem.Children.Remove(rootChild); }

                if (removeItem.SystemParentKey is NamedScopeKey && this.ContainsKey(removeItem.SystemParentKey))
                {
                    while (this.RootItem.Children.FirstOrDefault(w => removeKey.Equals(w)) is NamedScopeKey parentChild)
                    { this.RootItem.Children.Remove(parentChild); }
                }

                removeItem.ClearEvents();

                return base.Remove(removeKey);
            }
            else { return false; }
        }

        /// <summary>
        /// Moves a item to a new parent
        /// </summary>
        /// <param name="item"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        [Obsolete("May Not be needed")]
        public virtual Boolean Move(INamedScopeKey item, INamedScopeKey? parent)
        { // TODO: Not Certain this is needed. May just need to manipulate the Children.
            NamedScopeKey currentKey = new NamedScopeKey(item);

            if (this.ContainsKey(currentKey))
            {
                NamedScopeItem currentItem = this[currentKey];

                if (currentItem.SystemParentKey is not null && this.ContainsKey(currentItem.SystemParentKey))
                {
                    while (this[currentItem.SystemParentKey].Children.Contains(currentKey))
                    { this[currentItem.SystemParentKey].Children.Remove(currentKey); }
                }
                else
                {
                    while (RootItem.Children.Contains(currentKey))
                    { RootItem.Children.Remove(currentKey); }
                }

                if (parent is INamedScopeKey
                    && new NamedScopeKey(parent) is NamedScopeKey parentKey
                    && this.ContainsKey(parentKey))
                {
                    this[parentKey].Children.Add(currentKey);
                    currentItem.SystemParentKey = parentKey;

                    return true;
                }
                else
                {
                    RootItem.Children.Add(currentKey);
                    currentItem.SystemParentKey = null;

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes all elements
        /// </summary>
        public new virtual void Clear()
        {
            RootItem.Children.Clear();
            Boolean success = true;

            while (this.Count > 0 && success)
            { success = this.Remove(this.First().Key); }

            base.Clear();
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Remove()
        { return new WorkItem() { WorkName = "Remove NamedScope", DoWork = this.Clear }.ToList(); }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Build()
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(Source.Models.Build(this));
            work.AddRange(Source.ModelSubjectAreas.Build(this));
            work.AddRange(Source.DomainModel.Build(this));
            work.AddRange(Source.ScriptingEngine.Build(this));
            work.AddRange(Source.DatabaseModel.Build(this));
            work.AddRange(Source.LibraryModel.Build(this));

            return work;
        }
    }
}
