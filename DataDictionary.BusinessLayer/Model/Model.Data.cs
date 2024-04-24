using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using Toolbox.BindingTable;
using Toolbox.Threading;
using DbLayer = DataDictionary.DataLayer.ModelData;

namespace DataDictionary.BusinessLayer.Model
{
    /// <summary>
    /// Interface component for the Domain Model 
    /// </summary>
    public interface IModelData<TValue> : IBindingData<TValue>
    where TValue : ModelValue, IModelValue
    {
        /// <summary>
        /// Create WorkItem that create a new Model instance.
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Create();
    }

    class ModelData<TValue> : DbLayer.ModelCollection<TValue>,
        ILoadData<DbLayer.IModelKey>, ISaveData<DbLayer.IModelKey>,
        IModelData<TValue>
        where TValue : ModelValue, IModelValue, new()
    {
        /// <inheritdoc/>
        /// <remarks>Model</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, DbLayer.IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Model</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, DbLayer.IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Model</remarks>
        public IReadOnlyList<System.Data.DataTable> Export()
        { return this.ToDataTable().ToList(); ; }

        /// <inheritdoc/>
        /// <remarks>Model</remarks>
        public void Import(System.Data.DataSet source)
        { Load(source); }

        public IReadOnlyList<WorkItem> Remove()
        { return new WorkItem() { WorkName = "Remove Model", DoWork = () => { Clear(); } }.ToList(); }

        public IReadOnlyList<WorkItem> Create()
        { return new WorkItem() { WorkName = "Create Model", DoWork = () => { Add(new TValue()); } }.ToList(); }

        public IReadOnlyList<WorkItem> BuildNamedScope(NamedScopeData target)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(new WorkItem()
            {
                WorkName = "Build NamedScope (Model)",
                DoWork = () => { target.AddRange(GetNamedScopes()); }
            });
            return work;
        }

        public IEnumerable<NamedScopePair> GetNamedScopes()
        { return this.Select(s => new NamedScopePair(s)); }
    }
}
