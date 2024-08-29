using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.ModelData;
using System.ComponentModel;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Database
{
    /// <summary>
    /// Wrapper for Catalog Constraint data
    /// </summary>
    public interface IConstraintData: IBindingData<ConstraintValue>
    { }

    class ConstraintData: DbConstraintCollection<ConstraintValue>, IConstraintData,
        ILoadData<IDbCatalogKey>, ISaveData<IDbCatalogKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IDatabaseModelItem, INamedScopeSourceData
    {
        /// <inheritdoc/>
        public required IDatabaseModel Database { get; init; }

        /// <inheritdoc/>
        /// <remarks>Constraint</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDbCatalogKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Constraint</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Constraint</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDbCatalogKey dataKey)
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
                    FirstOrDefault(w => new DbTableKeyName(value).Equals(w)));
        }

        /// <inheritdoc/>
        /// <remarks>Constraint</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
        {   return Delete(); }

        /// <inheritdoc/>
        /// <remarks>Constraint</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove Constraint", DoWork = () => { this.Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>Constraint</remarks>
        public IReadOnlyList<WorkItem> Delete(IDbCatalogKey dataKey)
        { return new WorkItem() { WorkName = "Remove Constraint", DoWork = () => { this.Remove(dataKey); } }.ToList(); }
    }
}
