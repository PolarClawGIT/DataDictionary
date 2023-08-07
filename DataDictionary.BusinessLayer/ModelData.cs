using DataDictionary.DataLayer;
using DataDictionary.DataLayer.ApplicationData;
using DataDictionary.DataLayer.DbMetaData;
using DataDictionary.DataLayer.DomainData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.BusinessLayer
{
    /// <summary>
    /// Represents the main data object used by the application.
    /// </summary>
    public class ModelData
    {
        public ModelItem Model { get { return Models.First(); } } // There should always be exactly one model.
        public FileInfo? ModelFile { get; internal set; }
        internal protected Context ModelContext { get; protected set; }= new Context();

        // Database Model
        public BindingTable<DbCatalogItem> DbCatalogs { get; } = ModelFactory.Create<DbCatalogItem>();
        public BindingTable<DbSchemaItem> DbSchemta { get; } = ModelFactory.Create<DbSchemaItem>();
        public BindingTable<DbTableItem> DbTables { get; } = ModelFactory.Create<DbTableItem>();
        public BindingTable<DbTableColumnItem> DbColumns { get; } = ModelFactory.Create<DbTableColumnItem>();
        public BindingTable<DbExtendedPropertyItem> DbExtendedProperties = ModelFactory.Create<DbExtendedPropertyItem>();
        public BindingTable<DbConstraintItem> DbConstraints { get; } = ModelFactory.Create<DbConstraintItem>();
        public BindingTable<DbConstraintColumnItem> DbConstraintColumns { get; } = ModelFactory.Create<DbConstraintColumnItem>();

        // Domain Model
        public BindingTable<DomainAttributeItem> DomainAttributes = ModelFactory.Create<DomainAttributeItem>();
        public BindingTable<DomainAttributeAliasItem> DomainAttributeAliases = ModelFactory.Create<DomainAttributeAliasItem>();
        public BindingTable<DomainAttributePropertyItem> DomainAttributeProperties = ModelFactory.Create<DomainAttributePropertyItem>();

        // Application Data
        public BindingTable<ModelItem> Models { get; } = ModelFactory.Create<ModelItem>();
        public BindingTable<HelpItem> HelpSubjects { get; } = ModelFactory.Create<HelpItem>();
        public BindingTable<PropertyItem> Properties { get; } = ModelFactory.Create<PropertyItem>();
        public BindingTable<PropertyScopeItem> PropertyScopes { get; } = ModelFactory.Create<PropertyScopeItem>();

        // Connection Data
        public String ServerName { get { return ModelContext.ServerName; } }
        public String DatabaseName { get { return ModelContext.DatabaseName; } }

        protected ModelData() : base()
        { Models.Add(new ModelItem()); }

        public ModelData(Context context) : this()
        {
            ModelContext = new Context()
            {
                ServerName = context.ServerName,
                DatabaseName = context.DatabaseName,
                ApplicationRole = context.ApplicationRole,
                ApplicationRolePassword = context.ApplicationRolePassword
            };
        }

        public void Clear()
        {
            DbCatalogs.Clear();
            DbSchemta.Clear();
            DbTables.Clear();
            DbColumns.Clear();
            DbExtendedProperties.Clear();
            DomainAttributes.Clear();
            DomainAttributeAliases.Clear();
            DomainAttributeProperties.Clear();
            Models.Clear();
        }
    }
}
