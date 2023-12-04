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
        /// Adds Database Catalogs to collection
        /// </summary>
        /// <param name="data"></param>
        public void Add(IEnumerable<IDbCatalogItem> data)
        {
            foreach (IDbCatalogItem item in data)
            {
                this.Add(
                    new ModelAliasKey(item),
                    new ModelAliasItem<IDbCatalogItem>()
                    {
                        AliasName = item.ToAliasName(),
                        ScopeId = item.ToScopeType(),
                        Source = item,
                        SystemSourceId = item.CatalogId ?? Guid.Empty
                    });
            }
        }

        /// <summary>
        /// Adds Database Schema to collection
        /// </summary>
        /// <param name="data"></param>
        public void Add(IEnumerable<IDbSchemaItem> data)
        {
            foreach (IDbSchemaItem item in data)
            {
                this.Add(
                    new ModelAliasKey(item),
                    new ModelAliasItem<IDbSchemaItem>()
                    {
                        AliasName = item.ToAliasName(),
                        ScopeId = item.ToScopeType(),
                        Source = item,
                        SystemSourceId = item.SchemaId ?? Guid.Empty
                    });
            }
        }

        /// <summary>
        /// Adds Database Table to collection
        /// </summary>
        /// <param name="data"></param>
        public void Add(IEnumerable<IDbTableItem> data)
        {
            foreach (IDbTableItem item in data)
            {
                this.Add(
                    new ModelAliasKey(item),
                    new ModelAliasItem<IDbTableItem>()
                    {
                        AliasName = item.ToAliasName(),
                        ScopeId = item.ToScopeType(),
                        Source = item,
                        SystemSourceId = item.TableId ?? Guid.Empty
                    });
            }
        }

        /// <summary>
        /// Adds Database Table Column to collection
        /// </summary>
        /// <param name="data"></param>
        public void Add(IEnumerable<IDbTableColumnItem> data)
        {
            foreach (IDbTableColumnItem item in data)
            {
                this.Add(
                    new ModelAliasKey(item),
                    new ModelAliasItem<IDbTableColumnItem>()
                    {
                        AliasName = item.ToAliasName(),
                        ScopeId = item.ToScopeType(),
                        Source = item,
                        SystemSourceId = item.ColumnId ?? Guid.Empty
                    });
            }
        }

        /// <summary>
        /// Adds Database Table Column to collection
        /// </summary>
        /// <param name="data"></param>
        public void Add(IEnumerable<IDbDomainItem> data)
        {
            foreach (IDbDomainItem item in data)
            {
                this.Add(
                    new ModelAliasKey(item),
                    new ModelAliasItem<IDbDomainItem>()
                    {
                        AliasName = item.ToAliasName(),
                        ScopeId = item.ToScopeType(),
                        Source = item,
                        SystemSourceId = item.DomainId ?? Guid.Empty
                    });
            }
        }

        /// <summary>
        /// Adds Database Constraints to collection
        /// </summary>
        /// <param name="data"></param>
        public void Add(IEnumerable<IDbConstraintItem> data)
        {
            foreach (IDbConstraintItem item in data)
            {
                this.Add(
                    new ModelAliasKey(item),
                    new ModelAliasItem<IDbConstraintItem>()
                    {
                        AliasName = DbConstraintKeyNameExtension.ToAliasName(item),
                        ScopeId = item.ToScopeType(),
                        Source = item,
                        SystemSourceId = item.ConstraintId ?? Guid.Empty
                    });
            }
        }

        /// <summary>
        /// Adds Database Routine to collection
        /// </summary>
        /// <param name="data"></param>
        public void Add(IEnumerable<IDbRoutineItem> data)
        {
            foreach (IDbRoutineItem item in data)
            {
                this.Add(
                    new ModelAliasKey(item),
                    new ModelAliasItem<IDbRoutineItem>()
                    {
                        AliasName = item.ToAliasName(),
                        ScopeId = item.ToScopeType(),
                        Source = item,
                        SystemSourceId = item.RoutineId ?? Guid.Empty
                    });
            }
        }

        /// <summary>
        /// Adds Database Routine Parameter to collection
        /// </summary>
        /// <param name="data"></param>
        public void Add(IEnumerable<IDbRoutineParameterItem> data)
        {
            foreach (IDbRoutineParameterItem item in data)
            {
                this.Add(
                    new ModelAliasKey(item),
                    new ModelAliasItem<IDbRoutineParameterItem>()
                    {
                        AliasName = item.ToAliasName(),
                        ScopeId = item.ToScopeType(),
                        Source = item,
                        SystemSourceId = item.ParameterId ?? Guid.Empty
                    });
            }
        }

        /// <summary>
        /// Adds Library Source to collection
        /// </summary>
        /// <param name="data"></param>
        public void Add(IEnumerable<ILibrarySourceItem> data)
        {
            foreach (ILibrarySourceItem item in data)
            {
                this.Add(
                    new ModelAliasKey(item),
                    new ModelAliasItem<ILibrarySourceItem>()
                    {
                        AliasName = item.ToAliasName(),
                        ScopeId = item.ToScopeType(),
                        Source = item,
                        SystemSourceId = item.LibraryId ?? Guid.Empty
                    });
            }
        }

        /// <summary>
        /// Adds Library Source to collection
        /// </summary>
        /// <param name="data"></param>
        public void Add(IEnumerable<ILibraryMemberItem> data)
        {
            foreach (ILibraryMemberItem item in data)
            {
                this.Add(
                    new ModelAliasKey(item),
                    new ModelAliasItem<ILibraryMemberItem>()
                    {
                        AliasName = LibraryMemberKeyNameExtension.ToAliasName(item),
                        ScopeId = item.ToScopeType(),
                        Source = item,
                        SystemSourceId = item.LibraryId ?? Guid.Empty
                    });
            }
        }

    }
}
