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
        /// <param name="toScopeType"></param>
        protected void Add<T>(
            T data,
            ModelAliasKey parentKey,
            Func<T, ModelAliasKey> newAliasKey,
            Func<T, String> toAliasName,
            Func<T, ScopeType> toScopeType)
            where T: class, IToAliasName, IToScopeType
        {
            if (this.TryGetValue(parentKey, out ModelAliasItem? parentItem))
            {
                ModelAliasKey newKey = newAliasKey(data);

                ModelAliasItem newItem = new ModelAliasItem<T>()
                {
                    AliasName = toAliasName(data),
                    ScopeId = toScopeType(data),
                    Source = data,
                    SystemId = newKey.SystemId
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
                toScopeType: (T) => data.ToScopeType());
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
                newAliasKey : (T) => new ModelAliasKey((IDbSchemaKey)data),
                toAliasName : (T) => ((IDbSchemaKeyName)data).ToAliasName(),
                toScopeType : (T) => data.ToScopeType());
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
                toScopeType: (T) => data.ToScopeType());
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
                toScopeType: (T) => data.ToScopeType());
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
                toScopeType: (T) => data.ToScopeType());
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
                toScopeType: (T) => data.ToScopeType());
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
                toScopeType: (T) => data.ToScopeType());
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
                toScopeType: (T) => data.ToScopeType());
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
                toScopeType: (T) => data.ToScopeType());
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
                toScopeType: (T) => data.ToScopeType());
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
                toScopeType: (T) => data.ToScopeType());
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
            ModelAliasKey rootKey = new ModelAliasKey() { SystemId = Guid.Empty };
            ModelAliasItem rootItem = new ModelAliasItem() { AliasName = String.Empty, ScopeId = ScopeType.Null, SystemId = Guid.Empty };

            RootItem = rootItem;
            this.Add(rootKey, rootItem);
        }
    }
}
