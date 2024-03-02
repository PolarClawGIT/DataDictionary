namespace DataDictionary.BusinessLayer.NameScope
{
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
    /// </remarks>
    public class NameScopeDictionary : SortedDictionary<NameScopeKey, NameScopeItem>
    {
        /// <summary>
        /// Root key for the hierarchy.
        /// </summary>
        public NameScopeItem RootItem { get; private set; }

        /// <summary>
        /// Constructor for ModelAliasCollection
        /// </summary>
        public NameScopeDictionary() : base()
        {
            NameScopeItem rootItem = new NameScopeItem();
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
        public void Add(INameScopeKey key, INameScopeItem value)
        { throw new InvalidOperationException("Do not use. Use Add by ContextNameItem."); }

        /// <summary>
        /// Adds an item to the collection.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="isChild"></param>
        public virtual void Add(NameScopeItem value, Boolean isChild = false)
        {
            if (this.ContainsKey(value.SystemKey))
            {
                Exception ex = new ArgumentException("Item already exists");
                ex.Data.Add("Child", value.ToString());
                throw ex;
            }

            if (value.SystemParentKey is NameScopeKey parentKey)
            {
                if (this.ContainsKey(parentKey))
                { this[parentKey].Children.Add(value.SystemKey); }
                else { this.RootItem.Children.Add(value.SystemKey); }
            }
            else { this.RootItem.Children.Add(value.SystemKey); }

            base.Add(value.SystemKey, value);
            OnListChanged(NameScopeChangedType.ItemAdded, value);
        }

        /// <summary>
        /// Adds a list of items to the collection
        /// </summary>
        /// <param name="values"></param>
        public virtual void AddRange(IEnumerable<NameScopeItem> values)
        {
            OnListChanged(NameScopeChangedType.BeginBatch);

            foreach (NameScopeItem item in values)
            { Add(item); }

            OnListChanged(NameScopeChangedType.EndBatch);
        }

        /// <summary>
        /// Removes an item and the children
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <remarks>
        /// This method is expected to catch calls to the base.Remove.
        /// </remarks>
        public virtual Boolean Remove(INameScopeKey key)
        {
            Boolean result = false;

            NameScopeKey removeKey = new NameScopeKey(key);
            if (this.ContainsKey(removeKey) && this[removeKey] is NameScopeItem removeItem)
            {
                if (removeItem.Children.Count > 0)
                {
                    OnListChanged(NameScopeChangedType.BeginBatch);
                    result = RemoveCore(key);
                    OnListChanged(NameScopeChangedType.EndBatch);
                }
                else { result = RemoveCore(key); }
            }
            return result;
        }

        /// <summary>
        /// Removes an item and the children
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected virtual Boolean RemoveCore(INameScopeKey key)
        {
            NameScopeKey removeKey = new NameScopeKey(key);
            if (this.ContainsKey(removeKey) && this[removeKey] is NameScopeItem removeItem)
            {
                List<NameScopeKey> children = removeItem.Children.ToList();


                foreach (NameScopeKey childKey in children)
                { this.RemoveCore(childKey); }

                while (this.RootItem.Children.FirstOrDefault(w => removeKey.Equals(w)) is NameScopeKey rootChild)
                { this.RootItem.Children.Remove(rootChild); }

                if (removeItem.SystemParentKey is NameScopeKey && this.ContainsKey(removeItem.SystemParentKey))
                {
                    while (this.RootItem.Children.FirstOrDefault(w => removeKey.Equals(w)) is NameScopeKey parentChild)
                    { this.RootItem.Children.Remove(parentChild); }
                }

                OnListChanged(NameScopeChangedType.ItemDeleted, removeItem);
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
        public virtual Boolean Move(INameScopeKey item, INameScopeKey? parent)
        { // TODO: Not Certain this is needed. May just need to manipulate the Children.
            NameScopeKey currentKey = new NameScopeKey(item);

            if (this.ContainsKey(currentKey))
            {
                NameScopeItem currentItem = this[currentKey];

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

                if (parent is INameScopeKey
                    && new NameScopeKey(parent) is NameScopeKey parentKey
                    && this.ContainsKey(parentKey))
                {
                    this[parentKey].Children.Add(currentKey);
                    currentItem.SystemParentKey = parentKey;

                    OnListChanged(NameScopeChangedType.ItemMoved, currentItem);
                    return true;
                }
                else
                {
                    RootItem.Children.Add(currentKey);
                    currentItem.SystemParentKey = null;

                    OnListChanged(NameScopeChangedType.ItemMoved, currentItem);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Raised when add, remove, or Clear is called.
        /// </summary>
        public event EventHandler<NameScopeChangedEventArgs>? ListChanged;

        /// <summary>
        /// Used to raise the ListChanged event.
        /// </summary>
        /// <param name="changedType"></param>
        /// <param name="data"></param>
        protected virtual void OnListChanged(NameScopeChangedType changedType, INameScopeItem? data = null)
        {
            if (ListChanged is EventHandler<NameScopeChangedEventArgs> handler)
            { handler(this, new NameScopeChangedEventArgs(changedType, data)); }
        }

        /// <summary>
        /// Removes all elements
        /// </summary>
        public new virtual void Clear()
        {
            RootItem.Children.Clear();

            while (this.Count > 0)
            { this.Remove(this.First().Key); }

            base.Clear();
        }
    }
}
