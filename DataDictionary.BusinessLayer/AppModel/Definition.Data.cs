// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
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
    class DefinitionData : DefinitionCollection<DefinitionValue>, IDefinitionData,
        ILoadData<IModelIndex>, ISaveData<IModelIndex>, IDataTableFile
    {
        /// <inheritdoc/>
        /// <remarks>Definition</remarks>
        public virtual IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Definition</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IModelKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Definition</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDefinitionIndex dataKey)
        { return factory.CreateLoad(this, (IDefinitionKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Definition</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDefinitionIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IDefinitionKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Definition</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDefinitionIndex dataKey)
        { return factory.CreateSave(this, (IDefinitionKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Definition</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateLoad(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Definition</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateSave(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Definition</remarks>
        public IReadOnlyList<System.Data.DataTable> Export()
        { return this.ToDataTable().ToList(); }

        /// <inheritdoc/>
        /// <remarks>Definition</remarks>
        public void Import(System.Data.DataSet source)
        { Load(source); }

        /// <inheritdoc/>
        /// <remarks>Definition</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove Property", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>Definition</remarks>
        public IReadOnlyList<WorkItem> Delete(IDefinitionIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Property", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>Definition</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
        { return Delete(); }


    }
}
