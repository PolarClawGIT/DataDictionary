﻿using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.LibraryData.Member;
using DataDictionary.DataLayer.LibraryData.Source;
using DataDictionary.DataLayer.ModelData;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Library
{
    /// <summary>
    /// Interface representing .Net Library Data
    /// </summary>
    public interface ILibraryModel :
        ILoadData<ILibrarySourceIndex>, ISaveData<ILibrarySourceIndex>, IRemoveData<ILibrarySourceIndex>,
        ILoadData<IModelKey>, ISaveData<IModelKey>
    {
        /// <summary>
        /// List of .Net Library Members within the Model
        /// </summary>
        ILibraryMemberData<LibraryMemberValue> LibraryMembers { get; }

        /// <summary>
        /// List of .Net Libraries within the Model
        /// </summary>
        ILibrarySourceData<LibrarySourceValue> LibrarySources { get; }

        /// <summary>
        /// Imports a Library from Visual Studio XML Documentation file.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Import(FileInfo source);
    }

    class LibraryModel : ILibraryModel, IDataTableFile
    {
        /// <inheritdoc/>
        public ILibraryMemberData<LibraryMemberValue> LibraryMembers { get { return members; } }
        private readonly LibraryMemberData<LibraryMemberValue> members;

        /// <inheritdoc/>
        public ILibrarySourceData<LibrarySourceValue> LibrarySources { get { return sources; } }
        private readonly LibrarySourceData<LibrarySourceValue> sources;

        public LibraryModel() : base()
        {
            sources = new LibrarySourceData<LibrarySourceValue>() { Library = this };
            members = new LibraryMemberData<LibraryMemberValue>() { Library = this };
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
        public IReadOnlyList<WorkItem> Remove(ILibrarySourceIndex key)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem() { WorkName = "Remove Library Source", DoWork = () => { sources.Remove(key); } });
            work.Add(new WorkItem() { WorkName = "Remove Library Members", DoWork = () => { members.Remove(key); } });

            return work;
        }

        /// <inheritdoc />
        /// <remarks>Library</remarks>
        public IReadOnlyList<WorkItem> Remove()
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem() { WorkName = "Remove Library Source", DoWork = () => { sources.Clear(); } });
            work.Add(new WorkItem() { WorkName = "Remove Library Members", DoWork = () => { members.Clear(); } });

            return work;
        }

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

        public IReadOnlyList<WorkItem> BuildNamedScope(NamedScopeData target)
        {
            List<WorkItem> work = new List<WorkItem>();
            ProgressTracker progress = new ProgressTracker();

            WorkItem workItem = new WorkItem()
            {
                WorkName = "Build NamedScope (Libraries)",
                DoWork = () =>
                {
                    target.AddRange(sources.GetNamedScopes());
                    target.AddRange(members.GetNamedScopes());
                }
            };
            progress.OnProgressChanged = workItem.OnProgressChanged;

            work.Add(workItem);
            return work;
        }
    }
}

