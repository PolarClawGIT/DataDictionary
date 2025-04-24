// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.AppGeneral;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource.Enumerations;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <summary>
    /// Interface representing ER/DFD Model data
    /// </summary>
    public interface IModel :
        ILoadData<IModelIndex>, ISaveData<IModelIndex>,
        IDeleteData, IScopeType, DataLayer.AppModel.IModel,
        IBindListChanged
    {
        /// <summary>
        /// Index of the Model currently loaded.
        /// </summary>
        ModelIndex ModelIndex { get; }

        /// <summary>
        /// The Model Definitions (0 or one Model expected)
        /// </summary>
        IModelData Models { get; }

        /// <summary>
        /// List of Subject Areas within the Model
        /// </summary>
        ISubjectAreaData SubjectAreas { get; }

        /// <summary>
        /// Container for Attribute within the Model.
        /// </summary>
        IAttribute Attributes { get; }

        /// <summary>
        /// Container for Entity within the Model.
        /// </summary>
        IEntity Entities { get; }

        /// <summary>
        /// The Properties for the Model (includes common)
        /// </summary>
        IPropertyData Properties { get; }

        /// <summary>
        /// The Definitions for the Model (includes common)
        /// </summary>
        IDefinitionData Definitions { get; }
    }

    class Model : IModel, IDataTableFile
    {
        /// <inheritdoc/>
        public IModelData Models { get { return modelValues; } }
        private readonly ModelData modelValues;

        ModelValue emptyModel = new ModelValue();
        protected ModelValue CurrentModel
        {
            get
            {
                if (Models.FirstOrDefault() is ModelValue value) { return value; }
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
        public String? ModelDescription
        {
            get { return CurrentModel.ModelDescription; }
            set { CurrentModel.ModelDescription = value; }
        }

        /// <inheritdoc/>
        public ModelIndex ModelIndex
        { get { return new ModelIndex(CurrentModel); } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return CurrentModel.Scope; } }

        /// <inheritdoc/>
        public ISubjectAreaData SubjectAreas { get { return subjectValues; } }
        private readonly SubjectAreaData subjectValues;

        /// <inheritdoc/>
        public IAttribute Attributes { get { return attributeValues; } }
        private readonly Attribute attributeValues;

        /// <inheritdoc/>
        public IEntity Entities { get { return entityValues; } }
        private readonly Entity entityValues;

        /// <inheritdoc/>
        public IPropertyData Properties { get { return propertyValues; } }
        private readonly PropertyData propertyValues = new PropertyData();

        /// <inheritdoc/>
        public IDefinitionData Definitions { get { return definitionValues; } }
        private readonly DefinitionData definitionValues = new DefinitionData();

        /// <inheritdoc/>
        public Boolean RaiseListChangedEvents
        {
            get
            {
                return attributeValues.RaiseListChangedEvents
                    && entityValues.RaiseListChangedEvents
                    && propertyValues.RaiseListChangedEvents
                    && definitionValues.RaiseListChangedEvents
                    && subjectValues.RaiseListChangedEvents;
            }
            set
            {
                attributeValues.RaiseListChangedEvents = value;
                entityValues.RaiseListChangedEvents = value;
                propertyValues.RaiseListChangedEvents = value;
                definitionValues.RaiseListChangedEvents = value;
                subjectValues.RaiseListChangedEvents = value;
            }
        }

        public Model() : base()
        {
            modelValues = new ModelData();
            subjectValues = new SubjectAreaData();
            attributeValues = new Attribute();
            entityValues = new Entity();
        }

        /// <summary>
        /// Sets up the Domain Model by importing application common data.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> Create(IApplicationData source)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(new WorkItem() { DoWork = () => { modelValues.Add(new ModelValue()); } });
            work.Add(new WorkItem() { DoWork = () => propertyValues.Load(source.Properties.CreateDataReader()) });
            work.Add(new WorkItem() { DoWork = () => definitionValues.Load(source.Definitions.CreateDataReader()) });

            return work;
        }

        #region ILoadData, ISaveData
        /// <inheritdoc/>
        /// <remarks>Model</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(modelValues.Load(factory, dataKey));
            work.AddRange(subjectValues.Load(factory, dataKey));
            work.AddRange(attributeValues.Load(factory, dataKey));
            work.AddRange(entityValues.Load(factory, dataKey));
            work.AddRange(propertyValues.Load(factory, dataKey));
            work.AddRange(definitionValues.Load(factory, dataKey));
            work.Add(new WorkItem() { DoWork = () => { entityValues.FindAttributes = FindAttributes; } });

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Model</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(modelValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(subjectValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(attributeValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(entityValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(propertyValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(definitionValues.Load(factory, dataKey, asOfUtcDate));
            work.Add(new WorkItem() { DoWork = () => { entityValues.FindAttributes = FindAttributes; } });
            return work;
        }

        IEnumerable<IAttributeValue> FindAttributes(PathIndex path)
        {
            List<IAttributeValue> result = new List<IAttributeValue>();
            PathIndex key = new PathIndex(path);

            result.AddRange(attributeValues.Values.Where(w => key.Equals(w.AttributePath)));

            result.AddRange(
                attributeValues.Values.
                Where(w => attributeValues.Values.
                    Any(a => key.Equals(w.AttributePath))));

            return result.DistinctBy(d => new AttributeIndex(d));
        }

        /// <inheritdoc/>
        /// <remarks>Model</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
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
        /// <remarks>Model</remarks>
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
        /// <remarks>Model</remarks>
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
        /// <remarks>Model</remarks>
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
        /// <remarks>Model</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
        { return Delete(); }

        #endregion

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> LoadNamedScope(Action<INamedScopeSourceValue?, NamedScopeValue> addNamedScope)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem()
            {
                DoWork = () =>
                {
                    NamedScopeValue newItem = new NamedScopeValue(CurrentModel)
                    { GetPath = () => new PathIndex(((IPathValue)CurrentModel).Path) };

                    addNamedScope(null, newItem);
                }
            });

            work.AddRange(subjectValues.LoadNamedScope(CurrentModel, addNamedScope));
            work.AddRange(entityValues.LoadNamedScope(CurrentModel, subjectValues, addNamedScope));
            work.AddRange(attributeValues.LoadNamedScope(CurrentModel, subjectValues, addNamedScope));

            return work;
        }

        /// <inheritdoc/>
        public void Remove(IModelIndex dataKey)
        {
            modelValues.Remove(dataKey);
            subjectValues.Remove(dataKey);
            attributeValues.Remove(dataKey);
            entityValues.Remove(dataKey);
            propertyValues.Remove(dataKey);
            definitionValues.Remove(dataKey);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            modelValues.Clear();
            subjectValues.Clear();
            attributeValues.Clear();
            entityValues.Clear();
            propertyValues.Clear();
            definitionValues.Clear();
        }

        /// <inheritdoc/>
        public void ResetBindings()
        {
            modelValues.ResetBindings();
            subjectValues.ResetBindings();
            attributeValues.ResetBindings();
            entityValues.ResetBindings();
            propertyValues.ResetBindings();
            definitionValues.ResetBindings();
        }
    }
}
