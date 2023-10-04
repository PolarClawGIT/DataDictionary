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
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.LibraryData.Member;
using DataDictionary.DataLayer.LibraryData.Source;
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
    /// <remarks>
    /// The data is shared across the entire application.
    /// Forms need to connect to the Key for the data object they are presenting.
    /// If the data should change, the Forms need to reset the bindings and get new data from this object.
    /// </remarks>
    public partial class ModelData
    {
        // Model
        ModelItem defaultModel;
        ModelKey modelKey;

        /// <summary>
        /// The Key of the Model that is currently opened by the application.
        /// </summary>
        public ModelKey ModelKey { get { return modelKey; } internal set { modelKey = new ModelKey(value); } }

        /// <summary>
        /// List of Models from the Application Database.
        /// </summary>
        public ModelCollection Models { get; } = new ModelCollection();

        /// <summary>
        /// The current Model (by ModelKey) opened by the application
        /// </summary>
        public ModelItem Model
        {
            get
            {
                if (Models.FirstOrDefault(w => new ModelKey(w) == ModelKey) is ModelItem item)
                { return item; }
                else { return defaultModel; }
            }
        }

        /// <summary>
        /// The File Object the Model was read from.
        /// </summary>
        public FileInfo? ModelFile { get; internal set; } //TODO implement Model Save to files system.

        #region Application Connection
        /// <summary>
        /// The Database Context of the Application for the Model.
        /// </summary>
        internal static Context ModelContext { get; private set; } = new Context();

        /// <summary>
        /// The Application Server.
        /// </summary>
        public String ServerName { get { return ModelContext.ServerName; } }

        /// <summary>
        /// The Application Database
        /// </summary>
        public String DatabaseName { get { return ModelContext.DatabaseName; } }
        #endregion

        #region Database data
        /// <summary>
        /// List of Database Catalogs within the Model.
        /// </summary>
        public DbCatalogCollection DbCatalogs { get; } = new DbCatalogCollection();

        /// <summary>
        /// List of Database Schemta within the Model.
        /// </summary>
        public DbSchemaCollection DbSchemta { get; } = new DbSchemaCollection();

        /// <summary>
        /// List of Database Domains (types) within the Model.
        /// </summary>
        public DbDomainCollection DbDomains { get; } = new DbDomainCollection();

        /// <summary>
        /// List of Database Tables and Views within the Model.
        /// </summary>
        public DbTableCollection DbTables { get; } = new DbTableCollection();

        /// <summary>
        /// List of Database Columns for the Tables/Views within the Model.
        /// </summary>
        public DbTableColumnCollection DbTableColumns { get; } = new DbTableColumnCollection();

        /// <summary>
        /// List of Database Extended Properties within the Model.
        /// </summary>
        public DbExtendedPropertyCollection DbExtendedProperties = new DbExtendedPropertyCollection();

        /// <summary>
        /// List of Database Constraints (keys...) within the Model.
        /// </summary>
        public DbConstraintCollection DbConstraints { get; } = new DbConstraintCollection();

        /// <summary>
        /// List of Database Constraint Columns within the Model.
        /// </summary>
        public DbConstraintColumnCollection DbConstraintColumns { get; } = new DbConstraintColumnCollection();

        /// <summary>
        /// List of Database Routines (procedures, functions, ...) within the Model.
        /// </summary>
        public DbRoutineCollection DbRoutines { get; } = new DbRoutineCollection();

        /// <summary>
        /// List of Database Parameters for the Routines within the Model.
        /// </summary>
        public DbRoutineParameterCollection DbRoutineParameters { get; } = new DbRoutineParameterCollection();

        /// <summary>
        /// List of Database Dependencies for the Routines within the Model.
        /// </summary>
        public DbRoutineDependencyCollection DbRoutineDependencies { get; } = new DbRoutineDependencyCollection();
        #endregion

        #region Domain data
        /// <summary>
        /// List of Domain Attributes within the Model.
        /// </summary>
        public DomainAttributeCollection DomainAttributes = new DomainAttributeCollection();

        /// <summary>
        /// List of Domain Aliases for the Attributes within the Model.
        /// </summary>
        public DomainAttributeAliasCollection DomainAttributeAliases = new DomainAttributeAliasCollection();

        /// <summary>
        /// List of Domain Properties for the Attributes within the Model.
        /// </summary>
        public DomainAttributePropertyCollection DomainAttributeProperties = new DomainAttributePropertyCollection();

        /// <summary>
        /// List of Domain Entities within the Model.
        /// </summary>
        public DomainEntityCollection DomainEntities = new DomainEntityCollection();

        /// <summary>
        /// List of Domain Aliases for the Entities within the Model.
        /// </summary>
        public DomainEntityAliasCollection DomainEntityAliases = new DomainEntityAliasCollection();

        /// <summary>
        /// List of Domain Properties for the Entities within the Model.
        /// </summary>
        public DomainEntityPropertyCollection DomainEntityProperties = new DomainEntityPropertyCollection();
        #endregion

        #region Library data

        /// <summary>
        /// List of Domain Properties for the Entities within the Model.
        /// </summary>
        public LibrarySourceCollection LibrarySources = new LibrarySourceCollection();

        /// <summary>
        /// List of Domain Properties for the Entities within the Model.
        /// </summary>
        public LibraryMemberCollection LibraryMembers = new LibraryMemberCollection();

        #endregion

        #region Application data
        /// <summary>
        /// List of Help Subjects for the Application (the help system).
        /// </summary>
        public HelpCollection HelpSubjects { get; } = new HelpCollection();

        /// <summary>
        /// List Properties defined for the Application.
        /// </summary>
        public DataLayer.ApplicationData.Property.PropertyCollection Properties { get; } = new DataLayer.ApplicationData.Property.PropertyCollection();
        #endregion

        // Connection Data

        /// <summary>
        /// Constructor for the Model Data
        /// </summary>
        protected ModelData() : base()
        {
            defaultModel = new ModelItem();
            modelKey = new ModelKey(defaultModel);
        }

        /// <summary>
        /// Constructor for the Model Data
        /// </summary>
        /// <param name="context"></param>
        public ModelData(Context context) : this()
        { ModelContext = context; }

        /// <summary>
        /// Clears all the Model Data.
        /// </summary>
        public void Clear()
        {
            DbCatalogs.Clear();
            DbSchemta.Clear();
            DbTables.Clear();
            DbTableColumns.Clear();
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
            DomainEntities.Clear();
            DomainEntityAliases.Clear();
            DomainEntityProperties.Clear();
            LibraryMembers.Clear();
            LibrarySources.Clear();
            Models.Clear();
        }

        /// <summary>
        /// Initializes a New Model (does not clear the data).
        /// </summary>
        public void NewModel()
        {
            defaultModel = new ModelItem();
            ModelKey = new ModelKey(defaultModel);
        }
    }
}
