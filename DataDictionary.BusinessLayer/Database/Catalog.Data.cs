using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.ModelData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Database
{
    /// <summary>
    /// Wrapper of Catalog Data (The Database)
    /// </summary>
    public interface ICatalogData: IBindingData<CatalogValue>, INamedScopeData
    { }

    class CatalogData: DbCatalogCollection<CatalogValue>,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        ILoadData<IDbCatalogKey>, ISaveData<IDbCatalogKey>,
        IDatabaseModelItem, ICatalogData
    {
        /// <inheritdoc/>
        public required IDatabaseModel Database { get; init; }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDbCatalogKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDbCatalogKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Export(IList<NamedScopeItem> target)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem()
            {
                WorkName = "Load NameScope, Catalog",
                DoWork = () =>
                {
                    foreach (DbCatalogItem item in this.Where(w => w.IsSystem == false))
                    { target.Add(new NamedScopeItem(item)); }
                }
            });
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        [Obsolete("Will need to change to NamedScopeData")]
        public IReadOnlyList<WorkItem> Build(INamedScopeDictionary target)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem()
            {
                WorkName = "Build NamedScope Catalog",
                DoWork = () =>
                {
                    foreach (DbCatalogItem item in this.Where(w => w.IsSystem == false))
                    {
                        target.Remove(new NamedScopeKey(item));
                        target.Add(new NamedScopeItem(item));
                    }
                }
            });

            return work;
        }
    }
}
