using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.ModelData;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <summary>
    /// Interface for the Model Entity Attribute
    /// </summary>
    public interface IEntityAttributeData : IBindingData<EntityAttributeValue> 
    { }

    /// <summary>
    /// Implementation for the Model Entity Attribute
    /// </summary>
    public class EntityAttributeData : DomainEntityAttributeCollection<EntityAttributeValue>, IEntityAttributeData,
        ILoadData<IDomainEntityKey>, ISaveData<IDomainEntityKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>
    {
        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDomainEntityKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }
        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDomainEntityKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove EntityAttribute", DoWork = () => { this.Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public IReadOnlyList<WorkItem> Delete(IDomainEntityKey dataKey)
        { return new WorkItem() { WorkName = "Remove EntityAttribute", DoWork = () => { this.Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
        { return Delete(); }
    }
}
