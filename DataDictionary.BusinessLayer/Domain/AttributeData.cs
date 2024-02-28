using DataDictionary.BusinessLayer.Database;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NameScope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.ModelData;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <summary>
    /// Interface component for the Model Attribute
    /// </summary>
    public interface IAttributeData :
        IBindingData<DomainAttributeItem>,
        ILoadData<IDomainAttributeKey>, ISaveData<IDomainAttributeKey>
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
        /// List of Model Attribute Subject Areas within the Model.
        /// </summary>
        IAttributeSubjectAreaData SubjectAreas { get; }

        void Import(IDatabaseModel source, IDbCatalogKeyName key);

        void Import(IDatabaseModel source, IDbTableKeyName key);

        void Import(IDatabaseModel source, IDbTableColumnKeyName key);
    }

    class AttributeData : DomainAttributeCollection, IAttributeData,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IDataTableFile, INameScopeData<IModelKey>
    {
        /// <inheritdoc/>
        public IAttributeAliasData Aliases { get { return aliasValues; } }
        private readonly AttributeAliasData aliasValues;

        /// <inheritdoc/>
        public IAttributePropertyData Properties { get { return propertyValues; } }
        private readonly AttributePropertyData propertyValues;

        /// <inheritdoc/>
        public IAttributeSubjectAreaData SubjectAreas { get { return subjectAreaValues; } }
        private readonly AttributeSubjectAreaData subjectAreaValues;

        public AttributeData() : base()
        {
            aliasValues = new AttributeAliasData();
            propertyValues = new AttributePropertyData();
            subjectAreaValues = new AttributeSubjectAreaData();
        }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDomainAttributeKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDomainAttributeKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        public override void Remove(IDomainAttributeKey attributeItem)
        {
            base.Remove(attributeItem);
            DomainAttributeKey key = new DomainAttributeKey(attributeItem);
            aliasValues.Remove(key);
            propertyValues.Remove(key);
            subjectAreaValues.Remove(key);
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Remove()
        {
            List<WorkItem> result = new List<WorkItem>();

            result.Add(new WorkItem()
            {
                WorkName = "Remove Attributes",
                DoWork = () =>
                {
                    aliasValues.Clear();
                    propertyValues.Clear();
                    this.Clear();
                }
            });

            return result;
        }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<System.Data.DataTable> Export()
        {
            List<System.Data.DataTable> result = new List<System.Data.DataTable>();
            result.Add(this.ToDataTable());
            result.Add(aliasValues.ToDataTable());
            result.Add(propertyValues.ToDataTable());
            result.Add(subjectAreaValues.ToDataTable());
            return result;
        }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public void Import(System.Data.DataSet source)
        {
            this.Load(source);
            aliasValues.Load(source);
            propertyValues.Load(source);
            subjectAreaValues.Load(source);
        }

        public void Import(IDatabaseModel source, IDbCatalogKeyName key)
        {
            throw new NotImplementedException();
        }

        public void Import(IDatabaseModel source, IDbTableKeyName key)
        {
            throw new NotImplementedException();
        }

        public void Import(IDatabaseModel source, IDbTableColumnKeyName key)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<WorkItem> Export(IList<NameScopeItem> target, Func<IModelKey?> parent)
        {
            return new WorkItem()
            {
                WorkName = "Load NameScope, Attributes",
                DoWork = () =>
                {
                    if (parent() is IModelKey key)
                    {
                        foreach (DomainAttributeItem item in this)
                        { target.Add(new NameScopeItem(key, item)); }
                    }
                }
            }.ToList();
        }
    }
}
