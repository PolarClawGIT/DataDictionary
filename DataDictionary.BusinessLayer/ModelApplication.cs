using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.ApplicationData.Help;
using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.ApplicationData.Scope;
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
    /// Interface component for the Application data
    /// </summary>
    /// <remarks>When combined with the Extension class, this implements multi-inheritance.</remarks>
    public interface IModelApplication
    {
        /// <summary>
        /// List of Help Subjects for the Application (the help system).
        /// </summary>
        HelpCollection HelpSubjects { get; }

        /// <summary>
        /// List Properties defined for the Application.
        /// </summary>
        PropertyCollection Properties { get; }

        /// <summary>
        /// List of Scopes defined for the Application.
        /// </summary>
        ScopeCollection Scopes { get; }

    }

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

            work.Add(factory.CreateWork(
                workName: "Load Scopes",
                target: data.Scopes,
                command: data.Scopes.LoadCommand));

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
                    LoadTable(workSet, data.Scopes);
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

            work.Add(factory.CreateWork(
                workName: "Save Scopes",
                command: data.Scopes.SaveCommand));

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
                    workSet.Tables.Add(data.Scopes.ToDataTable());

                    workSet.WriteXml(file.FullName, System.Data.XmlWriteMode.WriteSchema);
                }
            }
        }

        /// <summary>
        /// Saves the Model to a file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> SaveModel<T>(this T data, FileInfo file)
            where T : IModelCatalog, IModelLibrary, IModelDomain
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
                    workSet.Tables.Add(data.ModelSubjectAreas.ToDataTable());

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
        public static IReadOnlyList<WorkItem> LoadModel<T>(this T data, FileInfo file)
            where T : IModelCatalog, IModelLibrary, IModelDomain
        {
            List<WorkItem> workItems = new List<WorkItem>();

            workItems.AddRange(data.RemoveCatalog());
            workItems.AddRange(data.RemoveLibrary());
            workItems.AddRange(data.RemoveDomain());
            workItems.Add(new WorkItem() { WorkName = "Load Model Data", DoWork = DoWork });

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
                    LoadTable(workSet, data.ModelSubjectAreas);
                }
            }
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
