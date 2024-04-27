using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.ModelData;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <summary>
    /// Interface component for the Model Attribute Property
    /// </summary>
<<<<<<< HEAD
    public interface IAttributePropertyData<TValue> : IBindingData<TValue>
        where TValue : AttributePropertyValue, IAttributePropertyValue
    { }

    class AttributePropertyData<TValue> : DomainAttributePropertyCollection<TValue>, IAttributePropertyData<TValue>,
=======
    public interface IAttributePropertyData : IBindingData<AttributePropertyValue>
    {

    }

    class AttributePropertyData : DomainAttributePropertyCollection<AttributePropertyValue>, IAttributePropertyData,
>>>>>>> RenameIndexValue
        ILoadData<IDomainAttributeKey>, ISaveData<IDomainAttributeKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>
        where TValue : AttributePropertyValue, IAttributePropertyValue, new()
    {
        /// <inheritdoc/>
        /// <remarks>AttributeProperty</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDomainAttributeKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeProperty</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeProperty</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDomainAttributeKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeProperty</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }
    }
}
