using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.ModelData;
using System.ComponentModel;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Database
{
    /// <summary>
    /// Interface representing Catalog Routine data
    /// </summary>
    public interface IRoutineData: IBindingData<RoutineValue>
    { }

    class RoutineData : DbRoutineCollection<RoutineValue>, IRoutineData,
        ILoadData<IDbCatalogKey>, ISaveData<IDbCatalogKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IDatabaseModelItem, INamedScopeSourceData
    {
        /// <inheritdoc/>
        public required IDatabaseModel Database { get; init; }

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
        public IReadOnlyList<WorkItem> LoadNamedScope(Action<INamedScopeSourceValue?, NamedScopeValue> addNamedScope)
        {
            return INamedScopeSourceData.LoadNamedScope<RoutineData, RoutineValue>
                (this, addNamedScope,
                (value) => Database.DbSchemta.
                    FirstOrDefault(w => new DbSchemaKeyName(value).Equals(w)));
        }

        /// <inheritdoc/>
        /// <remarks>Routine</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        /// <remarks>Routine</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove Routine", DoWork = () => { this.Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>Routine</remarks>
        public IReadOnlyList<WorkItem> Delete(IDbCatalogKey dataKey)
        { return new WorkItem() { WorkName = "Remove Routine", DoWork = () => { this.Remove(dataKey); } }.ToList(); }

    }
}
