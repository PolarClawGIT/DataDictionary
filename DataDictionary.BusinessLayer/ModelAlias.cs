using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Alias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer
{
    /// <summary>
    /// Interface component for the Model Alias
    /// </summary>
    /// <remarks>When combined with the Extension class, this approximates multi-inheritance.</remarks>
    public interface IModelAlias
    {
        /// <summary>
        /// List of available Model Alias Items from Catalogs and Libraries.
        /// </summary>
        ModelAliasCollection ModelAlias { get; }
    }

    /// <summary>
    /// Implementation component for the Model Alias data
    /// </summary>
    /// <remarks>When combined with the Extension class, this implements multi-inheritance.</remarks>
    public static class ModelAlias
    {
        public static IReadOnlyList<WorkItem> LoadAlias(this ModelData data)
        {
            List<WorkItem> work = new List<WorkItem>();
            Action<Int32, Int32> progress = (x, y) => { };
            WorkItem loadWork = new WorkItem()
            {
                WorkName = "Load aliases for Catalogs",
                DoWork = AliasWork
            };
            progress = loadWork.OnProgressChanged;

            work.Add(loadWork);
            return work;

            void AliasWork()
            {
                Int32 totalWork = data.DbCatalogs.Count + data.DbSchemta.Count + data.DbTables.Count;
                Int32 completedWork = 0;

                foreach (DbCatalogItem catalogItem in data.DbCatalogs)
                {
                    DbCatalogKey catalogKey = new DbCatalogKey(catalogItem);
                    DbCatalogKeyName catalogName = new DbCatalogKeyName(catalogItem);
                    data.ModelAlias.Add(catalogItem);
                    progress(completedWork++, totalWork);

                    foreach (DbSchemaItem schemaItem in data.DbSchemta.Where(w => catalogName.Equals(w)))
                    {
                        DbSchemaKey schemaKey = new DbSchemaKey(schemaItem);
                        DbSchemaKeyName schemaName = new DbSchemaKeyName(schemaItem);
                        data.ModelAlias.Add(catalogKey, schemaItem);
                        progress(completedWork++, totalWork);

                        foreach (DbTableItem? tableItem in data.DbTables.Where(w => schemaName.Equals(w)))
                        {
                            DbTableKey tableKey = new DbTableKey(tableItem);
                            data.ModelAlias.Add(schemaKey, tableItem);
                            progress(completedWork++, totalWork);
                        }
                    }
                }
            }
        }


    }
}
