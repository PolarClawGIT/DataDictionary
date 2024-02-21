using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NameScope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.ModelData;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Database
{
    /// <summary>
    /// Interface representing Catalog Routine data
    /// </summary>
    public interface IRoutineData : IBindingData<DbRoutineItem>
    {

    }

    class RoutineData: DbRoutineCollection, IRoutineData,
        ILoadData<IDbCatalogKey>, ISaveData<IDbCatalogKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IDatabaseDataItem, INameScopeData
    {
        /// <inheritdoc/>
        public required IDatabaseData Database { get; init; }

        /// <inheritdoc/>
        /// <remarks>Routine</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDbCatalogKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Routine</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Routine</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDbCatalogKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Routine</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Routine</remarks>
        public IReadOnlyList<WorkItem> Export(IList<NameScopeItem> target)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem()
            {
                WorkName = "Load NameScope, Routine",
                DoWork = () =>
                {
                    foreach (DbRoutineItem item in this.Where(w => w.IsSystem == false))
                    {
                        DbSchemaKeyName nameKey = new DbSchemaKeyName(item);
                        if (Database.DbSchemta.FirstOrDefault(w => nameKey.Equals(w)) is IDbSchemaItem parent)
                        { target.Add(new NameScopeItem(parent, item)); }
                    }
                }
            });

            return work;
        }
    }
}
