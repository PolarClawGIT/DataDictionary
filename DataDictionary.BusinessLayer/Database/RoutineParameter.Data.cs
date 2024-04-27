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
    public interface IRoutineParameterData: IBindingData<RoutineParameterValue>
    { }

    class RoutineParameterData: DbRoutineParameterCollection<RoutineParameterValue>, IRoutineParameterData,
        ILoadData<IDbCatalogKey>, ISaveData<IDbCatalogKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IDatabaseModelItem, IGetNamedScopes
<<<<<<< HEAD
        where TValue : RoutineParameterValue, new()

=======
>>>>>>> RenameIndexValue
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
        public IEnumerable<NamedScopePair> GetNamedScopes()
        {
            List<NamedScopePair> result = new List<NamedScopePair>();
<<<<<<< HEAD

            foreach (TValue item in this)
            {
                RoutineIndexName keyName = new RoutineIndexName(item);

                if (Database.DbRoutines.FirstOrDefault(w => keyName.Equals(w)) is RoutineValue routine)
                { result.Add(new NamedScopePair(routine.GetSystemId(), item)); }
            }

=======
            foreach (RoutineParameterValue item in this)
            {
                DbRoutineKeyName nameKey = new DbRoutineKeyName(item);
                if (Database.DbRoutines.FirstOrDefault(w => nameKey.Equals(w)) is RoutineValue parent)
                { result.Add(new NamedScopePair(parent.GetSystemId(), item)); }
            }

>>>>>>> RenameIndexValue
            return result;
        }
    }
}
