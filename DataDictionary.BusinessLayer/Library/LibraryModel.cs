﻿using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.ModelData;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Library
{
    /// <summary>
    /// Interface representing .Net Library Data
    /// </summary>
    public interface ILibraryModel :
        ILoadData<ILibrarySourceIndex>, ISaveData<ILibrarySourceIndex>, IDeleteData<ILibrarySourceIndex>,
        ILoadData<IModelKey>, ISaveData<IModelKey>
    {
        /// <summary>
        /// List of .Net Library Members within the Model
        /// </summary>
        ILibraryMemberData LibraryMembers { get; }

        /// <summary>
        /// List of .Net Libraries within the Model
        /// </summary>
        ILibrarySourceData LibrarySources { get; }

        /// <summary>
        /// Imports a Library from Visual Studio XML Documentation file.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Import(FileInfo source);
    }

    class LibraryModel : ILibraryModel, IDataTableFile,
        INamedScopeSourceData
    {
        /// <inheritdoc/>
        public ILibraryMemberData LibraryMembers { get { return members; } }
        private readonly LibraryMemberData members;

        /// <inheritdoc/>
        public ILibrarySourceData LibrarySources { get { return sources; } }
        private readonly LibrarySourceData sources;

        public LibraryModel() : base()
        {
            sources = new LibrarySourceData() { Library = this };
            members = new LibraryMemberData() { Library = this };
        }

        /// <inheritdoc/>
        /// <remarks>Library</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ILibrarySourceIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(sources.Load(factory, dataKey));
            work.AddRange(members.Load(factory, dataKey));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Library</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(sources.Load(factory, dataKey));
            work.AddRange(members.Load(factory, dataKey));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Library</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ILibrarySourceIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(sources.Save(factory, dataKey));
            work.AddRange(members.Save(factory, dataKey));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Library</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(sources.Save(factory, dataKey));
            work.AddRange(members.Save(factory, dataKey));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Library</remarks>
        public IReadOnlyList<System.Data.DataTable> Export()
        {
            List<System.Data.DataTable> result = new List<System.Data.DataTable>();
            result.Add(sources.ToDataTable());
            result.Add(members.ToDataTable());
            return result;
        }

        /// <inheritdoc/>
        /// <remarks>Library</remarks>
        public void Import(System.Data.DataSet source)
        {
            sources.Load(source);
            members.Load(source);
        }

        /// <inheritdoc />
        /// <remarks>Library</remarks>
        public IReadOnlyList<WorkItem> Delete(ILibrarySourceIndex key)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(sources.Delete(key));
            work.AddRange(members.Delete(key));
            return work;
        }

        /// <inheritdoc />
        /// <remarks>Library</remarks>
        public IReadOnlyList<WorkItem> Delete()
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(sources.Delete());
            work.AddRange(members.Delete());
            return work;
        }

        /// <inheritdoc />
        /// <remarks>Library</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
        { return Delete(); }

        /// <inheritdoc />
        /// <remarks>Library</remarks>
        public IReadOnlyList<WorkItem> Import(FileInfo source)
        {
            List<WorkItem> work = new List<WorkItem>();
            LibraryImport import = new LibraryImport();

            WorkItem item = new WorkItem()
            {
                WorkName = "Import Library",
                DoWork = () => { import.Import(source); }
            };
            import.Progress = item.OnProgressChanged;
            work.Add(item);

            work.Add(new WorkItem()
            {
                WorkName = "Import Library",
                DoWork = () =>
                {
                    sources.AddRange(import.Sources);
                    members.AddRange(import.Members);
                }
            });

            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> LoadNamedScope(Action<INamedScopeSourceValue?, NamedScopeValue> addNamedScope)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(sources.LoadNamedScope(addNamedScope));
            work.AddRange(members.LoadNamedScope(addNamedScope));

            return work;
        }

    }
}

