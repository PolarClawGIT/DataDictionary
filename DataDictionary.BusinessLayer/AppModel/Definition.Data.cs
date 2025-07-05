// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using System.Diagnostics.CodeAnalysis;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <summary>
    /// Provides methods to retrieve definition values.
    /// </summary>
    public interface IDefinitionGetValue
    {
        /// <summary>
        /// Attempts to retrieve the Definition value associated with the specified definition index.
        /// </summary>
        /// <param name="definitionIndex">The index of the definition to retrieve.</param>
        /// <param name="definitionValue">When this method returns, contains the definition value associated with the specified index, if the index is found; otherwise, null.</param>
        /// <returns>True if the definition value is found; otherwise, false.</returns>
        Boolean TryGetValue(IDefinitionIndex definitionIndex, [NotNullWhen(true)] out IDefinitionValue? definitionValue);
    }


    /// <summary>
    /// Interface component for the Definition data
    /// </summary>
    /// <remarks>Used to hide the DataLayer methods from the Application Layer.</remarks>
    public interface IDefinitionData :
        IBindingData<DefinitionValue>,
        ILoadData, ILoadData<IDefinitionIndex>, ISaveData<IDefinitionIndex>,
        IDefinitionGetValue
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

        /// <inheritdoc/>
        /// <remarks>Definition</remarks>
        public void Remove(IDefinitionIndex dataKey)
        { base.Remove(dataKey); }

        /// <inheritdoc/>
        /// <remarks>Definition</remarks>
        public void Remove(IModelIndex dataKey)
        { Clear(); }

        /// <inheritdoc/> 
        public Boolean TryGetValue(IDefinitionIndex definitionIndex, [NotNullWhen(true)] out IDefinitionValue? definitionValue)
        {
            DefinitionIndex key = new DefinitionIndex(definitionIndex);

            if (this.FirstOrDefault(w => key.Equals(w)) is IDefinitionValue value)
            { definitionValue = value; return true; }
            else { definitionValue = null; return false; }
        }
    }
}
