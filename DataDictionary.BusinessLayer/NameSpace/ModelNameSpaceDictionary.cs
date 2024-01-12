using DataDictionary.DataLayer.ApplicationData.Model;
using DataDictionary.DataLayer.ApplicationData.Model.SubjectArea;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Domain;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Alias;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.LibraryData.Member;
using DataDictionary.DataLayer.LibraryData.Source;

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
        public void Add(IModelNameSpaceKey key, IModelNameSpaceItem value)
        { throw new InvalidOperationException("Do not use Base Add Method."); }

        /// <summary>
        /// Adds a ModelNameSpaceItem to the collection.
        /// </summary>
        /// <param name="value"></param>
        public void Add(ModelNameSpaceItem value)
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
        }

        /// <summary>
        /// Removes an item and the children of that items.
        /// </summary>
        /// <param name="key"></param>
        /// <remarks>
        /// This method is expected to catch any call to the Base.Remove.
        /// </remarks>
        public void Remove(IModelNameSpaceKey key)
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

                if (this.ContainsKey(removeKey))
                { base.Remove(removeKey); }
            }
        }


        /// <summary>
        /// Removes all elements
        /// </summary>
        public new void Clear()
        {
            RootItem.Children.Clear();
            base.Clear();
        }
    }
}
