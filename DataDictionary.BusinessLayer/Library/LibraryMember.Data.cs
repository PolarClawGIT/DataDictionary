using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ModelData;
using DbLayer = DataDictionary.DataLayer.LibraryData;
using Toolbox.Threading;
using System.ComponentModel;

namespace DataDictionary.BusinessLayer.Library
{
    /// <summary>
    /// Interface representing Library Member data
    /// </summary>
    public interface ILibraryMemberData : IBindingData<LibraryMemberValue>
    { }

    class LibraryMemberData : DbLayer.Member.LibraryMemberCollection<LibraryMemberValue>, ILibraryMemberData,
        ILoadData<DbLayer.Source.ILibrarySourceKey>, ISaveData<DbLayer.Source.ILibrarySourceKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        INamedScopeSource
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

            // Performance. Put the items into a Lookup.
            // Dictionary or SortedDictionary would also work.
            // All are faster then doing a FirstOrDefault on each item.
            ILookup<LibraryMemberIndex, LibraryMemberValue> members = this.ToLookup(a => new LibraryMemberIndex(a), b => b);
            
            foreach (LibraryMemberValue item in this)
            {
                LibrarySourceIndex libraryKey = new LibrarySourceIndex(item);
                LibraryMemberIndex parentKey = new LibraryMemberIndex(new LibraryMemberIndexParent(item));
                LibrarySourceValue? library = Library.LibrarySources.FirstOrDefault(w => libraryKey.Equals(w));
                LibraryMemberValue? parent = null;
                if (members.Contains(parentKey)) { parent = members[parentKey].First(); }

                if (parent is null && library is not null)
                { result.Add(new NamedScopePair(library.GetIndex(), GetValue(item))); }
                else if (parent is not null)
                { result.Add(new NamedScopePair(parent.GetIndex(), GetValue(item))); }
                else { throw new InvalidOperationException("Could not determine Parent"); }
            }

            return result;

            NamedScopeValueCore GetValue(LibraryMemberValue source)
            {
                NamedScopeValueCore result = new NamedScopeValueCore(source);
                source.PropertyChanged += Source_PropertyChanged;

                return result;

                void Source_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
                {
                    if (e.PropertyName is
                        nameof(source.MemberName) or
                        nameof(source.MemberNameSpace))
                    { result.TitleChanged(); }
                }
            }
        }

    }
}
