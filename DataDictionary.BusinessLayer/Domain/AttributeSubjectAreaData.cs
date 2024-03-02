using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.ModelData;
using DataDictionary.DataLayer.ModelData.Attribute;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <summary>
    /// Interface component for the Model Attribute Subject Areas
    /// </summary>
    public interface IAttributeSubjectAreaData :
        IBindingData<ModelAttributeItem>
    { }


    class AttributeSubjectAreaData : ModelAttributeCollection, IAttributeSubjectAreaData,
        ILoadData<IDomainAttributeKey>, ISaveData<IDomainAttributeKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>
    {
        /// <inheritdoc/>
        /// <remarks>ModelAttribute</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDomainAttributeKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ModelAttribute</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ModelAttribute</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDomainAttributeKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ModelAttribute</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }
    }
}
