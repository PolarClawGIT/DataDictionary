using DataDictionary.BusinessLayer.Application;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.Model;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.ModelData;
using DataDictionary.DataLayer.ModelData.SubjectArea;
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
        IAttributeData Attributes { get; }

        /// <summary>
        /// List of Domain Entities within the Model.
        /// </summary>
        IEntityData Entities { get; }
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
        public IAttributeData Attributes { get { return attributeValues; } }
        private readonly AttributeData attributeValues;

        /// <inheritdoc/>
        public IEntityData Entities { get { return entityValues; } }
        private readonly EntityData entityValues;

        public DomainModel() : base()
        {
            attributeValues = new AttributeData() { Model = this };
            entityValues = new EntityData() { DomainModel = this };
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
