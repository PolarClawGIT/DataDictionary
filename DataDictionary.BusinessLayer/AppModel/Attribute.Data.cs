using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.Domain;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;
using System.Xml.Linq;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <summary>
    /// Interface component for the Model Attribute
    /// </summary>
    public interface IAttributeData :
        IBindingData<AttributeValue>,
        ILoadData<IAttributeIndex>, ISaveData<IAttributeIndex>
    {
        /// <summary>
        /// List of Domain Aliases for the Attributes within the Model.
        /// </summary>
        IAttributeAliasData Aliases { get; }

        /// <summary>
        /// List of Domain Properties for the Attributes within the Model.
        /// </summary>
        IAttributePropertyData Properties { get; }

        /// <summary>
        /// List of Domain Definitions for the Attributes within the Model.
        /// </summary>
        IAttributeDefinitionData Definitions { get; }

        /// <summary>
        /// List of Subject Areas for the Attributes within the Model.
        /// </summary>
        IAttributeSubjectAreaData SubjectArea { get; }

        /// <summary>
        /// Generates the XElement using the ScriptingData
        /// </summary>
        /// <param name="scripting"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <remarks>Not for use outside of BusinessLayer</remarks>
        XElement? GetXElement(ScriptingWork scripting, IAttributeIndex index);
    }

    class AttributeData : DomainAttributeCollection<AttributeValue>, IAttributeData,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IDataTableFile, INamedScopeSourceData
    {
        public required DomainModel Model { get; init; }

        /// <inheritdoc/>
        public IAttributeAliasData Aliases { get { return aliasValues; } }
        private readonly AttributeAliasData aliasValues;

        /// <inheritdoc/>
        public IAttributePropertyData Properties { get { return propertyValues; } }
        private readonly AttributePropertyData propertyValues;

        /// <inheritdoc/>
        public IAttributeDefinitionData Definitions { get { return definitionValues; } }
        private readonly AttributeDefinitionData definitionValues;

        /// <inheritdoc/>
        public IAttributeSubjectAreaData SubjectArea { get { return subjectAreaValues; } }
        private readonly AttributeSubjectAreaData subjectAreaValues;

        public AttributeData() : base()
        {
            aliasValues = new AttributeAliasData();
            propertyValues = new AttributePropertyData() { Attributes = this };
            definitionValues = new AttributeDefinitionData();
            subjectAreaValues = new AttributeSubjectAreaData();
        }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public override void Remove(IAttributeKey attributeItem)
        {
            base.Remove(attributeItem);
            AttributeKey key = new AttributeKey(attributeItem);
            aliasValues.Remove(key);
            propertyValues.Remove(key);
            definitionValues.Remove(key);
            subjectAreaValues.Remove(key);
            Model.Entities.Attributes.Remove(key);
        }

        #region ILoadData, ISaveData
        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IAttributeKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateLoad(this, dataKey));
            work.Add(factory.CreateLoad(aliasValues, dataKey));
            work.Add(factory.CreateLoad(propertyValues, dataKey));
            work.Add(factory.CreateLoad(definitionValues, dataKey));
            work.Add(factory.CreateLoad(subjectAreaValues, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IAttributeIndex dataKey)
        { return Save(factory, (IAttributeKey)dataKey); }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateLoad(this, dataKey));
            work.Add(factory.CreateLoad(aliasValues, dataKey));
            work.Add(factory.CreateLoad(propertyValues, dataKey));
            work.Add(factory.CreateLoad(definitionValues, dataKey));
            work.Add(factory.CreateLoad(subjectAreaValues, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IAttributeKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateSave(this, dataKey));
            work.Add(factory.CreateSave(aliasValues, dataKey));
            work.Add(factory.CreateSave(propertyValues, dataKey));
            work.Add(factory.CreateSave(definitionValues, dataKey));
            work.Add(factory.CreateSave(subjectAreaValues, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IAttributeIndex dataKey)
        { return Load(factory, (IAttributeKey)dataKey); }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.CreateSave(this, dataKey));
            work.Add(factory.CreateSave(aliasValues, dataKey));
            work.Add(factory.CreateSave(propertyValues, dataKey));
            work.Add(factory.CreateSave(definitionValues, dataKey));
            work.Add(factory.CreateSave(subjectAreaValues, dataKey));
            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Delete()
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem() { WorkName = "Remove Attribute", DoWork = () => { Clear(); } });
            work.AddRange(aliasValues.Delete());
            work.AddRange(propertyValues.Delete());
            work.AddRange(definitionValues.Delete());
            work.AddRange(subjectAreaValues.Delete());

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Delete(IAttributeIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Attribute", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
        { return Delete(); }
        #endregion

        #region IDataTableFile

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<System.Data.DataTable> Export()
        {
            List<System.Data.DataTable> result = new List<System.Data.DataTable>();
            result.Add(this.ToDataTable());
            result.Add(aliasValues.ToDataTable());
            result.Add(propertyValues.ToDataTable());
            result.Add(definitionValues.ToDataTable());
            result.Add(subjectAreaValues.ToDataTable());
            return result;
        }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public void Import(System.Data.DataSet source)
        {
            Load(source);
            aliasValues.Load(source);
            propertyValues.Load(source);
            definitionValues.Load(source);
            subjectAreaValues.Load(source);
        }


        #endregion

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> LoadNamedScope(Action<INamedScopeSourceValue?, NamedScopeValue> addNamedScope)
        {
            List<WorkItem> work = new List<WorkItem>();
            Action<Int32, Int32> progressChanged = (completed, total) => { };

            WorkItem newWork = new WorkItem(ref progressChanged)
            {
                WorkName = "Adding NamedScopes (Attribute)",
                DoWork = () =>
                {
                    Int32 completed = 0;
                    Int32 total = this.Count();

                    ModelValue? model = Model.Models.FirstOrDefault();

                    foreach (AttributeValue attribute in this)
                    {
                        //NamedScopeValue newItem = new NamedScopeValue(attribute);
                        Boolean hasParent = false;

                        foreach (EntityValue entityParent in ParentEntites(attribute))
                        {
                            NamedScopeValue newItem = new NamedScopeValue(attribute)
                            {
                                GetPath = () => new PathIndex(
                                    ((IPathValue)entityParent).Path,
                                     ((IPathValue)attribute).Path)
                            };
                            addNamedScope(entityParent, newItem);
                            hasParent = true;
                        }

                        foreach (SubjectAreaValue subjectParent in ParentSubjects(attribute))
                        {
                            NamedScopeValue newItem = new NamedScopeValue(attribute)
                            {
                                GetPath = () => new PathIndex(
                                    ((IPathValue)subjectParent).Path,
                                    ((IPathValue)attribute).Path)
                            };
                            addNamedScope(subjectParent, newItem);
                            hasParent = true;
                        }

                        if (!hasParent) // No Parents found
                        {
                            NamedScopeValue newItem = new NamedScopeValue(attribute);
                            addNamedScope(model, newItem);
                        }

                        progressChanged(completed++, total);
                    }
                }
            };

            work.Add(newWork);

            return work;

            IEnumerable<EntityValue> ParentEntites(AttributeValue attribute)
            {
                AttributeIndex key = new AttributeIndex(attribute);

                return this.
                    Where(w => key.Equals(w)).
                    Join(Model.Entities.Attributes,
                        attribute => new AttributeIndex(attribute),
                        entity => new AttributeIndex(entity),
                        (attribute, entity) => new EntityIndex(entity)).
                    Join(Model.Entities,
                        entityKey => entityKey,
                        entity => new EntityIndex(entity),
                        (key, entity) => entity).
                    ToList();
            }

            IEnumerable<SubjectAreaValue> ParentSubjects(AttributeValue attribute)
            {

                AttributeIndex key = new AttributeIndex(attribute);

                return this.
                    Where(w => key.Equals(w)).
                    Join(SubjectArea,
                        attribute => new AttributeIndex(attribute),
                        subject => new AttributeIndex(subject),
                        (attribute, subject) => new SubjectAreaIndex(subject)).
                    Join(Model.SubjectAreas,
                        subjectKey => subjectKey,
                        subject => new SubjectAreaIndex(subject),
                        (key, subject) => subject).
                    ToList();
            }
        }

        #region XML Scripting

        /// <inheritdoc/>
        public XElement? GetXElement(ScriptingWork scripting, IAttributeIndex index)
        {
            XElement? result = null;
            AttributeIndex key = new AttributeIndex(index);
            if (this.FirstOrDefault(w => key.Equals(w)) is AttributeValue attribute)
            {
                foreach (TemplateNodeValue node in scripting.Nodes.Where(w => w.PropertyScope == attribute.Scope))
                {
                    XObject? value = null;

                    switch (node.PropertyName)
                    {
                        case nameof(attribute.AttributeTitle): value = node.BuildXObject(attribute.AttributeTitle); break;
                        case nameof(attribute.AttributeDescription): value = node.BuildXObject(attribute.AttributeDescription); break;
                        case nameof(attribute.IsCompositeType): value = node.BuildXObject(attribute.IsCompositeType); break;
                        case nameof(attribute.IsDerived): value = node.BuildXObject(attribute.IsDerived); break; ;
                        case nameof(attribute.IsIntegral): value = node.BuildXObject(attribute.IsIntegral); break; ;
                        case nameof(attribute.IsKey): value = node.BuildXObject(attribute.IsKey); break; ;
                        case nameof(attribute.IsMultiValue): value = node.BuildXObject(attribute.IsMultiValue); break; ;
                        case nameof(attribute.IsNonKey): value = node.BuildXObject(attribute.IsNonKey); break; ;
                        case nameof(attribute.IsNullable): value = node.BuildXObject(attribute.IsNullable); break; ;
                        case nameof(attribute.IsSimpleType): value = node.BuildXObject(attribute.IsSimpleType); break; ;
                        case nameof(attribute.IsSingleValue): value = node.BuildXObject(attribute.IsSingleValue); break; ;
                        case nameof(attribute.IsValued): value = node.BuildXObject(attribute.IsValued); break; ;
                        default:
                            break;
                    }

                    if (value is XObject)
                    {
                        if (result is null) { result = new XElement(ScopeEnumeration.Cast(attribute.Scope).Name); }
                        result.Add(value);

                        IReadOnlyList<XAttribute> attributes = Model.Properties.GetXAttributes(scripting, node, Properties);

                        if (value is XElement element) { element.Add(attributes.ToArray()); }
                        else if (value.Parent is XElement) { value.Parent.Add(attributes.ToArray()); }
                    }
                }

                foreach (AttributeAliasValue alias in Aliases.Where(w => key.Equals(w)))
                {
                    XElement? aliasNode = alias.GetXElement(scripting, (node) => Model.Properties.GetXAttributes(scripting, node, Properties));
                    if (aliasNode is not null && result is null)
                    {
                        result = new XElement(ScopeEnumeration.Cast(attribute.Scope).Name);
                        result.Add(aliasNode);
                    }
                    else if (aliasNode is not null && result is XElement)
                    { result.Add(aliasNode); }
                }
            }

            return result;
        }

        #endregion
    }
}
