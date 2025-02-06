// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.DataLayer.AppModel;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <summary>
    /// Wrapper for Catalog Constraint data
    /// </summary>
    public interface IConstraintData : IBindingData<ConstraintValue>
    { }

    class ConstraintData : ConstraintCollection<ConstraintValue>, IConstraintData,
        ILoadData<ICatalogIndex>, ISaveData<ICatalogIndex>,
        ILoadData<IModelIndex>, ISaveData<IModelIndex>,
        ICatalogModel, INamedScopeSourceData
    {
        /// <inheritdoc/>
        public required ICatalog Model { get; init; }

        /// <inheritdoc/>
        /// <remarks>Constraint</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ICatalogIndex dataKey)
        { return factory.CreateLoad(this, (ICatalogKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Constraint</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ICatalogIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (ICatalogKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Constraint</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateLoad(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Constraint</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IModelKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Constraint</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ICatalogIndex dataKey)
        { return factory.CreateSave(this, (ICatalogKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Constraint</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateSave(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Constraint</remarks>
        public IReadOnlyList<WorkItem> LoadNamedScope(Action<INamedScopeSourceValue?, NamedScopeValue> addNamedScope)
        {
            return INamedScopeSourceData.LoadNamedScope<ConstraintData, ConstraintValue>
                (this, addNamedScope,
                (value) => Model.DbTables.
                    FirstOrDefault(w => new TableKeyName(value).Equals(w)));
        }

        /// <inheritdoc/>
        /// <remarks>Constraint</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        /// <remarks>Constraint</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove Constraint", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>Constraint</remarks>
        public IReadOnlyList<WorkItem> Delete(ICatalogIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Constraint", DoWork = () => { Remove(dataKey); } }.ToList(); }
    }
}
