// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using System.Diagnostics.CodeAnalysis;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <summary>
    /// Interface component for the Model Entity
    /// </summary>
    public interface IEntityData :
        IBindingData<EntityValue>,
        IGetTemporal<IModelIndex>, IGetTemporal<IEntityIndex>,
        ITryGetValue<IEntityIndex, IEntityValue>
    { }

    class EntityData : EntityCollection<EntityValue>, IEntityData,
        ILoadData<IModelIndex>, ISaveData<IModelIndex>,
        ILoadData<IEntityIndex>, ISaveData<IEntityIndex>
    {
        public EntityData() : base()
        { }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateLoad(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IModelKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IEntityIndex dataKey)
        { return factory.CreateLoad(this, (IEntityKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IEntityIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IEntityKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IEntityIndex dataKey)
        { return factory.CreateSave(this, (IEntityKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateSave(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove Entity", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Delete(IEntityIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Entity", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public void Remove(IEntityIndex dataKey)
        { base.Remove(dataKey); }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public void Remove(IModelIndex dataKey)
        { Clear(); }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public ITemporalData GetTemporal(IModelIndex key)
        {
            return new TemporalData<EntityData, EntityValue>()
            { CreateLoad = (factory, data) => factory.CreateHistory(data, (IModelKey)key) };
        }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public ITemporalData GetTemporal(IEntityIndex key)
        {
            return new TemporalData<EntityData, EntityValue>()
            { CreateLoad = (factory, data) => factory.CreateHistory(data, (IEntityKey)key) };
        }

        /// <inheritdoc/>
        /// <remarks>Entity</remarks>
        public Boolean TryGetValue(IEntityIndex index, [NotNullWhen(true)] out IEntityValue? value)
        {
            EntityIndex key = new EntityIndex(index);

            if (this.FirstOrDefault(w => key.Equals(w)) is IEntityValue entity)
            { value = entity; return true; }
            else { value = null; return false; }
        }
    }
}
