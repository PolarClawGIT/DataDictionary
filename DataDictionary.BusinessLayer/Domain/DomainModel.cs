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

        /// <inheritdoc/>
        /// <remarks>Domain</remarks>
        public IReadOnlyList<WorkItem> Build(INamedScopeDictionary target)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(attributeValues.Build(target));
            work.AddRange(entityValues.Build(target));
            return work;
        }


        [Obsolete("Need this code to build out NameScope")]
        public IReadOnlyList<NamedScopeItem> GetNameScopes()
        {
            List<NamedScopeItem> result = new List<NamedScopeItem>();
            /* TODO: Needs to be re-worked
            List<ModelItem> models = DomainModel.ToList();
            List<ModelSubjectAreaItem> subjectAreas = ModelSubjectAreas.ToList();
            List<DomainAttributeItem> attributeSubjects = DomainAttributes.ToList();
            List<DomainEntityItem> entitySubjects = DomainEntities.ToList();
            List<DomainEntityItem> entities = DomainEntities.ToList();
            List<DomainAttributeItem> attributes = DomainAttributes.ToList();

            List<DomainEntityItem> missingEntities = DomainEntities.ToList();
            List<DomainAttributeItem> missingAttributes = DomainAttributes.ToList();

            Int32 totalWork = models.Count;
            Int32 completedWork = 0;

            foreach (ModelItem modelItem in models) // There is only One, coded if future can support many
            {
                List<(NameSpaceKey nameSpace, ModelNameSpaceKey key)> modelNameSpace = NameSpaceKey.Group(
                    ModelSubjectAreas.
                        //Where(w => !String.IsNullOrWhiteSpace(w.SubjectAreaNameSpace)).
                        Select(s => new NameSpaceKey(s))).
                        Select(s => (s, new ModelNameSpaceKey(s))).
                        ToList();

                ModelKey modelKey = new ModelKey(modelItem);

                totalWork = totalWork +
                    attributes.Count +
                    entities.Count;

                result.Add(new NameScopeItem(modelItem));
                progress(completedWork++, totalWork);

                foreach ((NameSpaceKey nameSpace, ModelNameSpaceKey key) nameSpaceItem in modelNameSpace.OrderBy(o => o.nameSpace))
                {
                    (NameSpaceKey nameSpace, ModelNameSpaceKey key) parent = modelNameSpace.
                        FirstOrDefault(w =>
                            nameSpaceItem.nameSpace.ParentKey is NameSpaceKey nameSpaceParent
                            && nameSpaceParent.Equals(w.nameSpace));

                    ModelSubjectAreaItem? parentSubject = subjectAreas.
                        FirstOrDefault(w =>
                            nameSpaceItem.nameSpace.ParentKey is NameSpaceKey subjectNameSpaceParent
                            && subjectNameSpaceParent.Equals(new NameSpaceKey(w)));

                    IEnumerable<ModelSubjectAreaItem> subjectItems = subjectAreas.
                        Where(w => nameSpaceItem.nameSpace.Equals(new NameSpaceKey(w)));

                    foreach (ModelSubjectAreaItem subjectItem in subjectItems)
                    {
                        ModelNameSpaceItem newItem;

                        if (parentSubject is not null)
                        { newItem = new ModelNameSpaceItem(parentSubject, subjectItem); }
                        else
                        {
                            if (parent.Equals(default))
                            { newItem = new ModelNameSpaceItem(modelItem, subjectItem); }
                            else { newItem = new ModelNameSpaceItem(parent.key, subjectItem); }
                        }

                        result.Add(newItem);

                        ModelSubjectAreaKey subjectKey = new ModelSubjectAreaKey(subjectItem);
                        progress(completedWork++, totalWork);

                        // Add Entities
                        foreach (ModelEntityItem modelEntity in entitySubjects.Where(w => subjectKey.Equals(w)))
                        {
                            DomainEntityKey entityKey = new DomainEntityKey(modelEntity);

                            foreach (DomainEntityItem entityItem in entities.Where(w => entityKey.Equals(w)))
                            {
                                if (missingEntities.Contains(entityItem)) { missingEntities.Remove(entityItem); }

                                data.ModelNamespace.Add(new ModelNameSpaceItem(subjectItem, entityItem));
                                progress(completedWork++, totalWork);
                            }
                        }

                        // Add Attributes
                        foreach (ModelAttributeItem modelAttribute in attributeSubjects.Where(w => subjectKey.Equals(w)))
                        {
                            DomainAttributeKey attributeKey = new DomainAttributeKey(modelAttribute);

                            foreach (DomainAttributeItem attributeItem in attributes.Where(w => attributeKey.Equals(w)))
                            {
                                if (missingAttributes.Contains(attributeItem)) { missingAttributes.Remove(attributeItem); }

                                data.ModelNamespace.Add(new ModelNameSpaceItem(subjectItem, attributeItem));
                                progress(completedWork++, totalWork);
                            }
                        }

                    }

                    // Handle No Subject Area matching (normally does not occur)
                    if (subjectItems.Count() == 0)
                    {
                        ModelNameSpaceItem newItem;

                        if (parentSubject is not null)
                        { newItem = new ModelNameSpaceItem(parentSubject, nameSpaceItem.nameSpace, nameSpaceItem.key); }
                        else
                        {
                            if (parent.Equals(default))
                            { newItem = new ModelNameSpaceItem(modelItem, nameSpaceItem.nameSpace, nameSpaceItem.key); }
                            else { newItem = new ModelNameSpaceItem(parent.key, nameSpaceItem.nameSpace, nameSpaceItem.key); }
                        }

                        data.ModelNamespace.Add(newItem);
                    }

                }

                // Handle items not in a Subject Area scoped to the Model
                foreach (DomainEntityItem entityItem in missingEntities)
                {
                    data.ModelNamespace.Add(new ModelNameSpaceItem(modelItem, entityItem));
                    progress(completedWork++, totalWork);
                }

                foreach (DomainAttributeItem attributeItem in missingAttributes)
                {
                    data.ModelNamespace.Add(new ModelNameSpaceItem(modelItem, attributeItem));
                    progress(completedWork++, totalWork);
                }

            }*/

            return result;
        }

    }
}
