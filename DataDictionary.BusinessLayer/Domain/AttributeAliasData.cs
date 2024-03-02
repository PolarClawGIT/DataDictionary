using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.ModelData;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <summary>
    /// Interface component for the Model Attribute Alias
    /// </summary>
    public interface IAttributeAliasData :
        IBindingData<DomainAttributeAliasItem>
    {

    }

    class AttributeAliasData : DomainAttributeAliasCollection, IAttributeAliasData,
        ILoadData<IDomainAttributeKey>, ISaveData<IDomainAttributeKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>
    {
        /// <inheritdoc/>
        /// <remarks>AttributeAlias</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDomainAttributeKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeAlias</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeAlias</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDomainAttributeKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeAlias</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }
    }
}
