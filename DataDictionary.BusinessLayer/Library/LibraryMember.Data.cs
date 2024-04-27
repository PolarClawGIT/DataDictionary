using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ModelData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbLayer = DataDictionary.DataLayer.LibraryData;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Library
{
    /// <summary>
    /// Interface representing Library Member data
    /// </summary>
    public interface ILibraryMemberData<TValue> : IBindingData<TValue>
        where TValue : LibraryMemberValue
    { }

    class LibraryMemberData<TValue> : DbLayer.Member.LibraryMemberCollection<TValue>, ILibraryMemberData<TValue>,
        ILoadData<DbLayer.Source.ILibrarySourceKey>, ISaveData<DbLayer.Source.ILibrarySourceKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        IGetNamedScopes
        where TValue : LibraryMemberValue, new()
    {
        /// <inheritdoc/>
        public required ILibraryModel Library { get; init; }

        /// <inheritdoc/>
        /// <remarks>Library Member</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, DbLayer.Source.ILibrarySourceKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Library Member</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Library Member</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, DbLayer.Source.ILibrarySourceKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Library Member</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Library Member</remarks>
<<<<<<< HEAD
=======
        /// <inheritdoc/>
        /// <remarks>Library Source</remarks>
>>>>>>> RenameIndexValue
        public IEnumerable<NamedScopePair> GetNamedScopes()
        {
            List<NamedScopePair> result = new List<NamedScopePair>();

<<<<<<< HEAD
            foreach (TValue item in this.Where(w => w.MemberParentId is null))
            {
                result.Add(new NamedScopePair(item));
                result.AddRange(GetChildren(item));
=======
            foreach (TValue root in this.Where(w => w.MemberParentId is null))
            {
                LibrarySourceIndex libraryKey = new LibrarySourceIndex(root);
                if(Library.LibrarySources.Where(w => libraryKey.Equals(w)) is LibrarySourceValue library)
                {
                    result.Add(new NamedScopePair(library.GetSystemId(), root));
                    result.AddRange(GetChildren(root));
                }
>>>>>>> RenameIndexValue
            }

            return result;

<<<<<<< HEAD
            IEnumerable<NamedScopePair> GetChildren(TValue child)
            {
                List<NamedScopePair> result = new List<NamedScopePair>();
                LibraryMemberIndex key = new LibraryMemberIndex(child);

                foreach (TValue item in this.Where(w => key.Equals(new LibraryMemberIndexParent(w))))
                {
                    result.Add(new NamedScopePair(item));
=======
            IEnumerable<NamedScopePair> GetChildren(LibraryMemberValue parent)
            {
                List<NamedScopePair> result = new List<NamedScopePair>();
                LibraryMemberIndex parentKey = new LibraryMemberIndex(parent);

                foreach (TValue item in this.Where(w => parentKey.Equals(new LibraryMemberIndexParent(w))))
                {
                    result.Add(new NamedScopePair(parent.GetSystemId(), item));
>>>>>>> RenameIndexValue
                    result.AddRange(GetChildren(item));
                }

                return result;
            }
        }

    }
}
