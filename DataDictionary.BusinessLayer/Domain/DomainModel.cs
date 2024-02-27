using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.Model;
using DataDictionary.BusinessLayer.NameScope;
using DataDictionary.DataLayer.ModelData;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <summary>
    /// Interface representing Domain data
    /// </summary>
    public interface IDomainModel:
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IRemoveData

    {
        /// <summary>
        /// List of Domain Attributes within the Model.
        /// </summary>
        IAttributeData DomainAttributes { get; }

        /// <summary>
        /// List of Domain Entities within the Model.
        /// </summary>
        IEntityData DomainEntities { get; }
    }

    class DomainModel: IDomainModel,
        IDataTableFile
    //, INameScopeData
    {
        /// <inheritdoc/>
        public IAttributeData DomainAttributes { get { return attributes; } }
        private readonly AttributeData attributes;

        /// <inheritdoc/>
        public IEntityData DomainEntities { get { return entites; } }
        private readonly EntityData entites;

        public DomainModel() : base ()
        {
            attributes = new AttributeData();
            entites = new EntityData();
        }

        /// <inheritdoc/>
        /// <remarks>Domain</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            
            work.AddRange(attributes.Load(factory, dataKey));
            work.AddRange(entites.Load(factory, dataKey));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Domain</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(attributes.Save(factory, dataKey));
            work.AddRange(entites.Save(factory, dataKey));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Domain</remarks>
        public IReadOnlyList<System.Data.DataTable> Export()
        {
            List<System.Data.DataTable> result = new List<System.Data.DataTable>();
            result.AddRange(attributes.Export());
            result.AddRange(entites.Export());
            return result;
        }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public void Import(System.Data.DataSet source)
        {
            attributes.Import(source);
            entites.Import(source);
        }

        /// <inheritdoc/>
        /// <remarks>Domain</remarks>
        public IReadOnlyList<WorkItem> Remove()
        {
            List<WorkItem> result = new List<WorkItem>();
            attributes.Remove();
            entites.Remove();

            return result;
        }



        [Obsolete("Need this code to build out NameScope")]
        public IReadOnlyList<NameScopeItem> GetNameScopes()
        {
            List<NameScopeItem> result = new List<NameScopeItem>();
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
