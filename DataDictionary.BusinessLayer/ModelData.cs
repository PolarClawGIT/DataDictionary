using DataDictionary.DataLayer;
using DataDictionary.DataLayer.ApplicationData.Help;
using DataDictionary.DataLayer.ApplicationData.Model;
using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Domain;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.LibraryData;
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
        // Model
        ModelItem defaultModel;
        ModelKey modelKey;
        public ModelKey ModelKey { get { return modelKey; } internal set { modelKey = new ModelKey(value); } }
        public ModelList Models { get; } = new ModelList();

        public ModelItem Model
        {
            get
            {
                if (Models.FirstOrDefault(w => new ModelKey(w) == ModelKey) is ModelItem item)
                { return item; }
                else { return defaultModel; }
            }
        }

        public FileInfo? ModelFile { get; internal set; }
        internal protected Context ModelContext { get; protected set; } = new Context();

        // Database Model
        public DbCatalogList DbCatalogs { get; } = new DbCatalogList();
        public DbSchemaList DbSchemta { get; } = new DbSchemaList();
        public DbDomainList DbDomains { get; } = new DbDomainList();
        public DbTableList DbTables { get; } = new DbTableList();
        public DbTableColumnList DbColumns { get; } = new DbTableColumnList();
        public DbExtendedPropertyList DbExtendedProperties = new DbExtendedPropertyList();
        public DbConstraintList DbConstraints { get; } = new DbConstraintList();
        public DbConstraintColumnList DbConstraintColumns { get; } = new DbConstraintColumnList();
        public DbRoutineList DbRoutines { get; } = new DbRoutineList();
        public DbRoutineParameterList DbRoutineParameters { get; } = new DbRoutineParameterList();
        public DbRoutineDependencyList DbRoutineDependencies { get; } = new DbRoutineDependencyList();

        // Domain Model
        public DomainAttributeList DomainAttributes = new DomainAttributeList();
        public DomainAttributeAliasList DomainAttributeAliases = new DomainAttributeAliasList();
        public DomainAttributePropertyList DomainAttributeProperties = new DomainAttributePropertyList();

        // Library Model, POC
        public BindingTable<LibraryAssemblyItem> LibraryAssemblies = ModelFactory.Create<LibraryAssemblyItem>();
        public BindingTable<LibraryMemberItem> LibraryMembers = ModelFactory.Create<LibraryMemberItem>();

        // Application Data
        public HelpList HelpSubjects { get; } = new HelpList();
        public PropertyList Properties { get; } = new PropertyList();

        // Connection Data
        public String ServerName { get { return ModelContext.ServerName; } }
        public String DatabaseName { get { return ModelContext.DatabaseName; } }

        protected ModelData() : base()
        {
            defaultModel = new ModelItem();
            modelKey = new ModelKey(defaultModel);
        }

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
            DbDomains.Clear();
            DbConstraints.Clear();
            DbConstraintColumns.Clear();
            DbRoutines.Clear();
            DbRoutineParameters.Clear();
            DbRoutineDependencies.Clear();
            DbExtendedProperties.Clear();
            DomainAttributes.Clear();
            DomainAttributeAliases.Clear();
            DomainAttributeProperties.Clear();
            Models.Clear();
        }

        public void NewModel()
        {
            defaultModel = new ModelItem();
            ModelKey = new ModelKey(defaultModel);
        }
    }
}
