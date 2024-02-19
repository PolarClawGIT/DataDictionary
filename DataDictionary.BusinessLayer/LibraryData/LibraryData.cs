using DataDictionary.BusinessLayer.ContextName;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.LibraryData.Member;
using DataDictionary.DataLayer.LibraryData.Source;
using DataDictionary.DataLayer.ModelData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.LibraryData
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

    class LibraryData : ILibraryData, IContextNameData
    {
        /// <inheritdoc/>
        public ILibraryMemberData LibraryMembers { get { return members; } }
        private readonly LibraryMemberData members;

        /// <inheritdoc/>
        public ILibrarySourceData LibrarySources { get { return sources; } }
        private readonly LibrarySourceData sources;

        public LibraryData() :base()
        {
            members = new LibraryMemberData();
            sources = new LibrarySourceData();
        }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ILibrarySourceKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(sources.Load(factory, dataKey));
            work.AddRange(members.Load(factory, dataKey));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(sources.Load(factory, dataKey));
            work.AddRange(members.Load(factory, dataKey));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ILibrarySourceKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(sources.Save(factory, dataKey));
            work.AddRange(members.Save(factory, dataKey));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(sources.Save(factory, dataKey));
            work.AddRange(members.Save(factory, dataKey));

            return work;
        }

        /// <inheritdoc />
        public void Remove(ILibrarySourceKey sourceItem)
        {
            sources.Remove(sourceItem);
            members.Remove(sourceItem);
        }

        /// <inheritdoc />
        public IReadOnlyList<ContextNameItem> GetContextNames(Action<Int32, Int32> progress) 
        {
            List<ContextNameItem> result = new List<ContextNameItem>();
            List<LibrarySourceItem> libraries = LibrarySources.ToList();
            List<LibraryMemberItem> members = LibraryMembers.ToList();

            Int32 totalWork = libraries.Count + members.Count();
            Int32 completedWork = 0;

            foreach (LibrarySourceItem libraryitem in libraries)
            {
                LibrarySourceKey libraryKey = new LibrarySourceKey(libraryitem);
                result.Add(new ContextNameItem(libraryitem));
                progress(completedWork++, totalWork);

                foreach (LibraryMemberItem memberItem in members.
                    Where(w => w.MemberParentId is null))
                {
                    LibraryMemberKey memberKey = new LibraryMemberKey(memberItem);
                    result.Add(new ContextNameItem(libraryKey, memberItem));
                    progress(completedWork++, totalWork);

                    AddChildMember(libraryKey, memberKey);
                }

                void AddChildMember(LibrarySourceKey sourceKey, LibraryMemberKey memberKey)
                {
                    foreach (LibraryMemberItem memberItem in members.Where(w => new LibraryMemberKeyParent(w).Equals(memberKey)))
                    {
                        LibraryMemberKey childKey = new LibraryMemberKey(memberItem);
                        result.Add(new ContextNameItem(memberKey, memberItem));
                        progress(completedWork++, totalWork);

                        AddChildMember(sourceKey, childKey);
                    }
                }
            }

            return result;
        }

    }
}
