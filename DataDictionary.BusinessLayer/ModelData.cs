using DataDictionary.BusinessLayer.DbWorkItem;
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
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer
{
    /// <summary>
    /// Represents the main data object used by the application.
    /// </summary>
    public class ModelData
    {
        public IModelItem Model { get { return Models.First(); } }
        public FileInfo? ModelFile { get; internal set; }
        internal Context modelContext = new Context();

        // Database Model
        public BindingTable<DbCatalogItem> DbCatalogs { get; } = ModelFactory.Create<DbCatalogItem>();
        public BindingTable<DbSchemaItem> DbSchemta { get; } = ModelFactory.Create<DbSchemaItem>();
        public BindingTable<DbTableItem> DbTables { get; } = ModelFactory.Create<DbTableItem>();
        public BindingTable<DbColumnItem> DbColumns { get; } = ModelFactory.Create<DbColumnItem>();
        public BindingTable<DbExtendedPropertyItem> DbExtendedProperties = ModelFactory.Create<DbExtendedPropertyItem>();

        // Domain Model
        public BindingTable<DomainAttributeItem> DomainAttributes = ModelFactory.Create<DomainAttributeItem>();
        public BindingTable<DomainAttributeAliasItem> DomainAttributeAliases = ModelFactory.Create<DomainAttributeAliasItem>();
        public BindingTable<DomainAttributePropertyItem> DomainAttributeProperties = ModelFactory.Create<DomainAttributePropertyItem>();

        // Application Data
        public BindingTable<ModelItem> Models { get; } = ModelFactory.Create<ModelItem>();
        public BindingTable<HelpItem> HelpSubjects { get; } = ModelFactory.Create<HelpItem>();

        // Connection Data
        public String ServerName { get { return modelContext.ServerName; } }
        public String DatabaseName { get { return modelContext.DatabaseName; } }

        protected ModelData() : base()
        { Models.Add(new ModelItem()); }

        public ModelData(Context context) : this()
        {
            modelContext = new Context()
            {
                ServerName = context.ServerName,
                DatabaseName = context.DatabaseName,
                ApplicationRole = context.ApplicationRole,
                ApplicationRolePassword = context.ApplicationRolePassword
            };
        }
    }
}
