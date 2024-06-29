using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.DomainData.Definition;
using DataDictionary.DataLayer.ModelData;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <summary>
    /// Interface component for the Definition data
    /// </summary>
    /// <remarks>Used to hide the DataLayer methods from the Application Layer.</remarks>
    public interface IDefinitionData :
        IBindingData<DefinitionValue>,
        ILoadData, ILoadData<IDefinitionIndex>, ISaveData<IDefinitionIndex>
    { }

    /// <inheritdoc/>
    class DefinitionData : DomainDefinitionCollection<DefinitionValue>, IDefinitionData,
        ILoadData<IModelKey>, ISaveData<IModelKey>, IDataTableFile
    {
        /// <inheritdoc/>
        /// <remarks>Definition</remarks>
        public virtual IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Definition</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDefinitionIndex dataKey)
        { return Load(factory, (IDomainDefinitionKey)dataKey); }

        /// <inheritdoc/>
        /// <remarks>Definition</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDomainDefinitionKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Definition</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDefinitionIndex dataKey)
        { return Save(factory, (IDomainDefinitionKey)dataKey); }

        /// <inheritdoc/>
        /// <remarks>Definition</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDomainDefinitionKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Definition</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Definition</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Definition</remarks>
        public IReadOnlyList<System.Data.DataTable> Export()
        { return this.ToDataTable().ToList(); }

        /// <inheritdoc/>
        /// <remarks>Definition</remarks>
        public void Import(System.Data.DataSet source)
        { this.Load(source); }

        /// <inheritdoc/>
        /// <remarks>Definition</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove Property", DoWork = () => { this.Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>Definition</remarks>
        public IReadOnlyList<WorkItem> Delete(IDefinitionIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Property", DoWork = () => { this.Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>Definition</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
        { return Delete(); }
    }
}
