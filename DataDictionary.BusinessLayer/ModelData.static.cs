using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.ModelData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer
{
    /// <summary>
    /// Method for the Model Data
    /// </summary>
    [Obsolete("To be replaced with BusinessLayerData")]
    public static class ModelData_Extension
    {
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
            where T : IModelCatalog, IModelLibrary, IModelDomain, IModel//, IModelContextName
        {
            List<WorkItem> workItems = new List<WorkItem>();

            workItems.AddRange(data.RemoveCatalog());
            workItems.AddRange(data.RemoveLibrary());
            workItems.AddRange(data.RemoveDomain());
            //workItems.AddRange(data.RemoveContextName());

            workItems.Add(new WorkItem() { WorkName = "Load Model Data", DoWork = DoWork });

            //workItems.AddRange(data.LoadContextName());

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

    }
}
