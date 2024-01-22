using DataDictionary.BusinessLayer.NameSpace;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Domain;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.LibraryData.Member;
using DataDictionary.DataLayer.LibraryData.Source;
using DataDictionary.DataLayer.ModelData;
using DataDictionary.DataLayer.ModelData.SubjectArea;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer
{
    /// <summary>
    /// Interface component for the Model Alias
    /// </summary>
    /// <remarks>When combined with the Extension class, this approximates multi-inheritance.</remarks>
    public interface IModelNamespace
    {
        /// <summary>
        /// List of available Model Alias Items from Catalogs and Libraries.
        /// </summary>
        ModelNameSpaceDictionary ModelNamespace { get; }
    }

    /// <summary>
    /// Implementation component for the Model Alias data
    /// </summary>
    /// <remarks>When combined with the Extension class, this implements multi-inheritance.</remarks>
    public static class ModelNamespace
    {
        /// <summary>
        /// Load NameSpace Data for the Model.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadNameSpace<T>(this T data)
            where T : IModelCatalog, IModelLibrary, IModel, IModelDomain, IModelNamespace
        {
            List<WorkItem> work = new List<WorkItem>();
            Action<Int32, Int32> progress = (x, y) => { };

            WorkItem loadWork = new WorkItem()
            {
                WorkName = "Load NameSpace",
                DoWork = ThreadWork
            };
            progress = loadWork.OnProgressChanged;

            work.Add(loadWork);

            return work;

            void ThreadWork()
            {
                foreach (ModelItem item in data.Models)
                {
                    ModelKey key = new ModelKey(item);
                    LoadNameSpaceCore(data, key, progress);
                }

                foreach (DbCatalogItem item in data.DbCatalogs)
                {
                    DbCatalogKey catalogKey = new DbCatalogKey(item);
                    LoadNameSpaceCore(data, catalogKey, progress);
                }

                foreach (LibrarySourceItem item in data.LibrarySources)
                {
                    LibrarySourceKey sourceKey = new LibrarySourceKey(item);
                    LoadNameSpaceCore(data, sourceKey, progress);
                }
            }
        }

        /// <summary>
        /// Load NameSpace Data from a Catalog
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadNameSpace<T>(this T data, IDbCatalogKeyName key)
            where T : IModelCatalog, IModelNamespace
        {
            List<WorkItem> work = new List<WorkItem>();
            Action<Int32, Int32> progress = (x, y) => { };

            WorkItem loadWork = new WorkItem()
            {
                WorkName = "Load Catalog NameSpace",
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
                    LoadNameSpaceCore(data, catalogKey, progress);
                }
            }
        }

        /// <summary>
        /// Load NameSpace Data from a Catalog
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadNameSpace<T>(this T data, IDbCatalogKey key)
            where T : IModelCatalog, IModelNamespace
        {
            List<WorkItem> work = new List<WorkItem>();
            Action<Int32, Int32> progress = (x, y) => { };

            WorkItem loadWork = new WorkItem()
            {
                WorkName = "Load Catalog NameSpace",
                DoWork = ThreadWork
            };
            progress = loadWork.OnProgressChanged;

            work.Add(loadWork);

            return work;

            void ThreadWork()
            { LoadNameSpaceCore(data, key, progress); }
        }

        private static void LoadNameSpaceCore<T>(T data, IDbCatalogKey key, Action<Int32, Int32> progress)
            where T : IModelCatalog, IModelNamespace
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
                List<DbDomainItem> domains = data.DbDomains.Where(w => catalogKey.Equals(w)).ToList();

                totalWork = totalWork +
                    schemta.Count +
                    tables.Count +
                    tableColumns.Count +
                    constraints.Count +
                    routines.Count +
                    routineParameters.Count +
                    domains.Count;

                DbCatalogKeyName catalogName = new DbCatalogKeyName(catalogItem);
                data.ModelNamespace.Add(new ModelNameSpaceItem(catalogItem));
                progress(completedWork++, totalWork);

                foreach (DbSchemaItem schemaItem in schemta)
                {
                    DbSchemaKey schemaKey = new DbSchemaKey(schemaItem);
                    DbSchemaKeyName schemaName = new DbSchemaKeyName(schemaItem);
                    data.ModelNamespace.Add(new ModelNameSpaceItem(catalogKey, schemaItem));
                    progress(completedWork++, totalWork);

                    foreach (DbTableItem tableItem in tables.Where(w => schemaName.Equals(w)))
                    {
                        DbTableKey tableKey = new DbTableKey(tableItem);
                        DbTableKeyName tableName = new DbTableKeyName(tableItem);
                        data.ModelNamespace.Add(new ModelNameSpaceItem(schemaKey, tableItem));
                        progress(completedWork++, totalWork);

                        foreach (DbTableColumnItem columnItem in tableColumns.Where(w => tableName.Equals(w)))
                        {
                            data.ModelNamespace.Add(new ModelNameSpaceItem(tableKey, columnItem));
                            progress(completedWork++, totalWork);
                        }

                        foreach (DbConstraintItem constraintItem in constraints.Where(w => tableName.Equals(w)))
                        {
                            data.ModelNamespace.Add(new ModelNameSpaceItem(tableKey, constraintItem));
                            progress(completedWork++, totalWork);
                        }
                    }

                    foreach (DbRoutineItem routineItem in routines.Where(w => schemaName.Equals(w)))
                    {
                        DbRoutineKey routineKey = new DbRoutineKey(routineItem);
                        DbRoutineKeyName routineName = new DbRoutineKeyName(routineItem);
                        data.ModelNamespace.Add(new ModelNameSpaceItem(schemaKey, routineItem));
                        progress(completedWork++, totalWork);

                        foreach (DbRoutineParameterItem parameterItem in routineParameters.Where(w => routineName.Equals(w)))
                        {
                            data.ModelNamespace.Add(new ModelNameSpaceItem(routineKey, parameterItem));
                            progress(completedWork++, totalWork);
                        }
                    }

                    foreach (DbDomainItem domainItem in domains.Where(w => schemaName.Equals(w)))
                    {
                        data.ModelNamespace.Add(new ModelNameSpaceItem(schemaKey, domainItem));
                        progress(completedWork++, totalWork);
                    }
                }
            }
        }

        /// <summary>
        /// Load NameSpace Data from a Library
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadNameSpace<T>(this T data, FileInfo key)
            where T : IModelLibrary, IModelNamespace
        {
            List<WorkItem> work = new List<WorkItem>();
            Action<Int32, Int32> progress = (x, y) => { };

            WorkItem loadWork = new WorkItem()
            {
                WorkName = "Load Library NameSpace",
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
                    LoadNameSpaceCore(data, sourceKey, progress);
                }
            }

        }

        /// <summary>
        /// Load NameSpace Data from a Library
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadNameSpace<T>(this T data, ILibrarySourceKeyName key)
            where T : IModelLibrary, IModelNamespace
        {
            List<WorkItem> work = new List<WorkItem>();
            Action<Int32, Int32> progress = (x, y) => { };

            WorkItem loadWork = new WorkItem()
            {
                WorkName = "Load Library NameSpace",
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
                    LoadNameSpaceCore(data, sourceKey, progress);
                }
            }
        }

        /// <summary>
        /// Load NameSpace Data from a Library
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadNameSpace<T>(this T data, ILibrarySourceKey key)
            where T : IModelLibrary, IModelNamespace
        {
            List<WorkItem> work = new List<WorkItem>();
            Action<Int32, Int32> progress = (x, y) => { };

            WorkItem loadWork = new WorkItem()
            {
                WorkName = "Load Library NameSpace",
                DoWork = ThreadWork
            };
            progress = loadWork.OnProgressChanged;

            work.Add(loadWork);

            return work;

            void ThreadWork()
            { LoadNameSpaceCore(data, key, progress); }
        }

        private static void LoadNameSpaceCore<T>(T data, ILibrarySourceKey key, Action<int, int> progress)
            where T : IModelLibrary, IModelNamespace
        {
            LibrarySourceKey libraryKey = new LibrarySourceKey(key);
            List<LibrarySourceItem> libraries = data.LibrarySources.Where(w => libraryKey.Equals(w)).ToList();
            Int32 totalWork = libraries.Count;
            Int32 completedWork = 0;

            foreach (LibrarySourceItem libraryitem in libraries)
            {
                List<LibraryMemberItem> members = data.LibraryMembers.Where(w => libraryKey.Equals(w)).ToList();
                totalWork = totalWork + members.Count;

                data.ModelNamespace.Add(new ModelNameSpaceItem(libraryitem));
                progress(completedWork++, totalWork);

                foreach (LibraryMemberItem memberItem in members.
                    Where(w => w.MemberParentId is null))
                {
                    LibraryMemberKey memberKey = new LibraryMemberKey(memberItem);
                    data.ModelNamespace.Add(new ModelNameSpaceItem(libraryKey, memberItem));
                    progress(completedWork++, totalWork);

                    AddChildMember(libraryKey, memberKey);
                }

                void AddChildMember(LibrarySourceKey sourceKey, LibraryMemberKey memberKey)
                {
                    foreach (LibraryMemberItem memberItem in members.Where(w => new LibraryMemberKeyParent(w).Equals(memberKey)))
                    {
                        LibraryMemberKey childKey = new LibraryMemberKey(memberItem);
                        data.ModelNamespace.Add(new ModelNameSpaceItem(memberKey, memberItem));
                        progress(completedWork++, totalWork);

                        AddChildMember(sourceKey, childKey);
                    }
                }
            }
        }

        /// <summary>
        /// Load NameSpace Data from a Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadNameSpace<T>(this T data, IModelKey key)
            where T : IModel, IModelDomain, IModelNamespace
        {
            List<WorkItem> work = new List<WorkItem>();
            Action<Int32, Int32> progress = (x, y) => { };

            WorkItem loadWork = new WorkItem()
            {
                WorkName = "Load Domain NameSpace",
                DoWork = ThreadWork
            };
            progress = loadWork.OnProgressChanged;

            work.Add(loadWork);

            return work;

            void ThreadWork()
            { LoadNameSpaceCore(data, key, progress); }
        }

        private static void LoadNameSpaceCore<T>(T data, IModelKey key, Action<int, int> progress)
            where T : IModel, IModelDomain, IModelNamespace
        {
            ModelKey modelKey = new ModelKey(key);
            List<ModelItem> models = data.Models.Where(w => modelKey.Equals(w)).ToList();

            Int32 totalWork = models.Count;
            Int32 completedWork = 0;

            foreach (ModelItem modelItem in models) // There is only One, coded if future can support many
            {
                // This improve performance as it is working off of a fixed sub-set of data.
                List<ModelSubjectAreaItem> subjectAreas = data.ModelSubjectAreas.ToList();
                List<DomainEntityItem> entities = data.DomainEntities.ToList();
                List<DomainAttributeItem> attributes = data.DomainAttributes.ToList();

                List<DomainEntityItem> missingEntities = data.DomainEntities.ToList();
                List<DomainAttributeItem> missingAttributes = data.DomainAttributes.ToList();

                totalWork = totalWork +
                    attributes.Count +
                    entities.Count;

                data.ModelNamespace.Add(new ModelNameSpaceItem(modelItem));
                progress(completedWork++, totalWork);

                foreach (ModelSubjectAreaItem subjectItem in subjectAreas)
                {
                    ModelSubjectAreaKey subjectKey = new ModelSubjectAreaKey(subjectItem);
                    data.ModelNamespace.Add(new ModelNameSpaceItem(modelItem, subjectItem));
                    progress(completedWork++, totalWork);


                    //TODO: Hierarchy of Subject areas (Db Change)

                    foreach (DomainEntityItem entityItem in entities.Where(w => subjectKey.Equals(w)))
                    {
                        if (missingEntities.Contains(entityItem)) { missingEntities.Remove(entityItem); }

                        data.ModelNamespace.Add(new ModelNameSpaceItem(subjectItem, entityItem));
                        progress(completedWork++, totalWork);
                    }

                    foreach (DomainAttributeItem attributeItem in attributes.Where(w => subjectKey.Equals(w)))
                    {
                        if (missingAttributes.Contains(attributeItem)) { missingAttributes.Remove(attributeItem); }

                        data.ModelNamespace.Add(new ModelNameSpaceItem(subjectItem, attributeItem));
                        progress(completedWork++, totalWork);
                    }
                }

                // Handle items not in a Subject Area scoped to the Model
                foreach (DomainEntityItem entityItem in missingEntities)
                {
                    data.ModelNamespace.Add(new ModelNameSpaceItem(modelItem, entityItem));
                    progress(completedWork++, totalWork);
                }

                foreach (DomainAttributeItem attributeItem in missingAttributes)
                {
                    data.ModelNamespace.Add(new ModelNameSpaceItem(modelItem, attributeItem));
                    progress(completedWork++, totalWork);
                }

            }
        }

        /// <summary>
        /// Remove NameSpace Data for a Catalog
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> RemoveNameSpace(this IModelNamespace data, IDbCatalogKey key)
        {
            List<WorkItem> work = new List<WorkItem>();

            WorkItem deleteWork = new WorkItem()
            {
                WorkName = "Remove Catalog NameSpace",
                DoWork = () => data.ModelNamespace.Remove(new ModelNameSpaceKey(key)),
            };

            work.Add(deleteWork);
            return work;
        }

        /// <summary>
        /// Remove NameSpace Data for a Library
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> RemoveNameSpace(this IModelNamespace data, ILibrarySourceKey key)
        {
            List<WorkItem> work = new List<WorkItem>();

            WorkItem deleteWork = new WorkItem()
            {
                WorkName = "Remove Library NameSpace",
                DoWork = () => data.ModelNamespace.Remove(new ModelNameSpaceKey(key))
            };

            work.Add(deleteWork);
            return work;
        }

        /// <summary>
        /// Remove NameSpace Data for a Model
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> RemoveNameSpace(this IModelNamespace data, IModelKey key)
        {
            List<WorkItem> work = new List<WorkItem>();
            ModelKey modelKey = new ModelKey(key);

            WorkItem deleteWork = new WorkItem()
            {
                WorkName = "Remove Model NameSpace",
                DoWork = () => data.ModelNamespace.Remove(new ModelNameSpaceKey(modelKey))
            };

            work.Add(deleteWork);
            return work;
        }

        /// <summary>
        /// Remove NameSpace Data for the entire dataset XX
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> RemoveNameSpace<T>(this T data)
            where T : IModelNamespace, IModel, IModelCatalog, IModelLibrary
        {
            List<WorkItem> work = new List<WorkItem>();

            foreach (ModelItem item in data.Models)
            { work.AddRange(RemoveNameSpace(data, item)); }

            foreach (DbCatalogItem item in data.DbCatalogs)
            { work.AddRange(RemoveNameSpace(data, item)); }

            foreach (LibrarySourceItem item in data.LibrarySources)
            { work.AddRange(RemoveNameSpace(data, item)); }

            return work;
        }
    }
}
