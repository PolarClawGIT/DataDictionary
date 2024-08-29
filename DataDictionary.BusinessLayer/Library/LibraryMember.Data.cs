using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ModelData;
using Toolbox.Threading;
using System.ComponentModel;
using DataDictionary.DataLayer.LibraryData;

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
        INamedScopeSourceData
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
        public IReadOnlyList<WorkItem> LoadNamedScope(Action<INamedScopeSourceValue?, NamedScopeValue> addNamedScope)
        {
            return INamedScopeSourceData.LoadNamedScope<LibraryMemberData, LibraryMemberValue>
                (this, addNamedScope,
                (value) =>
                {
                    LibrarySourceIndex libraryKey = new LibrarySourceIndex(value);
                    LibraryMemberIndexParent parentKey = new LibraryMemberIndexParent(value);

                    if (this.FirstOrDefault(w => parentKey.Equals(w)) is LibraryMemberValue memberParent)
                    { return memberParent; }
                    else if (Library.LibrarySources.FirstOrDefault(w => libraryKey.Equals(w)) is LibrarySourceValue sourceParent)
                    { return sourceParent; }
                    else { return null; }
                });
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
