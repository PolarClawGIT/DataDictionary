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
    /// Collection of Model Alias Items.
    /// </summary>
    public class ModelAliasCollection : SortedList<ModelAliasKey, ModelAliasItem>
    {
        /// <summary>
        /// Adds Database Catalog to collection
        /// </summary>
        /// <param name="data"></param>
        public void Add(IDbCatalogItem data)
        {
            this.Add(
                new ModelAliasKey(data),
                new ModelAliasItem<IDbCatalogItem>()
                {
                    AliasName = data.ToAliasName(),
                    ScopeId = data.ToScopeType(),
                    Source = data,
                    SystemSourceId = data.CatalogId ?? Guid.Empty
                });
        }

        /// <summary>
        /// Adds Database Schema to collection
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parent"></param>
        public void Add(IDbCatalogKey parent, IDbSchemaItem data)
        {
            this.Add(
                new ModelAliasKey(parent, data),
                new ModelAliasItem<IDbSchemaItem>()
                {
                    AliasName = data.ToAliasName(),
                    ScopeId = data.ToScopeType(),
                    Source = data,
                    SystemSourceId = data.SchemaId ?? Guid.Empty
                });
        }

        /// <summary>
        /// Adds Database Table to collection
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parent"></param>
        public void Add(IDbSchemaKey parent, IDbTableItem data)
        {
            this.Add(
                new ModelAliasKey(parent, data),
                new ModelAliasItem<IDbTableItem>()
                {
                    AliasName = data.ToAliasName(),
                    ScopeId = data.ToScopeType(),
                    Source = data,
                    SystemSourceId = data.TableId ?? Guid.Empty
                });
        }


    }
}
