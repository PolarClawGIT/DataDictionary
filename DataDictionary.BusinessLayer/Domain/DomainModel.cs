using DataDictionary.BusinessLayer.Application;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.Model;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ModelData;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <summary>
    /// Interface representing Domain data
    /// </summary>
    public interface IDomainModel :
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IRemoveData

    {
        /// <summary>
        /// The Properties of the Model
        /// </summary>
        /// <remarks>Reference to IApplicationData.Properties</remarks>
        IPropertyData ModelProperty { get; }

        /// <summary>
        /// List of Domain Attributes within the Model.
        /// </summary>
        IAttributeData<AttributeValue> Attributes { get; }

        /// <summary>
        /// List of Domain Entities within the Model.
        /// </summary>
        IEntityData<EntityValue> Entities { get; }
    }

    class DomainModel : IDomainModel, IDataTableFile
    {
        /// <inheritdoc/>
        public required IPropertyData ModelProperty { get; init; }

        /// <summary>
        /// Reference to the containing Model
        /// </summary>
        public required IModelData<ModelValue> Models { get; init; }

        /// <inheritdoc/>
        public IAttributeData<AttributeValue> Attributes { get { return attributeValues; } }
        private readonly AttributeData<AttributeValue> attributeValues;

        /// <inheritdoc/>
        public IEntityData<EntityValue> Entities { get { return entityValues; } }
        private readonly EntityData<EntityValue> entityValues;

        public DomainModel() : base()
        {
            attributeValues = new AttributeData<AttributeValue>() { Model = this };
            entityValues = new EntityData<EntityValue>() { DomainModel = this };
        }

        /// <inheritdoc/>
        /// <remarks>Domain</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(attributeValues.Load(factory, dataKey));
            work.AddRange(entityValues.Load(factory, dataKey));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Domain</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(attributeValues.Save(factory, dataKey));
            work.AddRange(entityValues.Save(factory, dataKey));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Domain</remarks>
        public IReadOnlyList<System.Data.DataTable> Export()
        {
            List<System.Data.DataTable> result = new List<System.Data.DataTable>();
            result.AddRange(attributeValues.Export());
            result.AddRange(entityValues.Export());
            return result;
        }

        /// <inheritdoc/>
        /// <remarks>Domain</remarks>
        public void Import(System.Data.DataSet source)
        {
            attributeValues.Import(source);
            entityValues.Import(source);
        }

        /// <inheritdoc/>
        /// <remarks>Domain</remarks>
        public IReadOnlyList<WorkItem> Remove()
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(attributeValues.Remove());
            work.AddRange(entityValues.Remove());
            return work;
        }

        public IReadOnlyList<WorkItem> BuildNamedScope(NamedScopeData target)
        {
            List<WorkItem> work = new List<WorkItem>();
            ProgressTracker progress = new ProgressTracker();

            WorkItem workItem = new WorkItem()
            {
                WorkName = "Build NamedScope (Domain)",
                DoWork = () =>
                {
                    target.AddRange(attributeValues.GetNamedScopes());
                    target.AddRange(entityValues.GetNamedScopes());
                }
            };
            progress.OnProgressChanged = workItem.OnProgressChanged;

            work.Add(workItem);
            return work;
        }
    }
}
