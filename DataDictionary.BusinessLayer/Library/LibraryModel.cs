// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.DbWorkItem;
using Toolbox.BindingTable;
using Toolbox.Threading;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.ToolSet;

namespace DataDictionary.BusinessLayer.Library
{
    /// <summary>
    /// Interface representing .Net Library Data
    /// </summary>
    public interface ILibraryModel :
        ILoadData<ILibrarySourceIndex>, ISaveData<ILibrarySourceIndex>, IDeleteData<ILibrarySourceIndex>,
        ILoadData<IModelIndex>, ISaveData<IModelIndex>,
        IBindData
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

    class LibraryModel : ILibraryModel, IDataTableFile
    {
        /// <inheritdoc/>
        public ILibraryMemberData LibraryMembers { get { return memberValues; } }
        private readonly LibraryMemberData memberValues;

        /// <inheritdoc/>
        public ILibrarySourceData LibrarySources { get { return sourceValues; } }
        private readonly LibrarySourceData sourceValues;

        /// <inheritdoc/>
        public Boolean RaiseListChangedEvents
        {
            get
            {
                return memberValues.RaiseListChangedEvents
                    && sourceValues.RaiseListChangedEvents;
            }
            set
            {
                memberValues.RaiseListChangedEvents = value;
                sourceValues.RaiseListChangedEvents = value;
            }
        }

        public LibraryModel() : base()
        {
            sourceValues = new LibrarySourceData() { Library = this };
            memberValues = new LibraryMemberData() { Library = this };
        }

        /// <inheritdoc/>
        /// <remarks>Library</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ILibrarySourceIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(sourceValues.Load(factory, dataKey));
            work.AddRange(memberValues.Load(factory, dataKey));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Library</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ILibrarySourceIndex dataKey, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(sourceValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(memberValues.Load(factory, dataKey, asOfUtcDate));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Library</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(sourceValues.Load(factory, dataKey));
            work.AddRange(memberValues.Load(factory, dataKey));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Library</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(sourceValues.Load(factory, dataKey, asOfUtcDate));
            work.AddRange(memberValues.Load(factory, dataKey, asOfUtcDate));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Library</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ILibrarySourceIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(sourceValues.Save(factory, dataKey));
            work.AddRange(memberValues.Save(factory, dataKey));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Library</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(sourceValues.Save(factory, dataKey));
            work.AddRange(memberValues.Save(factory, dataKey));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Library</remarks>
        public IReadOnlyList<System.Data.DataTable> Export()
        {
            List<System.Data.DataTable> result = new List<System.Data.DataTable>();
            result.Add(sourceValues.ToDataTable());
            result.Add(memberValues.ToDataTable());
            return result;
        }

        /// <inheritdoc/>
        /// <remarks>Library</remarks>
        public void Import(System.Data.DataSet source)
        {
            sourceValues.Load(source);
            memberValues.Load(source);
        }

        /// <inheritdoc />
        /// <remarks>Library</remarks>
        public IReadOnlyList<WorkItem> Delete(ILibrarySourceIndex key)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(sourceValues.Delete(key));
            work.AddRange(memberValues.Delete(key));
            return work;
        }

        /// <inheritdoc />
        /// <remarks>Library</remarks>
        public IReadOnlyList<WorkItem> Delete()
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(sourceValues.Delete());
            work.AddRange(memberValues.Delete());
            return work;
        }

        /// <inheritdoc />
        /// <remarks>Library</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
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
                    sourceValues.AddRange(import.Sources);
                    memberValues.AddRange(import.Members);
                }
            });

            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> LoadNamedScope(Action<INamedScopeSourceValue?, NamedScopeValue> addNamedScope)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(NameSpaceSource.Load<LibrarySourceData, LibrarySourceValue>(sourceValues, addNamedScope));
            work.AddRange(NameSpaceSource.Load<LibraryMemberData, LibraryMemberValue>(memberValues, addNamedScope,
                (parent) =>
                {
                    LibrarySourceIndex libraryKey = new LibrarySourceIndex(parent);
                    LibraryMemberIndex parentKey = new LibraryMemberIndex(new LibraryMemberIndexParent(parent));

                    if (memberValues.FirstOrDefault(w => parentKey.Equals(w)) is LibraryMemberValue memberParent)
                    { return memberParent; }
                    else if (sourceValues.FirstOrDefault(w => libraryKey.Equals(w)) is LibrarySourceValue sourceParent)
                    { return sourceParent; }
                    else { return null; }
                }));

            return work;
        }

        /// <inheritdoc/>
        public void Remove(ILibrarySourceIndex dataKey)
        {
            sourceValues.Remove(dataKey);
            memberValues.Remove(dataKey);
        }

        /// <inheritdoc/>
        public void Remove(IModelIndex dataKey)
        {
            sourceValues.Remove(dataKey);
            memberValues.Remove(dataKey);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            sourceValues.Clear();
            memberValues.Clear();
        }

        /// <inheritdoc/>
        public void ResetBindings()
        {
            sourceValues.ResetBindings();
            memberValues.ResetBindings();
        }
    }
}

