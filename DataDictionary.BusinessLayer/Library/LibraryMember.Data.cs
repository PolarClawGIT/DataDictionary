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
        /// <inheritdoc/>
        /// <remarks>Library Source</remarks>
        public IEnumerable<NamedScopePair> GetNamedScopes()
        {
            List<NamedScopePair> result = new List<NamedScopePair>();

            foreach (TValue item in this)
            {
                LibrarySourceIndex libraryKey = new LibrarySourceIndex(item);
                LibraryMemberIndexParent parentKey = new LibraryMemberIndexParent(item);

                LibrarySourceValue? library = Library.LibrarySources.FirstOrDefault(w => libraryKey.Equals(w));
                TValue? parent = this.FirstOrDefault(w => parentKey.Equals(new LibraryMemberIndex(w)));

                if (parent is null && library is not null)
                { result.Add(new NamedScopePair(library.GetKey(), item)); }
                else if (parent is not null)
                { result.Add(new NamedScopePair(parent.GetKey(), item)); }
                else { throw new InvalidOperationException("Could not determine Parent"); }
            }

            return result;
        }

    }
}
