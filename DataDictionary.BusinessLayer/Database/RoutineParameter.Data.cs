using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.ModelData;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Database
{
    /// <summary>
    /// Interface representing Catalog RoutineParameter data
    /// </summary>
    public interface IRoutineParameterData<TValue> : IBindingData<TValue>
        where TValue : RoutineParameterValue
    { }

    class RoutineParameterData<TValue> : DbRoutineParameterCollection<TValue>, IRoutineParameterData<TValue>,
        ILoadData<IDbCatalogKey>, ISaveData<IDbCatalogKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IDatabaseModelItem
        where TValue : RoutineParameterValue, new()

    {
        /// <inheritdoc/>
        public required IDatabaseModel Database { get; init; }

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
        /// <remarks>RoutineParameter</remarks>
        public IReadOnlyList<WorkItem> Build(INamedScopeDictionary target)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem()
            {
                WorkName = "Build NamedScope RoutineParameter",
                DoWork = () =>
                {
                    foreach (DbRoutineParameterItem item in this)
                    {
                        //target.Remove(new NamedScopeKey(item)); Done by Catalog

                        DbRoutineKeyName nameKey = new DbRoutineKeyName(item);
                        if (Database.DbRoutines.FirstOrDefault(w => nameKey.Equals(w)) is IDbRoutineItem parent)
                        { target.Add(new NamedScopeItem(parent, item)); }
                    }
                }
            });

            return work;
        }
    }
}
