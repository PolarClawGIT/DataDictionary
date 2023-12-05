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
        /// Adds Database Catalog to collection
        /// </summary>
        /// <param name="data"></param>
        public void Add(IDbCatalogItem data)
        {
            if (data.CatalogId is null || data.CatalogId == Guid.Empty)
            { throw new ArgumentException(nameof(data.CatalogId)); }

            ModelAliasKey newKey = new ModelAliasKey()
            {
                SystemId = data.CatalogId.Value,
                SystemParentId = RootItem.SystemSourceId
            };

            ModelAliasItem newItem = new ModelAliasItem<IDbCatalogItem>()
            {
                AliasName = data.ToAliasName(),
                ScopeId = data.ToScopeType(),
                Source = data,
                SystemSourceId = data.CatalogId.Value
            };

            base.Add(newKey, newItem);
            RootItem.Children.Add(newKey);
        }

        /// <summary>
        /// Adds Database Schema to collection
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parent"></param>
        public void Add(IDbCatalogKey parent, IDbSchemaItem data)
        {
            if (parent.CatalogId is null || parent.CatalogId == Guid.Empty)
            { throw new ArgumentNullException(nameof(parent.CatalogId)); }

            if (data.SchemaId is null || data.SchemaId == Guid.Empty)
            { throw new ArgumentNullException(nameof(data.SchemaId)); }

            ModelAliasKey parentKey = new ModelAliasKey()
            {
                SystemId = parent.CatalogId.Value,
                SystemParentId = RootItem.SystemSourceId
            };

            if (this.ContainsKey(parentKey) && this[parentKey] is ModelAliasItem parentItem)
            {
                ModelAliasKey newKey = new ModelAliasKey()
                {
                    SystemId = data.SchemaId.Value,
                    SystemParentId = parentKey.SystemId
                };

                ModelAliasItem newItem = new ModelAliasItem<IDbSchemaItem>()
                {
                    AliasName = data.ToAliasName(),
                    ScopeId = data.ToScopeType(),
                    Source = data,
                    SystemSourceId = data.SchemaId.Value
                };

                if (this.ContainsKey(newKey))
                {
                    Exception ex = new ArgumentException("Item already exists");
                    ex.Data.Add("Parent", parent.ToString());
                    ex.Data.Add("Child", data.ToString());
                    throw ex;
                }

                base.Add(newKey, newItem);
                parentItem.Children.Add(newKey);
            }
            else
            {
                Exception ex = new ArgumentException("Parent Key not found");
                ex.Data.Add("Parent", parent.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// Adds Database Table to collection
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parent"></param>
        public void Add(IDbSchemaKey parent, IDbTableItem data)
        {
            if (parent.SchemaId is null || parent.SchemaId == Guid.Empty)
            { throw new ArgumentNullException(nameof(parent.SchemaId)); }

            if (data.TableId is null || data.TableId == Guid.Empty)
            { throw new ArgumentNullException(nameof(data.TableId)); }

            ModelAliasKey parentKey = new ModelAliasKey()
            {
                SystemId = parent.SchemaId.Value,
                SystemParentId = RootItem.SystemSourceId
            };

            if (this.ContainsKey(parentKey) && this[parentKey] is ModelAliasItem parentItem)
            {
                ModelAliasKey newKey = new ModelAliasKey()
                {
                    SystemId = data.TableId.Value,
                    SystemParentId = parentKey.SystemId
                };

                ModelAliasItem newItem = new ModelAliasItem<IDbTableItem>()
                {
                    AliasName = data.ToAliasName(),
                    ScopeId = data.ToScopeType(),
                    Source = data,
                    SystemSourceId = data.TableId.Value
                };

                if (this.ContainsKey(newKey))
                {
                    Exception ex = new ArgumentException("Item already exists");
                    ex.Data.Add("Parent", parent.ToString());
                    ex.Data.Add("Child", data.ToString());
                    throw ex;
                }

                base.Add(newKey, newItem);
                parentItem.Children.Add(newKey);
            }
            else
            {
                Exception ex = new ArgumentException("Parent Key not found");
                ex.Data.Add("Parent", parent.ToString());
                throw ex;
            }
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
            ModelAliasKey rootKey = new ModelAliasKey() { SystemId = Guid.Empty, SystemParentId = null };
            ModelAliasItem rootItem = new ModelAliasItem() { AliasName = String.Empty, ScopeId = ScopeType.Null, SystemSourceId = Guid.Empty };

            RootItem = rootItem;
            this.Add(rootKey, rootItem);
        }
    }
}
