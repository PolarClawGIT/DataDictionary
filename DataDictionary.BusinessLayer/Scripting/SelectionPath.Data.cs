using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.ModelData;
using DataDictionary.DataLayer.ScriptingData.Selection;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <summary>
    /// Interface component for the Scripting Engine Selection Path
    /// </summary>
    [Obsolete("To be removed", true)]
    public interface ISelectionPathData :
        IBindingData<SelectionPathValue>,
        ILoadData, ILoadData<ISelectionKey>, ILoadData<IModelKey>,
        ISaveData, ISaveData<ISelectionKey>, ISaveData<IModelKey>
    { }

    [Obsolete("To be removed", true)]
    class SelectionPathData : SelectionPathCollection<SelectionPathValue>, ISelectionPathData
    {
        public IReadOnlyList<WorkItem> Delete(ISelectionKey dataKey)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<WorkItem> Delete()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        /// <remarks>Selection Path</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Selection Path</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ISelectionKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Selection Path</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Selection Path</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Selection Path</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ISelectionKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Selection Path</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return factory.CreateSave(this).ToList(); }

    }
}
