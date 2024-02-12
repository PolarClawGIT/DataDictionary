using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.ApplicationData.Help;
using DataDictionary.DataLayer.ModelData;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer
{
    /// <summary>
    /// Implementation component for the Application data
    /// </summary>
    /// <remarks>When combined with the Extension class, this implements multi-inheritance.</remarks>
    public static class ModelApplication
    {
        /// <summary>
        /// Loads the Application Data from the Database.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadApplicationData(this IModelApplication data, IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem()
            {
                WorkName = "Clear Help",
                DoWork = data.HelpSubjects.Clear
            });

            work.Add(new WorkItem()
            {
                WorkName = "Clear Properties",
                DoWork = data.Properties.Clear
            });

            work.Add(factory.CreateWork(
                workName: "Load HelpSubjects",
                target: data.HelpSubjects,
                command: data.HelpSubjects.LoadCommand));

            work.Add(factory.CreateWork(
                workName: "Load Properties",
                target: data.Properties,
                command: data.Properties.LoadCommand));

            return work;
        }

        /// <summary>
        /// Loads the Application Data from a File
        /// </summary>
        /// <param name="data"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadApplicationData(this IModelApplication data, FileInfo file)
        {
            List<WorkItem> workItems = new List<WorkItem>
            {
                new WorkItem() { WorkName = "Load Application Data", DoWork = DoWork }
            };

            return workItems.AsReadOnly();

            void DoWork()
            {
                using (System.Data.DataSet workSet = new System.Data.DataSet())
                {
                    workSet.ReadXml(file.FullName, System.Data.XmlReadMode.ReadSchema);
                    LoadTable(workSet, data.HelpSubjects);
                    LoadTable(workSet, data.Properties);
                }
            }
        }

        /// <summary>
        /// Saves the Application Data to the Database.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> SaveApplicationData(this IModelApplication data, IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(factory.CreateWork(
                workName: "Save HelpSubjects",
                command: data.HelpSubjects.SaveCommand));

            work.Add(factory.CreateWork(
                workName: "Save Properties",
                command: data.Properties.SaveCommand));

            return work;
        }

        /// <summary>
        /// Saves the Application Data to a file.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> SaveApplicationData(this IModelApplication data, FileInfo file)
        {
            List<WorkItem> workItems = new List<WorkItem>();

            workItems.Add(new WorkItem() { WorkName = "Save Application Data", DoWork = DoWork });

            return workItems.AsReadOnly();

            void DoWork()
            {
                using (System.Data.DataSet workSet = new System.Data.DataSet())
                {
                    workSet.Tables.Add(data.HelpSubjects.ToDataTable());
                    workSet.Tables.Add(data.Properties.ToDataTable());

                    workSet.WriteXml(file.FullName, System.Data.XmlWriteMode.WriteSchema);
                }
            }
        }

        /// <summary>
        /// Creates the work items to Load the Help Subjects using the Help key passed.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="factory"></param>
        /// <param name="helpKey"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadHelp(this IModelApplication data, IDatabaseWork factory, IHelpKey helpKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            HelpKey key = new HelpKey(helpKey);

            work.Add(factory.CreateWork(
                workName: "Load Help",
                target: data.HelpSubjects,
                command: (conn) => data.HelpSubjects.LoadCommand(conn, key)));

            return work;
        }

        /// <summary>
        /// Creates the work items to Save the Help Subjects using the Help key passed.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="factory"></param>
        /// <param name="helpKey"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> SaveHelp(this IModelApplication data, IDatabaseWork factory, IHelpKey helpKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            HelpKey key = new HelpKey(helpKey);

            work.Add(factory.CreateWork(
                workName: "Save Help",
                command: (conn) => data.HelpSubjects.SaveCommand(conn, key)));

            return work;
        }

        /// <summary>
        /// Saves the Model to a file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> SaveModelData<T>(this T data, FileInfo file)
            where T : IModelCatalog, IModelLibrary, IModelDomain, IModel
        {
            List<WorkItem> workItems = new List<WorkItem>();

            workItems.Add(new WorkItem() { WorkName = "Save Model Data", DoWork = DoWork });

            return workItems.AsReadOnly();

            void DoWork()
            {
                using (System.Data.DataSet workSet = new System.Data.DataSet())
                {
                    // IModelCatalog
                    workSet.Tables.Add(data.DbCatalogs.ToDataTable());
                    workSet.Tables.Add(data.DbSchemta.ToDataTable());
                    workSet.Tables.Add(data.DbDomains.ToDataTable());
                    workSet.Tables.Add(data.DbTables.ToDataTable());
                    workSet.Tables.Add(data.DbTableColumns.ToDataTable());
                    workSet.Tables.Add(data.DbExtendedProperties.ToDataTable());
                    workSet.Tables.Add(data.DbConstraints.ToDataTable());
                    workSet.Tables.Add(data.DbConstraintColumns.ToDataTable());
                    workSet.Tables.Add(data.DbRoutines.ToDataTable());
                    workSet.Tables.Add(data.DbRoutineParameters.ToDataTable());
                    workSet.Tables.Add(data.DbRoutineDependencies.ToDataTable());

                    //IModelLibrary
                    workSet.Tables.Add(data.LibrarySources.ToDataTable());
                    workSet.Tables.Add(data.LibraryMembers.ToDataTable());

                    //IModelDomain
                    workSet.Tables.Add(data.DomainAttributes.ToDataTable());
                    workSet.Tables.Add(data.DomainAttributeAliases.ToDataTable());
                    workSet.Tables.Add(data.DomainAttributeProperties.ToDataTable());
                    workSet.Tables.Add(data.DomainEntities.ToDataTable());
                    workSet.Tables.Add(data.DomainEntityAliases.ToDataTable());
                    workSet.Tables.Add(data.DomainEntityProperties.ToDataTable());

                    //IModel
                    workSet.Tables.Add(data.Models.ToDataTable());
                    workSet.Tables.Add(data.ModelSubjectAreas.ToDataTable());
                    workSet.Tables.Add(data.ModelAttributes.ToDataTable());
                    workSet.Tables.Add(data.ModelEntities.ToDataTable());

                    // Write the Data
                    workSet.WriteXml(file.FullName, System.Data.XmlWriteMode.WriteSchema);
                }
            }
        }

        /// <summary>
        /// Loads the Model from a File.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadModelData<T>(this T data, FileInfo file)
            where T : IModelCatalog, IModelLibrary, IModelDomain, IModel, IModelNamespace
        {
            List<WorkItem> workItems = new List<WorkItem>();

            workItems.AddRange(data.RemoveCatalog());
            workItems.AddRange(data.RemoveLibrary());
            workItems.AddRange(data.RemoveDomain());
            workItems.AddRange(data.RemoveNameSpace());

            workItems.Add(new WorkItem() { WorkName = "Load Model Data", DoWork = DoWork });

            workItems.AddRange(data.LoadNameSpace());

            return workItems.AsReadOnly();

            void DoWork()
            {
                using (System.Data.DataSet workSet = new System.Data.DataSet())
                {
                    workSet.ReadXml(file.FullName, System.Data.XmlReadMode.ReadSchema);

                    // IModelCatalog
                    LoadTable(workSet, data.DbCatalogs);
                    LoadTable(workSet, data.DbSchemta);
                    LoadTable(workSet, data.DbDomains);
                    LoadTable(workSet, data.DbTables);
                    LoadTable(workSet, data.DbTableColumns);
                    LoadTable(workSet, data.DbExtendedProperties);
                    LoadTable(workSet, data.DbConstraints);
                    LoadTable(workSet, data.DbConstraintColumns);
                    LoadTable(workSet, data.DbRoutines);
                    LoadTable(workSet, data.DbRoutineParameters);
                    LoadTable(workSet, data.DbRoutineDependencies);

                    //IModelLibrary
                    LoadTable(workSet, data.LibrarySources);
                    LoadTable(workSet, data.LibraryMembers);

                    //IModelDomain
                    LoadTable(workSet, data.DomainAttributes);
                    LoadTable(workSet, data.DomainAttributeAliases);
                    LoadTable(workSet, data.DomainAttributeProperties);
                    LoadTable(workSet, data.DomainEntities);
                    LoadTable(workSet, data.DomainEntityAliases);
                    LoadTable(workSet, data.DomainEntityProperties);

                    //IModel
                    LoadTable(workSet, data.Models);
                    LoadTable(workSet, data.ModelSubjectAreas);
                    LoadTable(workSet, data.ModelAttributes);
                    LoadTable(workSet, data.ModelEntities);
                }
            }
        }

        /// <summary>
        /// Loads the Model from the Database.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="factory"></param>
        /// <param name="modelKey"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadModelData(this IModel data, IDatabaseWork factory, IModelKey modelKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            ModelKey key = new ModelKey(modelKey);

            work.Add(factory.CreateWork(
                workName: "Load Models",
                target: data.Models,
                command: (conn) => data.Models.LoadCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load Model SubjectAreas",
                target: data.ModelSubjectAreas,
                command: (conn) => data.ModelSubjectAreas.LoadCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load Model Attributes",
                target: data.ModelAttributes,
                command: (conn) => data.ModelAttributes.LoadCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load Model Entities",
                target: data.ModelEntities,
                command: (conn) => data.ModelEntities.LoadCommand(conn, key)));

            return work;
        }

        /// <summary>
        /// Saves the Model from the Database.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="factory"></param>
        /// <param name="modelKey"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> SaveModelData(this IModel data, IDatabaseWork factory, IModelKey modelKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            ModelKey key = new ModelKey(modelKey);

            work.Add(factory.CreateWork(
                workName: "Save Models",
                command: (conn) => data.Models.SaveCommand(conn)));

            work.Add(factory.CreateWork(
                workName: "Save Model SubjectAreas",
                command: (conn) => data.ModelSubjectAreas.SaveCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Save Model Attributes",
                command: (conn) => data.ModelAttributes.SaveCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Save Model Entities",
                command: (conn) => data.ModelEntities.SaveCommand(conn, key)));

            return work;
        }

        /// <summary>
        /// Loads a Single Table from a DataSet.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        static void LoadTable(System.Data.DataSet source, IBindingTable target)
        {
            if (source.Tables.Contains(target.BindingName) &&
                source.Tables[target.BindingName] is System.Data.DataTable helpData)
            {
                target.Clear();
                target.Load(helpData.CreateDataReader());
            }
        }
    }
}
