﻿using DataDictionary.BusinessLayer.Database;
using DataDictionary.BusinessLayer.DbWorkItem;
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
        IAttributeAliasData DomainAttributeAliases { get; }

        /// <summary>
        /// List of Domain Properties for the Attributes within the Model.
        /// </summary>
        IAttributePropertyData DomainAttributeProperties { get; }

        void Import(IDatabaseData source, IDbCatalogKeyName key);

        void Import(IDatabaseData source, IDbTableKeyName key);

        void Import(IDatabaseData source, IDbTableColumnKeyName key);
    }

    class AttributeData : DomainAttributeCollection, IAttributeData,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IDataTableFile
    {
        /// <inheritdoc/>
        public IAttributeAliasData DomainAttributeAliases { get { return attributeAlias; } }
        private readonly AttributeAliasData attributeAlias;

        /// <inheritdoc/>
        public IAttributePropertyData DomainAttributeProperties { get { return attributeProperty; } }
        private readonly AttributePropertyData attributeProperty;

        public AttributeData() : base()
        {
            attributeAlias = new AttributeAliasData();
            attributeProperty = new AttributePropertyData();
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
        /// <remarks>Attribute</remarks>
        public IReadOnlyList<System.Data.DataTable> Export()
        {
            List<System.Data.DataTable> result = new List<System.Data.DataTable>();
            result.Add(this.ToDataTable());
            result.Add(attributeAlias.ToDataTable());
            result.Add(attributeProperty.ToDataTable());
            return result;
        }

        /// <inheritdoc/>
        /// <remarks>Attribute</remarks>
        public void Import(System.Data.DataSet source)
        {
            this.Load(source);
            attributeAlias.Load(source);
            attributeProperty.Load(source);
        }

        public void Import(IDatabaseData source, IDbCatalogKeyName key)
        {
            throw new NotImplementedException();
        }

        public void Import(IDatabaseData source, IDbTableKeyName key)
        {
            throw new NotImplementedException();
        }

        public void Import(IDatabaseData source, IDbTableColumnKeyName key)
        {
            throw new NotImplementedException();
        }
    }
}
