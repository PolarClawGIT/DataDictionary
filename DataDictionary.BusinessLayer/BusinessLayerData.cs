using DataDictionary.BusinessLayer.DbWorkItem;
using Toolbox.Threading;
using Toolbox.BindingTable;
using DbConnection = Toolbox.DbContext.Context;
using DataDictionary.DataLayer.ModelData;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ModelData.SubjectArea;
using Toolbox.DbContext;
using DataDictionary.BusinessLayer.Model;


namespace DataDictionary.BusinessLayer
{
    /// <summary>
    /// Main Data Container for all Business Data.
    /// </summary>
    public partial class BusinessLayerData :
        ILoadData<IModelKey>, ISaveData<IModelKey>, IRemoveData
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
                if (modelValue.Count > 0)
                { return modelValue.First(); }
                else { throw new ArgumentException("No Model exists"); }
            }
        }


        /// <summary>
        /// Constructor for the Business Layer Data Object
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="databaseName"></param>
        /// <param name="applicationRole"></param>
        /// <param name="ApplicationRolePassword"></param>
        public BusinessLayerData(String serverName, String databaseName, String? applicationRole, String? ApplicationRolePassword) : base()
        {
            DbConnection = new DbConnection()
            {
                ServerName = serverName,
                DatabaseName = databaseName,
                ApplicationRole = applicationRole,
                ApplicationRolePassword = ApplicationRolePassword,
                ValidateCommand = true
            };

            modelValue = new Model.ModelData();
            subjectAreaValues = new Model.SubjectAreaData() { Models = modelValue };
            namedScopeValue = new NamedScopeData();

            modelValue.Add(new ModelValue());
            applicationValue = new Application.ApplicationData();

            domainValue = new Domain.DomainModel() { Models = modelValue, ModelProperty = applicationValue.Properties, SubjectAreas = subjectAreaValues };
            databaseValue = new Database.DatabaseModel();
            libraryValue = new Library.LibraryModel();

            scriptingValue = new Scripting.ScriptingEngine() { Models = modelValue };
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

            work.AddRange(modelValue.Remove());
            work.AddRange(modelValue.Load(factory, key));
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

            work.AddRange(modelValue.Save(factory, key));
            work.AddRange(subjectAreaValues.Save(factory, key));

            work.AddRange(DomainModel.Save(factory, key));
            work.AddRange(DatabaseModel.Save(factory, key));
            work.AddRange(LibraryModel.Save(factory, key));

            work.AddRange(ScriptingEngine.Save(factory, key));

            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Remove()
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(modelValue.Remove());
            work.AddRange(subjectAreaValues.Remove());

            work.AddRange(DomainModel.Remove());
            work.AddRange(DatabaseModel.Remove());
            work.AddRange(LibraryModel.Remove());

            work.AddRange(ScriptingEngine.Remove());

            work.Add(new WorkItem() { DoWork = namedScopeValue.Clear });

            return work;
        }

        /// <summary>
        /// Creates a new empty Model (old model is removed).
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> Create()
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(Remove());

            work.AddRange(modelValue.Create());
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
            work.AddRange(Remove());
            work.Add(new WorkItem() { WorkName = "Load Model Data", DoWork = DoWork });
            return work;

            void DoWork()
            {
                using (System.Data.DataSet workSet = new System.Data.DataSet())
                {
                    workSet.ReadXml(file.FullName, System.Data.XmlReadMode.ReadSchema);

                    modelValue.Import(workSet);
                    subjectAreaValues.Import(workSet);

                    domainValue.Import(workSet);
                    databaseValue.Import(workSet);
                    libraryValue.Import(workSet);

                    scriptingValue.Import(workSet);
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
                    workSet.Tables.Add(modelValue.ToDataTable());
                    workSet.Tables.Add(subjectAreaValues.ToDataTable());

                    workSet.Tables.AddRange(domainValue.Export().ToArray());
                    workSet.Tables.AddRange(databaseValue.Export().ToArray());
                    workSet.Tables.AddRange(libraryValue.Export().ToArray());

                    workSet.Tables.AddRange(scriptingValue.Export().ToArray());

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
                    applicationValue.Import(workSet);
                    scriptingValue.Import(workSet);
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
                    workSet.Tables.AddRange(applicationValue.Export().ToArray());
                    workSet.Tables.AddRange(scriptingValue.Export().ToArray());

                    workSet.WriteXml(file.FullName, System.Data.XmlWriteMode.WriteSchema);
                }
            }
        }
    }
}
