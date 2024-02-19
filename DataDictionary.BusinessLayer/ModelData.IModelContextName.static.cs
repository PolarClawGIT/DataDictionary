using DataDictionary.BusinessLayer.CatalogData;
using DataDictionary.BusinessLayer.ContextName;
using DataDictionary.DataLayer.ApplicationData;
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
using DataDictionary.DataLayer.ModelData.Attribute;
using DataDictionary.DataLayer.ModelData.Entity;
using DataDictionary.DataLayer.ModelData.SubjectArea;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer
{
    /// <summary>
    /// Implementation component for the Model Context Name data
    /// </summary>
    /// <remarks>When combined with the Extension class, this implements multi-inheritance.</remarks>
    public static class ModelContextName
    {
        /// <summary>
        /// Load Context Name Data for the Model.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadContextName<T>(this T data)
            where T : IModelCatalog, IModelLibrary, IModel, IModelDomain, IModelContextName
        {
            List<WorkItem> work = new List<WorkItem>();
            Action<Int32, Int32> progress = (x, y) => { };

            WorkItem loadWork = new WorkItem()
            {
                WorkName = "Load Context Names",
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
                    LoadContextNameCore(data, key, progress);
                }

                foreach (DbCatalogItem item in data.DbCatalogs)
                {
                    DbCatalogKey catalogKey = new DbCatalogKey(item);
                    LoadContextNameCore(data, catalogKey, progress);
                }

                foreach (LibrarySourceItem item in data.LibrarySources)
                {
                    LibrarySourceKey sourceKey = new LibrarySourceKey(item);
                    LoadContextNameCore(data, sourceKey, progress);
                }
            }
        }

        /// <summary>
        /// Load Context Name Data from a Catalog
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadContextName<T>(this T data, IDbCatalogKeyName key)
            where T : IModelCatalog, IModelContextName
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
                    LoadContextNameCore(data, catalogKey, progress);
                }
            }
        }

        /// <summary>
        /// Load Context Name Data from a Catalog
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadContextName<T>(this T data, IDbCatalogKey key)
            where T : ICatalogData, IModelContextName
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
            { LoadContextNameCore(data, key, progress); }
        }



        /// <summary>
        /// Load Context Name Data from a Library
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadContextName<T>(this T data, FileInfo key)
            where T : IModelLibrary, IModelContextName
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
                    LoadContextNameCore(data, sourceKey, progress);
                }
            }

        }

        /// <summary>
        /// Load Context Name Data from a Library
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadContextName<T>(this T data, ILibrarySourceKeyName key)
            where T : IModelLibrary, IModelContextName
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
                    LoadContextNameCore(data, sourceKey, progress);
                }
            }
        }

        /// <summary>
        /// Load Context Name Data from a Library
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadContextName<T>(this T data, ILibrarySourceKey key)
            where T : IModelLibrary, IModelContextName
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
            { LoadContextNameCore(data, key, progress); }
        }

        private static void LoadContextNameCore<T>(T data, ILibrarySourceKey key, Action<int, int> progress)
            where T : IModelLibrary, IModelContextName
        {
            LibrarySourceKey libraryKey = new LibrarySourceKey(key);
            List<LibrarySourceItem> libraries = data.LibrarySources.Where(w => libraryKey.Equals(w)).ToList();
            Int32 totalWork = libraries.Count;
            Int32 completedWork = 0;

            foreach (LibrarySourceItem libraryitem in libraries)
            {
                List<LibraryMemberItem> members = data.LibraryMembers.Where(w => libraryKey.Equals(w)).ToList();
                totalWork = totalWork + members.Count;

                data.ContextName.Add(new ContextNameItem(libraryitem));
                progress(completedWork++, totalWork);

                foreach (LibraryMemberItem memberItem in members.
                    Where(w => w.MemberParentId is null))
                {
                    LibraryMemberKey memberKey = new LibraryMemberKey(memberItem);
                    data.ContextName.Add(new ContextNameItem(libraryKey, memberItem));
                    progress(completedWork++, totalWork);

                    AddChildMember(libraryKey, memberKey);
                }

                void AddChildMember(LibrarySourceKey sourceKey, LibraryMemberKey memberKey)
                {
                    foreach (LibraryMemberItem memberItem in members.Where(w => new LibraryMemberKeyParent(w).Equals(memberKey)))
                    {
                        LibraryMemberKey childKey = new LibraryMemberKey(memberItem);
                        data.ContextName.Add(new ContextNameItem(memberKey, memberItem));
                        progress(completedWork++, totalWork);

                        AddChildMember(sourceKey, childKey);
                    }
                }
            }
        }

        /// <summary>
        /// Load Context Name Data from a Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadContextName<T>(this T data, IModelKey key)
            where T : IModel, IModelDomain, IModelContextName
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
            { LoadContextNameCore(data, key, progress); }
        }

        private static void LoadContextNameCore<T>(T data, IModelKey key, Action<int, int> progress)
            where T : IModel, IModelDomain, IModelContextName
        {
            ModelKey modelKey = new ModelKey(key);
            List<ModelItem> models = data.Models.Where(w => modelKey.Equals(w)).ToList();

            Int32 totalWork = models.Count;
            Int32 completedWork = 0;

            foreach (ModelItem modelItem in models) // There is only One, coded if future can support many
            {
                // This improve performance as it is working off of a fixed sub-set of data.
                List<ModelSubjectAreaItem> subjectAreas = data.ModelSubjectAreas.ToList();
                List<ModelAttributeItem> attributeSubjects = data.ModelAttributes.ToList();
                List<ModelEntityItem> entitySubjects = data.ModelEntities.ToList();
                List<DomainEntityItem> entities = data.DomainEntities.ToList();
                List<DomainAttributeItem> attributes = data.DomainAttributes.ToList();

                List<DomainEntityItem> missingEntities = data.DomainEntities.ToList();
                List<DomainAttributeItem> missingAttributes = data.DomainAttributes.ToList();

                List<(NameSpaceKey nameSpace, ContextNameKey key)> modelNameSpace = NameSpaceKey.Group(
                    data.ModelSubjectAreas.
                        //Where(w => !String.IsNullOrWhiteSpace(w.SubjectAreaNameSpace)).
                        Select(s => new NameSpaceKey(s))).
                        Select(s => (s, new ContextNameKey(s))).
                        ToList();

                totalWork = totalWork +
                    attributes.Count +
                    entities.Count;

                data.ContextName.Add(new ContextNameItem(modelItem));
                progress(completedWork++, totalWork);

                foreach ((NameSpaceKey nameSpace, ContextNameKey key) nameSpaceItem in modelNameSpace.OrderBy(o => o.nameSpace))
                {
                    (NameSpaceKey nameSpace, ContextNameKey key) parent = modelNameSpace.
                        FirstOrDefault(w =>
                            nameSpaceItem.nameSpace.ParentKey is NameSpaceKey nameSpaceParent
                            && nameSpaceParent.Equals(w.nameSpace));

                    ModelSubjectAreaItem? parentSubject = subjectAreas.
                        FirstOrDefault(w =>
                            nameSpaceItem.nameSpace.ParentKey is NameSpaceKey subjectNameSpaceParent
                            && subjectNameSpaceParent.Equals(new NameSpaceKey(w)));

                    IEnumerable<ModelSubjectAreaItem> subjectItems = subjectAreas.
                        Where(w => nameSpaceItem.nameSpace.Equals(new NameSpaceKey(w)));

                    foreach (ModelSubjectAreaItem subjectItem in subjectItems)
                    {
                        ContextNameItem newItem;

                        if (parentSubject is not null)
                        { newItem = new ContextNameItem(parentSubject, subjectItem); }
                        else
                        {
                            if (parent.Equals(default))
                            { newItem = new ContextNameItem(modelItem, subjectItem); }
                            else { newItem = new ContextNameItem(parent.key, subjectItem); }
                        }

                        data.ContextName.Add(newItem);

                        ModelSubjectAreaKey subjectKey = new ModelSubjectAreaKey(subjectItem);
                        progress(completedWork++, totalWork);

                        // Add Entities
                        foreach (ModelEntityItem modelEntity in entitySubjects.Where(w => subjectKey.Equals(w)))
                        {
                            DomainEntityKey entityKey = new DomainEntityKey(modelEntity);

                            foreach (DomainEntityItem entityItem in entities.Where(w => entityKey.Equals(w)))
                            {
                                if (missingEntities.Contains(entityItem)) { missingEntities.Remove(entityItem); }

                                data.ContextName.Add(new ContextNameItem(subjectItem, entityItem));
                                progress(completedWork++, totalWork);
                            }
                        }

                        // Add Attributes
                        foreach (ModelAttributeItem modelAttribute in attributeSubjects.Where(w => subjectKey.Equals(w)))
                        {
                            DomainAttributeKey attributeKey = new DomainAttributeKey(modelAttribute);

                            foreach (DomainAttributeItem attributeItem in attributes.Where(w => attributeKey.Equals(w)))
                            {
                                if (missingAttributes.Contains(attributeItem)) { missingAttributes.Remove(attributeItem); }

                                data.ContextName.Add(new ContextNameItem(subjectItem, attributeItem));
                                progress(completedWork++, totalWork);
                            }
                        }

                    }

                    // Handle No Subject Area matching (normally does not occur)
                    if (subjectItems.Count() == 0)
                    {
                        ContextNameItem newItem;

                        if (parentSubject is not null)
                        { newItem = new ContextNameItem(parentSubject, nameSpaceItem.nameSpace, nameSpaceItem.key); }
                        else
                        {
                            if (parent.Equals(default))
                            { newItem = new ContextNameItem(modelItem, nameSpaceItem.nameSpace, nameSpaceItem.key); }
                            else { newItem = new ContextNameItem(parent.key, nameSpaceItem.nameSpace, nameSpaceItem.key); }
                        }

                        data.ContextName.Add(newItem);
                    }

                }

                // Handle items not in a Subject Area scoped to the Model
                foreach (DomainEntityItem entityItem in missingEntities)
                {
                    data.ContextName.Add(new ContextNameItem(modelItem, entityItem));
                    progress(completedWork++, totalWork);
                }

                foreach (DomainAttributeItem attributeItem in missingAttributes)
                {
                    data.ContextName.Add(new ContextNameItem(modelItem, attributeItem));
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
        public static IReadOnlyList<WorkItem> RemoveContextName(this IModelContextName data, IDbCatalogKey key)
        {
            List<WorkItem> work = new List<WorkItem>();

            WorkItem deleteWork = new WorkItem()
            {
                WorkName = "Remove Catalog NameSpace",
                DoWork = () => data.ContextName.Remove(new ContextNameKey(key)),
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
        public static IReadOnlyList<WorkItem> RemoveContextName(this IModelContextName data, ILibrarySourceKey key)
        {
            List<WorkItem> work = new List<WorkItem>();

            WorkItem deleteWork = new WorkItem()
            {
                WorkName = "Remove Library NameSpace",
                DoWork = () => data.ContextName.Remove(new ContextNameKey(key))
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
        public static IReadOnlyList<WorkItem> RemoveContextName(this IModelContextName data, IModelKey key)
        {
            List<WorkItem> work = new List<WorkItem>();
            ModelKey modelKey = new ModelKey(key);

            WorkItem deleteWork = new WorkItem()
            {
                WorkName = "Remove Model Context Name",
                DoWork = () => data.ContextName.Remove(new ContextNameKey(modelKey))
            };

            work.Add(deleteWork);
            return work;
        }

        /// <summary>
        /// Remove NameSpace Data for the entire dataset
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> RemoveContextName<T>(this T data)
            where T : IModelContextName, IModel, IModelCatalog, IModelLibrary
        {
            List<WorkItem> work = new List<WorkItem>();

            foreach (ModelItem item in data.Models)
            { work.AddRange(RemoveContextName(data, item)); }

            foreach (DbCatalogItem item in data.DbCatalogs)
            { work.AddRange(RemoveContextName(data, item)); }

            foreach (LibrarySourceItem item in data.LibrarySources)
            { work.AddRange(RemoveContextName(data, item)); }

            return work;
        }
    }
}
