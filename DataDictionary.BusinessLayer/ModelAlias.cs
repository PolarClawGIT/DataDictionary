using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Alias;
using DataDictionary.DataLayer.LibraryData.Member;
using DataDictionary.DataLayer.LibraryData.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer
{
    /// <summary>
    /// Interface component for the Model Alias
    /// </summary>
    /// <remarks>When combined with the Extension class, this approximates multi-inheritance.</remarks>
    public interface IModelAlias
    {
        /// <summary>
        /// List of available Model Alias Items from Catalogs and Libraries.
        /// </summary>
        ModelAliasCollection ModelAlias { get; }
    }

    /// <summary>
    /// Implementation component for the Model Alias data
    /// </summary>
    /// <remarks>When combined with the Extension class, this implements multi-inheritance.</remarks>
    public static class ModelAlias
    {
        /// <summary>
        /// Load Alias Data for the Model.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadAlias<T>(this T data)
            where T : IModelCatalog, IModelLibrary, IModelAlias
        {
            List<WorkItem> work = new List<WorkItem>();
            Action<Int32, Int32> progress = (x, y) => { };

            WorkItem loadWork = new WorkItem()
            {
                WorkName = "Load Catalog Aliases",
                DoWork = ThreadWork
            };
            progress = loadWork.OnProgressChanged;

            work.Add(loadWork);

            return work;

            void ThreadWork()
            {
                data.ModelAlias.Clear();

                foreach (DbCatalogItem? item in data.DbCatalogs)
                {
                    DbCatalogKey catalogKey = new DbCatalogKey(item);
                    LoadAliasCore(data, catalogKey, progress);
                }

                foreach (LibrarySourceItem? item in data.LibrarySources)
                {
                    LibrarySourceKey sourceKey = new LibrarySourceKey(item);
                    LoadAliasCore(data, sourceKey, progress);
                }
            }
        }


        /// <summary>
        /// Load Alias Data from a Catalog
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadAlias<T>(this T data, IDbCatalogKeyName key)
            where T : IModelCatalog, IModelAlias
        {
            List<WorkItem> work = new List<WorkItem>();
            Action<Int32, Int32> progress = (x, y) => { };

            WorkItem loadWork = new WorkItem()
            {
                WorkName = "Load Catalog Aliases",
                DoWork = ThreadWork
            };
            progress = loadWork.OnProgressChanged;

            work.Add(loadWork);

            return work;

            void ThreadWork()
            {
                DbCatalogKeyName nameKey = new DbCatalogKeyName(key);
                foreach (DbCatalogItem? item in data.DbCatalogs.Where(w => nameKey.Equals(w)))
                {
                    DbCatalogKey catalogKey = new DbCatalogKey(item);
                    LoadAliasCore(data, catalogKey, progress);
                }
            }
        }

        /// <summary>
        /// Load Alias Data from a Catalog
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadAlias<T>(this T data, IDbCatalogKey key)
            where T : IModelCatalog, IModelAlias
        {
            List<WorkItem> work = new List<WorkItem>();
            Action<Int32, Int32> progress = (x, y) => { };

            WorkItem loadWork = new WorkItem()
            {
                WorkName = "Load Catalog Aliases",
                DoWork = ThreadWork
            };
            progress = loadWork.OnProgressChanged;

            work.Add(loadWork);

            return work;

            void ThreadWork()
            { LoadAliasCore(data, key, progress); }
        }

        private static void LoadAliasCore<T>(T data, IDbCatalogKey key, Action<Int32, Int32> progress)
            where T : IModelCatalog, IModelAlias
        {
            DbCatalogKey catalogKey = new DbCatalogKey(key);
            List<DbCatalogItem> catalogs = data.DbCatalogs.Where(w => catalogKey.Equals(w) && w.IsSystem == false).ToList();
            Int32 totalWork = catalogs.Count;
            Int32 completedWork = 0;

            foreach (DbCatalogItem catalogItem in catalogs) // Expect zero or one
            {
                // This improve performance as it is working off of a fixed sub-set of data.
                List<DbSchemaItem> schemta = data.DbSchemta.Where(w => catalogKey.Equals(w) && w.IsSystem == false).ToList();
                List<DbTableItem> tables = data.DbTables.Where(w => catalogKey.Equals(w) && w.IsSystem == false).ToList();
                List<DbTableColumnItem> tableColumns = data.DbTableColumns.Where(w => catalogKey.Equals(w)).ToList();
                List<DbConstraintItem> constraints = data.DbConstraints.Where(w => catalogKey.Equals(w)).ToList();
                List<DbRoutineItem> routines = data.DbRoutines.Where(w => catalogKey.Equals(w) && w.IsSystem == false).ToList();
                List<DbRoutineParameterItem> routineParameters = data.DbRoutineParameters.Where(w => catalogKey.Equals(w)).ToList();

                totalWork = totalWork +
                    schemta.Count +
                    tables.Count +
                    tableColumns.Count +
                    constraints.Count +
                    routines.Count +
                    routineParameters.Count;

                DbCatalogKeyName catalogName = new DbCatalogKeyName(catalogItem);
                data.ModelAlias.Add(catalogItem);
                progress(completedWork++, totalWork);

                foreach (DbSchemaItem schemaItem in schemta)
                {
                    DbSchemaKey schemaKey = new DbSchemaKey(schemaItem);
                    DbSchemaKeyName schemaName = new DbSchemaKeyName(schemaItem);
                    data.ModelAlias.Add(catalogKey, schemaItem);
                    progress(completedWork++, totalWork);

                    foreach (DbTableItem tableItem in tables.Where(w => schemaName.Equals(w)))
                    {
                        DbTableKey tableKey = new DbTableKey(tableItem);
                        DbTableKeyName tableName = new DbTableKeyName(tableItem);
                        data.ModelAlias.Add(schemaKey, tableItem);
                        progress(completedWork++, totalWork);

                        foreach (DbTableColumnItem columnItem in tableColumns.Where(w => tableName.Equals(w)))
                        {
                            data.ModelAlias.Add(tableKey, columnItem);
                            progress(completedWork++, totalWork);
                        }

                        foreach (DbConstraintItem constraintItem in constraints.Where(w => tableName.Equals(w)))
                        {
                            data.ModelAlias.Add(tableKey, constraintItem);
                            progress(completedWork++, totalWork);
                        }
                    }

                    foreach (DbRoutineItem routineItem in routines.Where(w => schemaName.Equals(w)))
                    {
                        DbRoutineKey routineKey = new DbRoutineKey(routineItem);
                        DbRoutineKeyName routineName = new DbRoutineKeyName(routineItem);
                        data.ModelAlias.Add(schemaKey, routineItem);
                        progress(completedWork++, totalWork);

                        foreach (DbRoutineParameterItem parameterItem in routineParameters.Where(w => routineName.Equals(w)))
                        {
                            data.ModelAlias.Add(routineKey, parameterItem);
                            progress(completedWork++, totalWork);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Load Alias Data from a Library
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadAlias<T>(this T data, FileInfo key)
            where T : IModelLibrary, IModelAlias
        {
            List<WorkItem> work = new List<WorkItem>();
            Action<Int32, Int32> progress = (x, y) => { };

            WorkItem loadWork = new WorkItem()
            {
                WorkName = "Load Library Aliases",
                DoWork = ThreadWork
            };
            progress = loadWork.OnProgressChanged;

            work.Add(loadWork);

            return work;

            void ThreadWork()
            {
                foreach (LibrarySourceItem? item in data.LibrarySources.Where(w => String.Equals(w.SourceFile, key.Name, StringComparison.CurrentCultureIgnoreCase)))
                {
                    LibrarySourceKey sourceKey = new LibrarySourceKey(item);
                    LoadAliasCore(data, sourceKey, progress);
                }
            }

        }

        /// <summary>
        /// Load Alias Data from a Library
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadAlias<T>(this T data, ILibrarySourceKeyName key)
            where T : IModelLibrary, IModelAlias
        {
            List<WorkItem> work = new List<WorkItem>();
            Action<Int32, Int32> progress = (x, y) => { };

            WorkItem loadWork = new WorkItem()
            {
                WorkName = "Load Library Aliases",
                DoWork = ThreadWork
            };
            progress = loadWork.OnProgressChanged;

            work.Add(loadWork);

            return work;

            void ThreadWork()
            {
                LibrarySourceKeyName nameKey = new LibrarySourceKeyName(key);
                foreach (LibrarySourceItem? item in data.LibrarySources.Where(w => nameKey.Equals(w)))
                {
                    LibrarySourceKey sourceKey = new LibrarySourceKey(item);
                    LoadAliasCore(data, sourceKey, progress);
                }
            }
        }

        /// <summary>
        /// Load Alias Data from a Library
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadAlias<T>(this T data, ILibrarySourceKey key)
            where T : IModelLibrary, IModelAlias
        {
            List<WorkItem> work = new List<WorkItem>();
            Action<Int32, Int32> progress = (x, y) => { };

            WorkItem loadWork = new WorkItem()
            {
                WorkName = "Load Library Aliases",
                DoWork = ThreadWork
            };
            progress = loadWork.OnProgressChanged;

            work.Add(loadWork);

            return work;

            void ThreadWork()
            { LoadAliasCore(data, key, progress); }
        }

        private static void LoadAliasCore<T>(T data, ILibrarySourceKey key, Action<int, int> progress)
            where T : IModelLibrary, IModelAlias
        {
            LibrarySourceKey libraryKey = new LibrarySourceKey(key);
            List<LibrarySourceItem> libraries = data.LibrarySources.Where(w => libraryKey.Equals(w)).ToList();
            Int32 totalWork = libraries.Count;
            Int32 completedWork = 0;

            foreach (LibrarySourceItem libraryitem in libraries)
            {
                List<LibraryMemberItem> members = data.LibraryMembers.Where(w => libraryKey.Equals(w)).ToList();
                totalWork = totalWork + members.Count;

                data.ModelAlias.Add(libraryitem);
                progress(completedWork++, totalWork);

                foreach (LibraryMemberItem memberItem in members.
                    Where(w => w.MemberParentId is null))
                {
                    LibraryMemberKey memberKey = new LibraryMemberKey(memberItem);
                    data.ModelAlias.Add(libraryKey, memberItem);
                    progress(completedWork++, totalWork);

                    AddChildMember(libraryKey, memberKey);
                }

                void AddChildMember(LibrarySourceKey sourceKey, LibraryMemberKey memberKey)
                {
                    foreach (LibraryMemberItem memberItem in members.Where(w => new LibraryMemberKeyParent(w).Equals(memberKey)))
                    {
                        LibraryMemberKey childKey = new LibraryMemberKey(memberItem);
                        data.ModelAlias.Add(memberKey, memberItem);
                        progress(completedWork++, totalWork);

                        AddChildMember(sourceKey, childKey);
                    }
                }
            }
        }

        /// <summary>
        /// Remove Alias Data for a Catalog
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> RemoveAlias(this IModelAlias data, IDbCatalogKey key)
        {
            List<WorkItem> work = new List<WorkItem>();

            WorkItem deleteWork = new WorkItem()
            {
                WorkName = "Remove Catalog aliases",
                DoWork = () => data.ModelAlias.Remove(key),
            };

            work.Add(deleteWork);
            return work;
        }

        /// <summary>
        /// Remove Alias Data for a Library
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> RemoveAlias(this IModelAlias data, ILibrarySourceKey key)
        {
            List<WorkItem> work = new List<WorkItem>();

            WorkItem deleteWork = new WorkItem()
            {
                WorkName = "Remove Library aliases",
                DoWork = () => data.ModelAlias.Remove(key)
            };

            work.Add(deleteWork);
            return work;
        }
    }
}
