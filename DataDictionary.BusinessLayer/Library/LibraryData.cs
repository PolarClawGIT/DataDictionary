using DataDictionary.BusinessLayer.NameScope;
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
    public interface ILibraryData :
        ILoadData<ILibrarySourceKey>, ILoadData<IModelKey>,
        ISaveData<ILibrarySourceKey>, ISaveData<IModelKey>
    {
        /// <summary>
        /// List of .Net Library Members within the Model
        /// </summary>
        ILibraryMemberData LibraryMembers { get; }

        /// <summary>
        /// List of .Net Libraries within the Model
        /// </summary>
        ILibrarySourceData LibrarySources { get; }

    }

    class LibraryData : ILibraryData, IDataTableFile//, INameScopeData
    {
        /// <inheritdoc/>
        public ILibraryMemberData LibraryMembers { get { return members; } }
        private readonly LibraryMemberData members;

        /// <inheritdoc/>
        public ILibrarySourceData LibrarySources { get { return sources; } }
        private readonly LibrarySourceData sources;

        public LibraryData() :base()
        {
            sources = new LibrarySourceData();
            members = new LibraryMemberData();
        }

        /// <inheritdoc/>
        /// <remarks>Library</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ILibrarySourceKey dataKey)
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
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ILibrarySourceKey dataKey)
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
        public void Remove(ILibrarySourceKey sourceItem)
        {
            sources.Remove(sourceItem);
            members.Remove(sourceItem);
        }

        /// <inheritdoc />
        public IReadOnlyList<NameScopeItem> GetNameScopes() 
        {
            List<NameScopeItem> result = new List<NameScopeItem>();
            List<LibrarySourceItem> libraries = LibrarySources.ToList();
            List<LibraryMemberItem> members = LibraryMembers.ToList();

            foreach (LibrarySourceItem libraryitem in libraries)
            {
                LibrarySourceKey libraryKey = new LibrarySourceKey(libraryitem);
                result.Add(new NameScopeItem(libraryitem));

                foreach (LibraryMemberItem memberItem in members.
                    Where(w => w.MemberParentId is null))
                {
                    LibraryMemberKey memberKey = new LibraryMemberKey(memberItem);
                    result.Add(new NameScopeItem(libraryKey, memberItem));

                    AddChildMember(libraryKey, memberKey);
                }

                void AddChildMember(LibrarySourceKey sourceKey, LibraryMemberKey memberKey)
                {
                    foreach (LibraryMemberItem memberItem in members.Where(w => new LibraryMemberKeyParent(w).Equals(memberKey)))
                    {
                        LibraryMemberKey childKey = new LibraryMemberKey(memberItem);
                        result.Add(new NameScopeItem(memberKey, memberItem));

                        AddChildMember(sourceKey, childKey);
                    }
                }
            }

            return result;
        }

    }
}
