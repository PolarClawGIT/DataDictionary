using DataDictionary.BusinessLayer.AppGeneral;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <summary>
    /// Interface representing ER/DFD Model data
    /// </summary>
    public interface IModel :
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IDeleteData, IScopeType, DataLayer.AppModel.IModel

    {
        /// <summary>
        /// The Model Definitions (0 or one Model expected)
        /// </summary>
        IModelData Models { get; }

        /// <summary>
        /// List of Subject Areas within the Model
        /// </summary>
        ISubjectAreaData SubjectAreas { get; }

        /// <summary>
        /// List of Attributes within the Model.
        /// </summary>
        IAttributeData Attributes { get; }

        /// <summary>
        /// List of Entities within the Model.
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

    class Model : IModel, IDataTableFile,
        INamedScopeSourceData
    {
        /// <inheritdoc/>
        public IModelData Models { get { return modelValues; } }
        private readonly ModelData modelValues;

        IModelValue emptyModel = new ModelValue();
        protected IModelValue CurrentModel
        {
            get
            {
                if (Models.FirstOrDefault() is IModelValue value) { return value; }
                else { return emptyModel; }
            }
        }

        /// <inheritdoc/>
        public String? ModelTitle
        {
            get { return CurrentModel.ModelTitle; }
            set { CurrentModel.ModelTitle = value; }
        }

        /// <inheritdoc/>
        public String? ModelDescription {
            get { return CurrentModel.ModelDescription; }
            set { CurrentModel.ModelDescription = value; }
        }

        /// <inheritdoc/>
        public ScopeType Scope { get { return CurrentModel.Scope; } }

        /// <inheritdoc/>
        public ISubjectAreaData SubjectAreas { get { return subjectValues; } }
        private readonly SubjectAreaData subjectValues;

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

        public Model() : base()
        {
            modelValues = new ModelData();
            subjectValues = new SubjectAreaData() { Model = this };
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

        #region ILoadData, ISaveData
        /// <inheritdoc/>
        /// <remarks>Domain</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(modelValues.Load(factory, dataKey));
            work.AddRange(subjectValues.Load(factory, dataKey));
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
            work.AddRange(modelValues.Save(factory, dataKey));
            work.AddRange(subjectValues.Save(factory, dataKey));
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
            result.AddRange(modelValues.Export());
            result.AddRange(subjectValues.Export());
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
            modelValues.Import(source);
            subjectValues.Import(source);
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
            work.AddRange(modelValues.Delete());
            work.AddRange(subjectValues.Delete());
            work.AddRange(attributeValues.Delete());
            work.AddRange(entityValues.Delete());
            work.AddRange(propertyValues.Delete());
            work.AddRange(definitionValues.Delete());
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Domain</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
        { return Delete(); }

        #endregion

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> LoadNamedScope(Action<INamedScopeSourceValue?, NamedScopeValue> addNamedScope)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(modelValues.LoadNamedScope(addNamedScope));
            work.AddRange(subjectValues.LoadNamedScope(addNamedScope));
            work.AddRange(entityValues.LoadNamedScope(addNamedScope));
            work.AddRange(attributeValues.LoadNamedScope(addNamedScope));

            return work;
        }
    }
}
