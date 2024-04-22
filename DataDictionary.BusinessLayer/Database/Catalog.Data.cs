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
    /// Interface for the Wrapper of Catalog Data (The Database)
    /// </summary>
    public interface ICatalogData<TValue> :
        IBindingData<TValue>
        where TValue : ICatalogValue
    { }

    class CatalogData<TValue> : DbCatalogCollection<TValue>,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        ILoadData<IDbCatalogKey>, ISaveData<IDbCatalogKey>,
        IDatabaseModelItem, ICatalogData<TValue>
        where TValue : CatalogValue, new()
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


        public void Export (INamedScopeData target)
        {
            foreach (TValue item in this.Where(w => w.IsSystem == false))
            {
                if(item is INamedScopeValue value)
                {
                    NamedScopeKey key = value.GetSystemId();
                    target.Remove(key);
                    target.Add(value);
                }
            }
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
