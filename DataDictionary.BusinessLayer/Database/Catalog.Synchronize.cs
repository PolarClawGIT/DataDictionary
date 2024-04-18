using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.DatabaseData.Catalog;
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
    /// Catalog Synchronize value
    /// </summary>
    public class CatalogSynchronizeValue : SynchronizeValue<CatalogValue>
    {
        /// <inheritdoc/>
        public CatalogSynchronizeValue(CatalogValue data, Boolean inModel, Boolean inDatabase) : base(data, inModel, inDatabase)
        { }
    }

    /// <summary>
    /// Catalog Synchronize to compare what Catalogs are in the Database vs the Model
    /// </summary>
    public class CatalogSynchronize : BindingList<CatalogSynchronizeValue>
    {
        class DatabaseData<TValue> : DbCatalogCollection<TValue>
            where TValue : CatalogValue, ICatalogValue, new()
        { }

        DatabaseData<CatalogValue> databaseData = new DatabaseData<CatalogValue>();
        IDatabaseModel modelData;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="modelData"></param>
        public CatalogSynchronize(IDatabaseModel modelData) : base()
        {
            this.modelData = modelData;

            foreach (CatalogValue item in modelData.DbCatalogs)
            { Add(new CatalogSynchronizeValue(item, true, false)); }
        }

        public IReadOnlyList<WorkItem> GetCatalogs(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(new WorkItem() { DoWork = databaseData.Clear });
            work.Add(factory.CreateLoad(databaseData));
            work.Add(new WorkItem() { DoWork = this.Refresh });
            return work;
        }

        public IReadOnlyList<WorkItem> ImportFromSchema(DbSchemaContext source)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(modelData.Import(source));
            work.Add(new WorkItem() { DoWork = this.Refresh });
            return work;
        }

        public IReadOnlyList<WorkItem> OpenFromDb(IDatabaseWork factory, ICatalogKey key)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(modelData.Remove(key));
            work.AddRange(modelData.Load(factory, key));
            work.Add(new WorkItem() { DoWork = this.Refresh });
            return work;
        }

        public IReadOnlyList<WorkItem> SaveToDb(IDatabaseWork factory, ICatalogKey key)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(modelData.Save(factory, key));
            work.AddRange(GetCatalogs(factory));
            return work;
        }

        public IReadOnlyList<WorkItem> DeleteFromDb(IDatabaseWork factory, ICatalogKey key)
        {
            List<WorkItem> work = new List<WorkItem>();
            IDbCatalogKey dbKey = new DbCatalogKey(key);

            work.Add(new WorkItem()
            {
                DoWork = () =>
                {
                    while (databaseData.FirstOrDefault(w => dbKey.Equals(w)) is CatalogValue item)
                    { databaseData.Remove(item); }
                }
            });
            work.Add(factory.CreateSave(databaseData, dbKey));
            return work;
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

    }
}
