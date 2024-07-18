using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ModelData;
using Toolbox.Threading;
using System.ComponentModel;
using DataDictionary.DataLayer.LibraryData.Member;
using DataDictionary.DataLayer.LibraryData.Source;

namespace DataDictionary.BusinessLayer.Library
{
    /// <summary>
    /// Interface representing Library Member data
    /// </summary>
    public interface ILibraryMemberData : IBindingData<LibraryMemberValue>
    { }

    class LibraryMemberData : LibraryMemberCollection<LibraryMemberValue>, ILibraryMemberData,
        ILoadData<ILibrarySourceKey>, ISaveData<ILibrarySourceKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        INamedScopeSource
    {
        /// <inheritdoc/>
        public required ILibraryModel Library { get; init; }

        /// <inheritdoc/>
        /// <remarks>Library Member</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ILibrarySourceKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Library Member</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Library Member</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ILibrarySourceKey dataKey)
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

            foreach (LibraryMemberValue item in this)
            {
                DataLayerIndex libraryKey = new LibrarySourceIndex(item);
                DataLayerIndex parentKey = new LibraryMemberIndexParent(item);

                if (parentKey.HasValue)
                { result.Add(new NamedScopePair(parentKey, GetValue(item))); }
                else if (libraryKey.HasValue)
                { result.Add(new NamedScopePair(libraryKey, GetValue(item))); }
                else { throw new InvalidOperationException("Could not determine Parent"); }
            }

            return result;

            NamedScopeValue GetValue(LibraryMemberValue source)
            {
                NamedScopeValue result = new NamedScopeValue(source);
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

        /// <inheritdoc/>
        /// <remarks>Library Member</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        /// <remarks>Library Member</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove Library Member", DoWork = () => { this.Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>Library Member</remarks>
        public IReadOnlyList<WorkItem> Delete(ILibrarySourceKey dataKey)
        { return new WorkItem() { WorkName = "Remove Library Member", DoWork = () => { this.Remove(dataKey); } }.ToList(); }

    }
}
