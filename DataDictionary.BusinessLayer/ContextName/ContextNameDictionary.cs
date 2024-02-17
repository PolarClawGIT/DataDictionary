namespace DataDictionary.BusinessLayer.ContextName
{
    /// <summary>
    /// Collection of Context Name Items with hierarchy support.
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
    /// </remarks>
    public class ContextNameDictionary : SortedDictionary<ContextNameKey, ContextNameItem>
    {
        /// <summary>
        /// Root key for the hierarchy.
        /// </summary>
        public ContextNameItem RootItem { get; private set; }

        /// <summary>
        /// Constructor for ModelAliasCollection
        /// </summary>
        public ContextNameDictionary() : base()
        {
            ContextNameItem rootItem = new ContextNameItem();
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
        public void Add(IContextNameKey key, IContextNameItem value)
        { throw new InvalidOperationException("Do not use. Use Add by ContextNameItem."); }

        /// <summary>
        /// Adds an item to the collection.
        /// </summary>
        /// <param name="value"></param>
        public virtual void Add(ContextNameItem value)
        {
            if (this.ContainsKey(value.SystemKey))
            {
                Exception ex = new ArgumentException("Item already exists");
                ex.Data.Add("Child", value.ToString());
                throw ex;
            }

            if (value.SystemParentKey is ContextNameKey parentKey)
            {
                if (this.ContainsKey(parentKey))
                { this[parentKey].Children.Add(value.SystemKey); }
                else { this.RootItem.Children.Add(value.SystemKey); }
            }
            else { this.RootItem.Children.Add(value.SystemKey); }

            base.Add(value.SystemKey, value);
            OnListChanged(ContextNameChangedType.ItemAdded, value);
        }

        /// <summary>
        /// Removes an item and the children
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <remarks>
        /// This method is expected to catch calls to the base.Remove.
        /// </remarks>
        public virtual Boolean Remove(IContextNameKey key)
        {
            ContextNameKey removeKey = new ContextNameKey(key);
            if (this.ContainsKey(removeKey) && this[removeKey] is ContextNameItem removeItem)
            {
                List<ContextNameKey> children = removeItem.Children.ToList();

                foreach (ContextNameKey childKey in children)
                { this.Remove(childKey); }

                while (this.RootItem.Children.FirstOrDefault(w => removeKey.Equals(w)) is ContextNameKey rootChild)
                { this.RootItem.Children.Remove(rootChild); }

                if (removeItem.SystemParentKey is ContextNameKey && this.ContainsKey(removeItem.SystemParentKey))
                {
                    while (this.RootItem.Children.FirstOrDefault(w => removeKey.Equals(w)) is ContextNameKey parentChild)
                    { this.RootItem.Children.Remove(parentChild); }
                }

                OnListChanged(ContextNameChangedType.ItemDeleted, removeItem);
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
        public virtual Boolean Move(IContextNameKey item, IContextNameKey? parent)
        { // TODO: Not Certain this is needed. May just need to manipulate the Children.
            ContextNameKey currentKey = new ContextNameKey(item);

            if (this.ContainsKey(currentKey))
            {
                ContextNameItem currentItem = this[currentKey];

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

                if(parent is IContextNameKey
                    && new ContextNameKey(parent) is ContextNameKey parentKey
                    && this.ContainsKey(parentKey))
                {
                    this[parentKey].Children.Add(currentKey);
                    currentItem.SystemParentKey = parentKey;

                    OnListChanged(ContextNameChangedType.ItemMoved, currentItem);
                    return true;
                }
                else
                {
                    RootItem.Children.Add(currentKey);
                    currentItem.SystemParentKey = null;

                    OnListChanged(ContextNameChangedType.ItemMoved, currentItem);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Raised when add, remove, or Clear is called.
        /// </summary>
        public event EventHandler<ContextNameChangedEventArgs>? ListChanged;

        /// <summary>
        /// Used to raise the ListChanged event.
        /// </summary>
        /// <param name="changedType"></param>
        /// <param name="data"></param>
        protected virtual void OnListChanged(ContextNameChangedType changedType, IContextNameItem? data)
        {
            if (ListChanged is EventHandler<ContextNameChangedEventArgs> handler)
            { handler(this, new ContextNameChangedEventArgs(changedType, data)); }
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
