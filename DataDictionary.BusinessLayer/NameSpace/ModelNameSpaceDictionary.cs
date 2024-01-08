using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Domain;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Alias;
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
            ModelNameSpaceItem rootItem = new ModelNameSpaceItem()
            { SystemId = Guid.Empty };

            RootItem = rootItem;
        }

        /// <summary>
        /// Generic Add of a new Item to the Collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="parentKey"></param>
        /// <param name="toKey"></param>
        /// <param name="toName"></param>
        /// <param name="toScope"></param>
        protected void Add<T>(
            T data,
            ModelNameSpaceKey parentKey,
            Func<T, ModelNameSpaceKey> toKey,
            Func<T, ModelNameSpaceKeyMember> toName,
            Func<T, ScopeKey> toScope)
            where T : class
        {
            ModelNameSpaceItem? parentItem;

            if (parentKey.Equals(this.RootItem))
            { parentItem = this.RootItem; }
            else if (this.TryGetValue(parentKey, out parentItem))
            { } // parentItem is already assigned

            if (parentItem is ModelNameSpaceItem)
            {
                ModelNameSpaceKey newKey = toKey(data);

                Int32 ordinalPosition = Int32.MaxValue;
                if (data is IDbColumnPosition pos && pos.OrdinalPosition is Int32 posValue)
                { ordinalPosition = posValue; }

                ModelNameSpaceItem newItem = new ModelNameSpaceItem()
                {
                    MemberNameKey = toName(data),
                    MemberScopeKey = toScope(data),
                    SystemId = toKey(data).SystemId,
                    Source = data,
                    SystemParentId = parentKey.SystemId,
                    OrdinalPosition = ordinalPosition
                };


                if (this.ContainsKey(newKey))
                {
                    Exception ex = new ArgumentException("Item already exists");
                    ex.Data.Add("Child", data.ToString());
                    throw ex;
                }

                base.Add(newKey, newItem);
                parentItem.Children.Add(newKey);
            }
            else
            {
                Exception ex = new ArgumentException("Parent Key not found");
                ex.Data.Add("Child", data.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// Adds Database Catalog to collection
        /// </summary>
        /// <param name="data"></param>
        public void Add(IDbCatalogItem data)
        {
            Add(data: data,
                parentKey: new ModelNameSpaceKey(RootItem),
                toKey: (T) => new ModelNameSpaceKey((IDbCatalogKey)data),
                toName: (T) => new ModelNameSpaceKeyMember((IDbCatalogKeyName)data),
                toScope: (T) => new ScopeKey(data));
        }

        /// <summary>
        /// Adds Database Schema to collection
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parent"></param>
        public void Add(IDbCatalogKey parent, IDbSchemaItem data)
        {
            Add(data: data,
                parentKey: new ModelNameSpaceKey(parent),
                toKey: (T) => new ModelNameSpaceKey((IDbSchemaKey)data),
                toName: (T) => new ModelNameSpaceKeyMember((IDbSchemaKeyName)data),
                toScope: (T) => new ScopeKey(data));
        }

        /// <summary>
        /// Adds Database Table to collection
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parent"></param>
        public void Add(IDbSchemaKey parent, IDbTableItem data)
        {
            Add(data: data,
                parentKey: new ModelNameSpaceKey(parent),
                toKey: (T) => new ModelNameSpaceKey((IDbTableKey)data),
                toName: (T) => new ModelNameSpaceKeyMember((IDbTableKeyName)data),
                toScope: (T) => new ScopeKey(data));
        }

        /// <summary>
        /// Adds Database Table Column to collection
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parent"></param>
        public void Add(IDbTableKey parent, IDbTableColumnItem data)
        {
            Add(data: data,
                parentKey: new ModelNameSpaceKey(parent),
                toKey: (T) => new ModelNameSpaceKey((IDbTableColumnKey)data),
                toName: (T) => new ModelNameSpaceKeyMember((IDbTableColumnKeyName)data),
                toScope: (T) => new ScopeKey(data));
        }

        /// <summary>
        /// Adds Database Routine to collection
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parent"></param>
        public void Add(IDbSchemaKey parent, IDbRoutineItem data)
        {
            Add(data: data,
                parentKey: new ModelNameSpaceKey(parent),
                toKey: (T) => new ModelNameSpaceKey((IDbRoutineKey)data),
                toName: (T) => new ModelNameSpaceKeyMember((IDbRoutineKeyName)data),
                toScope: (T) => new ScopeKey(data));
        }

        /// <summary>
        /// Adds Database Routine Parameter to collection
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parent"></param>
        public void Add(IDbRoutineKey parent, IDbRoutineParameterItem data)
        {
            Add(data: data,
                parentKey: new ModelNameSpaceKey(parent),
                toKey: (T) => new ModelNameSpaceKey((IDbRoutineParameterKey)data),
                toName: (T) => new ModelNameSpaceKeyMember((IDbRoutineParameterKeyName)data),
                toScope: (T) => new ScopeKey(data));
        }

        /// <summary>
        /// Adds Database Domain to collection
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parent"></param>
        public void Add(IDbSchemaKey parent, IDbDomainItem data)
        {

            Add(data: data,
                parentKey: new ModelNameSpaceKey(parent),
                toKey: (T) => new ModelNameSpaceKey((IDbDomainKey)data),
                toName: (T) => new ModelNameSpaceKeyMember((IDbDomainKeyName)data),
                toScope: (T) => new ScopeKey(data));
        }

        /// <summary>
        /// Adds Database Constraint to collection
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parent"></param>
        public void Add(IDbTableKey parent, IDbConstraintItem data)
        {
            Add(data: data,
                parentKey: new ModelNameSpaceKey(parent),
                toKey: (T) => new ModelNameSpaceKey((IDbConstraintKey)data),
                toName: (T) => new ModelNameSpaceKeyMember((IDbConstraintKeyName)data),
                toScope: (T) => new ScopeKey(data));
        }

        /// <summary>
        /// Adds Library Source to collection
        /// </summary>
        /// <param name="data"></param>
        public void Add(ILibrarySourceItem data)
        {
            Add(data: data,
                parentKey: new ModelNameSpaceKey(RootItem),
                toKey: (T) => new ModelNameSpaceKey((ILibrarySourceKey)data),
                toName: (T) => new ModelNameSpaceKeyMember((ILibrarySourceKeyName)data),
                toScope: (T) => new ScopeKey(data));
        }

        /// <summary>
        /// Adds Library Member (top level) to collection
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parent"></param>
        public void Add(ILibrarySourceKey parent, ILibraryMemberItem data)
        {
            Add(data: data,
                parentKey: new ModelNameSpaceKey(parent),
                toKey: (T) => new ModelNameSpaceKey((ILibraryMemberKey)data),
                toName: (T) => new ModelNameSpaceKeyMember((ILibraryMemberKeyName)data),
                toScope: (T) => new ScopeKey(data));
        }

        /// <summary>
        /// Adds Library Member to collection
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parent"></param>
        public void Add(ILibraryMemberKey parent, ILibraryMemberItem data)
        {
            Add(data: data,
                parentKey: new ModelNameSpaceKey(parent),
                toKey: (T) => new ModelNameSpaceKey((ILibraryMemberKey)data),
                toName: (T) => new ModelNameSpaceKeyMember((ILibraryMemberKeyName)data),
                toScope: (T) => new ScopeKey(data));
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

                if (this.RootItem.Children.FirstOrDefault(w => removeKey.Equals(w)) is ModelNameSpaceKey rootChild)
                { this.RootItem.Children.Remove(rootChild); }

                if (this.ContainsKey(removeKey))
                { base.Remove(removeKey); }
            }
        }

        /// <summary>
        /// Removes a Catalog and the children.
        /// </summary>
        /// <param name="key"></param>
        public void Remove(IDbCatalogKey key)
        {
            DbCatalogKey catalogKey = new DbCatalogKey(key);
            List<ModelNameSpaceKey> catalogs = this.Where(w => w.Value.Source is IDbCatalogItem && catalogKey.Equals(w.Value.Source)).Select(s => s.Key).ToList();

            foreach (ModelNameSpaceKey item in catalogs)
            { this.Remove(item); }
        }

        /// <summary>
        /// Removes a Library and the children.
        /// </summary>
        /// <param name="key"></param>
        public void Remove(ILibrarySourceKey key)
        {
            LibrarySourceKey libraryKey = new LibrarySourceKey(key);
            List<ModelNameSpaceKey> Libraries = this.Where(w => w.Value.Source is ILibrarySourceItem && libraryKey.Equals(w.Value.Source)).Select(s => s.Key).ToList();

            foreach (ModelNameSpaceKey item in Libraries)
            { this.Remove(item); }
        }


    }
}
