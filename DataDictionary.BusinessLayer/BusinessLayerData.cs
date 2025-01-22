using DataDictionary.BusinessLayer.DbWorkItem;
using Toolbox.Threading;
using Toolbox.BindingTable;
using DbConnection = Toolbox.DbContext.Context;
using DataDictionary.BusinessLayer.NamedScope;
using System.Security.Principal;
using DataDictionary.DataLayer.AppSecurity;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.BusinessLayer.AppModel;

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
        /// Model Context for accessing the Application Db.
        /// </summary>
        DbConnection DbConnection { get; init; }

        /// <summary>
        /// Model Connection information
        /// </summary>
        public (String ServerName, String DatabaseName) Connection
        { get { return (DbConnection.ServerName, DbConnection.DatabaseName); } }

        /// <summary>
        /// Current File, if any, used to load the Model.
        /// </summary>
        public FileInfo? ModelFile { get; set; }


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

            DbConnection = new DbConnection()
            {
                ServerName = serverName,
                DatabaseName = databaseName,
                ApplicationRole = applicationRole,
                ApplicationRolePassword = ApplicationRolePassword,
                ValidateCommand = true
            };

            namedScopeValues = new NamedScopeData(LoadNamedScope);

            applicationValues = new AppGeneral.ApplicationData();

            modelValues = new AppModel.Model();
            catalogValue = new AppCatalog.Catalog();
            libraryValues = new Library.LibraryModel();

            scriptingValues = new Scripting.ScriptingEngine() { Model = modelValues };
        }

        /// <summary>
        /// Returns a new Default factory Model Worker.
        /// </summary>
        public IDatabaseWork GetDbFactory()
        { return new DatabaseWork(DbConnection); }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey key)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(Model.Delete());
            work.AddRange(Model.Load(factory, key));
            work.AddRange(CatalogModel.Load(factory, key));
            work.AddRange(LibraryModel.Load(factory, key));

            work.AddRange(ScriptingEngine.Load(factory, key));

            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey key)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(Model.Save(factory, key));
            work.AddRange(CatalogModel.Save(factory, key));
            work.AddRange(LibraryModel.Save(factory, key));

            work.AddRange(ScriptingEngine.Save(factory, key));

            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete()
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(Model.Delete());
            work.AddRange(CatalogModel.Delete());
            work.AddRange(LibraryModel.Delete());

            work.AddRange(ScriptingEngine.Delete());

            work.Add(new WorkItem() { DoWork = namedScopeValues.Clear });

            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
        { return Delete(); }

        /// <summary>
        /// Creates a new empty Model (old currentModel is removed).
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> Create()
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(Delete());
            work.AddRange(modelValues.Create(applicationValues));
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
                    catalogValue.Import(workSet);
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
                    workSet.Tables.AddRange(modelValues.Export().ToArray());
                    workSet.Tables.AddRange(catalogValue.Export().ToArray());
                    workSet.Tables.AddRange(libraryValues.Export().ToArray());

                    workSet.Tables.AddRange(scriptingValues.Export().ToArray());

                    workSet.WriteXml(file.FullName, System.Data.XmlWriteMode.WriteSchema);
                }

                ModelFile = file;
            }
        }
    }
}
