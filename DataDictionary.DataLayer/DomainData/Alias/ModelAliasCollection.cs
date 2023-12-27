using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Domain;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.LibraryData;
using DataDictionary.DataLayer.LibraryData.Member;
using DataDictionary.DataLayer.LibraryData.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData.Alias
{
    /// <summary>
    /// Collection of Model Alias Items with hierarchy support.
    /// </summary>
    public class ModelAliasCollection : SortedList<ModelAliasKey, ModelAliasItem>
    {
        /// <summary>
        /// Generic Add of a new Item to the Collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="parentKey"></param>
        /// <param name="newAliasKey"></param>
        /// <param name="toAliasName"></param>
        /// <param name="toItemName"></param>
        /// <param name="toScopeType"></param>
        protected void Add<T>(
            T data,
            ModelAliasKey parentKey,
            Func<T, ModelAliasKey> newAliasKey,
            Func<T, String> toAliasName,
            Func<T, String> toItemName,
            Func<T, ScopeType> toScopeType)
            where T : class, IToAliasName, IToScopeType
        {
            ModelAliasItem? parentItem;

            if (parentKey.Equals(this.RootItem))
            { parentItem = this.RootItem; }
            else if (this.TryGetValue(parentKey, out parentItem))
            { } // parentItem is already assigned

            if (parentItem is ModelAliasItem)
            {
                ModelAliasKey newKey = newAliasKey(data);

                Int32 ordinalPosition = Int32.MaxValue;
                if (data is IDbColumnPosition pos && pos.OrdinalPosition is Int32 posValue)
                { ordinalPosition = posValue; }

                ModelAliasItem newItem = new ModelAliasItem<T>()
                {
                    AliasName = toAliasName(data),
                    ItemName = toItemName(data),
                    ScopeId = toScopeType(data),
                    Source = data,
                    SystemId = newKey.SystemId,
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
                parentKey: new ModelAliasKey(RootItem),
                newAliasKey: (T) => new ModelAliasKey((IDbCatalogKey)data),
                toAliasName: (T) => ((IDbCatalogKeyName)data).ToAliasName(),
                toItemName: (T) => { return data.DatabaseName ?? String.Empty; },
                toScopeType: (T) => new ScopeKey(data).ScopeId);
        }

        /// <summary>
        /// Adds Database Schema to collection
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parent"></param>
        public void Add(IDbCatalogKey parent, IDbSchemaItem data)
        {
            Add(data: data,
                parentKey: new ModelAliasKey(parent),
                newAliasKey: (T) => new ModelAliasKey((IDbSchemaKey)data),
                toAliasName: (T) => ((IDbSchemaKeyName)data).ToAliasName(),
                toItemName: (T) => { return data.SchemaName ?? String.Empty; },
                toScopeType: (T) => new ScopeKey(data).ScopeId);
        }

        /// <summary>
        /// Adds Database Table to collection
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parent"></param>
        public void Add(IDbSchemaKey parent, IDbTableItem data)
        {
            Add(data: data,
                parentKey: new ModelAliasKey(parent),
                newAliasKey: (T) => new ModelAliasKey((IDbTableKey)data),
                toAliasName: (T) => ((IDbTableKeyName)data).ToAliasName(),
                toItemName: (T) => { return data.TableName ?? String.Empty; },
                toScopeType: (T) => new ScopeKey(data).ScopeId);
        }

        /// <summary>
        /// Adds Database Table Column to collection
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parent"></param>
        public void Add(IDbTableKey parent, IDbTableColumnItem data)
        {
            Add(data: data,
                parentKey: new ModelAliasKey(parent),
                newAliasKey: (T) => new ModelAliasKey((IDbTableColumnKey)data),
                toAliasName: (T) => ((IDbTableColumnKeyName)data).ToAliasName(),
                toItemName: (T) => { return data.ColumnName ?? String.Empty; },
                toScopeType: (T) => new ScopeKey(data).ScopeId);
        }

        /// <summary>
        /// Adds Database Routine to collection
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parent"></param>
        public void Add(IDbSchemaKey parent, IDbRoutineItem data)
        {
            Add(data: data,
                parentKey: new ModelAliasKey(parent),
                newAliasKey: (T) => new ModelAliasKey((IDbRoutineKey)data),
                toAliasName: (T) => ((IDbRoutineKeyName)data).ToAliasName(),
                toItemName: (T) => { return data.RoutineName ?? String.Empty; },
                toScopeType: (T) => new ScopeKey(data).ScopeId);
        }

        /// <summary>
        /// Adds Database Routine Parameter to collection
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parent"></param>
        public void Add(IDbRoutineKey parent, IDbRoutineParameterItem data)
        {
            Add(data: data,
                parentKey: new ModelAliasKey(parent),
                newAliasKey: (T) => new ModelAliasKey((IDbRoutineParameterKey)data),
                toAliasName: (T) => ((IDbRoutineParameterKeyName)data).ToAliasName(),
                toItemName: (T) => { return data.ParameterName ?? String.Empty; },
                toScopeType: (T) => new ScopeKey(data).ScopeId);
        }

        /// <summary>
        /// Adds Database Domain to collection
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parent"></param>
        public void Add(IDbSchemaKey parent, IDbDomainItem data)
        {
            Add(data: data,
                parentKey: new ModelAliasKey(parent),
                newAliasKey: (T) => new ModelAliasKey((IDbDomainKey)data),
                toAliasName: (T) => ((IDbDomainKeyName)data).ToAliasName(),
                toItemName: (T) => { return data.DomainName ?? String.Empty; },
                toScopeType: (T) => new ScopeKey(data).ScopeId);
        }

        /// <summary>
        /// Adds Database Constraint to collection
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parent"></param>
        public void Add(IDbTableKey parent, IDbConstraintItem data)
        {
            Add(data: data,
                parentKey: new ModelAliasKey(parent),
                newAliasKey: (T) => new ModelAliasKey((IDbConstraintKey)data),
                toAliasName: (T) => ((IDbConstraintKeyName)data).ToAliasName(),
                toItemName: (T) => { return data.ConstraintName ?? String.Empty; },
                toScopeType: (T) => new ScopeKey(data).ScopeId);
        }

        /// <summary>
        /// Adds Library Source to collection
        /// </summary>
        /// <param name="data"></param>
        public void Add(ILibrarySourceItem data)
        {
            Add(data: data,
                parentKey: new ModelAliasKey(RootItem),
                newAliasKey: (T) => new ModelAliasKey((ILibrarySourceKey)data),
                toAliasName: (T) => ((ILibrarySourceKeyName)data).ToAliasName(),
                toItemName: (T) => { return data.AssemblyName ?? String.Empty; },
                toScopeType: (T) => new ScopeKey(data).ScopeId);
        }

        /// <summary>
        /// Adds Library Member (top level) to collection
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parent"></param>
        public void Add(ILibrarySourceKey parent, ILibraryMemberItem data)
        {
            Add(data: data,
                parentKey: new ModelAliasKey(parent),
                newAliasKey: (T) => new ModelAliasKey((ILibraryMemberKey)data),
                toAliasName: (T) => ((ILibraryMemberKeyName)data).ToAliasName(),
                toItemName: (T) => { return data.MemberName ?? String.Empty; },
                toScopeType: (T) => new ScopeKey(data).ScopeId);
        }

        /// <summary>
        /// Adds Library Member to collection
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parent"></param>
        public void Add(ILibraryMemberKey parent, ILibraryMemberItem data)
        {
            Add(data: data,
                parentKey: new ModelAliasKey(parent),
                newAliasKey: (T) => new ModelAliasKey((ILibraryMemberKey)data),
                toAliasName: (T) => ((ILibraryMemberKeyName)data).ToAliasName(),
                toItemName: (T) => { return data.MemberName ?? String.Empty; },
                toScopeType: (T) => new ScopeKey(data).ScopeId);
        }

        /// <summary>
        /// Removes an item and the children of that items.
        /// </summary>
        /// <param name="key"></param>
        /// <remarks>
        /// This method is expected to catch any call to the Base.Remove.
        /// </remarks>
        public void Remove(IModelAliasKey key)
        {
            ModelAliasKey removeKey = new ModelAliasKey(key);
            if (this.ContainsKey(removeKey) && this[removeKey] is ModelAliasItem removeItem)
            {
                List<ModelAliasKey> children = removeItem.Children.ToList();

                foreach (ModelAliasKey childKey in children)
                { this.Remove(childKey); }

                if (this.RootItem.Children.FirstOrDefault(w => removeKey.Equals(w)) is ModelAliasKey rootChild)
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
            List<ModelAliasKey> catalogs = this.Where(w => w.Value.Source is IDbCatalogItem && catalogKey.Equals(w.Value.Source)).Select(s => s.Key).ToList();

            foreach (ModelAliasKey item in catalogs)
            { this.Remove(item); }
        }

        /// <summary>
        /// Removes a Library and the children.
        /// </summary>
        /// <param name="key"></param>
        public void Remove(ILibrarySourceKey key)
        {
            LibrarySourceKey libraryKey = new LibrarySourceKey(key);
            List<ModelAliasKey> Libraries = this.Where(w => w.Value.Source is ILibrarySourceItem && libraryKey.Equals(w.Value.Source)).Select(s => s.Key).ToList();

            foreach (ModelAliasKey item in Libraries)
            { this.Remove(item); }
        }

        /// <summary>
        /// Root key for the hierarchy.
        /// </summary>
        public ModelAliasItem RootItem { get; private set; }

        /// <summary>
        /// Constructor for ModelAliasCollection
        /// </summary>
        public ModelAliasCollection() : base()
        {
            ModelAliasItem rootItem = new ModelAliasItem()
            {
                AliasName = String.Empty,
                ScopeId = ScopeType.Null,
                SystemId = Guid.Empty
            };

            RootItem = rootItem;
        }
    }
}
