using DataDictionary.BusinessLayer.DbWorkItem;
using Toolbox.Threading;
using Toolbox.BindingTable;
using DbConnection = Toolbox.DbContext.Context;
using DataDictionary.DataLayer.ModelData;
using DataDictionary.BusinessLayer.NamedScope;
using System.Security.Principal;

namespace DataDictionary.BusinessLayer
{
    /// <summary>
    /// Interface for the Business Data.
    /// </summary>
    public partial interface IBusinessLayerData
    { }

    /// <summary>
    /// Main Data Container for all Business Data.
    /// </summary>
    public partial class BusinessLayerData :
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IBusinessLayerData
    {
        /// <summary>
        /// Database Context for accessing the Application Db.
        /// </summary>
        DbConnection DbConnection { get; init; }

        /// <summary>
        /// Database Connection information
        /// </summary>
        public (String ServerName, String DatabaseName) Connection
        { get { return (DbConnection.ServerName, DbConnection.DatabaseName); } }

        /// <summary>
        /// Current File, if any, used to load the Model.
        /// </summary>
        public FileInfo? ModelFile { get; set; }

        /// <summary>
        /// The Current Model being used by the application
        /// </summary>
        /// <remarks>There should always be exactly one Model</remarks>
        public ModelItem Model
        {
            get
            {
                if (modelValues.Count > 0)
                { return modelValues.First(); }
                else { throw new ArgumentException("No Model exists"); }
            }
        }

        /// <summary>
        /// Constructor for the Business Layer Data Object
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="serverName"></param>
        /// <param name="databaseName"></param>
        /// <param name="applicationRole"></param>
        /// <param name="ApplicationRolePassword"></param>
        public BusinessLayerData(IIdentity identity,  String serverName, String databaseName, String? applicationRole, String? ApplicationRolePassword) : base()
        {
            UserIdentity = identity;
            userSecurityValue = new AppSecurity.Security(identity);

            DbConnection = new DbConnection()
            {
                ServerName = serverName,
                DatabaseName = databaseName,
                ApplicationRole = applicationRole,
                ApplicationRolePassword = ApplicationRolePassword,
                ValidateCommand = true
            };

            modelValues = new Model.ModelData();
            subjectAreaValues = new Model.SubjectAreaData() { Models = modelValues };
            namedScopeValues = new NamedScopeData(LoadNamedScope);

            applicationValues = new AppGeneral.ApplicationData();

            domainValues = new Domain.DomainModel() { Models = modelValues, SubjectAreas = subjectAreaValues };
            databaseValues = new Database.DatabaseModel();
            libraryValues = new Library.LibraryModel();

            scriptingValues = new Scripting.ScriptingEngine() { Models = modelValues };
        }

        /// <summary>
        /// Returns a new Default factory Database Worker.
        /// </summary>
        public IDatabaseWork GetDbFactory()
        { return new DatabaseWork(DbConnection); }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey key)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(modelValues.Delete());
            work.AddRange(modelValues.Load(factory, key));
            work.AddRange(subjectAreaValues.Load(factory, key));

            work.AddRange(DomainModel.Load(factory, key));
            work.AddRange(DatabaseModel.Load(factory, key));
            work.AddRange(LibraryModel.Load(factory, key));

            work.AddRange(ScriptingEngine.Load(factory, key));

            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey key)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(modelValues.Save(factory, key));
            work.AddRange(subjectAreaValues.Save(factory, key));

            work.AddRange(DomainModel.Save(factory, key));
            work.AddRange(DatabaseModel.Save(factory, key));
            work.AddRange(LibraryModel.Save(factory, key));

            work.AddRange(ScriptingEngine.Save(factory, key));

            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete()
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(modelValues.Delete());
            work.AddRange(subjectAreaValues.Delete());

            work.AddRange(DomainModel.Delete());
            work.AddRange(DatabaseModel.Delete());
            work.AddRange(LibraryModel.Delete());

            work.AddRange(ScriptingEngine.Delete());

            work.Add(new WorkItem() { DoWork = namedScopeValues.Clear });

            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
        { return Delete(); }

        /// <summary>
        /// Creates a new empty Model (old model is removed).
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> Create()
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(Delete());
            work.AddRange(modelValues.Create());
            work.AddRange(domainValues.Create(applicationValues));
            return work;
        }

        /// <summary>
        /// Imports the Model from a File
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> ImportModel(FileInfo file)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(Delete());
            work.Add(new WorkItem() { WorkName = "Load Model Data", DoWork = DoWork });
            return work;

            void DoWork()
            {
                using (System.Data.DataSet workSet = new System.Data.DataSet())
                {
                    workSet.ReadXml(file.FullName, System.Data.XmlReadMode.ReadSchema);

                    modelValues.Import(workSet);
                    subjectAreaValues.Import(workSet);

                    domainValues.Import(workSet);
                    databaseValues.Import(workSet);
                    libraryValues.Import(workSet);

                    scriptingValues.Import(workSet);
                }

                ModelFile = file;
            }
        }

        /// <summary>
        /// Exports the Model to a File
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> ExportModel(FileInfo file)
        {
            return new List<WorkItem>() { new WorkItem() { WorkName = "Save Model Data", DoWork = DoWork } };

            void DoWork()
            {
                using (System.Data.DataSet workSet = new System.Data.DataSet())
                {
                    workSet.Tables.Add(modelValues.ToDataTable());
                    workSet.Tables.Add(subjectAreaValues.ToDataTable());

                    workSet.Tables.AddRange(domainValues.Export().ToArray());
                    workSet.Tables.AddRange(databaseValues.Export().ToArray());
                    workSet.Tables.AddRange(libraryValues.Export().ToArray());

                    workSet.Tables.AddRange(scriptingValues.Export().ToArray());

                    workSet.WriteXml(file.FullName, System.Data.XmlWriteMode.WriteSchema);
                }

                ModelFile = file;
            }
        }


        /// <summary>
        /// Imports the Application Data from a File
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> ImportApplication(FileInfo file)
        {
            List<WorkItem> work = new List<WorkItem>
            {
                new WorkItem() { WorkName = "Load Application Data", DoWork = DoWork }
            };

            return work.AsReadOnly();

            void DoWork()
            {
                using (System.Data.DataSet workSet = new System.Data.DataSet())
                {
                    workSet.ReadXml(file.FullName, System.Data.XmlReadMode.ReadSchema);
                    applicationValues.Import(workSet);
                }
            }
        }

        /// <summary>
        /// Exports the Application Data to a File
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> ExportApplication(FileInfo file)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem() { WorkName = "Save Application Data", DoWork = DoWork });

            return work.AsReadOnly();

            void DoWork()
            {
                using (System.Data.DataSet workSet = new System.Data.DataSet())
                {
                    workSet.Tables.AddRange(applicationValues.Export().ToArray());

                    workSet.WriteXml(file.FullName, System.Data.XmlWriteMode.WriteSchema);
                }
            }
        }


    }
}
