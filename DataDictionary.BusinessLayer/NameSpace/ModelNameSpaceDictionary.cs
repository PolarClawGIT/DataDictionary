namespace DataDictionary.BusinessLayer.NameSpace
{
    /// <summary>
    /// Collection of Model Alias Items with hierarchy support.
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
    public class ModelNameSpaceDictionary : SortedDictionary<ModelNameSpaceKey, ModelNameSpaceItem>
    {
        /// <summary>
        /// Root key for the hierarchy.
        /// </summary>
        public ModelNameSpaceItem RootItem { get; private set; }

        /// <summary>
        /// Constructor for ModelAliasCollection
        /// </summary>
        public ModelNameSpaceDictionary() : base()
        {
            ModelNameSpaceItem rootItem = new ModelNameSpaceItem();
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
        public void Add(IModelNameSpaceKey key, IModelNameSpaceItem value)
        { throw new InvalidOperationException("Do not use Base Add Method."); }

        /// <summary>
        /// Adds a ModelNameSpaceItem to the collection.
        /// </summary>
        /// <param name="value"></param>
        public virtual void Add(ModelNameSpaceItem value)
        {
            if (this.ContainsKey(value.SystemKey))
            {
                Exception ex = new ArgumentException("Item already exists");
                ex.Data.Add("Child", value.ToString());
                throw ex;
            }

            if (value.SystemParentKey is ModelNameSpaceKey parentKey)
            {
                if (this.ContainsKey(parentKey))
                { this[parentKey].Children.Add(value.SystemKey); }
                else { this.RootItem.Children.Add(value.SystemKey); }
            }
            else { this.RootItem.Children.Add(value.SystemKey); }

            base.Add(value.SystemKey, value);
            OnListChanged(ModelNameSpaceChangedType.ItemAdded, value);
        }

        /// <summary>
        /// Removes an item and the children
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <remarks>
        /// This method is expected to catch calls to the base.Remove.
        /// </remarks>
        public virtual Boolean Remove(IModelNameSpaceKey key)
        {
            ModelNameSpaceKey removeKey = new ModelNameSpaceKey(key);
            if (this.ContainsKey(removeKey) && this[removeKey] is ModelNameSpaceItem removeItem)
            {
                List<ModelNameSpaceKey> children = removeItem.Children.ToList();

                foreach (ModelNameSpaceKey childKey in children)
                { this.Remove(childKey); }

                while (this.RootItem.Children.FirstOrDefault(w => removeKey.Equals(w)) is ModelNameSpaceKey rootChild)
                { this.RootItem.Children.Remove(rootChild); }

                if (removeItem.SystemParentKey is ModelNameSpaceKey && this.ContainsKey(removeItem.SystemParentKey))
                {
                    while (this.RootItem.Children.FirstOrDefault(w => removeKey.Equals(w)) is ModelNameSpaceKey parentChild)
                    { this.RootItem.Children.Remove(parentChild); }
                }

                OnListChanged(ModelNameSpaceChangedType.ItemDeleted, removeItem);
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
        public virtual Boolean Move(IModelNameSpaceKey item, IModelNameSpaceKey? parent)
        { // TODO: Not Certain this is needed. May just need to manipulate the Children.
            ModelNameSpaceKey currentKey = new ModelNameSpaceKey(item);

            if (this.ContainsKey(currentKey))
            {
                ModelNameSpaceItem currentItem = this[currentKey];

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

                if(parent is IModelNameSpaceKey
                    && new ModelNameSpaceKey(parent) is ModelNameSpaceKey parentKey
                    && this.ContainsKey(parentKey))
                {
                    this[parentKey].Children.Add(currentKey);
                    currentItem.SystemParentKey = parentKey;

                    OnListChanged(ModelNameSpaceChangedType.ItemMoved, currentItem);
                    return true;
                }
                else
                {
                    RootItem.Children.Add(currentKey);
                    currentItem.SystemParentKey = null;

                    OnListChanged(ModelNameSpaceChangedType.ItemMoved, currentItem);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Raised when add, remove, or Clear is called.
        /// </summary>
        public event EventHandler<ModelNameSpaceChangedEventArgs>? ListChanged;

        /// <summary>
        /// Used to raise the ListChanged event.
        /// </summary>
        /// <param name="changedType"></param>
        /// <param name="data"></param>
        protected virtual void OnListChanged(ModelNameSpaceChangedType changedType, IModelNameSpaceItem? data)
        {
            if (ListChanged is EventHandler<ModelNameSpaceChangedEventArgs> handler)
            { handler(this, new ModelNameSpaceChangedEventArgs(changedType, data)); }
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
