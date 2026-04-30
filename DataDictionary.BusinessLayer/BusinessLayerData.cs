// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.DbWorkItem;
using Toolbox.Threading;
using Toolbox.BindingTable;
using DbConnection = Toolbox.DbContext.Context;
using DataDictionary.BusinessLayer.NamedScope;
using System.Security.Principal;
using DataDictionary.DataLayer.AppSecurity;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.ToolSet;
using System.ComponentModel;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer
{
    /// <summary>
    /// Main Data Container for all Business Data.
    /// </summary>
    public partial class BusinessLayerData :
        ILoadData<IModelIndex>, ISaveData<IModelIndex>
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
        /// Messages from the Database Calls. Loaded after work items is complete.
        /// </summary>
        public BindingList<MessageItem> Messages = new BindingList<MessageItem>();

        /// <summary>
        /// Maximum Number of Messages to keep. 0 or less keeps all messages.
        /// </summary>
        public Int32? MaxMessages { get; init; } = 250;

        /// <summary>
        /// Constructor for the Business Layer Data Object
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="serverName"></param>
        /// <param name="databaseName"></param>
        /// <param name="applicationRole"></param>
        /// <param name="ApplicationRolePassword"></param>
        public BusinessLayerData(IIdentity identity, String serverName, String databaseName, String? applicationRole, String? ApplicationRolePassword) : base()
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
            libraryValues = new AppLibrary.LibraryModel();

            scriptingValue = InitScripting(modelValues);
            templateValues = new AppScripting.TemplateData();
        }

        /// <summary>
        /// Returns a new Default factory Model Worker.
        /// </summary>
        public IDatabaseWork GetDbFactory()
        {
            return new DatabaseWork(DbConnection)
            {
                CreateMessages = (m) =>
                {
                    Messages.AddRange(m.Select(s => new MessageItem(s)));

                    while (MaxMessages > 0 && Messages.Count > MaxMessages)
                    { Messages.RemoveAt(0); }
                }
            };
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex key)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(Delete(key));
            work.AddRange(Model.Load(factory, key));
            work.AddRange(CatalogModel.Load(factory, key));
            work.AddRange(LibraryModel.Load(factory, key));
            work.AddRange(Scripting.Load(factory, key));
            work.AddRange(templateValues.Load(factory, key));

            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex key, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(Delete(key));
            work.AddRange(Model.Load(factory, key, asOfUtcDate));
            work.AddRange(CatalogModel.Load(factory, key, asOfUtcDate));
            work.AddRange(LibraryModel.Load(factory, key, asOfUtcDate));
            work.AddRange(Scripting.Load(factory, key));
            work.AddRange(templateValues.Load(factory, key));

            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex key)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(Model.Save(factory, key));
            work.AddRange(CatalogModel.Save(factory, key));
            work.AddRange(LibraryModel.Save(factory, key));
            work.AddRange(Scripting.Save(factory, key));
            work.AddRange(templateValues.Save(factory, key));

            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete()
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(Model.Delete());
            work.AddRange(CatalogModel.Delete());
            work.AddRange(LibraryModel.Delete());
            work.AddRange(Scripting.Delete());
            work.AddRange(templateValues.Delete());

            work.Add(new WorkItem() { DoWork = namedScopeValues.Clear });

            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
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
                    scriptingValue.Import(workSet);
                    templateValues.Import(workSet);
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
                    workSet.Tables.AddRange(scriptingValue.Export().ToArray());
                    workSet.Tables.AddRange(templateValues.Export().ToArray());

                    workSet.WriteXml(file.FullName, System.Data.XmlWriteMode.WriteSchema);
                }

                ModelFile = file;
            }
        }

        /// <inheritdoc/>
        public void Remove(IModelIndex dataKey)
        {
            Model.Remove(dataKey);
            CatalogModel.Remove(dataKey);
            LibraryModel.Remove(dataKey);
            Scripting.Remove(dataKey);
            templateValues.Remove(dataKey);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            Model.Clear();
            CatalogModel.Clear();
            LibraryModel.Clear();
            Scripting.Clear();
            templateValues.Clear();
        }
    }
}
