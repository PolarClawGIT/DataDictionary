﻿using DataDictionary.BusinessLayer.DbWorkItem;
using Toolbox.Threading;
using Toolbox.BindingTable;
using DbConnection = Toolbox.DbContext.Context;
using DataDictionary.DataLayer.ModelData;
using DataDictionary.BusinessLayer.NameScope;
using DataDictionary.DataLayer.ModelData.SubjectArea;
using Toolbox.DbContext;


namespace DataDictionary.BusinessLayer
{
    /// <summary>
    /// Main Data Container for all Business Data.
    /// </summary>
    public partial class BusinessLayerData : IFileData,
        ILoadData<IModelKey>, ISaveData<IModelKey>, IRemoveData, INameScopeData
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
        public ModelItem Model { get { return models.First(); } }

        /// <summary>
        /// Returns a new Default factory Database Worker.
        /// </summary>
        public IDatabaseWork GetDbFactory()
        { return new DatabaseWork(DbConnection); }

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

            models.Add(new ModelItem());
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey key)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(models.Remove());
            work.AddRange(models.Load(factory, key));
            work.AddRange(subjectAreas.Load(factory, key));

            work.AddRange(DomainModel.Load(factory, key));
            work.AddRange(DatabaseModel.Load(factory, key));
            work.AddRange(LibraryModel.Load(factory, key));

            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey key)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(models.Save(factory, key));
            work.AddRange(subjectAreas.Save(factory, key));

            work.AddRange(DomainModel.Save(factory, key));
            work.AddRange(DatabaseModel.Save(factory, key));
            work.AddRange(LibraryModel.Save(factory, key));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Automatically makes a new empty Model</remarks>
        public IReadOnlyList<WorkItem> Remove()
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(models.Remove());
            work.AddRange(subjectAreas.Remove());

            work.AddRange(DomainModel.Remove());
            work.AddRange(DatabaseModel.Remove());
            work.AddRange(LibraryModel.Remove());

            work.AddRange(models.Create());

            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Import(FileInfo file)
        {
            return new List<WorkItem>() { new WorkItem() { WorkName = "Load Model Data", DoWork = DoWork } };

            void DoWork()
            {
                using (System.Data.DataSet workSet = new System.Data.DataSet())
                {
                    workSet.ReadXml(file.FullName, System.Data.XmlReadMode.ReadSchema);

                    models.Clear();
                    models.Import(workSet);
                    subjectAreas.Import(workSet);

                    domain.Import(workSet);
                    database.Import(workSet);
                    library.Import(workSet);
                }

                ModelFile = file;
            }
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Export(FileInfo file)
        {
            return new List<WorkItem>() { new WorkItem() { WorkName = "Save Model Data", DoWork = DoWork } };

            void DoWork()
            {
                using (System.Data.DataSet workSet = new System.Data.DataSet())
                {
                    workSet.Tables.Add(models.ToDataTable());
                    workSet.Tables.Add(subjectAreas.ToDataTable());

                    workSet.Tables.AddRange(domain.Export().ToArray());
                    workSet.Tables.AddRange(database.Export().ToArray());
                    workSet.Tables.AddRange(library.Export().ToArray());
                    workSet.WriteXml(file.FullName, System.Data.XmlWriteMode.WriteSchema);
                }

                ModelFile = file;
            }
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Export(IList<NameScopeItem> target)
        {
            List<WorkItem> work = new List<WorkItem>();
            Func<ModelItem?> model = models.FirstOrDefault;

            work.Add(new WorkItem()
            {
                WorkName = "Load NameScope, Models",
                DoWork = () =>
                {
                    if (model() is ModelItem modelItem)
                    { target.Add(new NameScopeItem(modelItem)); }
                }
            });

            work.AddRange(subjectAreas.Export(target, model));
            work.AddRange(domain.Export(target, model));
            work.AddRange(database.Export(target));
            work.AddRange(library.Export(target));

            return work;

        }
    }
}
