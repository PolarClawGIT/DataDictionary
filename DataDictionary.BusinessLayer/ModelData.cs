using DataDictionary.DataLayer.ApplicationData.Help;
using DataDictionary.DataLayer.ApplicationData.Model;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.DomainData.SubjectArea;
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

        /// <summary>
        /// List of Domain Subject Areas within the Model.
        /// </summary>
        public DomainSubjectAreaCollection DomainSubjectAreas = new DomainSubjectAreaCollection();
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
        [Obsolete("Needs to be replaced by Clear method for each area")]
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
            DomainSubjectAreas.Clear();
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
