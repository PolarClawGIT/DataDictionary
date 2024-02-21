using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NameScope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.ModelData;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Database
{
    /// <summary>
    /// Interface representing Catalog RoutineParameter data
    /// </summary>
    public interface IRoutineParameterData: IBindingData<DbRoutineParameterItem>
    {

    }

    class RoutineParameterData: DbRoutineParameterCollection, IRoutineParameterData,
        ILoadData<IDbCatalogKey>, ISaveData<IDbCatalogKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IDatabaseDataItem, INameScopeData
    {
        /// <inheritdoc/>
        public required IDatabaseData Database { get; init; }

        /// <inheritdoc/>
        /// <remarks>RoutineParameter</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDbCatalogKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>RoutineParameter</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>RoutineParameter</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDbCatalogKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>RoutineParameter</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>TableColumn</remarks>
        public IReadOnlyList<WorkItem> Export(IList<NameScopeItem> target)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem()
            {
                WorkName = "Load NameScope, Table Column",
                DoWork = () =>
                {
                    foreach (DbRoutineParameterItem item in this)
                    {
                        DbRoutineKeyName nameKey = new DbRoutineKeyName(item);
                        if (Database.DbRoutines.FirstOrDefault(w => nameKey.Equals(w)) is IDbRoutineItem parent)
                        { target.Add(new NameScopeItem(parent, item)); }
                    }
                }
            });

            return work;
        }
    }
}
