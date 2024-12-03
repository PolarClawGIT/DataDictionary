using DataDictionary.BusinessLayer.Database;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.ModelData;
using System.ComponentModel;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <summary>
    /// Wrapper for Catalog Constraint data
    /// </summary>
    public interface IConstraintData : IBindingData<ConstraintValue>
    { }

    class ConstraintData : ConstraintCollection<ConstraintValue>, IConstraintData,
        ILoadData<ICatalogKey>, ISaveData<ICatalogKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IDatabaseModelItem, INamedScopeSourceData
    {
        /// <inheritdoc/>
        public required IDatabaseModel Database { get; init; }

        /// <inheritdoc/>
        /// <remarks>Constraint</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ICatalogKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Constraint</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Constraint</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ICatalogKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Constraint</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Constraint</remarks>
        public IReadOnlyList<WorkItem> LoadNamedScope(Action<INamedScopeSourceValue?, NamedScopeValue> addNamedScope)
        {
            return INamedScopeSourceData.LoadNamedScope<ConstraintData, ConstraintValue>
                (this, addNamedScope,
                (value) => Database.DbTables.
                    FirstOrDefault(w => new TableKeyName(value).Equals(w)));
        }

        /// <inheritdoc/>
        /// <remarks>Constraint</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        /// <remarks>Constraint</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove Constraint", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>Constraint</remarks>
        public IReadOnlyList<WorkItem> Delete(ICatalogKey dataKey)
        { return new WorkItem() { WorkName = "Remove Constraint", DoWork = () => { Remove(dataKey); } }.ToList(); }
    }
}
