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
        IDeleteData

    {
        /// <summary>
        /// List of Domain Attributes within the Model.
        /// </summary>
        IAttributeData Attributes { get; }

        /// <summary>
        /// List of Domain Entities within the Model.
        /// </summary>
        IEntityData Entities { get; }

        /// <summary>
        /// The Properties for the Model (includes common)
        /// </summary>
        public IPropertyData Properties { get; }

        /// <summary>
        /// The Definitions for the Model (includes common)
        /// </summary>
        public IDefinitionData Definitions { get; }
    }

    class DomainModel : IDomainModel, IDataTableFile
    {
        /// <inheritdoc/>
        public required ISubjectAreaData SubjectAreas { get; init; }

        /// <summary>
        /// Reference to the containing Model
        /// </summary>
        public required IModelData Models { get; init; }

        /// <inheritdoc/>
        public IAttributeData Attributes { get { return attributeValues; } }
        private readonly AttributeData attributeValues;

        /// <inheritdoc/>
        public IEntityData Entities { get { return entityValues; } }
        private readonly EntityData entityValues;

        /// <inheritdoc/>
        public IPropertyData Properties { get { return propertyValues; } }
        private readonly PropertyData propertyValues = new PropertyData();

        /// <inheritdoc/>
        public IDefinitionData Definitions { get { return definitionValues; } }
        private readonly DefinitionData definitionValues = new DefinitionData();


        public DomainModel() : base()
        {
            attributeValues = new AttributeData() { Model = this };
            entityValues = new EntityData() { Model = this };
        }

        /// <summary>
        /// Sets up the Domain Model by importing application common data.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> Create(IApplicationData source)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(new WorkItem() { DoWork = () => propertyValues.Load(source.Properties.CreateDataReader()) });
            work.Add(new WorkItem() { DoWork = () => definitionValues.Load(source.Definitions.CreateDataReader()) });

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Domain</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(attributeValues.Load(factory, dataKey));
            work.AddRange(entityValues.Load(factory, dataKey));
            work.AddRange(propertyValues.Load(factory, dataKey));
            work.AddRange(definitionValues.Load(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Domain</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(attributeValues.Save(factory, dataKey));
            work.AddRange(entityValues.Save(factory, dataKey));
            work.AddRange(propertyValues.Save(factory, dataKey));
            work.AddRange(definitionValues.Save(factory, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Domain</remarks>
        public IReadOnlyList<System.Data.DataTable> Export()
        {
            List<System.Data.DataTable> result = new List<System.Data.DataTable>();
            result.AddRange(attributeValues.Export());
            result.AddRange(entityValues.Export());
            result.AddRange(propertyValues.Export());
            result.AddRange(definitionValues.Export());
            return result;
        }

        /// <inheritdoc/>
        /// <remarks>Domain</remarks>
        public void Import(System.Data.DataSet source)
        {
            attributeValues.Import(source);
            entityValues.Import(source);
            propertyValues.Import(source);
            definitionValues.Import(source);
        }

        /// <inheritdoc/>
        /// <remarks>Domain</remarks>
        public IReadOnlyList<WorkItem> Delete()
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(attributeValues.Delete());
            work.AddRange(entityValues.Delete());
            work.AddRange(propertyValues.Delete());
            work.AddRange(definitionValues.Delete());
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Domain</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
        {   return Delete(); }

        /// <inheritdoc/>
        /// <remarks>Domain</remarks>
        public IEnumerable<NamedScopePair> GetNamedScopes()
        {
            List<NamedScopePair> result = new List<NamedScopePair>();
            result.AddRange(attributeValues.GetNamedScopes());
            result.AddRange(entityValues.GetNamedScopes());
            return result;
        }

    }
}
